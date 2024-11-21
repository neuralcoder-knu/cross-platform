using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6.Data.Entities;

public class Card
{
    [Key]
    public int CardNumber { get; set; }
    public int CardholderId { get; set; }
    public int CardTypeCode { get; set; }
    public int CurrencyCode { get; set; }

    [ForeignKey("CardholderId")]
    public Cardholder Cardholder { get; set; }

    [ForeignKey("CardTypeCode")]
    public RefCardType CardType { get; set; }

    [ForeignKey("CurrencyCode")]
    public RefCurrencyCode Currency { get; set; }
}