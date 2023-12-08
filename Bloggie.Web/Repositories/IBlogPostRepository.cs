using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories
{
	public interface IBlogPostRepository
	{
		Task<IEnumerable<BlogPost?>> GetAllAsync();
		Task<IEnumerable<BlogPost?>> GetAllWithPagesAsync(int page, int itemsPerPage = 6);

		Task<int> GetCountAsync();

		Task<BlogPost?> GetAsync(Guid Id);

		Task<BlogPost> AddAsync(BlogPost blogPost);

		Task<BlogPost?> UpdateAsync(BlogPost blogPost);

		Task<BlogPost?> DeleteAsync(Guid Id);
	}
}
