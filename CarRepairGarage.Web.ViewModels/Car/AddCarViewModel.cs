using System;
using System.Collections.Generic;
namespace CarRepairGarage.Web.ViewModels.Car
{
    using System.ComponentModel.DataAnnotations;
    
    using CarRepairGarage.Common;

    public class AddCarViewModel
    {
        [Required]
        [StringLength(GeneralApplicationConstants.Validations.CarVINMaxLenght, MinimumLength = GeneralApplicationConstants.Validations.CarVINMinLenght)]
        public string VIN { get; set; } = null!;
        [Required]
        [StringLength(GeneralApplicationConstants.Validations.CarMakeMaxLenght, MinimumLength = GeneralApplicationConstants.Validations.CarMakeMinLenght)]
        public string Make { get; set; } = null!;
        [Required]
        [StringLength(GeneralApplicationConstants.Validations.CarModelMaxLenght, MinimumLength = GeneralApplicationConstants.Validations.CarModelMinLenght)]
        public string CarModel { get; set; } = null!;
        [Required]
        [Range(GeneralApplicationConstants.Validations.CarYearMinLenght, GeneralApplicationConstants.Validations.CarYearMaxYear)]
        public int Year { get; set; }
    }
}
