namespace Proveedores
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PROVEDORES.ESTADO")]
    public partial class ESTADO
    {

        [Key]
        public decimal ID_ESTADO { get; set; }

        [StringLength(20)]
        public string NOM_ESTADO { get; set; }

    }
}
