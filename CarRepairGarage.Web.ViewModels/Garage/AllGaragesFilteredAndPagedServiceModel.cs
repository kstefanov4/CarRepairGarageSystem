namespace CarRepairGarage.Web.ViewModels.Garage
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a service model containing a filtered and paged collection of garage view models.
    /// </summary>
    public class AllGaragesFilteredAndPagedServiceModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AllGaragesFilteredAndPagedServiceModel"/> class.
        /// </summary>
        public AllGaragesFilteredAndPagedServiceModel()
        {
            this.Garages = new HashSet<GarageViewModel>();
        }

        /// <summary>
        /// Gets or sets the total number of garages in the filtered collection.
        /// </summary>
        public int TotalGarageCount { get; set; }

        /// <summary>
        /// Gets or sets the filtered and paged collection of garage view models.
        /// </summary>
        public IEnumerable<GarageViewModel> Garages { get; set; }
    }
}
