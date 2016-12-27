using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bonafide.ModelViews
{
    public class StudSort
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime IssuedDate { get; set; }
        public string Course { get; set; }
        public int RemFees { get; set; }
        public string Agent { get; set; }
        public int RemCommission { get; set; }
        public string PassportNo { get; set; }
    }
}