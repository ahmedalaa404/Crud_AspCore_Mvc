using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal_CrudOperations.DomainModel
{
	[NotMapped]
	public  class Email
	{
        public int id { get; set; }
        public string  Subject { get; set; }

        public string  Body	 { get; set; }
		public string To { get; set; }




	}
}
