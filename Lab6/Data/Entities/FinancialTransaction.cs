using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6.Data.Entities;

public class FinancialTransaction
{
    [Key]
    public int TransactionId { get; set; }
    public int CardNumber { get; set; }
    public int CardNumberTransferFrom { get; set; }
    public int CardNumberTransferTo { get; set; }
    public int ATMId { get; set; }
    public int CurrencyCode { get; set; }
    public int MerchantId { get; set; }
    public string TransactionTypeCode { get; set; }
    
    public DateTime TransactionDate { get; set; }

    [ForeignKey("CardNumber")]
    public Card Card { get; set; }

    [ForeignKey("ATMId")]
    public ATMMachine ATM { get; set; }

    [ForeignKey("CurrencyCode")]
    public RefCurrencyCode Currency { get; set; }

    [ForeignKey("MerchantId")]
    public Merchant Merchant { get; set; }
}