using Domain.Model.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Presentation.Mvc.ViewModels
{
    public class AlbumViewModel
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

        public int GroupEntityId { get; set; }
        public GroupEntity Group { get; set; }

        public List<SelectListItem> Groups { get; }
        public AlbumViewModel()
        {

        }
        public AlbumViewModel(AlbumEntity albumEntity)
        {
            Title = albumEntity.Title;
            RecordCompany = albumEntity.RecordCompany;
            DataLancamento = albumEntity.DataLancamento;
            Remasterizado = albumEntity.Remasterizado;
            GroupEntityId = albumEntity.GroupEntityId;
            Group = albumEntity.Group;
        }

        public AlbumViewModel(AlbumEntity albumEntity, IEnumerable<GroupEntity> groups) : this(albumEntity)
        {
            Groups = ToGroupSelectListItem(groups);
        }
        public AlbumViewModel(IEnumerable<GroupEntity> groups)
        {
            Groups = ToGroupSelectListItem(groups);
        }

        private static List<SelectListItem> ToGroupSelectListItem(IEnumerable<GroupEntity> groups)
        {
            return groups.Select(x => new SelectListItem
            { Text = $"{x.Name} {x.Genre}", Value = x.Id.ToString() }).ToList();
        }

    }
}
