using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RolesIS.Models
{
    public class ProductCategory
    {
        [Display(Name = "Ropa")]
        public string Clothing { get; set; }
        [Display(Name = "Accesorios")]
        public string Accessories { get; set; }
        [Display(Name = "Hogar y Decoración")]
        public string HomeAndDecor { get; set; }
        [Display(Name = "Películas y Músia")]
        public string MoviesANdMusic{ get; set; }
        [Display(Name = "Juegos")]
        public string Games { get; set; }
        [Display(Name = "Libros y Revistas")]
        public string BooksAndMagazines { get; set; }
        [Display(Name = "Artesanía")]
        public string Handicraft { get; set; }
        [Display(Name = "Electrónicos")]
        public string Electronics { get; set; }
    }
}