using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ImportApi.Models
{
    [Table("tblRegistro")]
    public partial class TblRegistro
    {
        [Key]
        public int ArquivoId { get; set; }
        [Key]
        [StringLength(100)]
        public string Tconst { get; set; }
        [StringLength(50)]
        public string TitleType { get; set; }
        [StringLength(500)]
        public string PrimaryTitle { get; set; }
        [StringLength(500)]
        public string OriginalTitle { get; set; }
        public string IsAdult { get; set; }
        [StringLength(5)]
        public string StartYear { get; set; }
        [StringLength(5)]
        public string EndYear { get; set; }
        [StringLength(5)]
        public string RuntimeMinutes { get; set; }
        [StringLength(100)]
        public string Genres { get; set; }
        [Column("DTH_Insert", TypeName = "datetime")]
        public DateTime? DthInsert { get; set; }

        [ForeignKey(nameof(ArquivoId))]
        [InverseProperty(nameof(TblArquivo.TblRegistros))]
        public virtual TblArquivo Arquivo { get; set; }
    }
}
