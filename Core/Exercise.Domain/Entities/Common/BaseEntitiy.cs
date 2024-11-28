using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Domain.Entities.Common
{
	public class BaseEntitiy
	{
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }   
        public DateTime UpdatedDate { get; set; }


        //mig basmayı unutma 
    }
}
