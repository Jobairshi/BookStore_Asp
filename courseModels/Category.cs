using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace courseModels
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Dispaly Order")] // to show that string when error in displayName box
        [Range(1,100,ErrorMessage ="OutOff Range")] // is range is out off this it will show eror
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTIme { get; set; } = DateTime.Now;

    }
}
