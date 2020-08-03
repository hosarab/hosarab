using System;
using System.ComponentModel.DataAnnotations;

namespace SampleDemo.Dtos
{
    public class PersonCreateDto
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        public DateTime CreatedDate => DateTime.Now;
        
    }
}