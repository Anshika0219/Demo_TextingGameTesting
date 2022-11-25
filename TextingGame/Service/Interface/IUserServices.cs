using Domain;

namespace Service.Interface
{
    public interface IUserServices
    {
        List<TblUserDetail> GetUsers();
        bool CheckUserExist(string email);
        bool Register(Register register);
        bool UserLogIn(UserLogin login);
        bool ForgetPassword(UserLogin changePwd);
    }
}
