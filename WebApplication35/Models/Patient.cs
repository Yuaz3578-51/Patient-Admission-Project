using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication35.Models
{
    [Table("PATIENT")]
    public class Patient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("PATIENTID")]
        public int PATIENTID { get; set; }

        [Column("PATIENTNUMBER")]
        public int PATIENTNUMBER { get; set; }

        [Column("PATIENTFNAME")]
        public string? PATIENTFNAME { get; set; }

        [Column("PATIENTLNAME")]
        public string? PATIENTLNAME { get; set; }

        [Column("PATIENTADDRESS")]
        public string? PATIENTADDRESS { get; set; }

        [Column("PATIENTDATEOFBIRTH")]
        public DateTime? PATIENTDATEOFBIRTH { get; set; }

        [Column("PATIENTPHONE")]
        public string? PATIENTPHONE { get; set; }

        [Column("RECORD_DATE")]
        public DateTime? RECORD_DATE { get; set; }

        [Column("DIAGNOSIS_DATE")]
        public DateTime? DIAGNOSIS_DATE { get; set; }

        // New properties for UNITID, STAFFID, and APPID
        public int UNITID { get; set; }
        public int STAFFID { get; set; }
        public int APPID { get; set; }

        // Other properties
        // ...

        public Application Application { get; set; }
        public Unit Unit { get; set; }
        public Staff Staff { get; set; }
    }
}
