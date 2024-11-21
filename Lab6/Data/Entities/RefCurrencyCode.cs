using System.ComponentModel.DataAnnotations;

namespace Lab6.Data.Entities;

public class RefCurrencyCode
{
    [Key]
    public int CurrencyCode { get; set; }
    public string CurrencyName { get; set; }

    public ICollection<Card> Cards { get; set; }
    public ICollection<FinancialTransaction> FinancialTransactions { get; set; }
}