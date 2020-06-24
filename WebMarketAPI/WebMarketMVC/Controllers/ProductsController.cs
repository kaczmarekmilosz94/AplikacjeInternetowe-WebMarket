using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebMarket.Entities;
using WebMarket.Entities.Models;
using WebMarket.DataAccesLayer.Core.Abstract;
using System.Web.Security;
using WebMarket.Entities.Identity;
using Microsoft.AspNet.Identity;
using System.Drawing;
using System.IO;

namespace WebMarketMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Products
        public async Task<ActionResult> Index()
        {
            var products = await _unitOfWork.Products.GetAllAsync();

            return View(products);
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await _unitOfWork.Products.GetAsync(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.CategoryId = new SelectList(await _unitOfWork.Categories.GetAllAsync(), "Id", "Name");
           
            return View();
        }

        // POST: Products/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Price,ImageURL,CategoryId,SellerId,OrderId")] Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
                return View(product);

            try
            {
                if (file != null)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/ProductImages"), fileName);
                    file.SaveAs(path);

                    product.ImageURL = fileName;
                }
                else
                {
                    product.ImageURL = "no-image.png";
                }

                product.SellerId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                _unitOfWork.Products.Add(product);

                await _unitOfWork.Commit();

                var products = await _unitOfWork.Products.GetAllAsync();

                return View("Index", products);
            }
            catch 
            {
                return View("Error");
            }
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await _unitOfWork.Products.GetAsync(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(await _unitOfWork.Categories.GetAllAsync(), "Id", "Name", product.CategoryId);
           
            return View(product);
        }

        // POST: Products/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Price,ImageURL,CategoryId,SellerId,OrderId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Products.Modify(product);
                await _unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(await _unitOfWork.Categories.GetAllAsync(), "Id", "Name", product.CategoryId);
           
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await _unitOfWork.Products.GetAsync(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await _unitOfWork.Products.GetAsync(id);
            _unitOfWork.Products.Remove(product);
            await _unitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
