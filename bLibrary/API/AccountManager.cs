using bLibraryAPI.Models.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace bLibrary.API
{
    internal sealed class AccountManager
    {
        private string Host { get; }
        private int Timeout { get; }
        public AccountManager(int timeout = 86400000)
        {
            Host = "https://localhost:7282";
            Timeout = timeout;
        }
        #region Register
        public ActionResult Register(RegisterModel model)
        {
            var request = CreateRequest();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(model));
            var response = request.PostAsync($"{Host}/api/auth/register/", httpContent).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<ActionResult>().Result;
            else
                return null;
        }
        #endregion
        #region Login
        public ActionResult Login(LoginModel model)
        {
            var request = CreateRequest();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(model));
            var response = request.PostAsync($"{Host}/api/auth/login/", httpContent).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<ActionResult>().Result;
            else
                return null;
        }
        public ActionResult Logout()
        {
            var request = CreateRequest();
            var response = request.GetAsync($"{Host}/api/auth/logout/").Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<ActionResult>().Result;
            else
                return null;
        }
        #endregion
        #region Password
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            var request = CreateRequest();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(model));
            var response = request.PostAsync($"{Host}/api/auth/forgotpassword/", httpContent).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<ActionResult>().Result;
            else
                return null;
        }
        public ActionResult ForgotPasswordPost(ForgotPasswordPostModel model)
        {
            var request = CreateRequest();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(model));
            var response = request.PostAsync($"{Host}/api/auth/forgotpasswordpost/", httpContent).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<ActionResult>().Result;
            else
                return null;
        }
        #endregion
        #region Email
        public ActionResult ResetEmail(ResetEmailModel model)
        {
            var request = CreateRequest();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(model));
            var response = request.PostAsync($"{Host}/api/auth/resetemail/", httpContent).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<ActionResult>().Result;
            else
                return null;
        }
        public ActionResult ResetEmailPost(ResetEmailPostModel model)
        {
            var request = CreateRequest();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(model));
            var response = request.PostAsync($"{Host}/api/auth/resetemailpost/", httpContent).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<ActionResult>().Result;
            else
                return null;
        }
        public ActionResult ConfirmEmail(ConfirmEmailModel model)
        {
            var request = CreateRequest();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(model));
            var response = request.PostAsync($"{Host}/api/auth/confirmemail/", httpContent).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<ActionResult>().Result;
            else
                return null;
        }
        public ActionResult ConfirmEmailPost(ConfirmEmailPostModel model)
        {
            var request = CreateRequest();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(model));
            var response = request.PostAsync($"{Host}/api/auth/confirmemailpost/", httpContent).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<ActionResult>().Result;
            else
                return null;
        }
        #endregion
        private HttpClient CreateRequest()
        {
            return new HttpClient()
            {
                Timeout = new TimeSpan(Timeout)
            };
        }
    }
}
