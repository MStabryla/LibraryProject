using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryProject.Models
{
    public class Essay
    {
        public Essay()
        {
            Arguments = new HashSet<Argument>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public string Side { get; set; }
        public virtual ICollection<Argument> Arguments { get; set; }
        public ICollection<Argument> LiteratureArguments()
        {
            return Arguments.Where(x => x.Kind == (int)KindOfArgument.Lit).ToList();
        }
        public ICollection<Argument> MoviesArguments()
        {
            return Arguments.Where(x => x.Kind == (int)KindOfArgument.Fil).ToList();
        }
        public ICollection<Argument> OtherArguments()
        {
            return Arguments.Where(x => x.Kind == (int)KindOfArgument.Oth).ToList();
        }
        [Required]
        public string LibraryResource { get; set; }
        [Required]
        public virtual Author Author { get; set; }
    }
    public class Argument
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Source { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        [EnumDataType(typeof(KindOfArgument))]
        public int Kind { get; set; }
        public virtual Essay essay { get; set; }
    }
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
    public enum KindOfArgument : int
    {
        Lit = 1,
        Fil = 2,
        Oth = 3
    }
}