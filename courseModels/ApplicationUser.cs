using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace courseModels
{
    public class ApplicationUser:IdentityUser //extened lke this to add more collmns to a database
    {
        [Required]
        public string Name { get; set; }
        public string? streetAddress { get; set; } //add question mark to make them nullable
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode{ get; set; }
        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        [ValidateNever]
        public ComphanyPep comphany { get; set; }
    }
}
