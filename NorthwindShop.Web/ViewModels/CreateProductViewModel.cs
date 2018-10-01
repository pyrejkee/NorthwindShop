﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace NorthwindShop.Web.ViewModels
{
    public class CreateProductViewModel
    {
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
