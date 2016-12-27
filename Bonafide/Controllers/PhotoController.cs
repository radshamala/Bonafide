using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bonafide.Controllers
{
    public class PhotoController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            Session["val"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Imagename)
        {
            string sss = Session["val"].ToString();

            ViewBag.pic = Request.Url.ToString().Split('/')[0] + "/WebImages/" + Session["val"].ToString();

            return View();
        }


        public JsonResult Rebind()
        {
            string[] rad = Request.Url.ToString().Split('/');
            string path = rad[0] + "/" + rad[1] + "/" + rad[2] + "/WebImages/" + Session["val"].ToString();

            return Json(path, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FindPath()
        {
            string[] rad = Request.Url.ToString().Split('/');
            string path = rad[0] + "/" + rad[1] + "/" + rad[2] + "/Bonafide/Index";

            return Json(path, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Capture()
        {
            var stream = Request.InputStream;
            string dump;

            using (var reader = new System.IO.StreamReader(stream))
            {
                dump = reader.ReadToEnd();

                DateTime nm = DateTime.Now;

                string date = nm.ToString("yyyymmddMMss");

                var path = Server.MapPath("~/WebImages/" + date + "test.jpg");

                System.IO.File.WriteAllBytes(path, String_To_Bytes2(dump));

                ViewData["path"] = date + "test.jpg";

                Session["val"] = date + "test.jpg";
            }

            return View("Index");
        }

        private byte[] String_To_Bytes2(string strInput)
        {
            int numBytes = (strInput.Length) / 2;

            byte[] bytes = new byte[numBytes];

            for (int x = 0; x < numBytes; ++x)
            {
                bytes[x] = Convert.ToByte(strInput.Substring(x * 2, 2), 16);
            }

            return bytes;
        }
    }

}
