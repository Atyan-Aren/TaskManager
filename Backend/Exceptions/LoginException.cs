namespace TaskManager.Exceptions
{
    public class LoginException : CustomExceptionBase
    {
        #region Properties
        public override int StatusCode => StatusCodes.Status401Unauthorized;

        #endregion

        #region Constructor

        public LoginException(string message) : base(message) { }

        #endregion

        #region Methods: public

        public override string ToString()
        {
            return $"Ошибка сервиса авторизации:\n{Message}\n{StackTrace}";
        }

        #endregion
    }
}
