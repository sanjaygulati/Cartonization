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
                //new Product("1",15, 10, 12),
                //new Product("2",10, 22, 8),
                //new Product("3",33, 33, 2),
                //new Product("4",15, 13, 36),
                //new Product("5", 19, 17, 24),
                new Product("6", 5, 10, 2),
                new Product("7", 10, 2, 8),
                new Product("8", 3, 1, 2),
                new Product("9", 5, 3, 6),
                new Product("10", 9, 7, 4),
                new Product("10_1", 11, 7, 4),
                new Product("10_2", 13, 7, 4),
                new Product("10_3", 15, 7, 4),
                new Product("10_4", 9, 7, 4),
                new Product("10_5", 9, 7, 4),
                new Product("10_6", 19, 7, 4),
                new Product("10_7", 9, 7, 4),
                new Product("10_8", 11, 7, 4),
                //new Product("10_9", 9, 7, 4),
                //new Product("10_10", 9, 7, 4),
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
                //new Product("10_24", 9, 7, 4)

            };

            List<Carton> availbaleCartons = new List<Carton>()
            {
                //new Carton("carton3", 47, 32, 27),
                //new Carton("carton2", 32, 27, 26),
                new Carton("carton1", 10, 16, 23),
                new Carton("carton4", 16, 23, 30.5)
                //new Carton("carton5", 40, 17, 25)
            };

            var packer = new Packer(availbaleCartons);

            PackerResponse packerResponse = packer.Pack(productsToPack);

            foreach (Product product in packerResponse.OversizeProducts)
            {
                Console.WriteLine("Big Product: " + product.ProductId);
            }

            foreach (Product product in packerResponse.BadDimensionProducts)
            {
                Console.WriteLine("Bad Product: " + product.ProductId);
            }

            foreach (Carton carton in packerResponse.Cartons)
            {
                Console.WriteLine(carton);
            }
            
            Console.ReadLine();
        }
    }
}
