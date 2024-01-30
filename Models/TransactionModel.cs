using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Radit_BackEnd_30012024.Models;

[Table("transaction")]
public partial class Transaction
{
    [Key]
    public int? transactionId { get; set; }
    public int? accountId { get; set; }
    public DateTime? transactionDate { get; set; }
    public string? description { get; set; }
    public string? debitCreditStatus { get; set; }
    public int? amount { get; set; }
}
