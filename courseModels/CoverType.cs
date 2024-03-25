using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace courseModels
{
    public class CoverType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Dispaly Order")] // to show that string when error in displayName box
     
        public DateTime CreatedDateTIme { get; set; } = DateTime.Now;

    }
}
