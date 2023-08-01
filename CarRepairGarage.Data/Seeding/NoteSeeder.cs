
namespace CarRepairGarage.Data.Seeding
{
    using CarRepairGarage.Data.Models;
    using CarRepairGarage.Data.Seeding.Contracts;

    /// <summary>
    /// Class responsible for seeding the application database with initial Note data.
    /// </summary>
    public class NoteSeeder : ISeeder
    {
        /// <summary>
        /// Seeds the application database with initial Note data if the table is empty.
        /// </summary>
        /// <param name="dbContext">The application's database context.</param>
        /// <param name="serviceProvider">The service provider for resolving services and dependencies.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Notes.Any())
            {
                return;
            }

            Note[] notes = new Note[]
            {
                new Note
                {
                    Title = "Christmass Working Time",
                    Description = "There will be a new Christmass working time in the plase. Please note that all of the servicess will be with early close time.",
                    ImageUrl = "https://st.depositphotos.com/1021722/1392/i/600/depositphotos_13928596-stock-photo-christmas-eve-and-new-years.jpg"
                },
                new Note
                {
                    Title = "It`s time to change your car oil!",
                    Description = "Please note that we will start a promotion for everyone decided to change engine oil in one of our services. There will be 50% of the oil price!",
                    ImageUrl = "https://i1.wp.com/ssp.ng/wp-content/uploads/2020/06/promotional-offer.jpg?fit=509%2C363&ssl=1"

                }
            };
            await dbContext.Notes.AddRangeAsync(notes);
            await dbContext.SaveChangesAsync();
        }
    }
}
