using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountControlLib
{
	public class User
	{
		public int ID { get; set; }
		[Required]
		[MaxLength(32)]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }
		[Required]
		[MaxLength(32)]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }
		[Required]
		[RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$", ErrorMessage = "Email not valid!")]
		public string Email { get; set; }
		public Role Role { get; set; }
		[MaxLength(255)]
		public string Adress { get; set; }
		[Required]
		[ScaffoldColumn(false)]
		public string Password { get; set; }
		[ScaffoldColumn(false)]
		[Required]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		////Helpers property
		//[DontShowOnForm]
		//[Required(ErrorMessage = "ConfirmPassword is required!")]
		//[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		//public string ConfirmPassword { get; set; }
		//[DontShowOnForm]
		//public string RoleString { get; set; }
		//[DontShowOnForm]
		//public bool Selected { get; set; }
	}

	//[AttributeUsage(AttributeTargets.Property)]
	//public class DontShowOnForm: Attribute { }
}
