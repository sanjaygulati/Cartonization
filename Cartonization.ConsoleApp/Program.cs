using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cartonization.Business;

namespace Cartonization.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Product> productsToPack = new List<Product>()
            {
                new Product("1",4, 6, 2),
                new Product("2",3, 7, 4),
                new Product("3",5, 2, 2),
                new Product("4",10, 8, 4),
                new Product("5", 10, 5, 3),
                new Product("6", 10, 5, 2),
                new Product("7", 10, 4, 3),
                new Product("8", 3, 1, 2),
                new Product("9", 5, 3, 6),
                new Product("10", 9, 9, 9),
                new Product("10_1", 8, 7, 4),
                new Product("10_2", 9, 7, 4),
                new Product("10_3", 7, 7, 4),
                new Product("10_4", 6, 7, 4),
                //new Product("10_5", 9, 7, 4),
                //new Product("10_6", 9, 7, 4),
                //new Product("10_7", 9, 7, 4),
                //new Product("10_8", 9, 7, 4),
                //new Product("10_9", 9, 7, 4),
                //new Product("10_10", 1, 1, 1),
                //new Product("10_11", 9, 7, 4),
                //new Product("10_11", 9, 7, 4),
                //new Product("10_12", 9, 7, 4),
                //new Product("10_13", 9, 7, 4),
                //new Product("10_14", 9, 7, 4),
                //new Product("10_15", 9, 7, 4),
                //new Product("10_16", 9, 7, 4),
                //new Product("10_17", 9, 7, 4),
                //new Product("10_18", 9, 7, 4),
                //new Product("10_19", 9, 7, 4),
                //new Product("10_20", 9, 7, 4),
                //new Product("10_21", 9, 7, 4),
                //new Product("10_22", 9, 7, 4),
                //new Product("10_23", 9, 7, 4),
                //new Product("10_24", 1, 1, 1),
               // new Product("10_24", 9, 7, 4)

            };

            List<Carton> availbaleCartons = new List<Carton>()
            {
                new Carton("carton1", 10, 16, 23),
                new Carton("carton2", 16, 23, 30.5m),
                //new Carton("carton3", 25, 17, 40),
                //new Carton("carton4", 26, 27, 32),
                //new Carton("carton5", 27, 32, 47),
            };

            var packer = new Packer(availbaleCartons);

            PackerResponse packerResponse = packer.Pack(productsToPack);

            foreach (Product product in packerResponse.OversizeProducts)
            {
                Console.WriteLine("Big Product Id: " + product.ProductId + "\n\n");
            }

            foreach (Product product in packerResponse.BadDimensionProducts)
            {
                Console.WriteLine("Bad Product Id: " + product.ProductId + "\n\n");
            }

            foreach (Carton carton in packerResponse.Cartons)
            {
                Console.WriteLine(carton + "\n\n");
            }
            
            Console.ReadLine();
        }
    }
}
