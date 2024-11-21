namespace Lab5.Models;

public class SearchViewModel
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<int> CurrencyCodes { get; set; }
    public string MerchantCodePrefix { get; set; }
}