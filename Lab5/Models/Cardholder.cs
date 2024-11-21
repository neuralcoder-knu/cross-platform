using System.ComponentModel.DataAnnotations;

namespace Lab5.Models;

public class Cardholder
{
    public int CardholderId { get; set; }
    public string AccountNumber { get; set; }
    public string CountryCode { get; set; }
}