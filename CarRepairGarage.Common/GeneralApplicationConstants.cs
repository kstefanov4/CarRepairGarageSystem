namespace CarRepairGarage.Common
{
    public static class GeneralApplicationConstants
    {
        public const int ReleaseYear = 2023;
        public const string ApplicationTitle = "Car Repair Garage";
        public static class DataValidations
        {
            public const int TitleMaxLength = 35;
            public const int TitleMinLength = 7;

            public const int DescriptionMaxLength = 350;
            public const int DescriptionMinLength = 15;

            public const int NameMaxLenght = 50;
            public const int NameMinLenght = 10;

            public const int CarMakeMaxLenght = 30;
            public const int CarMakeMinLenght = 2;
        }

        public static class Roles
        {
            public const string AdminRole = "Admin";
            public const string ManagerRole = "Manager";
            public const string UserRole = "User";
        }
    }
}