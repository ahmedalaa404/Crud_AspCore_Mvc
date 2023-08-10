using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal_CrudOperations.DomainModel
{
	public class ApplicationsUser:IdentityUser
	{
        public string Fname { get; set; }
        public string Lname { get; set; }

        public bool IsAgree { get; set; }

        public string  PhoneNumber { get; set; }

    }
}
