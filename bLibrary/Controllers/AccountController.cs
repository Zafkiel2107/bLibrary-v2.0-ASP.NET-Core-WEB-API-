using bLibrary.API;
using bLibraryAPI.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bLibrary.Controllers
{
    public class AccountController : Controller
    {
        private AccountManager Manager { get; set; }
        public AccountController()
        {
            Manager = new AccountManager();
        }
        #region Register
        [HttpGet, ValidateAntiForgeryToken]
        public ActionResult Register() => View();
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
                return Manager.Register(registerModel);
            else
                return StatusCode(422);
        }
        #endregion
        #region Login
        [HttpGet, ValidateAntiForgeryToken]
        public ActionResult Login(string returnUrl)
        {
            return View(new LoginModel { ReturnUrl = returnUrl });
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
                return Manager.Login(loginModel);
            else
                return StatusCode(422);
        }
        [ValidateAntiForgeryToken, Authorize]
        public ActionResult Logout() => Manager.Logout();
        #endregion
        #region Password
        [HttpGet, ValidateAntiForgeryToken, Authorize]
        public ActionResult ForgotPassword() => View();
        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public ActionResult ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            if (ModelState.IsValid)
                return Manager.ForgotPassword(forgotPasswordModel);
            else
                return StatusCode(422);
        }
        [HttpGet, ValidateAntiForgeryToken, Authorize]
        public ActionResult ForgotPasswordPost(string currentEmail, string token)
        {
            if (token == null || currentEmail == null)
                return StatusCode(422);

            return View(new ForgotPasswordPostModel { CurrentEmail = currentEmail, Token = token });
        }
        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public ActionResult ForgotPasswordPost(ForgotPasswordPostModel forgotPasswordPostModel)
        {
            if (ModelState.IsValid)
                return Manager.ForgotPasswordPost(forgotPasswordPostModel);
            else
                return StatusCode(422);
        }
        #endregion
        #region Email
        [HttpGet, ValidateAntiForgeryToken, Authorize]
        public ActionResult ResetEmail(string currentEmail)
        {
            if(currentEmail == null)
                return StatusCode(422);

            return View(new ResetEmailModel { CurrentEmail = currentEmail });
        }
        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public ActionResult ResetEmail(ResetEmailModel resetEmailModel)
        {
            if (ModelState.IsValid)
                return Manager.ResetEmail(resetEmailModel);
            else
                return StatusCode(422);
        }
        [HttpGet, ValidateAntiForgeryToken, Authorize]
        public ActionResult ResetEmailPost(string currentEmail, string newEmail, string token)
        {
            if (token == null || currentEmail == null || newEmail == null)
                return StatusCode(422);

            return RedirectToAction("ResetEmailPost",
                new ResetEmailPostModel { CurrentEmail = currentEmail, NewEmail = newEmail, Token = token });
        }
        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public ActionResult ResetEmailPost(ResetEmailPostModel resetEmailPostModel)
        {
            if (ModelState.IsValid)
                return Manager.ResetEmailPost(resetEmailPostModel);
            else
                return StatusCode(422);
        }
        [HttpGet, ValidateAntiForgeryToken, Authorize]
        public ActionResult ConfirmEmail(ConfirmEmailModel confirmEmailModel)
        {
            if (ModelState.IsValid)
                return Manager.ConfirmEmail(confirmEmailModel);
            else
                return StatusCode(422);
        }
        [HttpGet, ValidateAntiForgeryToken, Authorize]
        public ActionResult ConfirmEmailPost(string currentEmail, string token)
        {
            if (token == null || currentEmail == null)
                return StatusCode(422);

            return RedirectToAction("ConfirmEmailPost",
                new ConfirmEmailPostModel { CurrentEmail = currentEmail, Token = token });
        }
        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public ActionResult ConfirmEmailPost(ConfirmEmailPostModel confirmEmailPostModel)
        {
            if (ModelState.IsValid)
                return Manager.ConfirmEmailPost(confirmEmailPostModel);
            else
                return StatusCode(422);
        }
        #endregion
    }
}
