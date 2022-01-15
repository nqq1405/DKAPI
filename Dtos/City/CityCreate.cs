using System;
using System.ComponentModel.DataAnnotations;

namespace DK_API.Dtos.City
{
    public record CityCreate
    {
        [Required]
        public string Name { get; set; }
    }
}