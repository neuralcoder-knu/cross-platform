using System.ComponentModel.DataAnnotations;

namespace Lab6.Data.Entities;

public class RefCardType
{
    [Key]
    public int CardTypeCode { get; set; }
    public string TypeName { get; set; }

    public ICollection<Card> Cards { get; set; }
}