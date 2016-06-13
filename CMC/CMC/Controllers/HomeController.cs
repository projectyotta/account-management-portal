using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMC.DAL;
using CMC.ViewModels; 


namespace CMC.Controllers
{
    public class HomeController : Controller
    {
        private CMCContext db = new CMCContext();

        public ActionResult Index()
        {

            string ppath = Server.MapPath(Url.Content("~\\HomePageData\\CSharpCodingStandards.pdf"));
            iTextSharp.text.pdf.PdfReader pdfReader = new iTextSharp.text.pdf.PdfReader(ppath);
            int numberOfPages = pdfReader.NumberOfPages;
            ViewBag.NumberOfPages = numberOfPages;
            return View();
        }

       public ActionResult About()
        {
            IQueryable<AllocationDateGroup> data = from project in db.Projects
                                                   group project by project.StartDate into dateGroup
                                                   select new AllocationDateGroup()
                                                   {
                                                       AllocationDate = dateGroup.Key,
                                                       ProjectCount = dateGroup.Count()
                                                   };
            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        protected override void Dispose(bool disposing)
{
    db.Dispose();
    base.Dispose(disposing);
}



    }
}

