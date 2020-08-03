using System;
using System.ComponentModel.DataAnnotations;

namespace SampleDemo.Dtos
{
    public class PersonUpdateDto
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        
        
        public DateTime CreatedDate { get; set; }
            
    }
}