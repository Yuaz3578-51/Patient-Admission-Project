using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("UNIT")]
public class Unit
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
    public Staff Staff { get; set; }

    // Other properties
    // ...
}
