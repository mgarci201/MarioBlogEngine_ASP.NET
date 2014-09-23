using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarioBlog.Core;
using MarioBlog.Core.Objects;
using MarioBlog.Models;


namespace MarioBlog.Controllers
{
    public class BlogController : Controller
    {
        //
        // GET: /Blog/
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public ViewResult Posts(int p = 1)
        {
            //pick latest 10 post
            var viewModel = new ListViewModel(_blogRepository, p);
            ViewBag.Title = "Latest Posts";
            return View("List", viewModel);
        }
        //pick post by category
        public ViewResult Category(string category, int p = 1)
        {
            var viewModel = new ListViewModel(_blogRepository, category, "Category", p);

            if (viewModel.Category == null)
                throw new HttpException(404, "Category does not exist");

            ViewBag.Title = String.Format(@"Latest posts category ""{0}""", viewModel.Category.Name);
            return View("List", viewModel);
        }
        //pick post by tags
        public ViewResult Tag(string tag, int p = 1)
        {
            var viewModel = new ListViewModel(_blogRepository, tag, "Tag", p);

            if (viewModel.Tag == null)
                throw new HttpException(404, "Tag not found");

            ViewBag.Title = String.Format(@"Latest posts tagged on ""{0}""", viewModel.Tag.Name);
            return View("List", viewModel);
        }
        //search post
        public ViewResult Search(string s, int p = 1)
        {
            ViewBag.Title = String.Format(@"Lists of posts found for search text ""{0}""", s);
            var viewModel = new ListViewModel(_blogRepository, s, "Search", p);
            return View("List", viewModel);
        }
        //single post view 
        public ViewResult Post(int year, int month, string title)
        {
            var post = _blogRepository.Post(year, month, title);
            if (post == null)
                throw new HttpException(404, "Sorry, Post not found");

            if (post.Published == false && User.Identity.IsAuthenticated == false)
                throw new HttpException(401, "The post is not published");

            return View(post);
        }
    }
}
