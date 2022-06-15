using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace N01516955_Proposal_Project1.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int PhoneNo { get; set; }
        public DateTime AppointmentDateTime { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }

    }
    public class AppointmentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int PhoneNo { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public int DoctorId { get; set; }
        public int Id { get; set; }

        
    }
}