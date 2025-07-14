using WBS_API.Model;

namespace WBS_API.Interface
{
    public interface ICommonRepository
    {
        ReturnResponse CommonAsync(string jsonRequest,string FunctionName,string connName);

        public ReturnResponse CommonRepositoryAsync(string jsonRequest, string FunctionName);

        ReturnResponse CommonRepositoryAsync1(string jsonRequest, string FunctionName);
        ReturnResponse GetTimesheetList(string jsonRequest);
        ReturnResponse GetNestedTimesheetList(string jsonRequest);

    }
}
