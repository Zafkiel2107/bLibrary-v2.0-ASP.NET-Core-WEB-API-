using bLibraryAPI.Models.Identity;
using bLibraryAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Security.Cryptography;
using System.Text;

namespace bLibraryAPI.ConnectionManager.DbContextManager
{
    internal class AuthManager : IDisposable
    {
        private UserManager<User> UserManager { get; }
        private SignInManager<User> SignInManager { get; }
        private IEmailSender sender { get; }
        public AuthManager(UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            sender = new EmailService();
        }
        #region Register
        public async Task<int> Register(RegisterModel model)
        {
            User user = new User
            {
                UserName = model.Name,
                Email = model.Email,
                Age = model.Age,
                PasswordHash = GetPasswordHash(model.Password)
            };
            IdentityResult identityUserResult = await UserManager.CreateAsync(user, model.Password);
            IdentityResult identityRoleResult = await UserManager.AddToRoleAsync(user, "User");
            if (identityUserResult.Succeeded && identityRoleResult.Succeeded)
                return 200;
            else
                return 400;
        }
        #endregion
        #region Login
        public async Task<int> Login(LoginModel model)
        {
            User user = UserManager.FindByEmailAsync(model.Email).Result;
            if (user == null)
                return 404;

            var IsValidPassword = await UserManager.CheckPasswordAsync(user,
                GetPasswordHash(model.Password));

            if (!IsValidPassword)
                return 403;

            await SignInManager.SignOutAsync();
            await SignInManager.SignInAsync(user, true);

            return 200;
        }
        public async void Logout() => await SignInManager.SignOutAsync();
        #endregion
        #region Password
        public async Task<int> ForgotPassword(ForgotPasswordModel model)
        {
            if (string.IsNullOrEmpty(model.CurrentEmail))
                return 400;

            User user = await UserManager.FindByEmailAsync(model.CurrentEmail);
            if (user == null)
                return 404;

            var token = await UserManager.GeneratePasswordResetTokenAsync(user);

            var callbackUrl = $"/Account/ForgotPasswordPost/{user.Email}?token={token}";
            await sender.SendEmailAsync(user.Email, "Сброс пароля",
                "Для сброса пароля, перейдите <a href=\"" + callbackUrl + "\">по ссылке</a>");

            return 200;
        }
        public async Task<int> ForgotPasswordPost(ForgotPasswordPostModel model)
        {
            if (model.Password != model.ConfirmPassword)
                return 403;

            User user = await UserManager.FindByEmailAsync(model.CurrentEmail);
            if (user == null)
                return 404;

            IdentityResult result = await UserManager.ResetPasswordAsync(user, model.Token,
                GetPasswordHash(model.Password));

            if (result.Succeeded)
            {
                Logout();
                return 200;
            }
            else
                return 400;
        }
        #endregion
        #region Email
        public async Task<int> ResetEmail(ResetEmailModel model)
        {
            if (string.IsNullOrEmpty(model.CurrentEmail)
                && string.IsNullOrEmpty(model.NewEmail))
                return 400;

            User user = await UserManager.FindByEmailAsync(model.CurrentEmail);
            if (user == null)
                return 404;

            var token = await UserManager.GenerateChangeEmailTokenAsync(user, model.NewEmail);

            var callbackUrl = $"/Account/ResetEmailPost/{model.CurrentEmail}?newEmail={model.NewEmail}?token={token}";
            await sender.SendEmailAsync(model.NewEmail, "Смена почты",
                "Для смены почты, перейдите <a href=\"" + callbackUrl + "\">по ссылке</a>");

            return 200;
        }
        public async Task<int> ResetEmailPost(ResetEmailPostModel model)
        {
            if (string.IsNullOrEmpty(model.CurrentEmail)
                && string.IsNullOrEmpty(model.NewEmail)
                && string.IsNullOrEmpty(model.Token))
                return 400;

            User user = await UserManager.FindByEmailAsync(model.CurrentEmail);
            if (user == null)
                return 404;

            IdentityResult result = await UserManager.ChangeEmailAsync(user, model.NewEmail, model.Token);

            if (result.Succeeded)
            {
                Logout();
                return 200;
            }
            else
                return 400;
        }
        public async Task<int> ConfirmEmail(ConfirmEmailModel model)
        {
            if (string.IsNullOrEmpty(model.CurrentEmail))
                return 400;

            User user = await UserManager.FindByEmailAsync(model.CurrentEmail);
            if (user == null)
                return 404;

            var token = await UserManager.GenerateEmailConfirmationTokenAsync(user);

            var callbackUrl = $"/Account/ConfirmEmailPost/{model.CurrentEmail}?token={token}";
            await sender.SendEmailAsync(model.CurrentEmail, "Подтверждение почты",
                "Для подтверждения почты, перейдите <a href=\"" + callbackUrl + "\">по ссылке</a>");

            return 200;
        }
        public async Task<int> ConfirmEmailPost(ConfirmEmailPostModel model)
        {
            if (string.IsNullOrEmpty(model.CurrentEmail)
                && string.IsNullOrEmpty(model.Token))
                return 400;

            User user = await UserManager.FindByEmailAsync(model.CurrentEmail);
            if (user == null)
                return 404;

            IdentityResult result = await UserManager.ConfirmEmailAsync(user, model.Token);

            if (result.Succeeded)
                return 200;
            else
                return 400;
        }
        #endregion
        public void Dispose()
        {
            this.UserManager.Dispose();
        }
        private string GetPasswordHash(string password)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }
    }
}
