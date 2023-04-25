using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetDotNet.Models
{
    public enum Status
    {
        Classified, PlaintDeposit, InProgress, Pending
    }

    public class Investigation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvestigationId { get; set; }

        public int InvestigatorId { get; set; }
        public Investigator? Investigator { get; set; }


        public Suspect? Suspect { get; set; }


        public Complainant? Complainant { get; set; }


        [Required]
        public string Reason { get; set; }

        [Required]
        public int NumberOfAnimals { get; set; }

        [Required]
        public string AnimalBreed { get; set; }


        public string? Comments { get; set; }

        [Required]
        public DateTime InvestigationStartDate { get; set; }


        public DateTime? InvestigationEndDate { get; set; }

        [Required]
        public Status Status { get; set; }

        public List<Visit> Visits { get; set; }
    }
}
