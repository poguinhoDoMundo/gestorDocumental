namespace Proveedores
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("PROVEDORES.DOCUMENTO")]
    public partial class DOCUMENTO
    {


        [Key]
        public decimal ID_DOCUMENTO { get; set; }

        [StringLength(150)]
        public string NOM_DOCUMENTO { get; set; }

        public decimal ID_TIPO { get; set; }

        [NotMapped]
        public string estadoDoc {get;set;}
        
        public  static List<DOCUMENTO> getDocumentosTipo(int tipo)
        {
            List<DOCUMENTO> documentos = new List<DOCUMENTO>();

            using ( BancoContext bc = new BancoContext()  )
            {
                string sql = " SELECT * "
                           + " FROM DOCUMENTO "
                           + " WHERE id_TIPO = :TIPO "
                           + " ORDER BY NOM_DOCUMENTO ";

                OracleParameter oT = new OracleParameter("TIPO",tipo );
                documentos = bc.Database.SqlQuery<DOCUMENTO>(sql, oT).ToList(); 

            }

            return documentos;
        }



        public static string getNomDocumento(decimal tipo)
        {
            string nombre = "";

            using (BancoContext bc = new BancoContext())
            {
                string sql = " SELECT NOM_DOCUMENTO "
                           + " FROM DOCUMENTO "
                           + " WHERE ID_DOCUMENTO = :TIPO ";

                OracleParameter oT = new OracleParameter("TIPO", tipo);
                nombre = bc.Database.SqlQuery<string>(sql, oT).Single();

            }

            return nombre;
        }
    }


    public partial class DocumentoRevision
    {
        public string RUTA { get; set; }	
        public DateTime FECHA_CARGA { get; set; }
        public int VERSION { get; set; }	
        public decimal ID_ESTADO { get; set; }
        public string NOM_DOCUMENTO { get; set; }
        public string NOM_EMPRESA { get; set; }
        public string NOM_ESTADO { get; set; }
        public decimal ID_DOCUMENTO { get; set; }
        public decimal ID_EMPRESA { get; set; }

        public decimal ID_CARGA { get; set; }


        public static  List<DocumentoRevision> getDocumentosRevision( int estado)
        {
            List<DocumentoRevision> documento = new List<DocumentoRevision>();

            string sql = " SELECT * "
                       + " FROM DOCUMENTO_RESUMEN "
                       + " WHERE ID_ESTADO = :ESTADO "
                       + " ORDER BY NOM_EMPRESA, NOM_DOCUMENTO ";

            try
            {
                using (BancoContext bc = new BancoContext())
                {
                    OracleParameter oEs = new OracleParameter("ESTADO", estado);

                    documento = bc.Database.SqlQuery<DocumentoRevision>(sql, oEs).ToList();
                }
            }
            catch { throw;  }


            return documento;
        }
            




    }

}
