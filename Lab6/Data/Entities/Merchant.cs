using System.ComponentModel.DataAnnotations;

namespace Lab6.Data.Entities;

public class Merchant
{
    [Key]
    public int MerchantId { get; set; }
    public string CountryCode { get; set; }
    public string MerchantCategoryCode { get; set; }

    public ICollection<MerchantBank> MerchantBanks { get; set; }
    public ICollection<FinancialTransaction> FinancialTransactions { get; set; }
}