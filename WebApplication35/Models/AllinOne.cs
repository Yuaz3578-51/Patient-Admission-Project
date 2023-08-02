/*




using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication35.Models
{
    [Table("ALLinONE")]
    public class AllinOne
    {

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

            [Column("RECORD_DATE")]
            // [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
            public DateTime RECORD_DATE { get; set; }

            [Column("DIAGNOSIS_ID")]
            public int DIAGNOSIS_ID { get; set; }

            [Column("DIAGNOSIS_DATE")]
            // [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
            public DateTime DIAGNOSIS_DATE { get; set; }

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



        [Table("DRUG_DEFINITION")]
        public class Drug_Definition
        {
            [Key]
            [Column("DRUG_ID")]
            public int Drug_ID { get; set; }

            [Column("FORMULA")]
            public string? Formula { get; set; }

            [Column("COST")]
            public int Cost { get; set; }

            [Column("TRADENAME")]
            public string? TradeName { get; set; }

            [Column("DRUGNAME")]
            public string? DrugName { get; set; }

            [Column("CATEGORY")]
            public string? Category { get; set; }

            // Other properties
            // ...
        }


        [Table("EXAMINATION")]
        public class Examination
        {
            [Key]
            [Column("EXAMID")]
            public int ExamID { get; set; }

            [Column("EXAMDEFID")]
            public int ExamDefID { get; set; }

            [Column("APPID")]
            public int APPID { get; set; }

            [Column("EXAMDATE")]
            public DateTime ExamDate { get; set; }

            // Navigation properties for the related entities
            [ForeignKey("APPID")]
            public Application? Application { get; set; }

            [ForeignKey("ExamDefID")]
            public ExamDefinition? ExaminationDefinition { get; set; }

            // Other properties
            // ...
        }



        [Table("EXAM_DEFINITION")]
        public class ExamDefinition
        {
            [Key]
            [Column("EXAMDEFID")]
            public int ExamDefID { get; set; }

            [Column("EXAMNAME")]
            public string? ExamName { get; set; }

            // Other properties
            // ...
        }



        [Table("HOSPITAL")]
        public class Hospital
        {
            [Key]
            [Column("HOSPITALID")]
            public int HospitalID { get; set; }

            [Column("HOSPITALNAME")]
            public string? HospitalName { get; set; }

            [Column("HOSPITALADDRESS")]
            public string? HospitalAdress { get; set; }

            [Column("HOSPITALPHONE")]
            public string? HospitalPhone { get; set; }

            [Column("HOSPITALEMAIL")]
            public string? HospitalEmail { get; set; }

            // Other properties
            // ...
        }



        [Table("PATIENT")]
        public class Patient
        {
            [Key]
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
            public DateTime PATIENTDATEOFBIRTH { get; set; }

            [Column("PATIENTPHONE")] // Add the PATIENTPHONE property
            public string? PATIENTPHONE { get; set; }

            // Other properties
            // ...
        }


        [Table("PAYMENT")]
        public class Payment
        {
            [Key]
            [Column("PAYMENTID")]
            public int PaymentID { get; set; }

            [Column("ADMISSIONID")]
            public int AdmissionID { get; set; }

            [Column("APPID")]
            public int APPID { get; set; }

            [Column("TOTALCOST")]
            public string? TotalCost { get; set; }

            // Navigation property for the related entity
            [ForeignKey("APPID")]
            public Application? Application { get; set; }

            // Other properties
            // ...
        }




        [Table("PRESCRIPTION")]
        public class Prescription
        {
            [Key]
            [Column("PRESCID")]
            public int PrescID { get; set; }

            [Column("DRUG_ID")]
            public int Drug_ID { get; set; }

            [Column("FORMULA")]
            public string? Formula { get; set; }

            [Column("APPID")]
            public int APPID { get; set; }

            [Column("QUANTITY")]
            public int Quantity { get; set; }

            [Column("DOSAGE")]
            public int Dosage { get; set; }

            [Column("PRESCDATE")]
            public string? PrescDate { get; set; }

            // Navigation properties for the related entities
            [ForeignKey("APPID")]
            public Application? Application { get; set; }

            [ForeignKey("Drug_ID")]
            public Drug_Definition? Drug_Definition { get; set; }

            // Other properties
            // ...
        }






        [Table("STAFF")]
        public class Staff
        {
            [Key]
            [Column("STAFFID")]
            public int STAFFID { get; set; }

            [Column("HOSPITALID")]
            public int HospitalID { get; set; }

            [Column("STID")]
            public int StID { get; set; }

            [Column("UNITID")]
            public int UNITID { get; set; }

            [Column("STAFFFNAME")]
            public string? StaffFName { get; set; }

            [Column("STAFFLNAME")]
            public string? StaffLName { get; set; }

            [Column("RECDATE")]
            public DateTime RecDate { get; set; }

            // Navigation properties for the related entities
            [ForeignKey("HospitalID")]
            public Hospital? Hospital { get; set; }

            [ForeignKey("StID")]
            public Staff_Type? StaffType { get; set; }

            [ForeignKey("UNITID")]
            public Unit? Unit { get; set; }

            // Other properties
            // ...
        }






        [Table("STAFF_TYPE")]
        public class Staff_Type
        {
            [Key]
            [Column("STID")]
            public int StID { get; set; }

            [Column("STAFFDESCRIPTION")]
            public string? StaffDescription { get; set; }

            [Column("STPHONE")]
            public string? StPhone { get; set; }

            [Column("STEMAIL")]
            public string? StEmail { get; set; }

            // Other properties
            // ...
        }






        [Table("UNIT")]
        public class Unit
        {
            [Key]
            [Column("UNITID")]
            public int UNITID { get; set; }

            [Column("STAFFID")]
            public int STAFFID { get; set; }

            [Column("UNITADDRESS")]
            public string? UnitAddress { get; set; }

            [Column("UNITPHONE")]
            public string? UnitPhone { get; set; }

            [Column("UNITEMAIL")]
            public string? UnitEmail { get; set; }

            [Column("UNITNAME")]
            public string? UnitName { get; set; }

            [Column("UNITDESCRIPTION")]
            public string? UnitDescription { get; set; }

            // Navigation property for the related entity
            [ForeignKey("STAFFID")]
            public Staff? Staff { get; set; }

            // Other properties
            // ...
        }






    }




}


*/