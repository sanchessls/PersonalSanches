﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerPlanning.Models
{
    public class FeatureUser
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; internal set; }
        [Required]
        public int FeatureId { get; set; }
        [ForeignKey("FeatureId")]
        public Feature Feature { get; internal set; }
        public float SelectedValue { get; internal set; }
        public bool Voted { get; internal set; }
    }
}
