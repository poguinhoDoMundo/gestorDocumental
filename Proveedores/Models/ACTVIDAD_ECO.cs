namespace Proveedores
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("PROVEDORES.ACTVIDAD_ECO")]
    public partial class ACTVIDAD_ECO
    {

        [Key]
        public decimal ID_ACTIVIDAD { get; set; }

        [StringLength(150)]
        public string NOM_ACTIVIDAD { get; set; }


        public static List<ACTVIDAD_ECO> getActividades()
        {

            List<ACTVIDAD_ECO> ae = new List<ACTVIDAD_ECO>();

            using (  BancoContext bc = new BancoContext() )
            {
                string sql = " SELECT * FROM ACTVIDAD_ECO  ";

                ae = bc.Database.SqlQuery<ACTVIDAD_ECO>(sql).ToList();
            }
            
            return ae;
        }


        public static List<string> getActividadesString()
        {

            List<string> ae = new List<string>();

            using (BancoContext bc = new BancoContext())
            {
                string sql = " SELECT '[' || ID_ACTIVIDAD || ']' || NOM_ACTIVIDAD  NOMBRE "
                           + " FROM ACTVIDAD_ECO  ";

                ae = bc.Database.SqlQuery<string>(sql).ToList();
            }

            return ae;
        }

        public static string getActividadNombre( string id )
        {
            string nombre = "";

            string sql = " SELECT '['|| ID_ACTIVIDAD || ']' || NOM_ACTIVIDAD actividad "
                       + " FROM ACTVIDAD_ECO "
                       + " WHERE ID_ACTIVIDAD = TO_NUMBER(:ID_ACTIVIDAD) ";

            try
            {
                using (BancoContext bc = new BancoContext())
                {
                    OracleParameter oA = new OracleParameter("ID_ACTIVIDAD", id);
                    nombre = bc.Database.SqlQuery<string>(sql,oA).Single();
                }
            }
            catch { throw; }

            return nombre;
        }


    }
}
