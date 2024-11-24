using Microsoft.AspNetCore.Http.HttpResults;
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
        public IActionResult AddTag()
        {
            return View();
        }

        [HttpPost]
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
                return RedirectToAction("GetAllTag");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTag()
        {
            var TagList = await tagRepository.GetAllTagsAsync();
            if(TagList == null)
            {
                return View();
            }
            return View(TagList);
            
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTag(Guid Id)
        {
            var UpdateTag = await tagRepository.GetSingelTagAsync(Id);
            if(UpdateTag != null)
            {
                var UpdateTagRequest = new UpdateTagRequest
                {
                    Id = UpdateTag.Id,
                    Name = UpdateTag.Name,
                    TagDescription = UpdateTag.TagDescription
                };
                return View(UpdateTagRequest);
            }
            else
            {
                return View(null);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult>UpdateTag(UpdateTagRequest updateTagRequest)
        {
            if(ModelState.IsValid == false)
            {
                return View(updateTagRequest);
            }
            else
            {
                var UpdateTag = new Tag
                {
                    Id = updateTagRequest.Id,
                    Name = updateTagRequest.Name,
                    TagDescription = updateTagRequest.TagDescription
                };
                var updateTagcheck = await tagRepository.UpdateTagAsync(UpdateTag);
                if (updateTagcheck != null)
                {
                    //Update Success Notification
                }
                else
                {
                    //Update Error Notification
                }
                return RedirectToAction("GetAllTag");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(UpdateTagRequest updateTagRequest)
        {
            var deleteTag = await tagRepository.DeleteTagAsync(updateTagRequest.Id);
            if (deleteTag != null)
            {
                return RedirectToAction("GetAllTag");
            }
            return RedirectToAction("GetAllTag");
        }
    }
}
