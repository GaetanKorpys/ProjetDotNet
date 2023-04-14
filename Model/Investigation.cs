using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetDotNet.Model
{
    public enum Status
    {
        Classified, PlaintDeposit, InProgress, Pending
    }

    public class Investigation
    {
        [Key] 
        public string InvestigationNumber { get; set; }

        public Investigator? Investigator { get; set; }

        [Required]
        public Suspect Suspect { get; set; }

        [Required]
        public Complainant Complainant { get; set; }

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
        public Status Status { get; set; } = Status.Pending;

        public List<Visit> Visits { get; set; }
    }
}
