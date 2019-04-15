using System.Collections.Generic;

namespace CQRS.Models
{
    public class PagedList<T>
    {
        public IList<T> Items { get; set; }
        public int? Count { get; set; }

        public PagedList()
        {
            Items = new List<T>();
        }

        public PagedList(IList<T> items, int? count)
        {
            Count = count;
            Items = items;
        }
    }
}
