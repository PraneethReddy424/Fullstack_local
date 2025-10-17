using System;

namespace UserApi.Models
{
    public class UserDetails
    {
        public Guid UserId { get; set; }                 // UUID in DB
        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public DateTime? ExpirationDate { get; set; }
        public bool IsFirstTimeLogging { get; set; } = true;
        public bool IsExternal { get; set; } = false;
        public bool Active { get; set; } = true;
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; } = DateTime.Now;
    }
}
