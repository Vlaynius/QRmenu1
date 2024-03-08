using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QRmenu.Models
{
	public class User
	{
		public int Id { get; set; }

		
		
		[StringLength(100,MinimumLength =3)]
		[Column(TypeName = "nvarchar(100)")]
		public string UserName { get; set; } = "";//Unique Olmalı

        [StringLength(128, MinimumLength = 3)]
        [Column(TypeName = "nvarchar(128)")]
        public string Password { get; set; } = "";

        
        [StringLength(200, MinimumLength = 3)]
        [Column(TypeName = "nvarchar(200)")]
        public string Name { get; set; } = "";
        
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Phone]
        [Column("varchar(30)")]
        public string Phone { get; set; } = "";

        [Column(TypeName = "smalldatetime")]
        public DateTime RegisterDate { get; set; }
        

        public byte StatusId { get; set; }

        [ForeignKey("StatusId")]
        public Status? Status { get; set; }

        public int CompanyId { set; get; }

        [ForeignKey("CompanyId")]
        public Company? Company { get; set; }

        public List<Restaurant>? Restaurants { get; set; }

    }
}

