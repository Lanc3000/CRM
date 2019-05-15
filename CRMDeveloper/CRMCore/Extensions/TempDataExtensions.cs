using Microsoft.AspNetCore.Mvc.ViewFeatures;


namespace CRMCore.Extensions
{
    public static class TempDataExtensions
    {
        private const string RedirectMessageKey = "RedirectMessage";
        private const string RedirectStatusKey = "RedirectStatus";

        public static void SetRedirectMessage(this ITempDataDictionary tempData, string message)
        {
            tempData[RedirectMessageKey] = message;
        }

        public static string GetRedirectMessage(this ITempDataDictionary tempData)
        {
            return tempData[RedirectMessageKey] as string;
        }

        public static void SetRedirectStatus(this ITempDataDictionary tempData, RedirectStatus status)
        {
            tempData[RedirectStatusKey] = status;
        }

        public static RedirectStatus GetRedirectStatus(this ITempDataDictionary tempData)
        {
            var status = tempData[RedirectStatusKey];
            if (status == null)
            {
                return RedirectStatus.None;
            }
            return (RedirectStatus)status;
        }

    }

    public enum RedirectStatus
    {
        None = 0,
        Success = 1,
        Error = 2
    }
}
