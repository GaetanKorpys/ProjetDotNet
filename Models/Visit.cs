using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Permissions;

namespace ProjetDotNet.Models
{
    public class Visit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VisitId { get; set; }

        [Required]
        public bool DeliveryNotice { get; set; }

        public List<ProofPicture> ProofPictures { get; set; } = new List<ProofPicture>();

        [Required]
        public string Comments { get; set; }

        [Required]
        public DateTime VisitDate { get; set; }

        public List<Investigator> Investigators { get; set; } = new List<Investigator>();

        //One to Many relationships
        public int? InvestigationId { get; set; }
        public Investigation Investigation { get; set; }

    }

   
}
