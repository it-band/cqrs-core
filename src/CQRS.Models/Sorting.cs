namespace CQRS.Models
{
    public class Sorting
    {
        private string _path;

        public string Path
        {
            get => _path.Replace(".Asc", string.Empty).Replace(".Desc", string.Empty);
            set => _path = value;
        }

        private string _order;

        public string Order
        {
            get
            {
                if (_path != null)
                {
                    if (_path.EndsWith(".Asc"))
                    {
                        return SortOrder.Asc;
                    }
                    if (_path.EndsWith(".Desc"))
                    {
                        return SortOrder.Desc;
                    }
                }
                return _order;
            }
            set => _order = value;
        }
    }

    public static class SortOrder
    {
        public const string Asc = "Asc";
        public const string Desc = "Desc";
    }
}
