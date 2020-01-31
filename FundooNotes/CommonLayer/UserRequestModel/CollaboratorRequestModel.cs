using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FundooCommonLayer.UserRequestModel
{
    public class CollaboratorRequestModel
    {
        public int UserId { get; set; }
    }
    public class CollaborateMultiple
    {
        public List<CollaboratorRequestModel> CollaboratorRequestModels { get; set; }
    }
}
