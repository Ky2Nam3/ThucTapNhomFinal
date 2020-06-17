namespace ThucTapNhomProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietPhieuNhap")]
    public partial class ChiTietPhieuNhap
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaPhieuNhap { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MANL { get; set; }

        public double? DonGia { get; set; }

        public int? SoLuong { get; set; }

        public virtual PhieuNhap PhieuNhap { get; set; }

        public virtual NguyenLieu NguyenLieu { get; set; }
    }
}
