using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace task2.Models
{
    public class Task
    {
        [Key]
        public int regid { get; set; }
        [Required]
        [DisplayName("First name")]
        public string firstname { get; set; }
        [Required]
        [DisplayName("Last name")]
        public string lastname { get; set; }
        [Required]
        [DisplayName("Age")]
        public int age { get; set; }
        [Required]
        [DisplayName("Photo")]
        public string photo { get; set; }
        [Required]
        [DisplayName("Photo")]
        public string biodata { get; set; }

    }

}