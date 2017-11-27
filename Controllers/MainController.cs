using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using LibraryProject.App_Start;
using LibraryProject.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LibraryProject.Controllers
{
    public class MainController : Controller
    {
        private _Models TemDatabase
        {
            get
            {
                return new _Models();
            }
        }
        // GET: Main
        public ActionResult Index()
        {
            return View(TemDatabase.Essays.AsNoTracking().ToList());
        }
        [Route("rozprawka/{id}")]
        public ActionResult Essay(int id)
        {
            var Essays = TemDatabase.Essays.Include(x => x.Arguments).Include(x => x.Author).ToArray();
            return View(Essays.Where(x => x.Id == id).First());
        }
        [HttpGet]
        [Route("dodaj")]
        public ActionResult Add()
        {
            ViewData.Add("authors", TemDatabase.Autors.AsNoTracking().ToList());
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Find(string query)
        {
            List<dynamic> essays = new List<dynamic>();
            try
            {
                string[] keys = query.Split(' ');
                foreach(string key in keys)
                {
                    var temessay = await TemDatabase.Essays.Where(x => x.Question.Contains(key)).ToListAsync();
                    foreach(Essay es in temessay)
                    {
                        if(essays.All(x => x.Id != es.Id))
                        {
                            var newobj = new
                            {
                                Id = es.Id,
                                Question = es.Question,
                                LibraryRes = es.LibraryResource,
                                Author = new
                                {
                                    Id = es.Author.Id,
                                    Name = es.Author.Name,
                                    Surname = es.Author.Surname
                                }
                            };
                            essays.Add(newobj);
                        }
                    }
                }
                return Json(new { list = essays.ToArray() });
            }
            catch
            {
                return Json(new { list = new string[] { } });
            }
        }
        [HttpPost]
        public async Task<ActionResult> FindAll()
        {
            List<dynamic> essays = new List<dynamic>();
            var temessay = await TemDatabase.Essays.ToListAsync();
            foreach (Essay es in temessay)
            {
                if (true)
                {
                    var newobj = new
                    {
                        Id = es.Id,
                        Question = es.Question,
                        LibraryRes = es.LibraryResource,
                        Author = new
                        {
                            Id = es.Author.Id,
                            Name = es.Author.Name,
                            Surname = es.Author.Surname
                        }
                    };
                    essays.Add(newobj);
                }
            }
            return Json(new { list = essays.ToArray() });
        }
        [HttpPost]
        [Route("dodaj")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(ArgumentView model)
        {
            if(ModelState.IsValid)
            {
                List<string> erorr = Validation(model);
                if(erorr.Count > 0 )
                {
                    return Json(
                        new {
                            err = JsonConvert.SerializeObject(erorr.ToArray()),
                            Completed = false
                        }
                    );
                }
                else
                {
                    string[] Contents = model.ConString.Split(new char[] { '|' });
                    string[] Sources = model.SouString.Split(new char[] { '|' });
                    string[] Types = model.TypeString.Split(new char[] { '|' });
                    List<Argument> arguments = new List<Argument>();
                    for(int i=0;i<Contents.Length;i++)
                    {
                        Argument arg = new Argument();
                        arg.Content = Contents[i];
                        arg.Source = Sources[i];
                        arg.Kind = Types[i] != "Lit" ? (Types[i] == "Fil" ? 2 : 3) : 1; 
                        arguments.Add(arg);
                    }
                    List<string> valErrors = ArgValidation(arguments);
                    if(valErrors.Count > 0)
                    {
                        return Json(
                            new
                            {
                                err = JsonConvert.SerializeObject(valErrors.ToArray()),
                                Completed = false
                            }
                        );
                    }
                    else
                    {
                        /*List<string> werErrors = new List<string>();
                        bool Completed = true;
                        foreach (Argument argy in arguments)
                        {
                            dynamic[] resp = {
                                await MyClient.ConnectWithAPI(argy.Content),
                                await MyClient.ConnectWithAPI(argy.Source)
                            };
                            if(!(bool)resp[0].Passed)
                            {
                                Completed = false;
                                werErrors.Add("Błąd walidacji treści w argumencie " + (arguments.IndexOf(argy) + 1));
                            }
                            if (!(bool)resp[1].Passed)
                            {
                                Completed = false;
                                werErrors.Add("Błąd walidacji źródła argumentu w argumencie " + (arguments.IndexOf(argy) + 1));
                            }
                        }
                        dynamic[] mresp =
                        {
                            await MyClient.ConnectWithAPI(model.Question),
                            await MyClient.ConnectWithAPI(model.Side),
                            await MyClient.ConnectWithAPI(model.LibraryResource)
                        };
                        foreach(dynamic d in mresp)
                        {
                            if(!(bool)d.Passed)
                            {
                                Completed = false;
                                werErrors.Add("Błąd walidacji treści w jednym z parametrów");
                            }
                        }*/
                        bool Completed = true;
                        if(Completed)
                        {
                            Essay newes = new Essay();
                            newes.Question = model.Question;
                            newes.Side = model.Side;
                            newes.LibraryResource = model.LibraryResource;
                            newes.Author = TemDatabase.Autors.First(x => x.Id == model.Autor_Id);
                            var TestDatabase = TemDatabase;
                            TestDatabase.Essays.Add(newes);
                            int f = await TestDatabase.SaveChangesAsync();
                            foreach(Argument argi in arguments)
                            {
                                argi.essay = newes;
                                TestDatabase.Arguments.Add(argi);
                            }
                            int sf = await TestDatabase.SaveChangesAsync();
                            if(f >= 0 && sf >= 0)
                            {
                                return Json(
                                    new
                                    {
                                        Completed = true
                                    }
                                );
                            }
                            else
                            {
                                return Json(
                                    new
                                    {
                                        Completed = false,
                                        Code=f + " & " + sf,
                                        err = new string[] {"Błąd zapisu danych"}
                                    }
                                );
                            }
                        }
                        /*else
                        {
                            return Json(
                                new
                                {
                                    err = JsonConvert.SerializeObject(werErrors.ToArray()),
                                    Completed = false
                                }
                            );
                        }*/
                    }
                }
            }
            return View();
        }
        [HttpGet]
        [Route("argument")]
        public ActionResult Argument(int type)
        {
            ViewBag.arg = (KindOfArgument)type;
            return PartialView();
        }
        private List<string> Validation(ArgumentView model)
        {
            List<string> error = new List<string>();
            string[] Contents = model.ConString.Split(new char[] { '|' });
            string[] Sources = model.SouString.Split(new char[] { '|' });
            string[] Types = model.TypeString.Split(new char[] { '|' });
            if(!((Contents.Length == Sources.Length) && Contents.Length == Types.Length))
            {
                error.Add("Błąd - niepoprawne dane przy przesylaniu ");
            }
            if(!Contents.All(x => x.Length > 0) || !Sources.All(x => x.Length > 0) || !Types.All(x => x.Length > 0))
            {
                error.Add("Błąd - niektóre elementy są puste");
            }
            return error;
        }
        private List<string> ArgValidation(List<Argument> args)
        {
            List<string> errors = new List<string>();
            int lc = args.Where(x => x.Kind == 1).Count();
            int fc = args.Where(x => x.Kind == 2).Count();
            int oc = args.Where(x => x.Kind == 3).Count();
            if (lc < 5)
            {
                errors.Add(" Brakujące argumenty literackie - brakuje " + (5 - lc));
            }
            if (fc < 2)
            {
                errors.Add(" Brakujące argumenty filmowe - brakuje " + (2 - fc));
            }
            if (oc < 1)
            {
                errors.Add(" Brakujące argumenty literackie - brakuje jakiegokolwiek");
            }
            return errors;
        }
    }
}