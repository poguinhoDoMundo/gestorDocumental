namespace Proveedores
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PROVEDORES.TIPO_EMPRESA")]
    public partial class TIPO_EMPRESA
    {
        

        [Key]
        public decimal ID_TIPO { get; set; }

        [StringLength(150)]
        public string NOM_TIPO { get; set; }

    }
}
