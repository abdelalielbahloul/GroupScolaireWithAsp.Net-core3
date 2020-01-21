using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tp3_MVC.Models
{
    public partial class Student
    {
        [Key]
        [Column("codeStudent", TypeName="int")]
        [Required]
        public int codeStudent { get; set; }
        [Column("codeStudent", TypeName = "VARCHAR(30)")]
        [Required(ErrorMessage ="This field is required")]
        [DisplayName("Full Name")]
        public string fullName { get; set; }
        [Column("old", TypeName = "int")]
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Student's old")]
        public int old { get; set; }
    }
}
