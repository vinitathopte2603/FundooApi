using System;
using System.Collections.Generic;
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
                    Type = "Advanced",
                    IsActive = true,
                    IsCreated = DateTime.Now,
                    IsModified = DateTime.Now,
                    UserRole = "Admin"
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
        public Dictionary<string, int> Statistics(int userId)
        {
            Dictionary<string, int> keys = new Dictionary<string, int>();
            int basic = 0;
            int advanced = 0;
            basic = _user.Users.Where(linq => linq.UserRole == "regular user" && linq.Type == "Basic").Count();
            advanced = _user.Users.Where(linq => linq.UserRole == "regular user" && linq.Type == "Advanced").Count();
            keys.Add("Basic", basic);
            keys.Add("Advanced", advanced);
            return keys;
        }
        public List<GetUsersResponseModel> GetUsers(int pageNumber, int pageSize)
        {
            List<GetUsersResponseModel> getUsers = _user.Users.Where(linq => linq.UserRole == "regular user").Select
                (linq => new GetUsersResponseModel
                {
                    UserId = linq.Id,
                    FirstName = linq.FirstName,
                    LastName = linq.LastName,
                    Email = linq.Email,
                    Type = linq.Type
                }).ToList();
            foreach (GetUsersResponseModel get in getUsers)
            {
                get.NumberOfNotes = _user.Notes.Where(linq => linq.ID == get.UserId).Count();
            }
            int count = getUsers.Count();
            int currentPage = pageNumber;
            int sizeOfPage = pageSize;
            int totalPages = (int)Math.Ceiling(count / (double)sizeOfPage);
            if (currentPage == 0)
            {
                currentPage++;
                var items = getUsers.Skip(currentPage - 1 * sizeOfPage).Take(pageSize).ToList();
            }
            int numberOfObjectsPerPage = pageSize;
            var result = getUsers.Skip(numberOfObjectsPerPage * pageNumber).Take(numberOfObjectsPerPage);
            return result.ToList();
        }
    }
}
