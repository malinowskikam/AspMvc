using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using projekt_dotnet4.Validators;

namespace projekt_dotnet.Models
{
    public class Manufacturer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Nazwa")]
        public String Name { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data powstania")]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        
        [Rating]
        [Display(Name = "Ocena")]
        public double Rating { get; set; }
    }

    public class ManufacturerVM
    {
        public string Man;
        public double Rat;
        public int TCount;
        public double TRating;
    }
}