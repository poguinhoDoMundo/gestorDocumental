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
    using System.Web;

    [Table("PROVEDORES.DOCS_CARGADO")]
    public partial class DOCS_CARGADO
    {
       
        [Key]
        public decimal ID_CARGA { get; set; }

        public decimal ID_DOCUMENTO { get; set; }

        public decimal ID_EMPRESA { get; set; }

        public DateTime FECHA_CARGA { get; set; }

        [StringLength(50)]
        public string RUTA { get; set; }
        
        [StringLength(50)]
        public string IP { get; set; }

        public int VERSION { get; set; }

        public decimal ID_ESTADO { get; set; }
        
        [NotMapped]
        public HttpPostedFileBase file { get; set; }


        public static bool isDocCargado( decimal documento, int empresa )
        {
            int resultTmp;

            try
            {
                using (BancoContext bc = new BancoContext())
                {
                    string sql = " SELECT IS_DOC_CARGADO( :DOC, :EMPRESA ) "
                               + " FROM DUAL "  ;

                    OracleParameter oD = new OracleParameter("DOC", documento);
                    OracleParameter oE = new OracleParameter("EMPRESA", empresa);

                    resultTmp = bc.Database.SqlQuery<int>(sql, oD, oE).Single();
                }
                
            }
            catch { throw; }

            return (resultTmp == 0) ? false:true ;
        }

        public string add_documento( string ip  )
        {
            string result = "";
            int total;

            try
            {
                using (BancoContext bc = new BancoContext())
                {
                    string sql = "BEGIN ADD_DOCUMENTO( :IID_DOCUMENTO, :IID_EMPRESA, :VRUTA, :VIP,:RESULT ); END;";

                    OracleParameter pD = new OracleParameter("IID_DOCUMENTO", this.ID_DOCUMENTO );
                    OracleParameter pE = new OracleParameter("IID_EMPRESA", this.ID_EMPRESA );
                    OracleParameter pR = new OracleParameter("VRUTA", this.RUTA );
                    OracleParameter pI = new OracleParameter("VIP", ip);
                    
                    OracleParameter pResult = new OracleParameter("RESULT", OracleDbType.Varchar2, 150);
                    pResult.Direction = ParameterDirection.Output;

                    total = bc.Database.ExecuteSqlCommand(sql, pD, pE, pR, pI,pResult);
                    result = Convert.ToString(pResult.Value);
                }

            }
            catch { throw; }

            return result;
        }


        public static string getEstadoNombre( int empresa, decimal documento )
        {

            string result = "";

            try
            {
                string sql = " SELECT GET_ESTADO_NOMBRE(  :IID_EMPRESA , :IID_DOCUMENTO ) "
                           + " FROM DUAL ";

                using (BancoContext bc = new BancoContext())
                {

                    OracleParameter oE = new OracleParameter("IID_EMPRESA", empresa);
                    OracleParameter oD = new OracleParameter("IID_DOCUMENTO", documento);

                    result = bc.Database.SqlQuery<string>(sql, oE, oD ).Single();
                }
               

            }
            catch { throw; }

            return result;
        }


        public static int getPenultimoEstado(  int empresa, decimal documento )
        {
            int result = 0;

            try
            {
                string sql = " SELECT GET_PENULTIMO_ESTADO(  :IID_EMPRESA , :IID_DOCUMENTO ) "
                           + " FROM DUAL ";

                using (BancoContext bc = new BancoContext())
                {

                    OracleParameter oE = new OracleParameter("IID_EMPRESA", empresa);
                    OracleParameter oD = new OracleParameter("IID_DOCUMENTO", documento);

                    result = bc.Database.SqlQuery<int>(sql, oE, oD).Single();
                }


            }
            catch { throw; }

            return result;            
        }


    }


    public partial class Detalle_documentos
    {
        public string RUTA { get; set; }
        public DateTime FECHA_CARGA { get; set; }
        public int VERSION { get; set; }
        public string NOM_ESTADO { get; set; }

        public static List<Detalle_documentos> get_documentoEmpresa(int empresa, int documento)
        {
            List<Detalle_documentos> cargados = new List<Detalle_documentos>();
            string sql = " SELECT DC.RUTA, DC.FECHA_CARGA, DC.VERSION, E.NOM_ESTADO "
                       + " FROM DOCS_CARGADO DC "
                       + " INNER JOIN ESTADO E ON E.ID_ESTADO = DC.ID_ESTADO "
                       + " WHERE DC.ID_EMPRESA = :ID_EMPRESA AND DC.ID_DOCUMENTO = :ID_DOCUMENTO ";
            try
            {
                using (BancoContext bc = new BancoContext())
                {
                    OracleParameter oE = new OracleParameter("ID_EMPRESA", empresa);
                    OracleParameter oD = new OracleParameter("ID_DOCUMENTO", documento);

                    cargados = bc.Database.SqlQuery<Detalle_documentos>(sql, oE, oD).ToList();
                }
            }
            catch { throw; }

            return cargados;
        }
        

    }

}
