using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MuktoBangla.Model.Domain;
using MuktoBangla.Model.Pagination;
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
                return RedirectToAction("ViewBlogPostList");
                }
          
            return View(addBlogPostRequest);
        }

        [HttpGet]
        public async Task<IActionResult> ViewBlogPostList(int page = 1)
        {
            
             var BlogPostList = await blogPostRepository.GetAllBlogPostAssync();
            if (BlogPostList != null)
            {
                const int pageSize = 10;
                if (page < 1)
                {
                    page = 1;
                }

                int recsCount = BlogPostList.Count();
                var pager = new Pager(recsCount, page, pageSize);
                int recSkip = (page - 1) * pageSize;
                var data = BlogPostList.Skip(recSkip).Take(pager.PageSize).ToList();
                ViewBag.Pager = pager;

                return View(data);
                //return View(BlogPostList);
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

        [HttpGet]
        public async Task<IActionResult> EditPostAsync(Guid Id)
        {
            var blogPost = await blogPostRepository.GetSingelBlogPostAsync(Id);
            var tagDomainModel = await tagRepository.GetAllTagsAsync();

            if (blogPost != null)
            {
                var model = new EditBlogPostRequest
                {
                    Id = blogPost.ID,
                    Heading = blogPost.Heading,
                    ShortDescription = blogPost.ShortDescription,
                    Description = blogPost.Description,
                    Author = blogPost.Author,
                    PageTitle = blogPost.PageTitle,
                    UrlHandle = blogPost.UrlHandle,
                    PublishDate = blogPost.PublishDate,
                    Visible = blogPost.Visible,
                    Tags = tagDomainModel.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray()
                };
                return View(model);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> EditPostAsync(EditBlogPostRequest editBlogPostRequest)
        {
            var updatePost = new BlogPost
            {
                ID = editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                ShortDescription = editBlogPostRequest.ShortDescription,
                Description = editBlogPostRequest.Description,
                Author = editBlogPostRequest.Author,
                PageTitle = editBlogPostRequest.PageTitle,
                UrlHandle = editBlogPostRequest.UrlHandle,
                Visible = editBlogPostRequest.Visible,

            };
            var SelectedTags = new List<Tag>();
            foreach(var tag in editBlogPostRequest.SelectedTags)
            {
                var tagIdAsGuid = Guid.Parse(tag);
                var existingTag = await tagRepository.GetSingelTagAsync(tagIdAsGuid);
                if (existingTag != null)
                {
                    SelectedTags.Add(existingTag);
                }
            }
            updatePost.Tags = SelectedTags;
            updatePost = await blogPostRepository.UpdateBlogPostAsync(updatePost);
            if(updatePost != null)
            {
                return RedirectToAction("ViewBlogPostList");
            }
            else
            {
                return View(editBlogPostRequest);
            }
        }
    }
}
