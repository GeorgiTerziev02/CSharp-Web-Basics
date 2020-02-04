﻿namespace SulsApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Problem
    {
        public Problem()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Submissions = new HashSet<Submission>();
        }

        public string Id { get; set; }
        
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        [Required]
        public int Points { get; set; }

        public virtual ICollection<Submission> Submissions { get; set; }
    }
}