namespace Sport_Match.Dtos
{
    public class EventSearchRequest
    {
        public string Sport { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public bool? IsPrivate { get; set; }

        public string SortBy { get; set; } = "StartDateTime";
        public bool SortDesc { get; set; } = false;

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

}
