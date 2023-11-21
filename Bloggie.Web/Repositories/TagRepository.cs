using Azure;
using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
	public class TagRepository : ITagRepository
	{
		private readonly BloggieDbContext bloggieDbContext;

		public TagRepository(BloggieDbContext bloggieDbContext)
		{
			this.bloggieDbContext = bloggieDbContext;
		}
		public async Task<Tag> AddAsync(Tag tag)
		{
			await bloggieDbContext.Tags.AddAsync(tag);
			bloggieDbContext.SaveChanges();
			return tag;
		}

		public async Task<Tag?> DeleteAsync(Guid id)
		{
			var existingTag = await bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.ID == id);

			if (existingTag != null)
			{
				bloggieDbContext.Tags.Remove(existingTag);
				await bloggieDbContext.SaveChangesAsync();
				return existingTag;
			}

			return null;
		}

		public async Task<IEnumerable<Tag>> GetAllAsync()
		{
			return await bloggieDbContext.Tags.ToListAsync();
		}

		public async Task<Tag?> GetAsync(Guid id)
		{
			return await bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.ID == id);
		}

		public async Task<Tag?> UpdateAsync(Tag tag)
		{
			var existingTag = await bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.ID == tag.ID);
			if (existingTag != null)
			{
				existingTag.Name = tag.Name;
				existingTag.DisplayName = tag.DisplayName;

				// save changes
				await bloggieDbContext.SaveChangesAsync();
				return existingTag;
			}
			return null;
		}
	}
}
