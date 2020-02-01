using FundooCommonLayer.UserRequestModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooBusinessLayer.Interfaces
{
    public interface IAdminSignUpBusiness
    {
        Task<ResponseModel> AdminRegistration(RegistrationRequestModel adminRegistration);
        ResponseModel AdminLogin(Login login);
    }
}
