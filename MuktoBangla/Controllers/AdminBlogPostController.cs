using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MuktoBangla.Model.Domain;
using MuktoBangla.Model.ViewModels;
using MuktoBangla.Repositories;

namespace MuktoBangla.Controllers
{
    public class AdminBlogPostController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ITagRepository tagRepository;

        public AdminBlogPostController(IBlogPostRepository blogPostRepository, ITagRepository tagRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public async Task<IActionResult> AddPost()
        {
            var tags = await tagRepository.GetAllTagsAsync();
            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(AddBlogPostRequest addBlogPostRequest)
        {

                var addpost = new BlogPost
                {
                    Heading = addBlogPostRequest.Heading,
                    ShortDescription = addBlogPostRequest.ShortDescription,
                    Description = addBlogPostRequest.Description,
                    Author = addBlogPostRequest.Author,
                    PageTitle = addBlogPostRequest.PageTitle,
                    UrlHandle = addBlogPostRequest.UrlHandle,
                    PublishDate = addBlogPostRequest.PublishDate,
                    Visible = addBlogPostRequest.Visible,
                    
                };

                var SelectedTags = new List<Tag>();
                foreach (var SelectedTagId in addBlogPostRequest.SelectedTags)
                {
                    var SelectedTagAsGuid = Guid.Parse(SelectedTagId);
                    var existingTagId = await tagRepository.GetSingelTagAsync(SelectedTagAsGuid);
                    if (existingTagId != null)
                    {
                        SelectedTags.Add(existingTagId);
                    }
                }
                addpost.Tags = SelectedTags;
                var addpostResult = await blogPostRepository.AddBlogPostAsync(addpost);
                if (addpostResult != null)
                {
                    return View(addBlogPostRequest);
                }
          

            return View(addBlogPostRequest);
        }

        [HttpGet]
        public async Task<IActionResult> ViewBlogPostList()
        {
            var BlogPostList = await blogPostRepository.GetAllBlogPostAssync();
            if (BlogPostList != null)
            {
                return View(BlogPostList);
            }
            return View(BlogPostList);
        }

        [HttpGet]
        public async Task<IActionResult> ViewBlogPost(Guid Id)
        {
            var blog = await blogPostRepository.GetSingelBlogPostAsync(Id);
            if (blog != null)
            {
                return View(blog);
            }
            return View(null);
        }

    }
}
