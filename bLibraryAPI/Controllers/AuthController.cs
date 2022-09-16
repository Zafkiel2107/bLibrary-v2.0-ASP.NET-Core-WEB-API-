using bLibraryAPI.ConnectionManager.DbContextManager;
using bLibraryAPI.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private AuthManager Manager { get; }
        public AuthController(UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            Manager = new AuthManager(userManager, signInManager);
        }
        #region Register
        // POST api/auth/register
        [HttpPost, Route("[action]")]
        public ActionResult Register(RegisterModel model)
        {
            var result = Manager.Register(model).Result;
            return ReturnPageWithStatusCode(result);
        }
        #endregion
        #region Login
        // POST api/auth/login
        [HttpPost, Route("[action]")]
        public ActionResult Login(LoginModel model)
        {
            var result = Manager.Login(model).Result;
            return ReturnPageWithStatusCode(result, model.ReturnUrl);
        }
        // POST api/auth/logout
        [HttpGet, Route("[action]")]
        public void Logout() => Manager.Logout();
        #endregion
        #region Password
        // POST api/auth/forgotpassword
        [HttpPost, Route("[action]")]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            var result = Manager.ForgotPassword(model).Result;
            return ReturnPageWithStatusCode(result);
        }
        // POST api/auth/forgotpasswordpost
        [HttpPost, Route("[action]")]
        public ActionResult ForgotPasswordPost(ForgotPasswordPostModel model)
        {
            var result = Manager.ForgotPasswordPost(model).Result;
            return ReturnPageWithStatusCode(result);
        }
        #endregion
        #region Email
        // POST api/auth/resetemail
        [HttpPost, Route("[action]")]
        public ActionResult ResetEmail(ResetEmailModel model)
        {
            var result = Manager.ResetEmail(model).Result;
            return ReturnPageWithStatusCode(result);
        }
        // POST api/auth/resetemailpost
        [HttpPost, Route("[action]")]
        public ActionResult ResetEmailPost(ResetEmailPostModel model)
        {
            var result = Manager.ResetEmailPost(model).Result;
            return ReturnPageWithStatusCode(result);
        }
        // POST api/auth/confirmemail
        [HttpPost, Route("[action]")]
        public ActionResult ConfirmEmail(ConfirmEmailModel model)
        {
            var result = Manager.ConfirmEmail(model).Result;
            return ReturnPageWithStatusCode(result);
        }
        // POST api/auth/confirmemailpost
        [HttpPost, Route("[action]")]
        public ActionResult ConfirmEmailPost(ConfirmEmailPostModel model)
        {
            var result = Manager.ConfirmEmailPost(model).Result;
            return ReturnPageWithStatusCode(result);
        }
        #endregion
        private ActionResult ReturnPageWithStatusCode(int statusCode, string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(returnUrl) && statusCode == 200)
                return Ok(returnUrl);
            else if (string.IsNullOrEmpty(returnUrl) && statusCode == 200)
                return Ok();
            else
                return StatusCode(statusCode);
        }
    }
}
