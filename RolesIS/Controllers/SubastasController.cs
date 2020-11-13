using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RolesIS.Models;

namespace RolesIS.Controllers
{
    [Authorize]
    public class SubastasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Subastas
        public ActionResult Index()
        {
            if (!User.IsInRole("Vendedor"))
                return Content("No tienes permisos de vendedor");

            return View(db.Subastas.Where(m=>m.Anunciante.UserName == User.Identity.Name));
        }

        public ActionResult SubastasDisponibles()
        {
            return View(db.Subastas.ToList().Where(m=>!m.SubastaTerminada && m.tiempoPublicacion != null));
        }

        public ActionResult Cancelar(int? idSubasta)
        {
            if (idSubasta == null)
                return Content("error");

            var subasta = db.Subastas.Find(idSubasta);
            if (subasta == null)
                return Content("Subasta inexistente");

            if (subasta.CompradorActual != null)
                return Content("No se puede cancelar la subasta, ya alguien pujo");

            subasta.tiempoPublicacion = null;

            db.Entry(subasta).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Descripcion", new { idSubasta = idSubasta });
        }

        public ActionResult CuentaBancariaParaSubasta(int? idSubasta)
        {
            if (idSubasta == null)
                return Content("error");

            return View(idSubasta);

        }

        [HttpPost]
        public ActionResult CuentaBancariaParaSubasta(int? idSubasta, string cuenta)
        {
            if (idSubasta == null || cuenta == null)
                return Content("ingrese su cuenta correctamente");

            return RedirectToAction("Pujar", new { idSubasta = idSubasta, cuenta = cuenta });
        }

        public ActionResult Publicar(int? idSubasta)
        {
            if (idSubasta == null)
                return Content("Error");

            Subasta subasta = db.Subastas.Find(idSubasta);

            if (subasta == null)
                return Content("subasta inexistente");

            return View(subasta);
        }

        [HttpPost]
        public ActionResult Publicar(int? margen, int? idSubasta)
        {
            if (margen == null || idSubasta == null)
                return Content("Error");

            var subasta = db.Subastas.Find(idSubasta);
            if (subasta == null)
                return Content("subasta inexistente");

            subasta.TiempoRestanteParaComenzar = (int)margen;
            subasta.tiempoPublicacion = DateTime.Now;

            db.Entry(subasta).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Descripcion", new { idSubasta = idSubasta });
        }

