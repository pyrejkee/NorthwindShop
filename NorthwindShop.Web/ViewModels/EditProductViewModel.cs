using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NorthwindShop.Web.ViewModels
{
    public class EditProductViewModel
    {
        [HiddenInput]
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int SupplierId { get; set; }

        public SelectList Categories { get; set; }
        public SelectList Suppliers { get; set; }
    }
}
