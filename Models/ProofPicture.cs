using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetDotNet.Models
{
    public class ProofPicture
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProofPictureId { get; set; }

        public byte[] Picture { get; set; }

        public int? VisitId { get; set; }
        public Visit Visit { get; set; }

    }
}