        public ActionResult Eliminar(int? idSubasta)
        {
            if (idSubasta == null)
                return Content("Error");

            var subasta = db.Subastas.Find(idSubasta);
            if (subasta == null)
                return Content("Subasta inexistente");

            var products = subasta.Productos.ToList();
            var counts = subasta.Cantidades.ToList();

            for (int i = 0; i < products.Count; i++)
            {
                products[i].Cantidad += counts[i].valor;
                db.Entry(products[i]).State = EntityState.Modified;
                
            }
            db.SaveChanges();
            db.Subastas.Remove(subasta);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Pujar(int? idSubasta, string cuenta)
        {
            
            if (idSubasta == null)
                return Content("error");

            Subasta subasta = db.Subastas.Find(idSubasta);
            if (subasta == null)
                return Content("error, subasta inexistente");

            if(subasta.tiempoUltimaPuja != null && !subasta.SubastaTerminada)
            {
                double time = DateTime.Now.Subtract(((DateTime)subasta.tiempoUltimaPuja)).TotalSeconds;
                if(time >= 15.1)
                {
                    subasta.SubastaTerminada = true;
                    db.Entry(subasta).State = EntityState.Modified;
                    if (subasta.CompradorActual.SubastasAdquiridas == null)
                        subasta.CompradorActual.SubastasAdquiridas = new List<Subasta>();

                    db.Entry(subasta.CompradorActual).State = EntityState.Modified;

                    db.SaveChanges();
                    
                }
            }
            ViewBag.cuenta = cuenta;
            return View(subasta);
        }

        [HttpPost]
        public ActionResult Pujar(int? idSubasta, decimal? monto, string cuenta)
        {
            
            if (idSubasta == null || monto == null || cuenta == null)
                return Content("Monto invalido");

            Subasta subasta = db.Subastas.Find(idSubasta);
            if (subasta == null)
                return Content("Subasta inexistente");

            if (subasta.SubastaTerminada)
                return RedirectToAction("Pujar", new { idSubasta = idSubasta });

            if(subasta.CompradorActual != null)
            {
                double time = DateTime.Now.Subtract(((DateTime)subasta.tiempoUltimaPuja)).TotalSeconds;
                if(time >= 15.1)
                {
                    subasta.SubastaTerminada = true;
                    db.Entry(subasta).State = EntityState.Modified;
                    if (subasta.CompradorActual.SubastasAdquiridas == null)
                        subasta.CompradorActual.SubastasAdquiridas = new List<Subasta>();

                    db.Entry(subasta.CompradorActual).State = EntityState.Modified;
                    
                    db.SaveChanges();

                    return RedirectToAction("Pujar", new { idSubasta = idSubasta });
                }
            }

            if (subasta.CompradorActual == null && monto < subasta.PrecioInicial)
                return Content("monto invalido");

            if (subasta.CompradorActual != null && monto <= subasta.PrecioActual)
                return Content("monto invalido");

            subasta.PrecioActual = (decimal)monto;
            var usuario = db.Users.FirstOrDefault(m => m.UserName == User.Identity.Name);
            if (usuario == null)
                return Content("Algo anda mal");

            subasta.CompradorActual = usuario;
            subasta.Id2 = usuario.Id;
            subasta.tiempoUltimaPuja = DateTime.Now;
            subasta.CuentaDelVendedor = cuenta;
            db.Entry(subasta).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Pujar", new { idSubasta = idSubasta , cuenta = cuenta});
        }

        public ActionResult Descripcion(int? idSubasta)
        {
            if (idSubasta == null)
                return Content("Error");

            var subasta = db.Subastas.Find(idSubasta);
            
            if (subasta == null)
                return Content("Subasta inexistente");



            return View(subasta);
        }

        // GET: Subastas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subasta subasta = db.Subastas.Find(id);
            if (subasta == null)
            {
                return HttpNotFound();
            }
            return View(subasta);
        }

        // GET: Subastas/Create
        public ActionResult Create()
        {
            return View();
        }

       
        public ActionResult Create2(string subastaNombre, string subastaDescripcion)
        {
            Subasta subasta = new Subasta() {
                Nombre = subastaNombre,
                Descripcion = subastaDescripcion
            };
            
           var products = db.Productoes.ToList().Where(m => m.Anunciante.UserName == User.Identity.Name).ToList();

            SubastaViewModel s = new SubastaViewModel()
            {
                subasta = subasta,
                cantidades = new List<int>(),
                productos = products,
                productosSeleccionados = new List<Producto>()
            };

            return View(s);
        }

        public ActionResult Create3(string nombre, string descripcion, List<int> productosSeleccionados, List<int> cantidades)
        {
            if (cantidades == null || cantidades.Count == 0)
                return Content("Seleccione algun producto para la subasta");

            List<Producto> productos = new List<Producto>();

            foreach (var item in productosSeleccionados)
            {
                var p = db.Productoes.Find(item);
                if (p == null)
                    return Content("Producto no encontrado");

                productos.Add(p);
            }

            SubastaViewModel s = new SubastaViewModel()
            {
                subasta = new Subasta() { Nombre = nombre, Descripcion = descripcion},
                cantidades = cantidades,
                productosSeleccionados = productos
            };

            Dictionary<int, int> productoCantidad = new Dictionary<int, int>();

            for (int i = 0; i < cantidades.Count; i++)
            {
                productoCantidad[productosSeleccionados[i]] = cantidades[i];
            }
            decimal valor = 0;

            foreach (var item in productos)
            {
                valor += item.Precio * productoCantidad[item.ProductoID];
            }

            ViewBag.valor = valor;

            return View(s);
        }

