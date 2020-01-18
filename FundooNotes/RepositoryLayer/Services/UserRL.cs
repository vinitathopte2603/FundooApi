using FundooCommonLayer.Model;
using FundooCommonLayer.UserRequestModel;
using FundooRepositoryLayer.Interfaces;
using FundooRepositoryLayer.ModelContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundooRepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly UserContext _context;
        public UserRL(UserContext context)
        {
            this._context = context;
        }

        public ResponseModel ForgotPassword(ForgotPassword forgotPassword)
        {
            var data = _context.Registration.FirstOrDefault(user => user.Email == forgotPassword.Email);
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
                    IsModified = data.IsModified
                };
                return userdata;
            }
            else
            {
                return null;
            }
        }

        public ResponseModel Login(Login login)
        {
            var data = _context.Registration.FirstOrDefault(user => user.Email == login.Email && user.Passwrod == login.Password);
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
                    IsModified = data.IsModified
                };
                return userdata;
            }
            else 
            {
                return null;
            }
        }

        public UserDB Registration(UserDB user)
        {
            user.Passwrod = EncodeDecode.EncodePassword(user.Passwrod);
            user.IsCreated = DateTime.Now;
            user.IsModified = DateTime.Now;
            _context.Registration.Add(user);
            _context.SaveChanges();
            return user;
        }

        public bool ResetPassword(ResetPassword resetPassword)
        {
            try
            {
                var data = _context.Registration.FirstOrDefault(usr => usr.Id == resetPassword.Id);
                if (data != null)
                {
                    resetPassword.password = EncodeDecode.EncodePassword(resetPassword.password);
                    data.Passwrod = resetPassword.password;
                    data.IsModified = DateTime.Now;

                    var user = _context.Registration.Attach(data);
                    user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
