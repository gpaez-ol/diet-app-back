using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AlgoFit.Data.BaseEntities
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
