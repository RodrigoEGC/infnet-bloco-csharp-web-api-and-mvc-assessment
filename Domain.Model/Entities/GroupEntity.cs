using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Entities
{
    public class GroupEntity
    {
        public int Id { get; set; }

        [StringLength(20, MinimumLength = 3)]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        //[Remote(
        //    action: "CheckName",
        //    controller: "Group",
        //    AdditionalFields = nameof(Id))]

        public string Name { get; set; }

        [StringLength(25, MinimumLength = 3)]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Genre { get; set; }

        [Display(Name = "Formed in")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Este campo é obrigatório")]

        public DateTime Formed { get; set; }

        [StringLength(25, MinimumLength = 3)]
        [Required(ErrorMessage = "Este campo é obrigatório")]

        public string City { get; set; }

        [StringLength(25, MinimumLength = 3)]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Nation { get; set; }
    }
}
