using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace PresentationLayer
{
    public class CreateSuggestionViewModel
    {
        [Required]
        [Display(Name = "Title")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Sport")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Sport { get; set; }

        [Required]
        [Display(Name = "Start date")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End date")]
        [DataType(DataType.DateTime)]
        [DateIsEarlier("StartDate")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Minimum Joined People: ")]
        [DataType(DataType.Text)]
        [RegularExpression("[1-9][0-9]*", ErrorMessage = "Must be a number above 0")]
        public int MinPeople { get; set; }

        [Required]
        [Display(Name = "Maximum Joined People: ")]
        [DataType(DataType.Text)]
        [RegularExpression("[1-9][0-9]*", ErrorMessage = "Must be a number above 0")]
        [IntegerIsLessThanAttribute("MinPeople")]
        public int MaxPeople { get; set; }

        public Weekdays Weekdays { get; set; }
    }
}