using System.ComponentModel.DataAnnotations;

namespace MuktoBangla.Model.ViewModels
{
    public class AddTagRequest
    {
        [Required]
        public string Name { get; set; }
        public string? TagDescription { get; set; }
    }
}
