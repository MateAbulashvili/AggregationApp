using System.Globalization;
using AggregationApp.Data;
using AggregationApp.Models;
using CsvHelper;
using Microsoft.EntityFrameworkCore;

namespace AggregationApp.Services
{

    public class ElectricityDataService : IElectricityDataService
    {
        private readonly ElectricityDbContext _dbContext;

        public ElectricityDataService(ElectricityDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task<bool> DownloadCsvFiles()
        {
            try
            {
                HttpClient _httpClient = new HttpClient();
                var uris = new List<string>
                  {
                     "https://data.gov.lt/dataset/1975/download/10766/2022-05.csv",
                     "https://data.gov.lt/dataset/1975/download/10765/2022-04.csv",
                     "https://data.gov.lt/dataset/1975/download/10764/2022-03.csv",
                     "https://data.gov.lt/dataset/1975/download/10763/2022-02.csv"                  
                  };

                foreach (var uri in uris)
                {
                    using (var response = await _httpClient.GetAsync(uri))
                    {
                        response.EnsureSuccessStatusCode();

                        using (var stream = await response.Content.ReadAsStreamAsync())
                        using (var reader = new StreamReader(stream))
                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            csv.Context.RegisterClassMap<CvdDataMap>();
                            var records = csv.GetRecords<CsvDataModel>();

                            var electricityDataList = records.Select(record => new ElectricityDataModel
                            {
                                Network = record.Tinklas,
                                ObjectName = record.ObjetPavadinimas,
                                ObjectType = record.ObjectGvTipas,
                                ObjectNumber = record.ObjectNumeris,
                                PPlus = record.PPlus,
                                Timestamp = record.PlT,
                                PMinus = record.PMinus
                            }).ToList();

                            await _dbContext.ElectricityData.AddRangeAsync(electricityDataList);
                            await _dbContext.SaveChangesAsync();
                        }

                    }
                }

                return true;
            }
            catch (HttpRequestException ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<APIResponseModel>> GetAggregatedDataAsync()
        {
            var aggregatedData = await _dbContext.ElectricityData
                .GroupBy(data => data.Network)
                .Select(group => new APIResponseModel
                {
                    Network = group.Key,
                    PPlus = group.Sum(data => data.PPlus),
                    PMinus = group.Sum(data => data.PMinus)
                })
                .ToListAsync();

            return aggregatedData;
        }

    }
}

