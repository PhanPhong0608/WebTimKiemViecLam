using Microsoft.AspNetCore.Builder;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace DACS_WebTimKiemViecLam.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Danh sách đơn ứng tuyển
        public virtual ICollection<JobApplication>? JobApplications { get; set; }
    }
}
