using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NorthwindShop.Web.ViewModels
{
    public class EditCategoryViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
