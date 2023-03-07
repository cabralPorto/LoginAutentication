using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using UserAultentication.Models;


namespace UserAultentication.Controllers
{
    public class AccountController : Controller
    {
       
        public ActionResult Login( string returnURL)
        {
            ViewBag.ReturnURL = returnURL;
            return View(new ACESSO());
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(ACESSO login, string returnUrl)
        {
            login.SENHA  = Util.Encrypto_Sha256.CriptografarSenha(login.SENHA);

            if (ModelState.IsValid)
            {
                using (UserAutenticationEntities db = new UserAutenticationEntities())
                {
                    var vLogin = db.ACESSO.Where(p => p.EMAIL.Equals(login.EMAIL)).FirstOrDefault();
                    if (vLogin != null)
                    {
                        if (Equals(vLogin.ATIVO, "S"))
                        {
                            if (Equals(vLogin.SENHA, login.SENHA))
                            {
                                FormsAuthentication.SetAuthCookie(vLogin.EMAIL, false);
                                if (Url.IsLocalUrl(returnUrl)
                                && returnUrl.Length > 1
                                && returnUrl.StartsWith("/")
                                && !returnUrl.StartsWith("//")
                                && returnUrl.StartsWith("/\\"))
                                {
                                    return Redirect(returnUrl);
                                }
                               
                                Session["Nome"] = vLogin.NOME;
                                Session["Sobrenome"] = vLogin.SOBRENOME;
                                
                                    return RedirectToAction("Index", "Home");
                            }
                            else
                            { 
                                ModelState.AddModelError("", "Senha informada Inválida!!!");
                              
                                return View(new ACESSO());
                            }
                        }
                        
                        else
                        {
                       
                            ModelState.AddModelError("", "Usuário sem acesso para usar o sistema!!!");
                           
                            return View(new ACESSO());
                        }
                    }
           
                    else
                    {
                        
                        ModelState.AddModelError("", "E-mail informado inválido!!!");
                      
                        return View(new ACESSO());
                    }
                }
            }
            
            return View(login);
     }

        
    }
}