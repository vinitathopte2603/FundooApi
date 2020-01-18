using FundooCommonLayer.Model;
using FundooCommonLayer.UserRequestModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepositoryLayer.Interfaces
{
   public interface IUserRL
    {
        UserDB Registration(UserDB user);
        ResponseModel Login(Login login);
        ResponseModel ForgotPassword(ForgotPassword forgotPassword);
        bool ResetPassword(ResetPassword resetPassword);
    }
}
