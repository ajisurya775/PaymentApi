using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using PaymentApi.Attributes;

namespace PaymentApi.DTOs.PaymentLink;

public class ItemsDto
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
}