namespace CarRepairGarage.Web.ViewModels.Pagination
{
    public class PaginationViewModel
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Area { get; set; } = null!;
        public string Controller { get; set; } = null!;
        public string Action { get; set; } = null!;
    }
}
