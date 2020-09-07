using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreLibrary.Infrastructure
{
    [Table("organization")]
    public class OrganizationDto
    {
        [Key]
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public bool IsEnabled { get; set; }
        public ICollection<UserDto> Users { get; set; }
    }
}