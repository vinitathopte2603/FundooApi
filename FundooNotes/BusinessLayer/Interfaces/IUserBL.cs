using FundooCommonLayer.Model;
using FundooCommonLayer.UserRequestModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooBusinessLayer.Interfaces
{
    public interface IUserBL
    {
         UserDB Registration(UserDB user);
         ResponseModel Login(Login login);
        ResponseModel ForgotPassword(ForgotPassword forgotPassword);
        bool ResetPassword(ResetPassword resetPassword);
    }
}
