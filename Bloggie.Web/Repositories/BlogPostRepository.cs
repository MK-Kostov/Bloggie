using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
	public class BlogPostRepository : IBlogPostRepository
	{
		private readonly BloggieDbContext bloggieDbContext;

		public BlogPostRepository(BloggieDbContext bloggieDbContext)
		{
			this.bloggieDbContext = bloggieDbContext;
		}
		public async Task<BlogPost> AddAsync(BlogPost blogPost)
		{
			await bloggieDbContext.AddAsync(blogPost);
			await bloggieDbContext.SaveChangesAsync();
			return blogPost;

		}

		public async Task<BlogPost?> DeleteAsync(Guid Id)
		{
			var existingBlog = await bloggieDbContext.BlogPost.FindAsync(Id);

			if (existingBlog != null)
			{
				bloggieDbContext.BlogPost.Remove(existingBlog);
				await bloggieDbContext.SaveChangesAsync();
				return existingBlog;
			}

			return null;
		}

		public async Task<IEnumerable<BlogPost?>> GetAllAsync(int page, int itemsPerPage = 6)
		{
			return await bloggieDbContext.BlogPost.Include(x => x.Tags).Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToListAsync();

		}

		public async Task<int> GetCountAsync()
		{
			return await bloggieDbContext.BlogPost.CountAsync();
		}

		public async Task<BlogPost?> GetAsync(Guid Id)
		{
			return await bloggieDbContext.BlogPost.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == Id);
		}

		public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
		{
			var existingBlog = await bloggieDbContext.BlogPost.Include(x => x.Tags)
					.FirstOrDefaultAsync(x => x.Id == blogPost.Id);

			if (existingBlog != null)
			{
				existingBlog.Id = blogPost.Id;
				existingBlog.Heading = blogPost.Heading;
				existingBlog.PageTitle = blogPost.PageTitle;
				existingBlog.Content = blogPost.Content;
				existingBlog.ShortDescription = blogPost.ShortDescription;
				existingBlog.Author = blogPost.Author;
				existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
				existingBlog.URLHandle = blogPost.URLHandle;
				existingBlog.Visible = blogPost.Visible;
				existingBlog.PublishedDate = blogPost.PublishedDate;
				existingBlog.Tags = blogPost.Tags;

				await bloggieDbContext.SaveChangesAsync();
				return existingBlog;
			}

			return null;
		}
	}
}
