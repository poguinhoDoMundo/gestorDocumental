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

    [Table("PROVEDORES.EMPRESA_NAT")]
    public partial class EMPRESA_NAT : EMPRESA
    {
        [Key]
        public decimal ID_NATURAL { get; set; }


        [DisplayName("Tarjeta profesional(en caso de aplicar)")]
        [StringLength(20)]
        public string COPNIA { get; set; }

        [DisplayName("Libreta militar(en caso de aplicar)")]
        [StringLength(20)]
        public string LIBRETA { get; set; }

        [DisplayName("Seleccione su profesión u oficio"), Required(ErrorMessage = "Por favor digite una profesión valida ")]
        public string ID_PROFESION { get; set; }
        
        public string addPropuesta( string ip )
        {
            string result = "";
            int total;

            try
            {
                using (BancoContext bc = new BancoContext())
                {
                    string sql = "BEGIN ADD_EMPRESA_NAT( :VNOMBRE, :VNIT, :VDIRECCION, :VEMAIL,:VIP , :VLIBRETA, :VCOPNIA, :IID_PROFESION, :RESULT ); END;";

                    OracleParameter pNombre = new OracleParameter("VNOMBRE", this.NOM_EMPRESA );
                    OracleParameter pNit = new OracleParameter("VNIT", this.NIT );
                    OracleParameter pDireccion = new OracleParameter("VDIRECCION", this.DIRECCION  );
                    OracleParameter pMail = new OracleParameter("VEMAIL", this.EMAIL );
                    OracleParameter pVIP = new OracleParameter("VIP", ip );
                    OracleParameter pLibreta = new OracleParameter("VLIBRETA", this.LIBRETA);
                    OracleParameter pCopnia = new OracleParameter("VCOPNIA", this.COPNIA  );
                    OracleParameter pProfes = new OracleParameter("IID_PROFESION", this.ID_PROFESION );

                    OracleParameter pResult = new OracleParameter("RESULT", OracleDbType.Varchar2, 150);
                    pResult.Direction = ParameterDirection.Output;

                    total = bc.Database.ExecuteSqlCommand(sql, pNombre, pNit, pDireccion, pMail, pVIP, pLibreta, pCopnia, pProfes, pResult );
                    result = Convert.ToString(pResult.Value);
                }

            }
            catch { throw; }

            return result;
        }


        public static EMPRESA_NAT getEmpresa( int id )
        {
            EMPRESA_NAT em = new EMPRESA_NAT();

            try
            {
                using (BancoContext bc = new BancoContext())
                {
                    string sql = " SELECT * "
                               + " FROM EMPRESA_NATURAL"
                               + " WHERE ID_EMPRESA = :ID_EMPRESA ";

                    OracleParameter pE = new OracleParameter("ID_EMPRESA",id);

                    em = bc.Database.SqlQuery<EMPRESA_NAT>(sql,pE).Single();
                }
            }
            catch { throw; }

            return em;
        }

        
        public string updEmpresa( string ip, int id )
        {
            string result = "";
            int total;

            try
            {
                using (BancoContext bc = new BancoContext())
                {
                    string sql = "BEGIN UPD_EMPRESA_NAT( :VNOMBRE, :VDIRECCION, :VEMAIL, :VID_EMPRESA , :VIP , :VLIBRETA, :VCOPNIA, :IID_PROFESION, :RESULT ); END;";
                    
                    OracleParameter pNombre = new OracleParameter("VNOMBRE", this.NOM_EMPRESA);
                    OracleParameter pDireccion = new OracleParameter("VDIRECCION", this.DIRECCION);
                    OracleParameter pMail = new OracleParameter("VEMAIL", this.EMAIL);
                    OracleParameter pNit = new OracleParameter("VID_EMPRESA", id);
                    OracleParameter pVIP = new OracleParameter("VIP", ip);
                    OracleParameter pLibreta = new OracleParameter("VLIBRETA", this.LIBRETA);
                    OracleParameter pCopnia = new OracleParameter("VCOPNIA", this.COPNIA);
                    OracleParameter pProfes = new OracleParameter("IID_PROFESION", this.ID_PROFESION);

                    OracleParameter pResult = new OracleParameter("RESULT", OracleDbType.Varchar2, 150);
                    pResult.Direction = ParameterDirection.Output;

                    total = bc.Database.ExecuteSqlCommand(sql, pNombre, pDireccion, pMail, pNit ,pVIP, pLibreta, pCopnia, pProfes, pResult);
                    result = Convert.ToString(pResult.Value);
                }

            }
            catch { throw; }

            return result;            
        }


        
    }

    public class NATURAL_DETALLE: EMPRESA_NAT
    {
        public string NOM_PROFESION { get; set; }


        public static NATURAL_DETALLE getNaturalDetalle( int id )
        {
            NATURAL_DETALLE nd = new NATURAL_DETALLE();

            try
            {
                using (BancoContext bc = new BancoContext())
                {
                    string sql = " SELECT * "
                               + " FROM EMPRESA_NATURAL_DETALLE "
                               + " WHERE ID_EMPRESA = :ID_EMPRESA ";

                    OracleParameter pE = new OracleParameter("ID_EMPRESA", id);

                    nd = bc.Database.SqlQuery<NATURAL_DETALLE>(sql, pE).Single();
                }
            }
            catch { throw; }

            return nd;
        }


        public static List<NATURAL_DETALLE> getNaturalesDetalle()
        {
            List<NATURAL_DETALLE> nd = new List<NATURAL_DETALLE>();

            try
            {
                using (BancoContext bc = new BancoContext())
                {
                    string sql = " SELECT * "
                               + " FROM EMPRESA_NATURAL_DETALLE "
                               + " ORDER BY NOM_EMPRESA"; 
                    
                    nd = bc.Database.SqlQuery<NATURAL_DETALLE>(sql).ToList();
                }
            }
            catch { throw; }

            return nd;
        }

        public static List<NATURAL_DETALLE> getNaturalDetalleCondicion( string empresa, string actividad, string nit )
        {
            List<NATURAL_DETALLE> nd = new List<NATURAL_DETALLE>();

            try
            {
                using (BancoContext bc = new BancoContext())
                {
                    string sql = " SELECT * "
                               + " FROM EMPRESA_NATURAL_DETALLE EM "
                               + " WHERE  UPPER(EM.NOM_EMPRESA) LIKE NVL('%' || UPPER(:EMPRESA) || '%', EM.NOM_EMPRESA ) AND "
                               + " UPPER(EM.NOM_PROFESION) LIKE NVL('%' || UPPER(:PROFESION) || '%', EM.NOM_PROFESION ) AND "
                               + " UPPER(EM.NIT) LIKE NVL('%' || UPPER(:VNIT) || '%', EM.NIT ) ";

                    OracleParameter oE = new OracleParameter("EMPRESA", empresa);
                    OracleParameter oP = new OracleParameter("PROFESION", actividad );
                    OracleParameter oN = new OracleParameter("VNIT", nit);
                    
                    nd = bc.Database.SqlQuery<NATURAL_DETALLE>(sql,oE,oP,oN).ToList();
                }
            }
            catch { throw; }

            return nd;
        }
        
    }


}



