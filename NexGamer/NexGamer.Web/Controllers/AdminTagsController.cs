using Microsoft.AspNetCore.Mvc;
using NexGamer.Web.Data;
using NexGamer.Web.Models.Domain;
using NexGamer.Web.Models.ViewModel;

namespace NexGamer.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly NexGamerDbContext nexGamerDbContext;

        public AdminTagsController(NexGamerDbContext nexGamerDbContext)
        {
            this.nexGamerDbContext = nexGamerDbContext;
        }
      
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult AddTag(AddTagRequest addTagRequest)
        {
            // Mapping AddTagRequest To Tag Domain Model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };
            nexGamerDbContext.Tags.Add(tag);
            nexGamerDbContext.SaveChanges();
            return RedirectToAction("List");
        }


        [HttpGet]
        public IActionResult List() 
        {
            // use dbContext to read the tags 
            var tags = nexGamerDbContext.Tags.ToList();
            return View(tags);
        }


        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var tag = nexGamerDbContext.Tags.FirstOrDefault(x => x.Id == id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    DisplayName = tag.DisplayName,
                    Name = tag.Name
                };

                return View(editTagRequest);
            }

            return View(null);
        }

        [HttpPost]
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };

            var existingTag = nexGamerDbContext.Tags.Find(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                
                nexGamerDbContext.SaveChanges();
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpPost]
        public IActionResult Delete (EditTagRequest editTagRequest)
        {
            var tag = nexGamerDbContext.Tags.Find(editTagRequest.Id);
            
            if (tag != null)
            {
                nexGamerDbContext.Remove(tag);
                nexGamerDbContext.SaveChanges();
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
    }
}
