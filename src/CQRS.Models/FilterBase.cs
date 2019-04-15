namespace CQRS.Models
{
    public class FilterBase
    {
        public FilterBase()
        {
            Paging = new Paging();
        }

        public string SearchText { get; set; }

        public Paging Paging { get; set; }

        public Sorting Sorting { get; set; }
    }
}
