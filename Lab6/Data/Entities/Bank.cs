using System.ComponentModel.DataAnnotations;

namespace Lab6.Data.Entities;

public class Bank
{
    [Key]
    public int BankId { get; set; }

    public ICollection<CardholderBank> CardholderBanks { get; set; }
    public ICollection<MerchantBank> MerchantBanks { get; set; }
}