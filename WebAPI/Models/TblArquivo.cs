using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ImportApi.Models
{
    [Table("tblArquivo")]
    public partial class TblArquivo
    {
        public TblArquivo()
        {
            TblRegistros = new HashSet<TblRegistro>();
        }

        [Key]
        public int ArquivoId { get; set; }
        public int QuantidadeRegistros { get; set; }
        public int QuantidadeRegistrosImportados { get; set; }
        [Column("DTH_Import", TypeName = "datetime")]
        public DateTime DthImport { get; set; }

        [InverseProperty(nameof(TblRegistro.Arquivo))]
        public virtual ICollection<TblRegistro> TblRegistros { get; set; }
    }
}