        public ActionResult AgregarQuitar(string nombre, string descripcion, int? producto,List<int> productosSeleccionados, List<int> cantidades, int quitar)
        {

            if(quitar != -1)
            {
                
                for (int i = 0; i < productosSeleccionados.Count; i++)
                {
                    if(productosSeleccionados[i] == quitar) {

                        cantidades[i]--;
                        if(cantidades[i] <= 0)
                        {
                            productosSeleccionados.RemoveAt(i);
                            cantidades.RemoveAt(i);
                        }
                    }
                }
            }

            if(producto != null)
            {
                bool encontre = false;
                if(cantidades != null)
                {
                for (int i = 0; i < cantidades.Count; i++)
                {
                    if (productosSeleccionados[i] == producto)
                    {
                        cantidades[i]++;
                        encontre = true;
                    }
                }
                }
                if (!encontre)
                {
                    if(productosSeleccionados == null)
                    {
                        productosSeleccionados = new List<int>();
                        cantidades = new List<int>();
                    }

                    productosSeleccionados.Add((int)producto);
                    cantidades.Add(1);
                }
            }

            Subasta subasta = new Subasta()
            {
                Nombre = nombre,
                Descripcion = descripcion
            };

            List<Producto> productsSelected = new List<Producto>();
            List<Producto> products = new List<Producto>();
            Dictionary<int, int> productosCantidades = new Dictionary<int, int>();
            if (productosSeleccionados != null)
            {
            foreach (var item in productosSeleccionados)
            {
                var p = db.Productoes.Find(item);
                if (p == null)
                    return Content("producto no encontrado");

                productsSelected.Add(p);
            }

                for (int i = 0; i < cantidades.Count; i++)
                {
                    productosCantidades[productosSeleccionados[i]] = cantidades[i];
                }

                foreach (var item in db.Productoes.ToList().Where(m => m.Anunciante.UserName == User.Identity.Name))
                {

                    if (!productosCantidades.ContainsKey(item.ProductoID) || item.Cantidad - productosCantidades[item.ProductoID] > 0)
                        products.Add(item);
                }

            }
            
            var s = new SubastaViewModel() {

                cantidades = cantidades,
                productos = products,
                productosSeleccionados = productsSelected,
                subasta = subasta

            };

            return View("Create2", s);
        }
        
        public ActionResult CreateFinish(string nombre, string descripcion,string cuenta, decimal PrecioInicial, List<int> productosSeleccionados, List<int> cantidades)
        {
            if (cuenta == null || PrecioInicial == null)
                return Content("rellene todos los campos");
            List<Producto> products = new List<Producto>();
            foreach (var item in productosSeleccionados)
            {
                var p = db.Productoes.Find(item);
                if (p == null)
                    return Content("error, producto no encontrado");
                products.Add(p);
            }
            List<Cantidades> cants = new List<Cantidades>();
            foreach (var item in cantidades)
            {
                cants.Add(new Cantidades() { valor = item });
            }

            Subasta subasta = new Subasta()
            {
                Nombre = nombre,
                Descripcion = descripcion,
                PrecioInicial = PrecioInicial,
                Productos = products,
                Cantidades = cants,
                CuentaDelVendedor = cuenta
                

            };
            Dictionary<int, int> productosCantidades = new Dictionary<int, int>();
            for (int i = 0; i < cantidades.Count; i++)
            {
                productosCantidades[productosSeleccionados[i]] = cantidades[i];
            }
            foreach (var item in products)
            {
                var product = item;
                product.Cantidad -= productosCantidades[item.ProductoID];
                db.Entry(product).State = EntityState.Modified;
                
            }
            
            ApplicationUser user = db.Users.FirstOrDefault(m => m.UserName == User.Identity.Name);
            if (user == null)
                return Content("Algo anda mal, inicia sesion");

            subasta.Id = user.Id;
            subasta.Anunciante = user;

            if (user.SubastasPublicadas == null)
                user.SubastasPublicadas = new List<Subasta>();

            db.Subastas.Add(subasta);
            user.SubastasPublicadas.Add(subasta);

            
            db.SaveChanges();
            
            return RedirectToAction("Index");

        }
        // GET: Subastas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subasta subasta = db.Subastas.Find(id);
            if (subasta == null)
            {
                return HttpNotFound();
            }
            return View(subasta);
        }

        // POST: Subastas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubastaID,Id,tiempoPublicacion,PrecioInicial,Id2,tiempoUltimaPuja,TiempoRestanteParaComenzar,SubastaTerminada")] Subasta subasta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subasta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subasta);
        }

        // GET: Subastas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subasta subasta = db.Subastas.Find(id);
            if (subasta == null)
            {
                return HttpNotFound();
            }
            return View(subasta);
        }

        // POST: Subastas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subasta subasta = db.Subastas.Find(id);
            db.Subastas.Remove(subasta);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
