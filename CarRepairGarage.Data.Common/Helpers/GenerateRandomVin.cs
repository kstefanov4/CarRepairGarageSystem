namespace CarRepairGarage.Data.Common.Helpers
{
    public class GenerateRandomVin
    {
        public static string Generate()
        {
            var random = new Random();

            // Generate 17 random alphanumeric characters
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var vinChars = new char[17];
            for (var i = 0; i < 17; i++)
            {
                vinChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(vinChars);
        }
    }
}
