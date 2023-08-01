namespace CarRepairGarage.Common
{
    /// <summary>
    /// Contains general application constants.
    /// </summary>
    public static class GeneralApplicationConstants
    {
        /// <summary>
        /// The release year of the application.
        /// </summary>
        public const int ReleaseYear = 2023;

        /// <summary>
        /// The title of the application.
        /// </summary>
        public const string ApplicationTitle = "Car Repair Garage";

        /// <summary>
        /// Contains role names used in the application.
        /// </summary>
        public static class Roles
        {
            /// <summary>
            /// The role name for the Admin role.
            /// </summary>
            public const string AdminRole = "Admin";

            /// <summary>
            /// The role name for the Manager role.
            /// </summary>
            public const string ManagerRole = "Manager";

            /// <summary>
            /// The role name for the User role.
            /// </summary>
            public const string UserRole = "User";
        }

        /// <summary>
        /// Contains email addresses for various accounts in the application.
        /// </summary>
        public static class AccountsData
        {
            /// <summary>
            /// The email address for the Admin account.
            /// </summary>
            public const string AdminEmail = "admin@mail.com";

            /// <summary>
            /// The email address for the User account.
            /// </summary>
            public const string UserEmail = "user@mail.com";

            /// <summary>
            /// The email address for the Manager account.
            /// </summary>
            public const string ManagerEmail = "garageManager@mail.com";
        }

        /// <summary>
        /// Contains validation constants for various entities in the application.
        /// </summary>
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
    }
}