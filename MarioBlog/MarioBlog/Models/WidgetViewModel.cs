using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MarioBlog.Core;
using MarioBlog.Core.Objects;

namespace MarioBlog.Models
{
    public class WidgetViewModel
    {
        public WidgetViewModel(IBlogRepository blogRepository)
        {
            Categories = blogRepository.Categories();
            Tags = blogRepository.Tags();
            LatestPost = blogRepository.Posts(0, 10);
        }

        public IList<Category> Categories { get; private set; }
        public IList<Tag> Tags { get; private set; }
        public IList<Post> LatestPost { get; private set; }
    }       
}