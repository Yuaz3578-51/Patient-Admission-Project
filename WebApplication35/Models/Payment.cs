using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication35.Models;


[Table("PAYMENT")]
public class Payment
{
    [Key]
    [Column("PAYMENTID")]
    public int PaymentID { get; set; }

    [Column("ADMISSIONID")]
    public int AdmissionID { get; set; }

    [Column("APPID")]
    public int AppID { get; set; }

    [Column("TOTALCOST")]
    public string? TotalCost { get; set; }

    // Navigation property for the related entity
    [ForeignKey("AppID")]
    public Application? Application { get; set; }

    // Other properties
    // ...
}