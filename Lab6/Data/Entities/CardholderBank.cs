using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab6.Data.Entities;

public class CardholderBank
{
    [Key]
    public int CardholderBankId { get; set; }
    public int CardholderId { get; set; }
    public int BankId { get; set; }

    [ForeignKey("CardholderId")]
    public Cardholder Cardholder { get; set; }

    [ForeignKey("BankId")]
    public Bank Bank { get; set; }
}