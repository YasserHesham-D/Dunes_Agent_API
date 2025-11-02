using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class Pagination<T>
    {
        public Pagination()
        {

        }
        private Pagination(List<T> _Items, int _PageSize, int _Page, int _TotalCount)
        {
            Items = _Items;
            Page = _Page;
            PageSize = _PageSize;
            TotalCount = _TotalCount;
        }
        public List<T> Items { get; set; }

        public int PageSize { get; set; }

        public int Page { get; set; }

        public int TotalCount { get; set; }

        public bool HasNextPage => Page * PageSize < TotalCount;

        public bool HasPreviousPage => Page > 1; // ✅ Fixes correct paging logic

        public int GetCount()
        {
            return Items.Count();
        }


        public static async Task<Pagination<T>> CreateAsync(IQueryable<T> _Items, int _Page, int _PageSize)
        {
            var TotalCount = await _Items.CountAsync();
            var items = await _Items.Skip((_Page - 1) * _PageSize).Take(_PageSize).ToListAsync();
            return new(items, _PageSize, _Page, TotalCount);
        }

        public static Pagination<T> Create(List<T> items, int page, int pageSize)
        {
            var totalCount = items.Count;
            var paginatedItems = items
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new Pagination<T>(paginatedItems, pageSize, page, totalCount);
        }

    }
}
