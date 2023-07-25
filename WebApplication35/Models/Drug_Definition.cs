using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication35.Models;

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