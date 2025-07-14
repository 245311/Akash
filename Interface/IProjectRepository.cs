using WBS_API.Model;

namespace WBS_API.Interface
{
    public interface IProjectRepository
    {
        public ReturnResponse ProjectAsync(string jsonRequest);
        public ReturnResponse FetchProjectAsync(string jsonRequest);
        ReturnResponse GetClientNameList(string jsonRequest);
        ReturnResponse GetProjectMasterDataList(string jsonRequest);
        ReturnResponse SaveProjectMasterData(string jsonRequest);
        ReturnResponse FunGetprojectListById(string jsonRequest);
        ReturnResponse EditProjectMasterData(string jsonRequest);
    }
}
