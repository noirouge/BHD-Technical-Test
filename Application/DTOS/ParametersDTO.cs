using Domain.ENUMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS
{
    public class ParametersDTO
    {
           public DateTime? uploadDateStart {  get; set; }
           public DateTime? uploadDateEnd {  get; set; }
           public string? filename {  get; set; }
           public string? contentType {  get; set; }
           public DocumentType? documentType {  get; set; }
           public DocumentStatus? status {  get; set; }
           public string? customerId {  get; set; }
           public Channel? channel {  get; set; }
           public SortBy sortBy    { get; set; }
           public SortDirection sortDirection = SortDirection.ASC;
    }
}
