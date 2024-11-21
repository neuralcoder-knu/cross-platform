namespace Lab5.Models;

public class FinancialTransaction
{
    public int TransactionId { get; set; }
    public int CardNumber { get; set; }
    public int CardNumberTransferFrom { get; set; }
    public int CardNumberTransferTo { get; set; }
    public int ATMId { get; set; }
    public int CurrencyCode { get; set; }
    public int MerchantId { get; set; }
    public string TransactionTypeCode { get; set; }
    public DateTime TransactionDate { get; set; }
    public Card Card { get; set; }

    public Merchant Merchant { get; set; }
}