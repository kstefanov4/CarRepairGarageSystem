namespace CarRepairGarage.Web.ViewModels.Garage
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    
    using CarRepairGarage.Web.ViewModels.Garage.Enums;


    /// <summary>
    /// Represents a query model used to filter and sort the collection of garage view models.
    /// </summary>
    public class AllGaragesQueryModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AllGaragesQueryModel"/> class.
        /// </summary>
        public AllGaragesQueryModel()
        {
            this.CurrentPage = 1;
            this.GaragesPerPage = 3;

            this.Categories = new HashSet<string>();
            this.Services = new HashSet<string>();
            this.Cities = new HashSet<string>();
            this.Garages = new HashSet<GarageViewModel>();
        }

        /// <summary>
        /// Gets or sets the selected category for filtering garages.
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        /// Gets or sets the selected service for filtering garages.
        /// </summary>
        public string? Service { get; set; }

        /// <summary>
        /// Gets or sets the selected city for filtering garages.
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// Gets or sets the search string used to filter garages by name or description.
        /// </summary>
        [Display(Name = "Search")]
        public string? SearchByString { get; set; }

        /// <summary>
        /// Gets or sets the selected sorting option for garages.
        /// </summary>
        [Display(Name = "Sort By")]
        public GarageSorting GarageSorting { get; set; }

        /// <summary>
        /// Gets or sets the current page number in the paged collection of garages.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the number of garages to display per page.
        /// </summary>
        public int GaragesPerPage { get; set; }

        /// <summary>
        /// Gets or sets the total number of garages in the filtered collection.
        /// </summary>
        public int TotalGarages { get; set; }

        /// <summary>
        /// Gets or sets the collection of available categories for filtering.
        /// </summary>
        public IEnumerable<string> Categories { get; set; }

        /// <summary>
        /// Gets or sets the collection of available services for filtering.
        /// </summary>
        public IEnumerable<string> Services { get; set; }

        /// <summary>
        /// Gets or sets the collection of available cities for filtering.
        /// </summary>
        public IEnumerable<string> Cities { get; set; }

        /// <summary>
        /// Gets or sets the collection of garage view models in the paged result.
        /// </summary>
        public IEnumerable<GarageViewModel> Garages { get; set; }
    }

}
