using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication35.Models
{
    [Table("APPLICATION")]
    public class Application
    {
        [Key]
        [Column("APPID")]
        public int APPID { get; set; }

        [Column("PATIENTID")]
        public int PATIENTID { get; set; }

        [Column("UNITID")]
        public int UNITID { get; set; }

        [Column("STAFFID")]
        public int STAFFID { get; set; }

        [Column("DIAGNOSIS_ID")]
        public int DIAGNOSIS_ID { get; set; }


        // Navigation properties for the related entities
        [ForeignKey("PATIENTID")]
        public Patient? Patient { get; set; }

        [ForeignKey("UNITID")]
        public Unit? Unit { get; set; }

        [ForeignKey("STAFFID")]
        public Staff? Staff { get; set; }

        // Other properties
        // ...
    }
}

