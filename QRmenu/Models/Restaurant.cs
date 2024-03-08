using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace QRmenu.Models
{
	public class Restaurant
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100,MinimumLength = 2)]
		[Column(TypeName = "nvarchar(100)")]
		public string Name { get; set; } = "";

		[Required]
		public int CompanyId { get; set; }

		[Required]
		public byte StatusId { get; set; }

        [Required]
        [Phone]
        [Column("varchar(30)")]
        public string Phone { get; set; } = "";

        [Required]
        [StringLength(5, MinimumLength = 5)]
        [Column(TypeName = "char(5)")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; } = "";

        [Required]
        [StringLength(200, MinimumLength = 5)]
        [Column(TypeName = "nvarchar(200)")]
        public string AddressDetail { get; set; } = "";

        [Column(TypeName = "smalldatetime")]
        public DateTime RegisterDate { get; set; }

        [ForeignKey("StatusId")]
        public Status? Status { get; set; }
        
        [ForeignKey("CompanyId")]
        public Company? Company { get; set; }

        public List<Category>? Categories { get; set; }
        public virtual List<User>? Users { get; set; }
        
    }
}

