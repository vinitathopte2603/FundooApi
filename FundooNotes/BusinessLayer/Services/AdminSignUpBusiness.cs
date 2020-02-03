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

        public Dictionary<string, int> Statistics(int userId)
        {
            if (userId != 0)
            {
                return this._adminSignUpRepository.Statistics(userId);
            }
            else
            {
                return null;
            }
        }
        public List<GetUsersResponseModel> GetUsers(int pageNumber, int pageSize)
        {
            return this._adminSignUpRepository.GetUsers(pageNumber, pageSize);
        }
    }
}
