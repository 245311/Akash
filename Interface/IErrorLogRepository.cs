using WBS_API.Model;

namespace WBS_API.Interface
{
    public interface IErrorLogRepository
    {
        ReturnResponseDownloadAPI ApiErrorLog(ErrorLog errorLog);
    }
}
