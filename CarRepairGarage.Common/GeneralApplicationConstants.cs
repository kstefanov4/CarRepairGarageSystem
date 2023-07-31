namespace CarRepairGarage.Common
{
    public static class GeneralApplicationConstants
    {
        public const int ReleaseYear = 2023;
        public const string ApplicationTitle = "Car Repair Garage";
        public static class Validations
        {
           
            //Car
            public const int CarVINMaxLenght = 17;
            public const int CarVINMinLenght = 17;

            public const int CarMakeMaxLenght = 30;
            public const int CarMakeMinLenght = 2;

            public const int CarModelMaxLenght = 30;
            public const int CarModelMinLenght = 2;

            public const int CarYearMaxYear = 9999;
            public const int CarYearMinLenght = 1900;

            //Garage
            public const int GarageNameMaxLenght = 50;
            public const int GarageNameMinLenght = 2;

            public const int GarageCityMaxLenght = 50;
            public const int GarageCityMinLenght = 2;

            public const int GarageStreetNameMaxLenght = 50;
            public const int GarageStreetNameLenght = 2;

            //Note
            public const int NoteTitleMaxLenght = 50;
            public const int NoteTitleMinLenght = 2;

            public const int NoteDescriptionMaxLenght = 2550;
            public const int NoteDescriptionMinLenght = 2;

            //Address
            public const int AddressStreetNameMaxLenght = 50;
            public const int AddressStreetNameMinLenght = 2;

            //Category
            public const int CategoryNameMaxLenght = 50;
            public const int CategoryDescriptionMaxLenght = 350;

            //Services
            public const int ServiceNameMaxLenght = 50;
        }

        public static class Roles
        {
            public const string AdminRole = "Admin";
            public const string ManagerRole = "Manager";
            public const string UserRole = "User";
        }
    }
}