using WBS_API.Model;

namespace WBS_API.Interface
{
    public interface IRequestResponseLogRepository
    {
        public ReturnResponseDownloadAPI ApiRequestResponseLog(RequestResponseLog requestResponseLog);
        public ReturnResponseDownloadAPI LOG_DB_ApiRequestResponseLog(RequestResponseLog requestResponseLog);
    }
}
