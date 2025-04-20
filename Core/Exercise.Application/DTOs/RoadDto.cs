using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exercise.Domain.Entities;

namespace Exercise.Application.DTOs
{
    
    public class RoadDto
    {
        public int Id { get; set; }
        public int RoadStatusId { get; set; }
      
      
    }
}

