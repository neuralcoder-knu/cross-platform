using System.ComponentModel.DataAnnotations;

namespace Lab6.Data.Entities;

public class ATMMachine
{
    [Key]
    public int ATMId { get; set; }
    public string Location { get; set; }

    public ICollection<FinancialTransaction> FinancialTransactions { get; set; }
}