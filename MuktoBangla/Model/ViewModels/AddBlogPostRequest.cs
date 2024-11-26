using Microsoft.AspNetCore.Mvc.Rendering;

namespace MuktoBangla.Model.ViewModels
{
    public class AddBlogPostRequest
    {
        public string Heading { get; set; }
        public string? ShortDescription { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string? PageTitle { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.Now;
        public bool Visible { get; set; }

        //Display Tags
        public IEnumerable<SelectListItem> Tags { get; set; }

        //Get Tags from View
        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}
