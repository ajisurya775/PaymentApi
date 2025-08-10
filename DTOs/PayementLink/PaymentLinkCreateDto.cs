using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using PaymentApi.Attributes;

namespace PaymentApi.DTOs.PaymentLink;

public class PaymentLinkCreateDto
{
    [Required]
    public Guid CredentialId { get; set; }
    public Guid? OrderId { get; set; }
    [Required]
    public string OrderNumber { get; set; } = string.Empty;
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "The ChargeAmount field is required.")]
    public decimal ChargeAmount { get; set; }

    [Required]
    [MinCollectionCount(1, ErrorMessage = "At least one item is required in Items.")]
    public List<ItemsDto> Items { get; set; } = [];
}