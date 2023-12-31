﻿using System.ComponentModel.DataAnnotations;

namespace Viettel_Solution.Models
{
    public class Solution
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Feature> Features { get; set; }
    }
}
