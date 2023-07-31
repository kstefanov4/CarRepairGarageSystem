
namespace CarRepairGarage.Services
{
    using Microsoft.Extensions.Logging;
    public class BaseService
    {
        private readonly ILogger<BaseService> _logger;

        public BaseService(ILogger<BaseService> logger)
        {
            _logger = logger;
        }
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
