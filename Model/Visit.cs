using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Permissions;

namespace ProjetDotNet.Model
{
    public class Visit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VisitId { get; set; }

        [Required]
        public bool DeliveryNotice { get; set; }

        public byte[]? ProofPicture { get; set; }

        public string? Comments { get; set; }

        [Required]
        public DateTime VisitDate { get; set; }

        public List<Investigator> Investigators { get; set;}
 
    }
}
