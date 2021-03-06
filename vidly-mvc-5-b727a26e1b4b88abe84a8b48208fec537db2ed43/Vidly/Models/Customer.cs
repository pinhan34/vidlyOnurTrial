﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToMyNewsletter { get; set; }

        [Display(Name = "Membership Type")]
        public MemberShipType MemberShipType { get; set; }
        public byte MemberShipTypeId { get; set; }
        public DateTime? Birthdate { get; set; }
        
    }
}