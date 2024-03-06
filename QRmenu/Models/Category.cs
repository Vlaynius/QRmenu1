using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QRmenu.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }

		
		[Required]
		[Column(TypeName = "nvarchar(100)")]
		[StringLength(100,MinimumLength = 2)]
		public string Name { get; set; } = "";

		
		public byte StatusId { get; set; }
		
        
        public int RestaurantId { get; set; }
		
        [ForeignKey("RestaurantId")]
		public Restaurant? Restaurant { get; set; }
		
        [ForeignKey("StatusId")]
        public Status? Status { get; set; }
		
    }
}

