using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication35.Models;


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