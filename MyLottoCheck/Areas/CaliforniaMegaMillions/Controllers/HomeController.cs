using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyLottoCheck.Areas.CaliforniaMegaMillions.ViewModels;
using MyLottoCheck.Models;

namespace MyLottoCheck.Areas.CaliforniaMegaMillions.Controllers
{
    //[Authorize]

    [ValidateAntiForgeryTokenOnAllPosts]
    public class HomeController : Controller
    {

        public Func<string> GetUserId; //For unit testing 

        private readonly ICaliforniaMegaMillionRepository _californiaMegaMillionRepository;

        public HomeController(ICaliforniaMegaMillionRepository californiaMegaMillionRepository)  //mock injection point for unit tests
        {
            _californiaMegaMillionRepository = californiaMegaMillionRepository;
            GetUserId = () => "0A4137E5-9582-464F-ADF6-E7087264E1EB";
        }

        public HomeController()
        {
            _californiaMegaMillionRepository = new CaliforniaMegaMillionsRepository(new MyLottoCheckModels());
            GetUserId = () => User.Identity.GetUserId();
        }

        [HttpGet]
        public ActionResult GetWinningDraws()
        {
            //if (User.Identity.IsAuthenticated)
            //{
                using (var context = new MyLottoCheckModels())
                {
                    var query = "MyLottoCheck.GetWinningCaliforniaMegaMillionsDrawsForUser @UserId";
                //var userId = new SqlParameter("@UserId", new Guid(User.Identity.GetUserId()));

                HttpCookie cookie = Request.Cookies["MegaGameUser"];
                var userId = new SqlParameter("@UserId", new Guid(cookie["GameUserId"]));
                    var winningDraws =
                        context.Database.SqlQuery<CaliforniaMegaMillionsUserWinningNumberAndPrize>(query, userId).ToList();
                    //ViewData.Add("LatestUpdateDate", string.Format("{0:dddd MMMM dd, yyyy h:mm tt}", _californiaMegaMillionRepository.GetLatestWinningNumberUpdateDate()));
                    ViewData.Add("LatestUpdateDate", string.Format("{0:MM/dd/yyyy h:mm tt}", _californiaMegaMillionRepository.GetLatestWinningNumberUpdateDate()));
                    return PartialView("_WinningDraws", winningDraws);
                }
            //}
            //return Content("");
        }
        
        [HttpGet]
        public ActionResult GetWinningQuickDraws()
        {
            using (var context = new MyLottoCheckModels())
            {
                var sessionUserId = System.Web.HttpContext.Current.Session["UserId"];

                var query = "MyLottoCheck.GetWinningCaliforniaMegaMillionsDrawsForUser @UserId";
                var userId = new SqlParameter("@UserId", sessionUserId);
                var winningDraws =
                    context.Database.SqlQuery<CaliforniaMegaMillionsUserWinningNumberAndPrize>(query, userId).ToList();
                //ViewData.Add("LatestUpdateDate", string.Format("{0:dddd MMMM dd, yyyy h:mm tt}", _californiaMegaMillionRepository.GetLatestWinningNumberUpdateDate()));
                ViewData.Add("LatestUpdateDate", string.Format("{0:MM/dd/yyyy h:mm tt}", _californiaMegaMillionRepository.GetLatestWinningNumberUpdateDate()));
                return PartialView("_WinningDraws", winningDraws);
            }
        }


        private string GetGameUserId()
        {
            string userId;
            //if (System.Web.HttpContext.Current.Session["CheckType"].ToString() == "set")
            var checkType = Request.QueryString["checkType"];
            //if (Request.QueryString["checkType"] == null || Request.QueryString["checkType"] == "quick" || Request.QueryString["checkType"] == "undefined")
            if (checkType == "quick" || (checkType == null && !User.Identity.IsAuthenticated)  || (checkType == "undefined" && !User.Identity.IsAuthenticated))
            {
                System.Web.HttpContext.Current.Session["UserId"] = Guid.NewGuid().ToString();
                userId = System.Web.HttpContext.Current.Session["UserId"].ToString();
            }
            else
            {
                if (User.Identity.IsAuthenticated)
                    checkType = "set";

                userId = GetUserId();

            }
            return userId;
        }

