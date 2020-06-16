using System;

namespace Domain.Model.Options
{
    public class LibraryMusicalHttpOptions
    {
        public Uri ApiBaseUrl { get; set; }
        public string GroupPath { get; set; }
        public string AlbumPath { get; set; }
        public string Name { get; set; }
        public int DayOut { get; set; }
    }
}
