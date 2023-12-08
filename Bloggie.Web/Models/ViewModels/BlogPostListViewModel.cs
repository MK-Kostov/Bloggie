using Bloggie.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggie.Web.Models.ViewModels
{
	public class BlogPostListViewModel : PagingViewModel
	{
		public IEnumerable<BlogPost> BlogPosts { get; set; }
	}
}
