using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;




namespace Bonafide.Models
{
    public class BStudent
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1,6,ErrorMessage ="Not a valid duration")]
        public int Duration { get; set; }

        [Required]
        public string Course { get; set; }

        public string InitDate { get; set; }


        [Required]
        public string Agent { get; set; }


        [Required]
        [Display(Name = "Agent Phone Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered mobile format is not valid.")]
        public string AgentPhone { get; set; }


        [Required]
        [StringLength(1000)]
        public string Note { get; set; }

        [Required]
        [Range(0, 80000)]
        [DataType(DataType.Currency,ErrorMessage = "not valid fees")]
        public int Fees { get; set; }

        [Required]
        [Display(Name = "Fees Paid")]
        [Range(0, 80000)]
        [DataType(DataType.Currency, ErrorMessage = "not valid fees")]
        public int FeesPaid { get; set; }

        [Required]
        [Range(0, 20000)]
        [DataType(DataType.Currency, ErrorMessage = "not valid fees")]
        public int Commission { get; set; }

        [Required]
        [Range(0, 10000)]
        [DataType(DataType.Currency, ErrorMessage = "not valid fees")]
        [Display(Name = "Commission Paid")]
        public int CommissionPaid { get; set; }

        [Required]
        public bool HasArrived { get; set; }
        
        [Required]
        public string BonafideID { get; set; }


    }
}