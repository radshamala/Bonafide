using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bonafide.Models;
using PagedList;

using Bonafide.ModelViews;
using System.Net;
using System.Text;

namespace Bonafide.Controllers
{
    public class BonafideController : Controller
    {

        private ApplicationDbContext _dbContext;

        public BonafideController()
        {
            _dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }

        // GET: Bonafide
        public ActionResult Index()
        {
            var stud = new StudBona();
            if (Convert.ToString(Session["val"]) != string.Empty)
            {
                ViewBag.pic = Request.Url.ToString().Split('/')[0] + "/WebImages/" + Session["val"].ToString();
            }
            else
            {
                ViewBag.pic = "../../WebImages/lakshmi.jpg";
            }
            //for camera
            Session["val"] = "";
            return View(stud);
        }


        public ActionResult Issued(string Bid, int? Iam)
        {

            if (Iam != 5)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            else
            {
                var stud = TempData["stud"];
                int rad = 0;
                if (Bid != null)
                {
                    stud = _dbContext.StudBonas.FirstOrDefault(s => s.PassportNo == Bid);

                }
                return View(stud);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(StudBona stud)
        {
            if (stud.ID == 0)
            {
                stud.HasArrived = false;
                stud.InitDate = DateTime.Now;
            }
            if (!ModelState.IsValid && stud.ID == 0)
            {
                return View("Index");
            }
            else
            {
                /*
                var tmpID = stud.Name.Substring(0, 3);


                                List<StudBona> tmp = _dbContext.StudBonas.Where(s => s.BonafideID.StartsWith(stud.Name.Substring(0, 3))).OrderByDescending(s => s.ID).ToList();
                                if (tmp.Count == 0)
                                {
                                    stud.BonafideID = tmpID + 1;
                                }
                                else
                                {
                                    stud.BonafideID = tmpID + (int.Parse(tmp[0].BonafideID.Substring(3)) + 1).ToString();
                                }
                                */
                if (stud.ID == 0)
                {//SAVE
                    stud.InitDate = DateTime.Now;
                    stud.Note = DateTime.Now.ToShortDateString() + " : " + stud.Note;
                    stud.Passport = Encoding.ASCII.GetBytes(Session["image"].ToString());
                    _dbContext.StudBonas.Add(stud);
                    TempData["stud"] = stud;
                }
                else
                {//EDIT

                    var studEdit = _dbContext.StudBonas.FirstOrDefault(i => i.ID == stud.ID);

                    studEdit.Duration = stud.Duration;
                    studEdit.FeesPaid = stud.FeesPaid == 0 ? studEdit.FeesPaid : (stud.FeesPaid + studEdit.FeesPaid);
                    studEdit.Commission = stud.Commission == 0 ? studEdit.Commission : (stud.CommissionPaid + studEdit.CommissionPaid);

                    if (!(studEdit.Note.Equals(stud.Note)))
                    {
                        studEdit.Note = studEdit.Note + Environment.NewLine + DateTime.Now.ToShortDateString() + " : " + stud.Note;
                    }

                    studEdit.InitDate = DateTime.Now;
                    TempData["stud"] = studEdit;

                }
                _dbContext.SaveChanges();

                

                return RedirectToAction("Issued", "Bonafide", new { Iam = 5});
            }

        }



        public ActionResult Details(string sortOrder, int? page, string SearchString, string currentFilter, string searchOn)
        {

            ViewBag.CurrentSord = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParam = sortOrder == "date" ? "date_desc" : "date";



            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }

            ViewBag.CurrentFilter = SearchString;

            var students = from s in _dbContext.StudBonas.Where(s => s.HasArrived == false)
                           select new StudSort
                           {
                               ID = s.ID,
                               PassportNo = s.PassportNo,
                               Agent = s.Agent,
                               RemCommission = s.Commission - s.CommissionPaid,
                               Name = s.Name,
                               RemFees = s.Fees - s.FeesPaid,
                               Course = s.Course,
                               IssuedDate = s.InitDate
                           };



            if (SearchString != null)
            {
                if (searchOn == "0")
                    students = students.Where(s => s.Name.Contains(SearchString));
                else
                    students = students.Where(s => s.PassportNo.Equals(SearchString));
            }


            switch (sortOrder)
            {
                case "date":
                    students = students.OrderBy(s => s.IssuedDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.IssuedDate);
                    break;
                case "name_desc":
                    students = students.OrderByDescending(s => s.Name);
                    break;
                default:
                    students = students.OrderBy(s => s.Name);
                    break;
            }


            int pageSize = 3;
            int pageNumber = (page ?? 1);
            ViewBag.CurrentPage = pageNumber;
            return View(students.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Edit(int ID)
        {

            var bona = _dbContext.StudBonas.First(s => s.ID == ID);
            return View(bona);
        }


        public ActionResult HasArrived(int ID)
        {
            var bona = _dbContext.StudBonas.First(s => s.ID == ID);
            return View(bona);
        }


        #region"Camera"



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
            Session["image"] = bytes;

            return bytes;
        }

        #endregion
    }
}