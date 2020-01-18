using FundooBusinessLayer.Interfaces;
using FundooCommonLayer.Model;
using FundooCommonLayer.UserRequestModel;
using FundooRepositoryLayer.Interfaces;
using FundooRepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooBusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL _userRL;
        public UserBL(IUserRL userRL)
        {
            this._userRL = userRL;
        }

        public ResponseModel ForgotPassword(ForgotPassword forgotPassword)
        {
            var data = _userRL.ForgotPassword(forgotPassword);
            if (data != null)
            {
                return data;
            }
            else
            {
                return null;
            }
        }

        public ResponseModel Login(Login login)
        {
            var data = _userRL.Login(login);
            if (data != null)
            {
                return data;
            }
            else
            {
                return null;
            }
        }

        public UserDB Registration(UserDB user)
        {
                if (user != null)
                {
                    return _userRL.Registration(user);
                }
                else 
                {
                    throw new Exception("user is empty");
                }
        }

        public bool ResetPassword(ResetPassword resetPassword)
        {
            if (resetPassword.Id == 0 || resetPassword.password == null)
            {
                return false;
            }
            else
            {
                return _userRL.ResetPassword(resetPassword);
            }
        }
    }
}
