using System;
using System.Collections.Generic;
using System.Text;

namespace FundooCommonLayer.UserRequestModel
{
    public class GetUsersResponseModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public int NumberOfNotes { get; set; }
        public string Profile { get; set; }
    }
}
