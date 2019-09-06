namespace Proveedores
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("PROVEDORES.EMPRESA")]
    public partial class EMPRESA
    {

        [Key]
        public decimal ID_EMPRESA { get; set; }

        [DisplayName("Nombre/Razón social"), Required(ErrorMessage = "Por favor digite el nombre o Razón social")]
        [StringLength(200)]
        public string NOM_EMPRESA { get; set; }
        
        [DisplayName("Documento/NIT"), Required(ErrorMessage = "Por favor digite el numero de documento o nit ")]
        [StringLength(50)]
        public string NIT { get; set; }

        [DisplayName("Dirección"), Required(ErrorMessage = "Por favor digite la dirección")]
        [StringLength(150)]
        public string DIRECCION { get; set; }

        [DisplayName("Correo electrónico"), Required(ErrorMessage = "Por favor digite la dirección")]
        [StringLength(50)]
        public string EMAIL { get; set; }
        
        public static EMPRESA getEmpresaNIT(string nit)
        {
            EMPRESA em = new EMPRESA();

            try
            {
                using (BancoContext bc = new BancoContext())
                {
                    string sql = " SELECT * "
                               + " FROM EMPRESA"
                               + " WHERE NIT = :NIT ";

                    OracleParameter pE = new OracleParameter("NIT", nit);

                    em = bc.Database.SqlQuery<EMPRESA>(sql, pE).Single();
                }
            }
            catch { throw; }

            return em;
        }
        
        public static int getTipoEmpresa(decimal empresa)
        {

            int tipo = -1;

            try
            {
                string sql = " SELECT E.ID_TIPO "
                           + " FROM EMPRESA E "
                           + " INNER JOIN TIPO_EMPRESA TE ON E.ID_TIPO = TE.ID_TIPO "
                           + " WHERE E.ID_EMPRESA = :EMPRESA ";

                using (BancoContext bc = new BancoContext())
                {
                    OracleParameter oE = new OracleParameter("EMPRESA", empresa);

                    tipo = bc.Database.SqlQuery<int>(sql, oE).Single();
                }
            }
            catch { throw; }


            return tipo;
        }

        public static EMPRESA getEmpresa( decimal empresa )
        {
            EMPRESA em = new EMPRESA();

            try
            {
                string sql = " SELECT * "
                           + " FROM EMPRESA E "
                           + " WHERE E.ID_EMPRESA = :EMPRESA ";

                using (BancoContext bc = new BancoContext())
                {
                    OracleParameter oE = new OracleParameter("EMPRESA", empresa);

                    em = bc.Database.SqlQuery<EMPRESA>(sql, oE).Single();
                }
            }
            catch { throw; }


            return em;


        }
        
        public static bool existsEmpresa( string NIT )
        {
            int conteo = -1;

            try
            {
                using (BancoContext bc = new BancoContext())
                {
                    string sql = " SELECT COUNT( ID_EMPRESA ) "
                               + " FROM EMPRESA"
                               + " WHERE NIT = :NIT ";

                    OracleParameter pE = new OracleParameter("NIT", NIT);

                    conteo = bc.Database.SqlQuery<int>(sql, pE).Single();
                }
            }
            catch { throw; }

            return (conteo==0)?false:true;
        }
        
        public static string delEmpresa( string NIT )
        {
            string result = "";
            int total;

            try
            {
                using (BancoContext bc = new BancoContext())
                {
                    string sql = "BEGIN DEL_USUARIO(:VNIT, :RESULT ); END;";
                    
                    OracleParameter pNit = new OracleParameter("VNIT", NIT);
                    OracleParameter pResult = new OracleParameter("RESULT", OracleDbType.Varchar2, 150);
                    pResult.Direction = ParameterDirection.Output;

                    total = bc.Database.ExecuteSqlCommand(sql, pNit,pResult  );
                    result = Convert.ToString(pResult.Value);
                }

            }
            catch { throw; }

            return result;
        }  


    }
}
