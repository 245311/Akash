using WBS_API.Model;

namespace WBS_API.Interface
{
    public interface IUserRepository
    {
        ReturnResponse AddUserAsync(string jsonRequest);
        ReturnResponse GetUserAsync(string jsonRequest);
        ReturnResponse GetUserIdAsync(string jsonRequest);
        ReturnResponse UpdateUserAsync(string jsonRequest);
        ReturnResponse DeleteUserAsync(string jsonRequest);
        ReturnResponse TeamAsync(string jsonRequest);
        ReturnResponse RoleAsync(string jsonRequest);
        ReturnResponse SkillSetAsync(string jsonRequest);
        ReturnResponse LocationAsync(string jsonRequest);
        ReturnResponse GetRoleList(string jsonRequest);
        ReturnResponse GetPermissionList(string jsonRequest);
        ReturnResponse UpdatePermission(string jsonRequest);
    }
}
