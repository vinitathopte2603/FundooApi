using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooCommonLayer.UserRequestModel
{
   public class ResponseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [RegularExpression(@"^[A-Z][a-zA-Z]*$")]
        public string FirstName { get; set; }
        [RegularExpression(@"^[A-Z][a-zA-Z]*$")]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public bool IsActive { get; set; }
        [RegularExpression(@"^[A-Z][a-zA-Z]*$")]
        public string Type { get; set; }
        public DateTime IsCreated { get; set; }
        public DateTime IsModified { get; set; }
    }
}
