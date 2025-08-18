using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentApi.Models;

public class MidtransCredential : BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string ClientKey { get; set; } = string.Empty;
    public string ServerKey { get; set; } = string.Empty;
    public string EndPointUrl { get; set; } = string.Empty;
    public string? CallBackToken { get; set; }

    public ICollection<PaymentLinkCharge> PaymentLinkCharges { get; set; } = new List<PaymentLinkCharge>();
}