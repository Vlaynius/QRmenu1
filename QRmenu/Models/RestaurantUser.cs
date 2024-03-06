using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QRmenu.Models
{
	public class RestaurantUser
	{
		[Key]
		[Required]
		public int UserID { get; set; }
		[Key]
		[Required]
		public int RestaurantId { get; set; }


		[ForeignKey("RestaurantId")]
		public Restaurant? Restaurant { get; set; }

		[ForeignKey("User")]
		public User? User { get; set; }
	}
}

