using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab5.Models;

public class Card
{
    public int CardNumber { get; set; }
    public int CardholderId { get; set; }
    public int CardTypeCode { get; set; }
    public int CurrencyCode { get; set; }
    
    public Cardholder Cardholder { get; set; }
    
    public RefCardType CardType { get; set; }
    
    public RefCurrencyCode Currency { get; set; }
}