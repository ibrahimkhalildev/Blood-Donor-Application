﻿using System.ComponentModel.DataAnnotations;

namespace BloodDonar.MVC.Models
{
    public class BloodDonorListViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string ContactNumber { get; set; }
        public required int Age { get; set; }
        public required string Email { get; set; }
        public required string BloodGroup { get; set; }
        public string? Address { get; set; }
        public required string LastDonationDate { get; set; }
        public string? ProfilePicture { get; set; }
        public bool isEligible { get; set; }
        public float Weight { get; internal set; }
    }
}
