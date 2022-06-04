using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace N01516955_Proposal_Project1.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Speciality { get; set; }
        // Experience is in years
        public int Experience { get; set; }
        //list of treatments
        public ICollection<Treatment> Treatments { get; set; }
    }

    public class DoctorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Speciality { get; set; }
        // Experience is in years
        public int Experience { get; set; }
    }
}