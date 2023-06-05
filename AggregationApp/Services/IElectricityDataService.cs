using AggregationApp.Models;

namespace AggregationApp.Services
{
    public interface IElectricityDataService
    {
        Task<IEnumerable<APIResponseModel>> GetAggregatedDataAsync();
        Task<bool> DownloadCsvFiles();
    }
}