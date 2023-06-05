using CsvHelper.Configuration;
using Newtonsoft.Json;

namespace AggregationApp.Models
{
    public class CsvDataModel
    {
        [JsonProperty("TINKLAS")]
        public string Tinklas { get; set; }

        [JsonProperty("OBT_PAVADINIMAS")]
        public string ObjetPavadinimas { get; set; }

        [JsonProperty("OBJ_GV_TIPAS")]
        public string ObjectGvTipas { get; set; }

        [JsonProperty("OBJ_NUMERIS")]
        public int ObjectNumeris { get; set; }

        [JsonProperty("P+")]
        public decimal? PPlus { get; set; }

        [JsonProperty("PL_T")]
        public DateTime PlT { get; set; }

        [JsonProperty("P-")]
        public decimal? PMinus { get; set; }
    }
    public class CvdDataMap : ClassMap<CsvDataModel>
    {
        public CvdDataMap()
        {
            Map(m => m.Tinklas).Name("TINKLAS");
            Map(m => m.ObjetPavadinimas).Name("OBT_PAVADINIMAS");
            Map(m => m.ObjectGvTipas).Name("OBJ_GV_TIPAS");
            Map(m => m.ObjectNumeris).Name("OBJ_NUMERIS");
            Map(m => m.PPlus).Name("P+");
            Map(m => m.PlT).Name("PL_T");
            Map(m => m.PMinus).Name("P-");
        }
    }
}
