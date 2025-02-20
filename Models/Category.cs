using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Mission07_Sampson.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name is required.")]
        public string CategoryName { get; set; } = "";

        public List<Movie>? Movies { get; set; }  // Navigation property
    }
}
