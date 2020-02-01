using System;
using System.Linq;
using System.Threading.Tasks;
using FundooCommonLayer.Model;
using FundooCommonLayer.UserRequestModel;
using FundooRepositoryLayer.Interfaces;
using FundooRepositoryLayer.ModelContext;

namespace FundooRepositoryLayer.Services
{
    public class AdminSignUpRepository : IAdminSignUpRepository
    {
        private readonly UserContext _user;
        public AdminSignUpRepository(UserContext context)
        {
            this._user = context;
        }

        public async Task<ResponseModel> AdminRegistration(RegistrationRequestModel adminRegistration)
        {
            try
            {
                adminRegistration.Passwrod = EncodeDecode.EncodePassword(adminRegistration.Passwrod);
                UserDB dB = new UserDB()
                {
                    FirstName = adminRegistration.FirstName,
                    LastName = adminRegistration.LastName,
                    Email = adminRegistration.Email,
                    Passwrod = adminRegistration.Passwrod,
                    Type = adminRegistration.Type,
                    IsActive = true,
                    IsCreated = DateTime.Now,
                    IsModified = DateTime.Now,
                    UserRole ="Admin"
                };
                _user.Users.Add(dB);
                await _user.SaveChangesAsync();

                ResponseModel responseModel = new ResponseModel()
                {
                    Id = dB.Id,
                    FirstName = dB.FirstName,
                    LastName = dB.LastName,
                    Email = dB.Email,
                    Type = dB.Type,
                    IsActive = dB.IsActive,
                    IsCreated = dB.IsCreated,
                    IsModified = dB.IsModified,
                    UserRole = dB.UserRole
                };
                return responseModel;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ResponseModel AdminLogin(Login login)
        {
            try
            {
                login.Password = EncodeDecode.EncodePassword(login.Password);
                var data = this._user.Users.FirstOrDefault(user => user.Email == login.Email && user.Passwrod == login.Password);
                if (data != null)
                {
                    var userdata = new ResponseModel()
                    {
                        Id = data.Id,
                        FirstName = data.FirstName,
                        LastName = data.LastName,
                        Email = data.Email,
                        IsActive = data.IsActive,
                        IsCreated = data.IsCreated,
                        IsModified = data.IsModified,
                        Type = data.Type,
                        UserRole = data.UserRole
                    };
                    return userdata;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
