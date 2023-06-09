﻿namespace AggregationApp.Models
{
    public class ElectricityDataDTO
    {
        public string Network { get; set; }
        public string ObjectName { get; set; }
        public string ObjectType { get; set; }
        public int ObjectNumber { get; set; }
        public decimal? PPlus { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal? PMinus { get; set; }
    }
}
