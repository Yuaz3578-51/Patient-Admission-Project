using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication35.Models;

[Table("EXAMINATION")]
public class Examination
{
    [Key]
    [Column("EXAMID")]
    public int ExamID { get; set; }

    [Column("EXAMDEFID")]
    public int ExamDefID { get; set; }

    [Column("APPID")]
    public int AppId { get; set; }

    [Column("EXAMDATE")]
    public DateTime ExamDate { get; set; }

    // Navigation properties for the related entities
    [ForeignKey("AppId")]
    public Application? Application { get; set; }

    [ForeignKey("ExamDefID")]
    public ExamDefinition? ExaminationDefinition { get; set; }

    // Other properties
    // ...
}