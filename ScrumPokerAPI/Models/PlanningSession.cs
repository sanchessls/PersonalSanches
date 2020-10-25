﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Models
{
    public class PlanningSession
    {
        public int Id { get; set; }
              
        
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser UserCreator { get; internal set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<Feature> Features { get; set; }
        public virtual ICollection<PlanningSessionUser> PlanningSessionUser { get; set; }
    }

    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<PlanningSessionUser> PlanningSessionUser { get; set; }
        public virtual ICollection<FeatureUser> FeatureUser { get; set; }
    }
}