        [AllowAnonymous]
        public ActionResult Index(string checkType)
        {
            //foreach (string x in Request.ServerVariables)
            //    Response.Write(x + ": " + Request[x]);
            //Response.End();

            if (Request["HTTP_HOST"].ToString().ToLower().Contains("techie-mail.com"))
                return View("TechieMail");

            //if (Request["HTTP_HOST"].ToString().ToLower().Contains("stockdetector.com"))
            //    //return View("TechieMail", _californiaMegaMillionRepository.GetMegaMillionPicksForUser(GetGameUserId()).ToList());
            //    return View("TechieMail");

            //if (Request["HTTP_HOST"].ToString().ToLower().Contains("mylottocheck.com"))
            //    return View("TechieMail", _californiaMegaMillionRepository.GetMegaMillionPicksForUser(GetGameUserId()).ToList());
            //    //return View("TechieMail");

            //if (Request["HTTP_HOST"].ToString().ToLower().Contains("favoritesong.info"))
            //    //return View("TechieMail", _californiaMegaMillionRepository.GetMegaMillionPicksForUser(GetGameUserId()).ToList());
            //    return View("FavoriteSong");

            //if (checkType == null)
            //{
            //    checkType = "";
            //    if (User.Identity.IsAuthenticated)
            //        checkType = "set";
            //}
            System.Web.HttpContext.Current.Session["CheckType"] = "set";

            //if (User.Identity.IsAuthenticated)
            //if (checkType == "set")
            //{
            //    return View("Index", _californiaMegaMillionRepository.GetMegaMillionPicksForUser(GetGameUserId()).ToList());
            //}
            //else
            //{
            //    return View("IndexQuickCheck", _californiaMegaMillionRepository.GetMegaMillionPicksForUser(GetGameUserId()).ToList());
            //}

            HttpCookie cookie = Request.Cookies["MegaGameUser"];
            if (cookie == null)
            {
                cookie = new HttpCookie("MegaGameUser");
                cookie["GameUserId"] = Guid.NewGuid().ToString();
                cookie.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(cookie);

            }

            return View("Index", _californiaMegaMillionRepository.GetMegaMillionPicksForUser(cookie["GameUserId"]).ToList());
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult Create(CaliforniaMegaMillionsUserPickViewModel megaMillionPickVm)
        {
            var megaMillionPick = new CaliforniaMegaMillionUserPick();
            if (ModelState.IsValid)
            {
                try
                {
                    var newPickId = Guid.NewGuid();
                    megaMillionPick.Id = newPickId;
                    //megaMillionPick.UserId = GetGameUserId(); 
                    HttpCookie cookie = Request.Cookies["MegaGameUser"];
                    megaMillionPick.UserId = cookie["GameUserId"];
                    megaMillionPick.FirstPick = megaMillionPickVm.FirstPick;
                    megaMillionPick.SecondPick = megaMillionPickVm.SecondPick;
                    megaMillionPick.ThirdPick = megaMillionPickVm.ThirdPick;
                    megaMillionPick.FourthPick = megaMillionPickVm.FourthPick;
                    megaMillionPick.FifthPick = megaMillionPickVm.FifthPick;
                    megaMillionPick.MegaPick = megaMillionPickVm.MegaPick;
                    megaMillionPick.DateCreated = DateTime.Now;
                    _californiaMegaMillionRepository.InsertMegaMillionPick(megaMillionPick);
                    _californiaMegaMillionRepository.Save(); 
                    Response.StatusCode = new HttpStatusCodeResult(HttpStatusCode.Created).StatusCode;
                    return Json(new { Id = newPickId });
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.InnerException.Message.Contains("UC_MyLottoCheck_CaliforniaMegaMillionUserPicks_RowCheckSum"))
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.Conflict);
                    }
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPut]
        public ActionResult Update(CaliforniaMegaMillionsUserPickViewModel megaMillionPickVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpCookie cookie = Request.Cookies["MegaGameUser"];
                    var UserId = cookie["GameUserId"];
                    var megaMillionPick = new CaliforniaMegaMillionUserPick
                    {
                        Id = megaMillionPickVm.Id,
                        FirstPick = megaMillionPickVm.FirstPick,
                        SecondPick = megaMillionPickVm.SecondPick,
                        ThirdPick = megaMillionPickVm.ThirdPick,
                        FourthPick = megaMillionPickVm.FourthPick,
                        FifthPick = megaMillionPickVm.FifthPick,
                        MegaPick = megaMillionPickVm.MegaPick,
                        //UserId = GetGameUserId()
                        UserId = cookie["GameUserId"]
                    };
                    _californiaMegaMillionRepository.UpdateMegaMillionPick(megaMillionPick);
                    _californiaMegaMillionRepository.Save();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("UC_MyLottoCheck_CaliforniaMegaMillionUserPicks_RowCheckSum"))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Conflict);
                }
                return new HttpStatusCodeResult(HttpStatusCode.NotModified);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotModified);
        }

        public ActionResult Delete(Guid id)
        {
            {
                try
                {
                    var megaMillionPick = _californiaMegaMillionRepository.GetMegaMillionPick(id);
                    _californiaMegaMillionRepository.DeleteMegaMillionPick(megaMillionPick);
                    _californiaMegaMillionRepository.Save();
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
            }
        }

        [HttpGet]
        public ActionResult ErrorPage404()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }

        [HttpGet]
        public ActionResult ElmahExceptionLog()
        {
            if (Request.Url != null)
            {
                string url = Request.Url.Scheme + Uri.SchemeDelimiter + Request.Url.Host +  Url.Content(@"~/Elmah/");

                HttpWebRequest myHttpWebRequest =
                    (HttpWebRequest)
                        WebRequest.Create(url);
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                using (Stream stream = myHttpWebResponse.GetResponseStream())
                {
                    if (stream != null)
                    {
                        StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                        String responseString = reader.ReadToEnd();
                        ViewBag.ElmahContent = responseString;
                    }
                    return View();
                }
            }
            return Content("Null Url!");
        }

        [HttpGet]
        public ActionResult OdataEndpoint()
        {
            if (Request.Url != null)
            {
                 string odataMetaUrl = "http://www.mylottocheck.com/odata/$metadata";
                string odataUrl = "http://www.mylottocheck.com/odata/CaliforniaMegaMillionsWinningNumbers?$orderby=DrawNumber desc,IsMegaNumber";

                HttpWebRequest myHttpWebRequest =
                    (HttpWebRequest)
                        WebRequest.Create(odataUrl);
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                using (Stream stream = myHttpWebResponse.GetResponseStream())
                {
                    if (stream != null)
                    {
                        StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                        String responseString = reader.ReadToEnd();
                        ViewBag.ODataEndpoint = responseString.Replace("{", "<br />{");
                    }
                    ViewBag.ODataEndpointUrl = odataUrl;
                    ViewBag.ODataEndpointMetaUrl = odataMetaUrl;
                    return View();
                }
            }
            return Content("Null Url!");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult SetUserAgreementFlag()
        {
            System.Web.HttpContext.Current.Session["HasUserAgreementBeenSeen"] = "yes";
            return new EmptyResult();
        }

        [HttpGet]
        public ActionResult GenerateError()
        {
            var i = 0;
            var j = 1 / i;
            return Content(j.ToString());
        }

        [AttributeUsage(AttributeTargets.Class)]
        public class ValidateAntiForgeryTokenOnAllPosts : AuthorizeAttribute
        {
            public override void OnAuthorization(AuthorizationContext filterContext)
            {
                var request = filterContext.HttpContext.Request;

                //  Only validate POSTs and PUTs
                if (request.HttpMethod == WebRequestMethods.Http.Post ||
                    request.HttpMethod == WebRequestMethods.Http.Put)
                {
                    //  Ajax POSTs and normal form posts have to be treated differently when it comes
                    //  to validating the AntiForgeryToken
                    if (request.IsAjaxRequest())
                    {
                        var antiForgeryCookie = request.Cookies[AntiForgeryConfig.CookieName];

                        var cookieValue = antiForgeryCookie?.Value;

                        AntiForgery.Validate(cookieValue, request.Headers["__RequestVerificationToken"]);
                    }
                    else
                    {
                        new ValidateAntiForgeryTokenAttribute()
                            .OnAuthorization(filterContext);
                    }
                }
            }
        }
    }
}