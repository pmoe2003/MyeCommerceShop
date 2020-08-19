using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{

    [Authorize(Roles = "Admin")]
    public class ProductManagerController : Controller
    {
        //Instance of the repository
        //New Class Input - DependencyUnjection
        //Now it refrences an injection class called IRepository 
        //to act as a go-between to the actual InMemoryRepository
        IRepository<Product> context;
        //Load the Product Category Repository from the db
        IRepository<ProductCategory>  productCategories;
        
        //Constructor-initialize
        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            //Beginning Method of how it was done
            //context = new InMemoryRepository<Product>();
            //productCategories = new InMemoryRepository<ProductCategory>();

            //Now using the IRespository - we use the parameters that are called and assign them
              context = productContext;
              productCategories = productCategoryContext;

        }

        // GET: ProductManager
        public ActionResult Index()
        {
            //Return a list of products
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        //Create Product Display a page with Product
        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategories.Collection();
            return View(viewModel);
        }

        //Details to be posted in
        [HttpPost]
        //In UploadingImages Course Need to change the Create Method to recieve a HTTP Post file (image)
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {

                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    //Function Server.MapPath allows to add an UNC path - and then saves to a physical location on the disk
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image) ;
                }
                //Insert to collection
                context.Insert(product);
                //save changes
                context.Commit();
            }

            return RedirectToAction("Index");
        }

        //Log the product from the db
        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            
            if (product == null)
                {
                return HttpNotFound();
                }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategories.Collection();
                return View(viewModel);
            }
        }

        //Takes in the product that we are updated
        [HttpPost]
        public ActionResult Edit(Product product, string Id, HttpPostedFileBase file)
        {
            Product productToEdit = context.Find(Id);
           
            if(productToEdit == null)
                {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                if (file != null)
                {
                    productToEdit.Image = product.Id + Path.GetExtension(file.FileName);
                    //Function Server.MapPath allows to add an UNC path - and then saves to a physical location on the disk
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image);
                }

                //if not valid - return after manually updating
                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                //Remove on addition for UploadingImages
                //productToEdit.Image = product.Image;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                //save changes
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        //Load the product from the database
        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        //Return from the db to delete
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);

            if (productToDelete == null)
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