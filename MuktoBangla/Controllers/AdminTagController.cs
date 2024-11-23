using Microsoft.AspNetCore.Mvc;
using MuktoBangla.Model.Domain;
using MuktoBangla.Model.ViewModels;
using MuktoBangla.Repositories;

namespace MuktoBangla.Controllers
{
    public class AdminTagController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        
        public async Task<IActionResult> AddTag(AddTagRequest addTagRequest)
        {
            if(ModelState.IsValid == false)
            {
                return View(addTagRequest);
            }
            else
            {
                var tag = new Tag
                {
                    Name = addTagRequest.Name,
                    TagDescription = addTagRequest.TagDescription
                };
                var addTag = await tagRepository.AddTagAsync(tag);
                if(addTag != null)
                {
                    //Add Notification
                }
                else
                {
                    //Add Error Notification
                }

                return View(new AddTagRequest());
            }
        }
    }
}
