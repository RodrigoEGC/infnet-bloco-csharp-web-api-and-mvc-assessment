using System;

namespace Domain.Model.Options
{
    public class LibraryHttpOptions
    {
        public Uri ApiBaseUrl { get; set; }
        public string GroupPath { get; set; }
        public string Name { get; set; }
        public int Timeout { get; set; }
    }
}
