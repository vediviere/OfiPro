using System.ComponentModel.DataAnnotations;

namespace OfiPro.Application.DTOs.Common.Pagination;

public class PaginationQueryDto
{
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; } = 1;
    [Range(1, 100)]
    public int PageSize { get; set; } = 10;
    public string SortBy { get; set; } = "createdAt";
    public string SortDirection { get; set; } = "desc";
}