using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication35.Models;


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