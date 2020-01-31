using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooCommonLayer.Model
{
    public class CollaborationModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Notes")]
        public int NoteId { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
    }
}
