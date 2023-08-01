namespace CarRepairGarage.Services
{
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Base service class providing common functionality for other service classes.
    /// </summary>
    public class BaseService
    {
        private readonly ILogger<BaseService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        /// <param name="logger">The logger used for logging messages.</param>
        public BaseService(ILogger<BaseService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Executes a database action with error handling.
        /// </summary>
        /// <param name="databaseAction">The database action to execute as a <see cref="Task"/>.</param>
        /// <exception cref="ApplicationException">Thrown if the database action fails to save the information.</exception>
        protected async Task ExecuteDatabaseAction(Func<Task> databaseAction)
        {
            try
            {
                await databaseAction.Invoke();
            }
            catch (Exception ex)
            {
                _logger.LogError(databaseAction.Method.Name, ex);
                throw new ApplicationException("Database failed to save info", ex);
            }
        }
    }
}
