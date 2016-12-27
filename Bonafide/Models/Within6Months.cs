using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Bonafide.Models
{
    public class Within6Months : ValidationAttribute
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {


            var bona = (StudBona)validationContext.ObjectInstance;

            var checkBona = from s in _dbContext.StudBonas
                            where s.PassportNo == bona.PassportNo
                            select s;

            if (checkBona.Count() > 0)
            {
                var check = checkBona.OrderByDescending(s => s.InitDate).First();

                if ( bona.ID==0 && DateTime.Now < check.InitDate.AddMonths(int.Parse(System.Configuration.ConfigurationManager.AppSettings["WithinMonths"].ToString())))
                    return new ValidationResult("Cannot issue BONAFIDE within 6 months");
            }
            return ValidationResult.Success;
        }

    }
}