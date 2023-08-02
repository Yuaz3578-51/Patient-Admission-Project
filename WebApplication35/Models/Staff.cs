using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("STAFF")]
public class Staff
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
