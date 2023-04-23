using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetDotNet.Models
{
    public class Suspect
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SuspectId { get; set; }

        public string? Name { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public int PostalCode { get; set; }

        public int? NumberAdress { get; set; }

        [Required]
        public string Street { get; set; }

        //One to One relationships
        public int? InvestigationId { get; set; }
        public Investigation Investigation { get; set; }
    }
}
