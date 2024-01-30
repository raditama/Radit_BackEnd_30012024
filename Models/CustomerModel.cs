using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Radit_BackEnd_30012024.Models;

[Table("customer")]
public partial class Customer
{
    [Key]
    public int accountId { get; set; }
    public string? name { get; set; }
    public int? points { get; set; }
}
