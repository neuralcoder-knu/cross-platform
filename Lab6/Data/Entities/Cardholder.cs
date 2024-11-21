using System.ComponentModel.DataAnnotations;

namespace Lab6.Data.Entities;

public class Cardholder
{
    [Key]
    public int CardholderId { get; set; }
    public string AccountNumber { get; set; }
    public string CountryCode { get; set; }

    public ICollection<Card> Cards { get; set; }
    public ICollection<CardholderBank> CardholderBanks { get; set; }
}