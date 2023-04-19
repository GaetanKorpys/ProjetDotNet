using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetDotNet.Model
{
    public class Investigator
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvestigatorId { get; set; }

        [Required]
        public bool IsMain { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public int PostalCode { get; set; }

        [Required]
        public int NumberAdress { get; set; }

        [Required]
        public string Street { get; set; }


        public List<Investigation> Investigations { get; set; }
    }
}
