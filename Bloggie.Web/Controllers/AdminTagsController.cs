using Azure;
using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminTagsController : Controller
	{
		private readonly ITagRepository tagRepository;

		public AdminTagsController(ITagRepository tagRepository)
		{
			this.tagRepository = tagRepository;
		}

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		[ActionName("Add")]
		//public  IActionResult Add(AddTagRequest addTagRequest)
		public async Task<IActionResult> Add(AddTagRequest addTagRequest)
		{

			if (ModelState.IsValid == false)
			{
				return View();
			}
			// Mapping AddTagRequest to Tag domain model
			var tag = new Tag
			{
				Name = addTagRequest.Name,
				DisplayName = addTagRequest.DisplayName,
			};

			//bloggieDbContext.Tags.Add(tag);
			await tagRepository.AddAsync(tag);

			return RedirectToAction("List");
		}

		[HttpGet]
		[ActionName("List")]
		//public  IActionResult List()
		public async Task<IActionResult> List()
		{
			// use dbContext to read the tags
			//var tags = bloggieDbContext.Tags.ToList();
			var tags = await tagRepository.GetAllAsync();

			return View(tags);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			// 1st method
			//var tag = bloggieDbContext.Tags.Find(id);

			//2nd method
			var tag = await tagRepository.GetAsync(id);

			if (tag != null)
			{
				var editTagRequest = new EditTagRequest
				{
					Id = tag.Id,
					Name = tag.Name,
					DisplayName = tag.DisplayName
				};

				return View(editTagRequest);
			}

			return View(null);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
		{
			var tag = new Tag
			{
				Id = editTagRequest.Id,
				Name = editTagRequest.Name,
				DisplayName = editTagRequest.DisplayName
			};

			var existingTag = await tagRepository.UpdateAsync(tag);

			if (existingTag != null)
			{
				return RedirectToAction("List");
			}
			else
			{
				return RedirectToAction("Edit", new { id = editTagRequest.Id });
			}

		}

		[HttpPost]
		public async Task<IActionResult> Delete(Guid id)
		{
			var deleteTag = await tagRepository.DeleteAsync(id);

			if (deleteTag != null)
			{
				return RedirectToAction("List");
			}

			//show an error notification
			return View(null);
		}
	}
}