using FundooBusinessLayer.Interfaces;
using FundooCommonLayer.UserRequestModel;
using FundooRepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooBusinessLayer.Services
{
    public class AdminSignUpBusiness : IAdminSignUpBusiness
    {
        private IAdminSignUpRepository _adminSignUpRepository;
        public AdminSignUpBusiness(IAdminSignUpRepository adminSignUpRepository)
        {
            this._adminSignUpRepository = adminSignUpRepository;
        }

        public ResponseModel AdminLogin(Login login)
        {
            if (login != null)
            {
                return  this._adminSignUpRepository.AdminLogin(login);
            }
            else
            {
                return null;
            }
        }

        public async Task<ResponseModel> AdminRegistration(RegistrationRequestModel adminRegistration)
        {
            if (adminRegistration != null)
            {
                return await this._adminSignUpRepository.AdminRegistration(adminRegistration);
            }
            else
            {
                return null;
            }
        }
    }
}
