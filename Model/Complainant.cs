using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetDotNet.Model
{
    public class Complainant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComplainantId { get; set; }

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


        //One to One relationships
        public string InvestigationId { get; set; }

        public Investigation Investigation { get; set; }
    }
}
