using HSBCReward.AppCode.Helpers;
using HSBCReward.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace HSBCReward.AppCode.BAL
{
    public class LoginService
    {

        private HSBCRewardDbContext _dbContext = new HSBCRewardDbContext();
        private LoginService _loginService;
        public int GetUserId(string userId)
        {

            var id = _dbContext.users.Where(x => x.uid == userId).Select(selector => selector.id).SingleOrDefault();

            return id;
        }

         public Guid? GetUserGuid(string Uid)
        {
           
                userlogin user = new userlogin();
                user.Guid = Guid.NewGuid();
                user.UID = Uid;
                user.CreatedDate = DateTime.Now;
                _dbContext.userlogins.Add(user);
                _dbContext.SaveChanges();
                return user.Guid;

            
        }

        public string GetUserUid(string UserGuid)
        {
            try 
            {
                if (UserGuid != "")
                {
                    Guid userGuids = Guid.Parse(UserGuid);
                    return _dbContext.userlogins.FirstOrDefault(x => x.Guid == userGuids)?.UID;
                }
                return UserGuid;
              
            }
            catch (Exception)
            {
                throw;
            }


        }

        public bool ValidateAdminUser(string username,string password )
        {


            string EncryptPassword = GetAdminUserPassword(username);
            string DecryptPassword = HSBCSecurity.ERMDecryptString(EncryptPassword).ToString();
            bool isValid =false;
            if(password == DecryptPassword)
            {
                AdminUser user = _dbContext.AdminUsers.FirstOrDefault(u => u.Email == username && u.HashedPassword == EncryptPassword.Trim() && u.Status == "Active");

                if (user == null)
                {
                    return isValid;
                }
                isValid = true;
            }

            return isValid;

               
            
      
       

        }
        public string GetAdminUserPassword(string email)
        {
            var adminUser = _dbContext.AdminUsers.FirstOrDefault(x => x.Email == email);

            
            if (adminUser != null)
            {
                return adminUser.HashedPassword;
            }
            else
            {
                return null; // Or handle the case when the email is not found in the database
            }
        }


    }
}