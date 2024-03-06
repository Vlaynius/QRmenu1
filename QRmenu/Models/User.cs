using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QRmenu.Models
{
	public class User
	{
		public int Id { get; set; }

		[Required]
		public int CompanyID { set; get; }

		[Required]
		[StringLength(100,MinimumLength =3)]
		[Column(TypeName = "nvarchar(100)")]
		public string UserName { get; set; } = "";//Unique Olmalı

        [Required]
        [StringLength(128, MinimumLength = 3)]
        [Column(TypeName = "nvarchar(128)")]
        public string Password { get; set; } = "";

        [Required]
        [StringLength(200, MinimumLength = 3)]
        [Column(TypeName = "nvarchar(200)")]
        public string Name { get; set; } = "";

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        [Phone]
        [Column("varchar(30)")]
        public string Phone { get; set; } = "";

        [Column(TypeName = "smalldatetime")]
        public DateTime RegisterDate { get; set; }

        [Required]
        public byte StatusId { get; set; }

        [ForeignKey("StatusId")]
        public Status? Status { get; set; }

    }
}

