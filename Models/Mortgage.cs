using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class Mortgage
{
    public int MortgageId { get; set; }
    public int UserId { get; set; }
    public double MaxLoan { get; set; }
    public double MonthlyPayment { get; set; }
    public double InterestRate { get; set; }
    public DateTime DateTime { get; set; }
    [ForeignKey("UserId")]
    public virtual User User { get; set; }

    public Mortgage()
    {
        DateTime = DateTime.UtcNow;
    }
}