using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace QRmenu.Models
{
	public class Status
	{
		//[Required]
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public byte Id { get; set; }
        [Required]
		[StringLength(10)]
		[Column(TypeName = "nvarchar(10)")]
		public string Name { get; set; } = "";

	}
}

