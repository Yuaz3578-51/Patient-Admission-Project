using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication35.Models;


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
    public int AppID { get; set; }

    [Column("QUANTITY")]
    public int Quantity { get; set; }

    [Column("DOSAGE")]
    public int Dosage { get; set; }

    [Column("PRESCDATE")]
    public string? PrescDate { get; set; }

    // Navigation properties for the related entities
    [ForeignKey("AppID")]
    public Application? Application { get; set; }

    [ForeignKey("Drug_ID")]
    public Drug_Definition? Drug_Definition { get; set; }

    // Other properties
    // ...
}
