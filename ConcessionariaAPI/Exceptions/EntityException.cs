using ConcessionariaAPI.Services;

namespace ConcessionariaAPI.Exceptions
{
    public class EntityException : Exception
    {
        private LogService LogService = new LogService();
        public int StatusCode { get; }
        public string Requisition { get; }
        public EntityException() : base() { }

        public EntityException(string message) : base(message)
        {
            string logMsg = DateTime.Now + " - " + message;
            LogService.SaveLog(logMsg);
        }

        public EntityException(string message, int statusCode, string requisition) : base(message)
        {
            StatusCode = statusCode;
            Requisition = requisition;
            string logMsg = DateTime.Now + " - " + message + " - " + statusCode + " - " + requisition;
            LogService.SaveLog(logMsg);
        }
    }
}
