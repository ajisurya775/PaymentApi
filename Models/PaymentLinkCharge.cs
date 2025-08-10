using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentApi.Models;

public class PaymentLinkCharge : BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public Guid MidtransCredentialId { get; set; }
    public Guid? OrderId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public decimal ChargeAmount { get; set; }
    public string Status { get; set; } = StatusEnumStrings.Pending;
    public string CallBackUrl { get; set; } = string.Empty;

    public MidtransCredential MidtransCredential { get; set; } = null!;
}

public static class StatusEnumStrings
{
    public const string Pending = "pending";
    public const string Paid = "paid";
    public const string Expired = "expired";
    public const string Refund = "refund";
}
