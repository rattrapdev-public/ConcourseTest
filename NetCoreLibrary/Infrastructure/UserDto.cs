using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreLibrary.Infrastructure
{
    [Table("user")]
    public class UserDto
    {
        [Key]
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public Guid OrganizationId { get; set; }
    }
}