﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAPI5.Models
{
    [Table("garagedistance")]
    public class GarageDistance
    {
        [Key]
        [Column("garagedistanceid")]
        public long GarageDistanceId { get; set; }

        [Column("distance")]
        public decimal Distance { get; set; }

        [Column("createddate")]
        public DateTime CreatedDate { get; set; }

        [Column("garagestatusid")]
        public int? GarageStatusId { get; set; }

        [ForeignKey("GarageStatusId")]
        public GarageStatus GarageStatus { get; set; }
    }
}
