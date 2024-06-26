﻿using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class PointOfInterestForUpdateDto
    {
        [Required(ErrorMessage = "You should provide a name")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;


        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
