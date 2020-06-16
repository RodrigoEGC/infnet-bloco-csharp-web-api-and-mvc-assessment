using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Entities
{
    public class AlbumEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string RecordCompany { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataLancamento { get; set; }

        public bool Remasterizado { get; set; }

        //public GroupEntity Group { get; set; }
    }
}
