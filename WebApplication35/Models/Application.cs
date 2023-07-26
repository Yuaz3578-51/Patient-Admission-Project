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
        public int AppID { get; set; }

        [Column("PATIENTID")]
        public int PatientID { get; set; }

        [Column("UNITID")]
        public int UnitID { get; set; }

        [Column("STAFFID")]
        public int StaffID { get; set; }

        [Column("RECORD_DATE")]
       // [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Record_Date { get; set; }

        [Column("DIAGNOSIS_ID")]
        public int Diagnosis_ID { get; set; }

        [Column("DIAGNOSIS_DATE")]
       // [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Diagnosis_Date { get; set; }

        // Navigation properties for the related entities
        [ForeignKey("PatientID")]
        public Patient? Patient { get; set; }

        [ForeignKey("UnitID")]
        public Unit? Unit { get; set; }

        [ForeignKey("StaffID")]
        public Staff? Staff { get; set; }

        // Other properties
        // ...
    }
}

