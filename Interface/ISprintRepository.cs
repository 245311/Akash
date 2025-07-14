using WBS_API.Model;

namespace WBS_API.Interface
{
    public interface ISprintRepository
    {
        public ReturnResponse AddSprintAsync(string jsonRequest);
        public ReturnResponse FetchSprintAsync(string jsonRequest);
    }
}
