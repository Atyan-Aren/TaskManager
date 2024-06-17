namespace TaskManager.Exceptions
{
    public abstract class CustomExceptionBase : Exception
    {
        #region Properties

        public abstract int StatusCode { get; }

        #endregion

        #region Constructor

        public CustomExceptionBase(string message) : base(message) { }

        #endregion
    }
}
