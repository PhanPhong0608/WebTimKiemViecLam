using System.ComponentModel.DataAnnotations;

namespace DACS_WebTimKiemViecLam.Models
{
    public class Field
    {
        [Key]
        public int FieldID { get; set; }

        [Required, StringLength(100)]
        public string FieldName { get; set; }

        [StringLength(500)] // Thêm dòng này
        public string? Description { get; set; }

        // Danh sách công ty thuộc lĩnh vực này
        public virtual ICollection<Company>? Companies { get; set; }

        // Danh sách công việc thuộc lĩnh vực này
        public virtual ICollection<JobPosition>? JobPositions { get; set; }
    }
}
