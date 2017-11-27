using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LibraryProject.Models
{
    public class ArgumentView
    {
        [Required]
        public string Question { get; set; }
        [Required]
        public string Side { get; set; }
        [Required]
        public int Autor_Id { get; set; }
        [Required]
        public string ConString { get; set; }
        [Required]
        public string SouString { get; set; }
        [Required]
        public string TypeString { get; set; }
        [Required]
        public string LibraryResource { get; set; }
    }
}