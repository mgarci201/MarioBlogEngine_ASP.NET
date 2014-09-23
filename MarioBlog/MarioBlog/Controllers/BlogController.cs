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
            var viewModel = new ListViewModel(_blogRepository, category, p);

            if (viewModel.Category == null)
                throw new HttpException(404, "Category does not exist");

            ViewBag.Title = String.Format(@"Latest posts category ""{0}""", viewModel.Category.Name);
            return View("List", viewModel);
        }
        //pick post by tags
        //public ViewResult Tag(string tag, int p = 1)
        //{
        //    var viewModel = new ListViewModel(_blogRepository, tag, "Tag", p);

        //    if (viewModel.Tag == null)
        //        throw new HttpException(404, "Tag not found");

        //    ViewBag.Title = String.Format(@"Latest posts tagged on ""{0}""", viewModel.Tag.Name);
        //    return View("List", viewModel);
        //}
    }
}
