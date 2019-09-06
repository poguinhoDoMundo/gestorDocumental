namespace Proveedores
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("PROVEDORES.PROFESION")]
    public partial class PROFESION
    {
        [Key]
        public decimal ID_PROFESION { get; set; }

        [StringLength(150)]
        public string NOM_PROFESION { get; set; }


        public static List<PROFESION> getProfesiones()
        {
            List<PROFESION> pr = new List<PROFESION>();

            using (  BancoContext bc = new BancoContext() )
            {
                string sql = "SELECT * FROM PROFESION ";
                pr = bc.Database.SqlQuery<PROFESION>(sql).ToList();
            }

            return pr;
        }


        public static List<string> getProfesionesString()
        {

            List<string> ae = new List<string>();

            using (BancoContext bc = new BancoContext())
            {
                string sql = " SELECT '[' || ID_PROFESION || ']' || NOM_PROFESION  NOMBRE "
                           + " FROM PROFESION  ";

                ae = bc.Database.SqlQuery<string>(sql).ToList();
            }

            return ae;
        }

        public static string getprofesionNombre(string id)
        {
            string nombre = "";

            string sql = " SELECT '['|| ID_PROFESION || ']' || NOM_PROFESION actividad "
                       + " FROM PROFESION "
                       + " WHERE ID_PROFESION = TO_NUMBER(:ID_PROFESION) ";

            try
            {
                using (BancoContext bc = new BancoContext())
                {
                    OracleParameter oA = new OracleParameter("ID_PROFESION", id);

                    nombre = bc.Database.SqlQuery<string>(sql, oA).Single();
                }
            }
            catch { throw; }

            return nombre;
        }


    }
}
