using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooCommonLayer.Model
{
    public class LabelModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public int? NoteId { get; set; }
        public string Label { get; set; }
        public DateTime IsCreated { get; set; }
        public DateTime IsModified { get; set; }
    }
}
