namespace ThucTapNhomProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string UsernameTK { get; set; }

        [Required]
        [StringLength(15)]
        public string PasswordTK { get; set; }
    }
}
