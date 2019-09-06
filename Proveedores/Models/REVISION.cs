namespace Proveedores
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    using System.Data.Entity.Spatial;

    [Table("PROVEDORES.REVISION")]
    public partial class REVISION
    {
        [Key]
        public decimal ID_REVISION { get; set; }

        public decimal ID_CARGA { get; set; }

        public DateTime FECHA_REVISION { get; set; }

        [StringLength(150)]
        public string USUARIO { get; set; }

        public decimal ID_ESTADO { get; set; }
        


        public static string addRevision(decimal carga, string usuario, int estado, string motivo )
        {
            string result = "";
            int total;

            try
            {
                using (BancoContext bc = new BancoContext())
                {
                    string sql = "BEGIN ADD_REVISION( :IID_CARGA , :VUSUARIO , :VESTADO , :VMOTIVO, :RESULT ); END;";

                    OracleParameter oC = new OracleParameter("IID_CARGA", carga );
                    OracleParameter oU = new OracleParameter("VUSUARIO", usuario );
                    OracleParameter oE = new OracleParameter("VESTADO",  estado );
                    OracleParameter oM = new OracleParameter("VMOTIVO", motivo);
                    
                    OracleParameter pResult = new OracleParameter("RESULT", OracleDbType.Varchar2, 150);
                    pResult.Direction = ParameterDirection.Output;

                    total = bc.Database.ExecuteSqlCommand(sql,oC,oU,oE,oM,pResult);
                    result = Convert.ToString(pResult.Value);
                }

            }
            catch { throw; }

            return result;
            
        }

    }

}
