using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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