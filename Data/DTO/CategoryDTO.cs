using System;

namespace AlgoFit.Data.DTO
{
    public class CategoryDTO
    {
        public Guid Id {get;set;}
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class CategoryCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CategoryNameDTO
    {
        public string Name {get;set;}
        public string Description {get;set;}
    }
}