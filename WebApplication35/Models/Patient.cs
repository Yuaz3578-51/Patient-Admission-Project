using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication35.Models;

namespace WebApplication35.Models
{
    [Table("PATIENT")]
    public class Patient
    {
        [Key]
        [Column("PATIENTID")]
        public int PatientID { get; set; }

        [Column("PATIENTNUMBER")]
        public int PatientNumber { get; set; }

        [Column("PATIENTFNAME")]
        public string? PatientFName { get; set; }

        [Column("PATIENTLNAME")]
        public string? PatientLName { get; set; }

        [Column("PATIENTADDRESS")]
        public string? PatientAddress { get; set; }

        [Column("PATIENTDATEOFBIRTH")]
        public DateTime PatientDateOfBirth { get; set; }

        [Column("PATIENTPHONE")]
        public string? PatientPhone { get; set; }

        // Other properties
        // ...
    }
}