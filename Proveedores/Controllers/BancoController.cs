using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proveedores.Controllers
{
    public class BancoController : Controller
    {
        // GET: Banco
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Natural()
        {
            EMPRESA_NAT en = new EMPRESA_NAT();
            ViewBag.profesiones = PROFESION.getProfesiones();
            
            return View( en );
        }


        public ActionResult Juridica()
        {
            EMPRESA_JUR ej = new EMPRESA_JUR();            
            return View(ej);
        }

        [HttpPost]
        public JsonResult getActividadesString()
        {
            List<string> actividades = ACTVIDAD_ECO.getActividadesString();
            return Json(actividades);
        }



        [HttpPost]
        public JsonResult getProfesionesString()
        {
            List<string> profesiones = PROFESION.getProfesionesString();
            return Json(profesiones);
        }

        public ActionResult updNatural()
        {
            if (Session["id"] == null)
                return RedirectToAction("Login");
            
            int id = Convert.ToInt32(Session["id"]);

            if ( EMPRESA.getTipoEmpresa(id) !=0 )
                return RedirectToAction("Login");
            
            EMPRESA_NAT en = EMPRESA_NAT.getEmpresa(id);
            ViewBag.profesiones = PROFESION.getProfesiones();

            return View( en );
        }


        public JsonResult updateNatural( EMPRESA_NAT em )
        {
            string result="No se pudo modicar, intento nuevamente";
            int id = 0;
            
            if (Session["id"] != null)
            {
                id = Convert.ToInt32(Session["id"]);

                int inicio = em.ID_PROFESION.IndexOf("[");
                int final = em.ID_PROFESION.IndexOf("]");
                if (inicio == -1)
                    return Json("Por favor seleccion un numero de la lista !!!");

                em.ID_PROFESION = em.ID_PROFESION.Substring((inicio + 1), (final - 1));

                if (ModelState.IsValid)
                    result = em.updEmpresa(System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"], id);
            }
            return Json( result );
        } 
        

        public JsonResult addNatural( EMPRESA_NAT en )
        {
            string result = "No se pudieron registrar sus datos, intentelo nuevamente";

            int inicio = en.ID_PROFESION.IndexOf("[");
            int final = en.ID_PROFESION.IndexOf("]");
            if (inicio == -1)
                return Json("Por favor seleccion un numero de la lista !!!");

            en.ID_PROFESION = en.ID_PROFESION.Substring((inicio + 1), (final - 1));
            
            if ( ModelState.IsValid )
            {
                result = en.addPropuesta(System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] );
                if (result == "OK")
                    armarEmail( en.NIT );                  
            }

            return Json( result );
        }


        public JsonResult addJuridica(EMPRESA_JUR ej)
        {
            string result = "No se pudieron registrar sus datos, intentelo nuevamente";
            int inicio = ej.ID_ACTIVIDAD.IndexOf("[");
            int final = ej.ID_ACTIVIDAD.IndexOf("]");
            if (inicio == -1)
                return Json("Por favor seleccion un numero de la lista !!!");

            ej.ID_ACTIVIDAD = ej.ID_ACTIVIDAD.Substring((inicio+1),(final-1));

            
           

            if (ModelState.IsValid)
            {
                result = ej.addJuridica(System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
                if (result == "OK")
                    armarEmail(ej.NIT);
            }

            return Json(result);
        }


        public ActionResult updJuridica()
        {
            if (Session["id"] == null)
                return RedirectToAction("Login");

            int id = Convert.ToInt32(Session["id"]);

            if (EMPRESA.getTipoEmpresa(id) != 1)
                return RedirectToAction("Login");
            
            ViewBag.Actividades = ACTVIDAD_ECO.getActividades();
            EMPRESA_JUR ej = EMPRESA_JUR.getEmpresaJuridica(id);
            
            return View(ej);
        } 

        public JsonResult updateJuridica( EMPRESA_JUR ej )
        {
            int id = 0;
            string result = "La solicitud no pudo ser procesada, inice sesion e intentelo mas tarde";

            if (Session["id"] != null)
            {
                id = Convert.ToInt32(Session["id"]);
                
                int inicio = ej.ID_ACTIVIDAD.IndexOf("[");
                int final = ej.ID_ACTIVIDAD.IndexOf("]");
                if (inicio == -1)
                    return Json("Por favor seleccion un numero de la lista !!!");

                ej.ID_ACTIVIDAD = ej.ID_ACTIVIDAD.Substring((inicio + 1), (final - 1));
                
                if (ModelState.IsValid)
                {
                    result = ej.updJuridica(System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"], id);
                }
            }
            
            return Json(result);
        }


        public ActionResult Message( int id )
        {
            if (id == 1)
                ViewBag.mensaje = " Usted se ha registrado correctamente. "
                                + " Se han enviado sus datos de acceso al correo electrónico digitado anteriormente, por favor revíselo y siga las instrucciones para continuar ...";
            
            return View();
        }

        public ActionResult JuridicaDetalle( int id )
        {
            if (EMPRESA.getTipoEmpresa(id) != 1)
                return RedirectToAction("Login");

            JURIDICA_DETALLE juridica = JURIDICA_DETALLE.getJuridicaDetalle(id);
            return View(juridica);
        }
        
        public ActionResult NaturalDetalle( int id )
        {
            if (EMPRESA.getTipoEmpresa(id) != 0)
                return RedirectToAction("Login");

            NATURAL_DETALLE nd = NATURAL_DETALLE.getNaturalDetalle(id);
            return View(nd);
        }
        

        public ActionResult cambioPassword()
        {
            return View();
        }

        public ActionResult updPassword( string txNIT )
        {
            string result="";

            if (!EMPRESA.existsEmpresa(txNIT))
                ViewBag.result = "NIT o cedula incorrectos, por favor ingrese de nuevo los datos e intentelo nuevamente !!!";
            else
            {
                armarEmail(txNIT);
                ViewBag.result = "Se ha generado y enviado una nueva contraseña al correo !!!";
            }
            return View(result);
        }



        public void armarEmail( string nit  )
        {
           EMPRESA empresa = EMPRESA.getEmpresaNIT(nit);
           
            string asunto = "Universidad de Caldas, Banco de proveedores";
            string body = "Estimado(a) " + empresa.NOM_EMPRESA + " : \n\n";

            body += " Bienvenido a banco de proveedores de la Universidad de Caldas, esta herramienta permite cargar los documentos necesarios para la contratación,"
                  + " una vez cargados entran en una etapa de revisión por parte del equipo de la Universidad"
                  + " y se le notificara en caso de ser rechazado. \n\n "
                  + " Se puede acceder desde cualquier dispositivo con internet desde la dirección "
                  + " http://bancoproveedores.ucaldas.edu.co \n\n "
                  + " Usuario: (número de documento registrado) \n"
                  + " Contraseña: " + USUARIO.changePass(empresa.NIT) + "\n\n"
                  + " Cordial Saludo, Equipo de contratación, Universidad de Caldas ";
  
            string mail = empresa.EMAIL ;
            string message = EnviarEmail("notificaciones.siaucaldas@gmail.com", mail, asunto, body);
        }
        
        public string EnviarEmail(string remitente, string destinatario, string asunto, string cuerpo)
        {
            if (String.IsNullOrEmpty(destinatario))
                return null;

            if (!destinatario.Contains("@"))
                return null;

            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();

            correo.From = new System.Net.Mail.MailAddress(remitente, "UNIVERSIDAD DE CALDAS");
            correo.To.Add(destinatario);
            correo.Subject = asunto;
            correo.Body = cuerpo;
            correo.IsBodyHtml = false;
            correo.Priority = System.Net.Mail.MailPriority.Normal;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Credentials = new System.Net.NetworkCredential("notificaciones.siaucaldas@gmail.com", "regacad2012");
            smtp.Port = 587;
            //smtp.Port = 465;
            smtp.Host = "smtp.googlemail.com";
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(correo);
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public ActionResult cargaArchivo( int doc )
        {
            if (Session["id"] == null)
                return RedirectToAction("Login");

            int id = Convert.ToInt32(Session["id"]);

            ViewBag.cargado = DOCS_CARGADO.isDocCargado( doc , id);
            
            DOCS_CARGADO dc = new DOCS_CARGADO();
            dc.ID_DOCUMENTO = doc;
          
            return PartialView(dc);
        }

       
        public ActionResult addDocumento( DOCS_CARGADO model )
        {
            if (Session["id"] == null)
                return RedirectToAction("Login");
            int id = Convert.ToInt32(Session["id"]);

            bool bandera = true;
            
            ViewBag.documento = model.ID_DOCUMENTO;
            ViewBag.subio = false;

            try
            {
                if (DOCS_CARGADO.isDocCargado(model.ID_DOCUMENTO, id))
                {
                    bandera = false;
                    ViewBag.FileStatus = "El documento se encuentra en etapa de revision. ";
                }

                if ( model.file == null)
                {
                    bandera = false;
                    ViewBag.FileStatus = "Se necesita un archivo adjunto para continuar. ";
                }

                if (!Path.GetFileName(model.file.FileName).ToLower().Contains(".pdf"))
                {
                    bandera = false;
                    ViewBag.FileStatus = "El archivo debe ser pdf no superior a 5 megas. ";
                }

                if (bandera)
                {
                    var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                    var path = Path.Combine(Server.MapPath("~/docs/"), fileName);

                    string ruta = "/docs/" + fileName; ;
                    model.file.SaveAs(path);

                    model.RUTA = ruta;
                    model.ID_EMPRESA = id; 
                    string result = model.add_documento(System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);

                    if (result == "OK")
                    {
                        ViewBag.FileStatus = "Archivo cargado correctamente. ";
                        ViewBag.subio = true;

                        if ( DOCS_CARGADO.getPenultimoEstado(id,model.ID_DOCUMENTO) == 2 )
                        {
                            armarEmailNotiAdmon(id);
                        }

                    }
                    else
                        ViewBag.FileStatus = result;
                }

            }
            catch (Exception ex)
            {
                ViewBag.FileStatus = "No se pudo cargar el archivo." + ex.Message + ". ";
            }

            return PartialView();
        }
        
        public ActionResult cargaDocumentos()
        {
            if (Session["id"] == null)
                return RedirectToAction("Login");

            int id = Convert.ToInt32(Session["id"]) ; 
            
            int tipo = EMPRESA.getTipoEmpresa(id);
            ViewBag.empresa = id;

            List<DOCUMENTO> d = DOCUMENTO.getDocumentosTipo(tipo);
            return View(d);
        }

        public ActionResult Login( string pass, string user  )
        {
            if (user != null && pass != null)
            {
               
                bool result = USUARIO.isUser(user, pass);

                if (result)
                {

                    USUARIO usuario = USUARIO.getUsuario(user);

                    Session["id"] = usuario.ID_EMPRESA;
                    Session["tipo"] = EMPRESA.getTipoEmpresa(usuario.ID_EMPRESA);

                    if (EMPRESA.getTipoEmpresa(usuario.ID_EMPRESA) == 0)
                        return RedirectToAction("NaturalDetalle/" + Convert.ToString(Session["id"])  );
                    else
                        return RedirectToAction("JuridicaDetalle/" + Convert.ToString(Session["id"]) );
                }
                else
                    ViewBag.isValid = "El usuario no se encuentra activo en la base de datos del SIA";
            }

            Session["id"] = null;
            Session["tipo"] = null;
            Session["Admon"] = null;

            return View();
        }
        
        public ActionResult documentosDetalle( )
        {
            if (Session["id"] == null)
                return RedirectToAction("Login");
            List<DOCUMENTO> d = DOCUMENTO.getDocumentosTipo( Convert.ToInt16(Session["tipo"]) );
            
            return View(d);
        }


        public ActionResult documentoEmpresa(int doc)
        {
            if (Session["id"] == null)
                return RedirectToAction("Login");

            int id = Convert.ToInt32(Session["id"]);
            List <Detalle_documentos> cargados =  Detalle_documentos.get_documentoEmpresa(id,doc);
            
            
            return PartialView( cargados );
        }
        
        /*********************/
        
        public ActionResult revisionDocumento()
        {
            if (Session["Admon"] == null)
                return RedirectToAction("LoginAdmon");

            if (!USUARIO.isRevisor(Convert.ToString(Session["Admon"])))
                    return RedirectToAction("LoginAdmon");

            List<DocumentoRevision> documento = DocumentoRevision.getDocumentosRevision(0);

            return View(documento);
        }

        public JsonResult addRevision(decimal carga, int estado, string motivo, decimal id_empresa  )
        {
            string result = "";

            if (Session["Admon"] != null)
            {
                if (USUARIO.isRevisor(Convert.ToString(Session["Admon"])))
                {
                    string usuario = Session["Admon"].ToString();

                    result = REVISION.addRevision(carga, usuario, estado, motivo);

                    if (estado == 2)
                        armarEmailRechazo(id_empresa, motivo);
                }
            }
            return Json(result);
        }
        

        public void armarEmailRechazo(decimal id, string motivo)
        {
            EMPRESA empresa = EMPRESA.getEmpresa(id);

            string asunto = "Universidad de Caldas, Banco de proveedores";
            string body = "Estimado(a) " + empresa.NOM_EMPRESA + " : \n\n";

            body += "Uno de los documentos cargados por usted fue rechazado con la observación: '" + motivo 
                  + "'.\n Por favor ingrese a la página http://bancoproveedores.ucaldas.edu.co con su usuario y contraseña," 
                  + " ingrese a la opción 'ver documentos' para ver la novedad y cargue el nuevo documento desde la opcion 'subir documentos'.\n\n"
                  + " Cordial Saludo, Equipo de contratación, Universidad de Caldas ";  


            string mail = empresa.EMAIL;
            string message = EnviarEmail("notificaciones.siaucaldas@gmail.com", mail, asunto, body);
        }


        public void armarEmailNotiAdmon(decimal id)
        {
            EMPRESA empresa = EMPRESA.getEmpresa(id);

            string asunto = "Universidad de Caldas, Banco de proveedores";
            string body = "Estimado(a) : \n\n";

            body += " La empresa "  +  empresa.NOM_EMPRESA +  " ha cargado un nuevo archivo despues de haber sido rechazado. '.\n\n"
                  + " Cordial Saludo, Equipo de contratación, Universidad de Caldas ";

            string message = EnviarEmail("notificaciones.siaucaldas@gmail.com", "banco.proveedores@ucaldas.edu.co", asunto, body);
        }

        public ActionResult ResumenAdmon()
        {
            if (Session["Admon"] == null)
                return RedirectToAction("LoginAdmon");

            return View();
        }

        public ActionResult Personasjuridicas()
        {
            if (Session["Admon"] == null)
                return RedirectToAction("LoginAdmon");

            List<JURIDICA_DETALLE> juridicas = JURIDICA_DETALLE.getPersonasJuridicas();            
            return PartialView(juridicas);
        }

        public ActionResult PersonasNaturales()
        {
            if (Session["Admon"] == null)
                return RedirectToAction("LoginAdmon");

            List<NATURAL_DETALLE> juridicas = NATURAL_DETALLE.getNaturalesDetalle();
            return PartialView(juridicas);
        }


        public ActionResult empresaJurAdmon( int id  )
        {
            if (Session["Admon"] == null)
                return RedirectToAction("LoginAdmon");

            JURIDICA_DETALLE juridica = JURIDICA_DETALLE.getJuridicaDetalle(id);            
            return PartialView(juridica);
        }

        public ActionResult empresaNatAdmon( int id )
        {
            if (Session["Admon"] == null)
                return RedirectToAction("LoginAdmon");

            NATURAL_DETALLE natural = NATURAL_DETALLE.getNaturalDetalle(id);
            return PartialView(natural);
        }

        public ActionResult documentosJurAdmon( int id )
        {
            if (Session["Admon"] == null)
                return RedirectToAction("LoginAdmon");

            int tipo = EMPRESA.getTipoEmpresa(id);
            ViewBag.empresa = id;

            List<DOCUMENTO> d = DOCUMENTO.getDocumentosTipo(tipo);
            return PartialView(d);
        }
        

        public ActionResult totalNatural(int id)
        {
            if (Session["Admon"] == null)
                return RedirectToAction("LoginAdmon");

            ViewBag.id = id;
            return View();
        }

        public ActionResult totalJuridica(int id)
        {
            if (Session["Admon"] == null)
                return RedirectToAction("LoginAdmon");

            ViewBag.id = id;
            return View();
        }


        public ActionResult documentoEmpresaAdmon(int id, int doc)
        {
            if (Session["Admon"] == null)
                return RedirectToAction("LoginAdmon");

            List<Detalle_documentos> cargados = Detalle_documentos.get_documentoEmpresa( id ,doc);
            return PartialView(cargados);
        }


        public ActionResult LoginAdmon( string user, string pass )
        {

            if (user != null && pass != null)
            {

                bool result = USUARIO.isUserAdmon(user, pass);

                if (result)
                {
                    Session["Admon"] = user;
                    return RedirectToAction("ResumenAdmon");
                }
                else
                    ViewBag.isValid = "El usuario no se encuentra activo en la base de datos del SIA";
            }

            Session["id"] = null;
            Session["tipo"] = null;
            Session["Admon"] = null;


            return View();
        }


        public ActionResult Reportes()
        {
            if (Session["Admon"] == null)
                return RedirectToAction("LoginAdmon");

            return View();
        }


        public ActionResult pvRepNatural(string nombre, string profesion, string cedula, string tipo)
        {
            if (Session["Admon"] == null)
                return RedirectToAction("LoginAdmon");

            List<NATURAL_DETALLE> en = NATURAL_DETALLE.getNaturalDetalleCondicion(nombre, profesion, cedula).ToList();
            return PartialView(en);
            
        }


        public ActionResult pvRepJuridica(string nombre, string profesion, string cedula, string tipo )
        {
            if (Session["Admon"] == null)
                return RedirectToAction("LoginAdmon");
            
            List<JURIDICA_DETALLE> ej = JURIDICA_DETALLE.getJuridicaDetalleCondicion(nombre, profesion, cedula);
            return PartialView(ej);
        }




        /************** ultima entrega *****************************************************************************/
       
        public ActionResult pvDelEmpresa(string txNIT)
        {
            if (Session["Admon"] == null)
                return RedirectToAction("LoginAdmon");

            if ( txNIT != null )
            { 
                if (!EMPRESA.existsEmpresa(txNIT))
                    ViewBag.result = "NIT o cedula incorrectos, por favor ingrese de nuevo los datos e intentelo nuevamente !!!";
                else
                {
                    ViewBag.result = EMPRESA.delEmpresa(txNIT) ;
                }
            }
            return View();
        }


        /******************************************************************************************************/

    }
}