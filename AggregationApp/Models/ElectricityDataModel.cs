using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace AggregationApp.Models
{
    public class ElectricityDataModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Network { get; set; }
        public string ObjectName { get; set; }
        public string ObjectType { get; set; }
        public int ObjectNumber { get; set; }
        public decimal? PPlus { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal? PMinus { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Network: {Network}, ObjectName: {ObjectName}, ObjectType: {ObjectType}, ObjectNumber: {ObjectNumber}, PPlus: {PPlus}, Timestamp: {Timestamp}, PMinus: {PMinus}";
        }
    }
}
