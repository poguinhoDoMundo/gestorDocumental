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

    [Table("PROVEDORES.EMPRESA_JUR")]
    public partial class EMPRESA_JUR : EMPRESA
    {
        [Key]
        public decimal ID_JURIDICA { get; set; }

        [DisplayName("Actividad Economica"), Required(ErrorMessage = "Por favor seleccione una actividad economica")]
        public string ID_ACTIVIDAD { get; set; }

        [DisplayName("Nombre del representante legal"), Required(ErrorMessage = "Por favor digite el nombre del representante legal")]
        [StringLength(150)]
        public string NOMREPRESENTANTE { get; set; }

        [DisplayName("Documento del representante legal"), Required(ErrorMessage = "Por favor digite el numero del documento del representante legal")]
        [StringLength(20)]
        public string CEDREPRESENTANE { get; set; }
        
        [DisplayName("Domicilio principal"), Required(ErrorMessage = "Por favor digite el domicilio principal")]
        [StringLength(100)]
        public string DIRECCIONPRINCI { get; set; }

        [DisplayName("Naturaleza de la empresa")]
        public string NATURALEZA { get; set; }

        public string addJuridica(string ip)
        {
            string result = "";
            int total;

            try
            {
                using (BancoContext bc = new BancoContext())
                {
                    string sql = "BEGIN ADD_EMPRESA_JUR( :VNOMBRE, :VNIT, :VDIRECCION, :VEMAIL,:VIP , :VNATURALEZA, "
                               + "                       :IID_ACTIVIDAD, :VNOMBREREP, :VCEDREP, :VDIR_PRINCI, :RESULT); END;";
                    
                    OracleParameter pNombre = new OracleParameter("VNOMBRE", this.NOM_EMPRESA);
                    OracleParameter pNit = new OracleParameter("VNIT", this.NIT);
                    OracleParameter pDireccion = new OracleParameter("VDIRECCION", this.DIRECCION);
                    OracleParameter pMail = new OracleParameter("VEMAIL", this.EMAIL);
                    OracleParameter pVIP = new OracleParameter("VIP", ip);

                    OracleParameter pNat = new OracleParameter("VNATURALEZA", this.NATURALEZA);
                    OracleParameter pActividad = new OracleParameter("IID_ACTIVIDAD", this.ID_ACTIVIDAD);
                    OracleParameter pNomRep = new OracleParameter("VNOMBREREP", this.NOMREPRESENTANTE );
                    OracleParameter pCedRep = new OracleParameter("VCEDREP", this.CEDREPRESENTANE );
                    OracleParameter pDomPrin = new OracleParameter("VDIR_PRINCI", this.DIRECCIONPRINCI );
                    


                    OracleParameter pResult = new OracleParameter("RESULT", OracleDbType.Varchar2, 150);
                    pResult.Direction = ParameterDirection.Output;

                    total = bc.Database.ExecuteSqlCommand(sql, pNombre, pNit, pDireccion, pMail, pVIP,  pNat,pActividad, pNomRep, pCedRep, pDomPrin, pResult);
                    result = Convert.ToString(pResult.Value);
                }

            }
            catch { throw; }

            return result;
        }


        public string updJuridica(string ip, int id)
        {
            string result = "";
            int total;

            try
            {
                using (BancoContext bc = new BancoContext())
                {
                    string sql = "BEGIN UPD_EMPRESA_JUR( :VNOMBRE, :VDIRECCION, :VEMAIL,:VID_EMPRESA ,:VIP , :VNATURALEZA, "
                               + "                       :IID_ACTIVIDAD, :VNOMBREREP, :VCEDREP, :VDIR_PRINCI, :RESULT); END;";
                    
                    OracleParameter pNombre = new OracleParameter("VNOMBRE", this.NOM_EMPRESA);
                    OracleParameter pDireccion = new OracleParameter("VDIRECCION", this.DIRECCION);
                    OracleParameter pMail = new OracleParameter("VEMAIL", this.EMAIL);
                    OracleParameter pEmpresa = new OracleParameter("VID_EMPRESA", id);
                    OracleParameter pVIP = new OracleParameter("VIP", ip);

                    OracleParameter pNat = new OracleParameter("VNATURALEZA", this.NATURALEZA);
                    OracleParameter pActividad = new OracleParameter("IID_ACTIVIDAD", this.ID_ACTIVIDAD);
                    OracleParameter pNomRep = new OracleParameter("VNOMBREREP", this.NOMREPRESENTANTE);
                    OracleParameter pCedRep = new OracleParameter("VCEDREP", this.CEDREPRESENTANE);
                    OracleParameter pDomPrin = new OracleParameter("VDIR_PRINCI", this.DIRECCIONPRINCI);
                    
                    OracleParameter pResult = new OracleParameter("RESULT", OracleDbType.Varchar2, 150);
                    pResult.Direction = ParameterDirection.Output;

                    total = bc.Database.ExecuteSqlCommand(sql, pNombre, pDireccion, pMail, pEmpresa ,pVIP, pNat, pActividad, pNomRep, pCedRep, pDomPrin, pResult);
                    result = Convert.ToString(pResult.Value);
                }

            }
            catch { throw; }

            return result;
        }


        public static EMPRESA_JUR getEmpresaJuridica( int id )
        {
            EMPRESA_JUR em = new EMPRESA_JUR();

            try
            {
                using (BancoContext bc = new BancoContext())
                {
                    string sql = " SELECT * "
                               + " FROM EMPRESA_JURIDICA"
                               + " WHERE ID_EMPRESA = :ID ";

                    OracleParameter pE = new OracleParameter("ID", id);

                    em = bc.Database.SqlQuery<EMPRESA_JUR>(sql, pE).Single();
                }
            }
            catch { throw; }

            return em;
        }
        
    }

    public class JURIDICA_DETALLE:EMPRESA_JUR
    {
        public string NOM_ACTIVIDAD { get; set; }

        public static JURIDICA_DETALLE getJuridicaDetalle(int id)
        {
            JURIDICA_DETALLE em = new JURIDICA_DETALLE();

            try
            {
                using (BancoContext bc = new BancoContext())
                {
                    string sql = " SELECT * "
                               + " FROM JURIDICA_DETALLE "
                               + " WHERE ID_EMPRESA = :ID ";

                    OracleParameter pE = new OracleParameter("ID", id);

                    em = bc.Database.SqlQuery<JURIDICA_DETALLE>(sql, pE).Single();
                }
            }
            catch { throw; }

            return em;
        }

        public static List<JURIDICA_DETALLE> getPersonasJuridicas()
        {
            List<JURIDICA_DETALLE> em = new List<JURIDICA_DETALLE>();

            try
            {
                using (BancoContext bc = new BancoContext())
                {
                    string sql = " SELECT * "
                               + " FROM JURIDICA_DETALLE "
                               + " ORDER BY NOM_EMPRESA";
                    
                    em = bc.Database.SqlQuery<JURIDICA_DETALLE>(sql).ToList();
                }
            }
            catch { throw; }

            return em;
        }

        
        public static List<JURIDICA_DETALLE> getJuridicaDetalleCondicion(string empresa, string actividad, string nit)
        {
            List<JURIDICA_DETALLE> nd = new List<JURIDICA_DETALLE>();

            try
            {
                using (BancoContext bc = new BancoContext())
                {
                    string sql = " SELECT * "
                               + " FROM JURIDICA_DETALLE EM "
                               + " WHERE  UPPER(EM.NOM_EMPRESA) LIKE NVL('%' || UPPER(:EMPRESA) || '%', EM.NOM_EMPRESA ) AND "
                               + " UPPER(EM.NOM_ACTIVIDAD) LIKE NVL('%' || UPPER(:PROFESION) || '%', EM.NOM_ACTIVIDAD ) AND "
                               + " UPPER(EM.NIT) LIKE NVL('%' || UPPER(:VNIT) || '%', EM.NIT ) ";

                    OracleParameter oE = new OracleParameter("EMPRESA", empresa);
                    OracleParameter oP = new OracleParameter("PROFESION", actividad);
                    OracleParameter oN = new OracleParameter("VNIT", nit);

                    nd = bc.Database.SqlQuery<JURIDICA_DETALLE>(sql, oE, oP, oN).ToList();
                }
            }
            catch { throw; }

            return nd;
        }

    }

}