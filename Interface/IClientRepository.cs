using WBS_API.Model;

namespace WBS_API.Interface
{
    public interface IClientRepository
    {
        ReturnResponse ClientAsync(string jsonRequest);
        ReturnResponse InsertSpocAsync(string jsonRequest);
        ReturnResponse FetchSpocAsync(string jsonRequest);
        ReturnResponse UpdateSpocAsync(string jsonRequest);

    }
}
