namespace CarRepairGarage.Web.ViewModels.Garage
{
    public class AllGaragesFilteredAndPagedServiceModel
    {
        public AllGaragesFilteredAndPagedServiceModel()
        {
            this.Garages = new HashSet<GarageViewModel>();
        }
        public int TotalGarageCount { get; set; }
        public IEnumerable<GarageViewModel> Garages { get; set; }
    }
}
