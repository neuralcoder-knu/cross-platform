using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6.Data.Entities;

public class MerchantBank
{
    [Key]
    public int MerchantBankId { get; set; }
    public int MerchantId { get; set; }
    public int BankId { get; set; }

    [ForeignKey("MerchantId")]
    public Merchant Merchant { get; set; }

    [ForeignKey("BankId")]
    public Bank Bank { get; set; }
}