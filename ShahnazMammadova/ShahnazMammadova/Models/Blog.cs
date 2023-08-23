﻿using ShahnazMammadova.Models.Base;

namespace ShahnazMammadova.Models
{
    public class Blog:BaseEntity
    {
        public string NameAz { get; set; }
        public string NameRu { get; set; }
        public string NameEng { get; set; }
        public string FirstImageUrl { get; set; }
        public string? SecondImageUrl { get; set; }
        public string DescriptionAz { get; set; }
        public string DescriptionRu { get; set; }
        public string DescriptionEng { get; set; }
        public string? VideoUrl { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}