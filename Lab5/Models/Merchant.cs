using System.ComponentModel.DataAnnotations;

namespace Lab5.Models;

public class Merchant
{
    public int MerchantId { get; set; }
    public string CountryCode { get; set; }
    public string MerchantCategoryCode { get; set; }
}