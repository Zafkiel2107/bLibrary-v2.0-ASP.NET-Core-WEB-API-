using System.ComponentModel.DataAnnotations;

namespace bLibraryAPI.Models.Identity
{
    #region Register
    public class RegisterModel
    {
        [Required, Display(Name = "Имя пользователя")]
        public string Name { get; set; }
        [Required, Display(Name = "Возраст")]
        public int Age { get; set; }
        [Required, Display(Name = "Электронная почта")]
        public string Email { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Пароль")]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare("Password"), Display(Name = "Повторить пароль")]
        public string ConfirmPassword { get; set; }
    }
    #endregion
    #region Login
    public class LoginModel
    {
        [Required, DataType(DataType.EmailAddress), Display(Name = "Электронная почта")]
        public string Email { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Пароль")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
    #endregion
    #region Password
    public class ForgotPasswordModel
    {
        [Required, DataType(DataType.EmailAddress), Display(Name = "Электронная почта")]
        public string CurrentEmail { get; set; }
    }
    public class ForgotPasswordPostModel
    {
        [Required, DataType(DataType.EmailAddress), Display(Name = "Электронная почта")]
        public string CurrentEmail { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Пароль")]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare("Password"), Display(Name = "Повторить пароль")]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
    }
    #endregion
    #region Email
    public class ResetEmailModel
    {
        [Required, DataType(DataType.EmailAddress), Display(Name = "Текущая электронная почта")]
        public string CurrentEmail { get; set; }
        [Required, DataType(DataType.EmailAddress), Display(Name = "Новая электронная почта")]
        public string NewEmail { get; set; }
    }
    public class ResetEmailPostModel
    {
        [Required, DataType(DataType.EmailAddress), Display(Name = "Новая электронная почта")]
        public string CurrentEmail { get; set; }
        [Required, DataType(DataType.EmailAddress), Display(Name = "Новая электронная почта")]
        public string NewEmail { get; set; }
        [Required]
        public string Token { get; set; }
    }
    public class ConfirmEmailModel
    {
        [Required, DataType(DataType.EmailAddress), Display(Name = "Новая электронная почта")]
        public string CurrentEmail { get; set; }
    }
    public class ConfirmEmailPostModel
    {
        [Required, DataType(DataType.EmailAddress), Display(Name = "Новая электронная почта")]
        public string CurrentEmail { get; set; }
        [Required]
        public string Token { get; set; }
    }
    #endregion
}
