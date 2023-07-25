using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication35.Models;


[Table("UNIT")]
public class Unit
{
    [Key]
    [Column("UNITID")]
    public int UnitID { get; set; }

    [Column("STAFFID")]
    public int StaffID { get; set; }

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
    [ForeignKey("StaffID")]
    public Staff? Staff { get; set; }

    // Other properties
    // ...
}