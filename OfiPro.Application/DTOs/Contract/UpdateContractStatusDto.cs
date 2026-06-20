using OfiPro.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace OfiPro.Application.DTOs.Contract;

public class UpdateContractStatusDto
{
    [Required]
    public ContractStatus Status { get; set; }
}