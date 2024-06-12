using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Models;
using TaskManager.Models.DBModels;

namespace TaskManager.Services
{
	public class LoginService : ILoginService
	{
		#region Fields

		private IUserRepository<UserModel> _userRepository;
		private IPasswordService _passwordService;

		#endregion

		#region Constructors

		public LoginService(IUserRepository<UserModel> userRepository, IPasswordService passwordService)
		{
			_userRepository = userRepository;
			_passwordService = passwordService;
		}

		#endregion

		#region Methods: Private

		private async void SignIn(UserModel user, HttpContext httpContext)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.TelegramNickname),
			};
			ClaimsIdentity claimsId = new ClaimsIdentity(claims, "TaskManagerCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
			await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsId));
		}

		private async Task CheckUserIsNotExists(string username)
		{
            var user = await _userRepository.GetUserByUsername(username);
            if (user != null)
            {
                throw new Exception("Такой пользователь уже существует");
            }
        }

        private void CheckUserIsExists(UserModel user)
        {
            if (user == null)
            {
                throw new Exception("Такого пользователя не существует");
            }
        }

		private void CheckPassword(string password, string userPassword)
		{
            if (!_passwordService.PasswordIsEquals(password, userPassword))
			{
                throw new Exception("Неверный пароль");
            }

        }

        private UserModel CreateUserModel(LoginDataModel loginData)
        {
            var userModel = new UserModel()
            {
                Name = loginData.Username,
                Email = loginData.Email,
                Password = _passwordService.GenerateHashedPassword(loginData.Password),
                TelegramNickname = loginData.TelegramNickname,
                CreatedDate = DateTime.UtcNow, // TODO: Autofill
                UpdatedDate = DateTime.UtcNow,
            };
            return userModel;
        }

        #endregion

        #region Methods: Public

        public async Task<ServiceResult> Login(LoginDataModel loginData, HttpContext httpContext)
		{
            var result = new ServiceResult() { Success = true };
            //try
			//{
                var user = await _userRepository.GetUserByUsername(loginData.Username);

				CheckUserIsExists(user);
				CheckPassword(loginData.Password, user.Password);

                SignIn(user, httpContext);
            //}
			//catch (Exception ex)
			//{
				result.Success = false;
				//result.Message = ex.Message;
			//}
			return result;
		}

		public async Task<ServiceResult> Register(LoginDataModel loginData, HttpContext httpContext)
		{
            var result = new ServiceResult() { Success = true };
			try
			{
                await CheckUserIsNotExists(loginData.Username);

                var userModel = CreateUserModel(loginData);
                await _userRepository.CreateUser(userModel);

                SignIn(userModel, httpContext);
            }
			catch (Exception ex)
			{
                result.Success = false;
                result.Message = ex.Message;
            }
			return result;
		}

		public async Task<ServiceResult> Logout(HttpContext httpContext)
		{
			var result = new ServiceResult() { Success = true };
			try
			{
				await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			}
			catch
			{
				result.Success = false;
				result.Message = "Не удалось выйти из учетной записи";
			}
			return result;
		}

		#endregion
	}
}