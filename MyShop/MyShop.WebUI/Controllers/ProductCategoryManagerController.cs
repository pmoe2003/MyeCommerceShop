using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductCategoryManagerController : Controller
    {
        //Begin Course
        //InMemoryRepository<ProductCategory> context;
        //Changing in New course DependencyInjection
        IRepository<ProductCategory> context;


        //Constructor
        public ProductCategoryManagerController(IRepository<ProductCategory> context)
        {
            this.context = context;
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            //Return a list of products
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }

        //Create Product Display a page with Product
        public ActionResult Create()
        {
            ProductCategory productCategories = new ProductCategory();
            return View(productCategories);
        }

        //Details to be posted in
        [HttpPost]
        public ActionResult Create(ProductCategory productCategories)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategories);
            }
            else
            {
                //Insert to collection
                context.Insert(productCategories);
                //save changes
                context.Commit();
            }

            return RedirectToAction("Index");
        }

        //Log the product from the db
        public ActionResult Edit(string Id)
        {
            ProductCategory ProductCategory = context.Find(Id);

            if (ProductCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(ProductCategory);
            }
        }

        //Takes in the product that we are updated
        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string Id)
        {
            ProductCategory productCategoryToEdit = context.Find(Id);

            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategory);
                }
                //if not valid - return after manually updating
                productCategoryToEdit.Category = productCategory.Category;

                //save changes
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        //Load the product from the database
        public ActionResult Delete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);

            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategoryToDelete);
            }
        }

        //Return from the db to delete
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);

            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}