using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Service.Interface;

namespace Service.Services
{
    public class UserServices : EncryptServices, IUserServices
    {
        //private readonly IUserServices _userServices;
        private readonly DbTextingGameContext _dbContext;
        private readonly IEncryptServices _encrypt;
        public UserServices(DbTextingGameContext dbContext, IEncryptServices encrypt)
        {
            //_userServices = userServices;
            _dbContext = dbContext;
            _encrypt = encrypt;
        }

        //...........fetch User detail.........//
        public List<TblUserDetail> GetUsers()
        {
            List<TblUserDetail> result = (from user in _dbContext.TblUserDetails
                                          select new TblUserDetail
                                          {
                                              UserId = user.UserId,
                                              UserName = user.UserName,
                                              EmailId = user.EmailId,
                                              IsActive = user.IsActive,
                                              CreatedDate = user.CreatedDate,
                                              UpdatedDate = user.UpdatedDate
                                          }).ToList();
            return result.ToList();

            //var users = _dbContext.TblUserDetails.ToList();
            //return users;
        }

        //............Check User Email...........// 
        public bool CheckUserExist(string email)
        {
            var user = _dbContext.TblUserDetails.Where(x => x.EmailId == email).FirstOrDefault();
            return user != null;
        }

        //............Check Password .................//
        public bool Register(Register register)
        {
            EncryptServices encrypt1 = new EncryptServices();
            string encryptPassword = encrypt1.EncodePasswordToBase64(register.Password!);
            register.Password = encryptPassword;
            register.CreatedDate = DateTime.Now;
            register.UpdatedDate = null;
            _dbContext.TblUserDetails.Add(register);
            _dbContext.SaveChanges();
            return true;
        }

        //...........User Login.....................//
        public bool UserLogIn(UserLogin login)
        {
            string encryptPassword = _encrypt.EncodePasswordToBase64(login.Password!);
            var user1 = _dbContext.TblUserDetails.Where(x => x.EmailId == login.EmailId && x.Password == encryptPassword).FirstOrDefault()!;
            if (user1 != null)
            {
                return true;
            }
            return false;
        }

        //...........Forget Password.....................//
        public bool ForgetPassword(UserLogin changePwd)
        {
            TblUserDetail user = _dbContext.TblUserDetails.Where(x => x.EmailId == changePwd.EmailId).FirstOrDefault()!;
            string encryptPassword = _encrypt.EncodePasswordToBase64(changePwd.Password!);
            user.Password = encryptPassword;
            _dbContext.Entry(user).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return true;
        }
    }
}
