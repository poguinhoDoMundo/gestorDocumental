namespace Proveedores
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("PROVEDORES.USUARIO")]
    public partial class USUARIO
    {
        public decimal ID_EMPRESA { get; set; }

        [Column("USUARIO")]
        [StringLength(50)]
        public string DOCUMENTO { get; set; }

        [StringLength(200)]
        public string CLAVE { get; set; }

        public DateTime FECHA_CREACION { get; set; }

        [Key]
        public decimal ID_USUARIO { get; set; }

        public static string changePass( string nit )
        {
            string result = "";
            int total;

            try
            {
                using (BancoContext bc = new BancoContext())
                {
                    string sql = "BEGIN UPD_CONTRA( :VNIT , :SALE ); END;";

                    OracleParameter pNit = new OracleParameter("SALE", nit  );

                    OracleParameter pResult = new OracleParameter("RESULT", OracleDbType.Varchar2, 150);
                    pResult.Direction = ParameterDirection.Output;

                    total = bc.Database.ExecuteSqlCommand(sql, pNit , pResult);
                    result = Convert.ToString(pResult.Value);
                }

            }
            catch { throw; }

            return result;

        }


        public static bool isUser( string user, string pass )
        {
            int result = -1;

            string sql = " SELECT COUNT( ID_USUARIO ) "
                       + " FROM USUARIO "
                       + " WHERE DOCUMENTO = :NIT AND CLAVE = REGIS.MD5(:CLAVE) ";

            try
            {
                using (BancoContext bc = new BancoContext())
                {
                    OracleParameter oN = new OracleParameter( "NIT", user );
                    OracleParameter oP = new OracleParameter( "PASS", pass );

                    result = bc.Database.SqlQuery<int>(sql, oN, oP).Single();

                }

            }
            catch { throw; }

            return (result == 0) ? false : true;
        }


        public static bool isUserAdmon(string user, string pass)
        {
            int result = -1;

            string sql = " SELECT COUNT( * ) "
                       + " FROM USUARIO_ADMON "
                       + " WHERE DOC = :NIT AND PASS = REGIS.MD5(:CLAVE) ";

            try
            {
                using (BancoContext bc = new BancoContext())
                {
                    OracleParameter oN = new OracleParameter("NIT", user);
                    OracleParameter oP = new OracleParameter("PASS", pass);

                    result = bc.Database.SqlQuery<int>(sql, oN, oP).Single();

                }

            }
            catch { throw; }

            return (result == 0) ? false : true;
        }

        public static USUARIO getUsuario( string nit )
        {
            USUARIO usuario = new USUARIO();

            string sql = " SELECT * "
                       + " FROM USUARIO "
                       + " WHERE DOCUMENTO = :NIT ";

            try
            {
                using (BancoContext bc = new BancoContext())
                {
                    OracleParameter oN = new OracleParameter("NIT", nit);
                    usuario = bc.Database.SqlQuery<USUARIO>(sql, oN).Single();
                }

            }
            catch { throw; }
            
            return usuario;
        }


        public static bool isRevisor( string documento )
        {
            int result = 0;

            using (BancoContext bc = new BancoContext())
            {
                string SQL = " SELECT COUNT(*) total "
                           + " FROM USUARIO_ADMON UA "
                           + " WHERE UA.DOC = :DOC AND TIPO = 1 ";

                OracleParameter od = new OracleParameter("DOC", documento );

                result = bc.Database.SqlQuery<int>(SQL, od).Single();                 
            }

            return (result==0)?false:true;
        }


    }
}