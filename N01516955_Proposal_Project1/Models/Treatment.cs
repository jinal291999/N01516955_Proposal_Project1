using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace N01516955_Proposal_Project1.Models
{
    public class Treatment
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        //Duration is in minutes
        public int Duration { get; set; }
        //cost is in $
        public decimal Cost { get; set; }

        //list of doctors
        public ICollection<Doctor> Doctors { get; set; }
    }
    public class TreatmentDto
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        //Duration is in minutes
        public int Duration { get; set; }
        //cost is in $
        public decimal Cost { get; set; }

        
        
    }

}