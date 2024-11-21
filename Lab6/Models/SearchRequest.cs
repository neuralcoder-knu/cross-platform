namespace Lab6.Models;

public class SearchRequest
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<int> CurrencyCodes { get; set; }
    public string MerchantCodePrefix { get; set; }
}