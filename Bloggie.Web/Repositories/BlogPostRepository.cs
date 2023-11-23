using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;

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
			bloggieDbContext.SaveChanges();
			return blogPost;

		}

		public Task<BlogPost?> DeleteAsync(Guid Id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<BlogPost?>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<BlogPost> GetAsync(Guid Id)
		{
			throw new NotImplementedException();
		}

		public Task<BlogPost?> UpdateAsync(BlogPost blogPost)
		{
			throw new NotImplementedException();
		}
	}
}
