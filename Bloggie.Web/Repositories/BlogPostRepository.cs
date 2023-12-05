﻿using Bloggie.Web.Data;
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

		public Task<BlogPost?> DeleteAsync(Guid Id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<BlogPost?>> GetAllAsync()
		{
			return await bloggieDbContext.BlogPost.Include(x => x.Tags).ToListAsync();

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
