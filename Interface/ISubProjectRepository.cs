using WBS_API.Model;

namespace WBS_API.Interface
{
    public interface ISubProjectRepository
    {
        public ReturnResponse AddSubProjectAsync(string jsonRequest);
        public ReturnResponse GetTeamAsync(string jsonRequest);
        public ReturnResponse GetClientAsync(string jsonRequest);
        public ReturnResponse GetProjectAsync(string jsonRequest);
        ReturnResponse GetSubProjectList(string jsonRequest);
        ReturnResponse GetViewTeamDataList(string jsonRequest);
        public ReturnResponse GetSprintAsync(string jsonRequest);
        public ReturnResponse GetPMAsync(string jsonRequest);
        public ReturnResponse GetDH(string jsonRequest);
        public ReturnResponse GetTL(string jsonRequest);
        public ReturnResponse GetCR(string jsonRequest);
    }
}
