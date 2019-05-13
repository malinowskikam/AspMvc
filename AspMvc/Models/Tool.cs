using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AspMvc.Validators;
using System.Web.Mvc;

namespace AspMvc.Models
{
    public class Tool
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
        [Display(Name = "Data produkcji")]
        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }

        [Required]
        [PositiveDouble]
        [Display(Name = "Cena")]
        public double Price { get; set; }

        [Required]
        [Rating]
        [Display(Name = "Ocena")]
        public double Rating { get; set; }

        [ForeignKey("Manufacturer")]
        public long ManufacturerId { get; set; }

        [Display(Name = "Producent")]
        public virtual Manufacturer Manufacturer { get; set; }
    }
}