using Application.Dtos;
using Application.Dtos.Employee;
using Application.Dtos.Login;
using Application.Dtos.Permission;
using Domain.Interfaces.IModelsRepo;
using Domain.Interfaces.IRepository;
using Domain.Interfaces.IUnitOfWork;
using Domain.Models.Accounts;
using Domain.Models.MTM;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Domain.Interfaces.IModelsRepo.IAccountsRepo;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Services.AccountServices
{

    // update mtm in patch not yet implemented
    public class AccountServices(UserManager<Employee> userManager,RoleManager<IdentityRole> roleManager
                                ,IConfiguration configuration, IPasswordHasher<Employee> passwordHasher
                                ,IRefreshToken refreshTokenRepo,IAccountsRepo accountsRepo
                                ,IUnitOfWork unitOfWork) : IAccountServices
    {
        public async Task<LoginResponseDTO> Login(Employee employee)
        {
            var Token = await CreateAccessToken(employee);
            var TokenExpTime = configuration.GetSection("JWT").GetSection("LifeTime").Value;

            int x = int.Parse(TokenExpTime);

            var Rtoken = GenerateRefreshToken();
            Rtoken.UserId = employee.Id;

            await refreshTokenRepo.AddAsync(Rtoken);
            await unitOfWork.SaveChangesAsync();

            return new LoginResponseDTO
            {
                Token = Token,
                ExpirationDT = DateTime.UtcNow.AddMinutes(x).ToString() ,
                RefreshToken = Rtoken,
            };
        }

        public async Task<(string Token, RefreshToken RefreshToken)> RefreshTokenAsync(string token)
        {
            var refreshToken = await refreshTokenRepo.GetFirstOrDefaultAsync(x => x.Token == token);

            if (refreshToken == null || !refreshToken.IsActive)
                throw new UnauthorizedAccessException("Invalid or expired refresh token");

            var user = await accountsRepo.GetByIdAsync(refreshToken.UserId);
            if (user == null) throw new UnauthorizedAccessException("User not found");

            refreshToken.Revoked = DateTime.UtcNow;
            var newRefresh = GenerateRefreshToken();
            newRefresh.UserId = user.Id;

            await refreshTokenRepo.AddAsync(newRefresh);
            await unitOfWork.SaveChangesAsync();

            var jwtToken = await CreateAccessToken(user);
            return (jwtToken, newRefresh);
        }

        public async Task LogoutAsync(string userId)
        {
            var tokens = await refreshTokenRepo.GetAllAsync(t => t.UserId == userId && t.Expires > DateTime.UtcNow );

            foreach (var token in tokens)
               await refreshTokenRepo.DeleteAsync(token.Id);


            await unitOfWork.SaveChangesAsync();


        }

        public async Task<bool> AddNewEmployee(AddEmployeeRequest request,string EmployeeAddedId)
        {
            var User = await userManager.FindByIdAsync(EmployeeAddedId);

            var userrole = await userManager.GetRolesAsync(User);

            if(userrole.Contains("DeskAgent") || userrole.Contains("TourAgent"))
                return false;

            if(userManager.FindByEmailAsync(request.Email).Result != null && userManager.FindByNameAsync(request.Name).Result != null)
                return false;

            

            var newEmployee = new Employee
            {
                UserName = request.Name,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                EmployeeAddedId = EmployeeAddedId,
                HasControlSystemAccess = request.HasControlSystemAccess,
                IsFromUAE = request.IsTheEmployeeEmirate,
                HotelId = request.HotelId,
                LocationId = request.AreaOfLocationId,
                SalaryType = request.SalaryType,
                StaffVisaCount =request.StaffVisaCost,
                CommissionRate = request.CommissionRate,

                Permissions = request.Permissions.Select(x => new EmployeePermission
                {
                    IsGranted = true,
                    PermissionId = x

                }).ToList()

            };

            var result = await userManager.CreateAsync(newEmployee,request.Password);
            if (!result.Succeeded)
                return false;

            var roleExist = await roleManager.RoleExistsAsync(request.Position);
            if (!roleExist)
                return false;

            await userManager.AddToRoleAsync(newEmployee, request.Position);
            await roleManager.CreateAsync(new IdentityRole(request.Position));

            return true;
        }

        public async Task<Pagination<GetAllEmployees>> GetAllEmployeesAsync
            (string? fullname,string? position,string? phonenumber,/*string? status,*/int page,int pageSize)
        {
            var employeesQuery = accountsRepo.GetAllEmployeesQuery(fullname,position,phonenumber).AsNoTracking();

            // Filter
            if (!string.IsNullOrEmpty(fullname))
                employeesQuery = employeesQuery.Where(e => e.UserName.Contains(fullname));

            if (!string.IsNullOrEmpty(phonenumber))
                employeesQuery = employeesQuery.Where(e => e.PhoneNumber.Contains(phonenumber));

            //if (!string.IsNullOrEmpty(status))
            //{
            //    bool isActive = status.Equals("Active", StringComparison.OrdinalIgnoreCase);
            //    employeesQuery = employeesQuery.Where(e => e.LockoutEnabled != isActive);
            //}

            // 🔍 Join roles
            var query =
                from emp in employeesQuery
                join ur in userManager.Users
                    on emp.EmployeeAddedId equals ur.Id into addedByJoin
                from addedBy in addedByJoin.DefaultIfEmpty()
                join userRole in userManager.Users
                    on emp.Id equals userRole.Id into empRoles
                from empRole in empRoles.DefaultIfEmpty()
                select new { emp, AddedBy = addedBy };

            // 🎯 Map to DTO
            var dtoQuery = query.Select(x => new GetAllEmployees
            {
                Id = x.emp.Id,
                FullName = x.emp.UserName,
                AddedBy = x.AddedBy != null ? x.AddedBy.UserName : "N/A",
                Position = "N/A", // We'll fill this next
                Status = x.emp.LockoutEnabled ? "Inactive" : "Active",
                PhoneNumber = x.emp.PhoneNumber,
                Salary = x.emp.SalaryValue,
                CommitionRate = x.emp.CommissionRate,
                EmployeeIsEmirates = x.emp.IsFromUAE,
                VisaCost = x.emp.StaffVisaCount,
                AreaOfLocation = x.emp.Location != null ? x.emp.Location.Name : "N/A",
                Dues = "N/A",
                JoiningDate = x.emp.JoinDate,
                Permissions = x.emp.Permissions.Select(p => new PermissionsDTO
                {
                    Id = p.Id,
                  //  Module = p.Module,
                 //   Action = p.Action,
                  //  IsGranted = p.IsGranted
                }).ToList()
            });

            return await Pagination<GetAllEmployees>.CreateAsync(dtoQuery, page, pageSize);
        }

        public async Task<GetEmployeeById> GetEmployeeByIdAsync(string id)
        {
            var employee = await accountsRepo.GetByIdAsync(id);

            if (employee == null)
                throw new ("Employee not found");

            var roles = await userManager.GetRolesAsync(employee);
            var roleName = roles.FirstOrDefault() ?? "No Role Assigned"; 

            var result = new GetEmployeeById
            {
                Id = employee.Id,
                userName = employee.UserName,
                position = roleName, 
                phoneNumber = employee.PhoneNumber,
                sallery = employee.SalaryValue,
                commissionRate = employee.CommissionRate,
                IsEmirate = employee.IsFromUAE,
                staffVisaCount = employee.StaffVisaCount,
                AreaOfLocation = employee.Location?.Name ?? string.Empty, 
                JoiningDate = employee.JoinDate,
                HotelName = employee.Hotel.Name,
                Email = employee.Email,
                HasControlOverSystem = employee.HasControlSystemAccess

                //,Permissions = employee.Permissions.Select(x => new GetAllPermissionsDTO
                //{
                   
                //}).ToList()
            };

            return result;
        }

        public async Task<bool> PatchEmployeeAsync(string id , UpdateEmployeeRequest request)
        {
            var employee = await accountsRepo.GetByIdAsync(id);

            if (employee == null)
                return false;

            if (request.UserName != null)
                employee.UserName = request.UserName;

            if (request.PhoneNumber != null)
                employee.PhoneNumber = request.PhoneNumber;

            if (request.SalaryValue.HasValue)
                employee.SalaryValue = request.SalaryValue.Value;

            if (request.CommissionRate.HasValue)
                employee.CommissionRate = request.CommissionRate.Value;

            if (request.IsFromUAE.HasValue)
                employee.IsFromUAE = request.IsFromUAE.Value;

            if (request.StaffVisaCount.HasValue)
                employee.StaffVisaCount = request.StaffVisaCount.Value;

            if (request.JoinDate.HasValue)
                employee.JoinDate = request.JoinDate.Value;

            if (request.LocationId != Guid.Empty)
                employee.LocationId = request.LocationId;

            await accountsRepo.UpdateAsync(employee);
            await unitOfWork.SaveChangesAsync();

            return true;
        }


        private async Task<string> CreateAccessToken(Employee employee)
        {
            var userroles = await  userManager.GetRolesAsync(employee);
            var userRole = userroles.FirstOrDefault();

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, employee.Id),
                new(ClaimTypes.Name, employee.UserName),
                new(ClaimTypes.Email, employee.Email),
                new(ClaimTypes.Role, userRole),
                new(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                
            };

            var JWT = configuration.GetSection("JWT");
            // SigningCredentials
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                (JWT.GetSection("SigningKey").Value));

            var SigningCredential = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            // Token
            var Token = new JwtSecurityToken
            (
                claims: claims,
                issuer: JWT.GetSection("Issuer").Value,
                audience: JWT.GetSection("Audience").Value,
                expires: DateTime.Now.AddMinutes(int.Parse(JWT.GetSection("LifeTime").Value)),
                signingCredentials: SigningCredential
            );

            var JWTTOKEN = new JwtSecurityTokenHandler().WriteToken(Token);

            return JWTTOKEN;
        }
        private RefreshToken GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };
        }
    }
}
