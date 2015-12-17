using Cartonization.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Cartonization.Web.Services
{
    public class CartonizationService
    {
        public List<Carton> GetAvailableCartons()
        {
            List<Carton> availableCartons = null;

            if(HttpContext.Current.Session["Cartons"] != null)
            {
                availableCartons = HttpContext.Current.Session["Cartons"] as List<Carton>;
            }
            else
            {
                availableCartons = new List<Carton>()
                {
                    new Carton("1", 10, 16, 23),
                    new Carton("2", 16, 23, 30.5m),
                    new Carton("3", 25, 17, 40),
                    new Carton("4", 26, 27, 32),
                    new Carton("5", 27, 32, 47),
                };
            }
            return availableCartons;
        }

        public List<Product> GetProducts()
        {
            List<Product> availableProducts = null;

            if (HttpContext.Current.Session["Products"] != null)
            {
                availableProducts = HttpContext.Current.Session["Products"] as List<Product>;
            }
            else
            {
                availableProducts = new List<Product>()
            {
                new Product(1, 4, 6, 2),
                new Product(2, 3, 7, 4),
                new Product(3, 5, 2, 2),
                new Product(4, 10, 8, 4),
                new Product(5, 10, 5, 3),
                new Product(6, 10, 5, 2),
                new Product(7, 10, 4, 3),
                new Product(8, 3, 1, 2),
                new Product(9, 5, 3, 6),
                new Product(10, 9, 9, 9),
                };
            }
            return availableProducts;
        }

        public List<Product> DeleteProduct(int productID)
        {
            List<Product> products = GetProducts();

            products.RemoveAll(p => p.ProductId == productID);

            HttpContext.Current.Session["Products"] = products;

            return products;
        }

        public List<Carton> DeleteCarton(string cartonID)
        {
            List<Carton> cartons = GetAvailableCartons();

            cartons.RemoveAll(p => p.Id == cartonID);

            HttpContext.Current.Session["Cartons"] = cartons;

            return cartons;
        }

        public List<Product> AddProduct(Product product)
        {
            List<Product> products = GetProducts();

            products.Add(product);

            HttpContext.Current.Session["Products"] = products;

            return products;
        }

        public void AddCarton(Carton carton)
        {
            List<Carton> cartons = GetAvailableCartons();

            cartons.Add(carton);

            HttpContext.Current.Session["Cartons"] = cartons;
        }

        public PackerResponse Pack()
        {
            var packer = new Packer(GetAvailableCartons());

            PackerResponse packerResponse = packer.Pack(GetProducts());

            return packerResponse;
        }

    }
}