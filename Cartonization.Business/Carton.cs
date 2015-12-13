﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartonization.Business
{
    public class Carton
    {
        private List<Product> _productsInCarton;
        private Dictionary<int, List<Product>> _layerdProducts;
        private double _usedHeight;

        public Carton(string id, double height, double length, double width)
        {
            Id = id;
            Space = new Space(new Dimension(width, length, height));
            _productsInCarton = new List<Product>();
            _layerdProducts = new Dictionary<int, List<Product>>();
        }

        public string Id { get; private set; }

        public Space Space { get; private set; }

        public double Width
        {
            get
            {
                return Space.Dimension.Width;
            }
        }

        public double Height
        {
            get
            {
                return Space.Dimension.Height;
            }
        }

        public double Length
        {
            get
            {
                return Space.Dimension.Length;
            }
        }

        public ReadOnlyCollection<Product> ProductsInCarton 
        { 
            get 
            {
                return _productsInCarton.AsReadOnly();
            }
        }

        public double PackedVolume 
        {
            get 
            {
                return ProductsInCarton.Sum(p => p.Space.Volume);
            }
        }

        public double WasteVolume 
        {
            get {
                return Space.Volume - PackedVolume;
            }
        }

        public Dictionary<int, List<Product>> LayeredProducts
        {
            get
            {
                return _layerdProducts;
            }
        }

        private double LevelSurfaceArea( int level )
        {
            if (_layerdProducts.ContainsKey(level))
            {
                return _layerdProducts[level].Sum(p => p.Space.SurfaceArea);
            }
            return 0;
        }

        public void Add(int level, Product product)
        {
            if (_layerdProducts.ContainsKey(level))
            {
                _layerdProducts[level].Add(product);
                
            }
            else
            {
                List<Product> products = new List<Product>();
                products.Add(product);
                _layerdProducts.Add(level, products);
                _usedHeight = _usedHeight + product.Space.Dimension.Height;
            }

            _productsInCarton.Add(product);
        }

        public bool CanAddLevel(double height)
        {
            return _usedHeight + height <= Space.Dimension.Height;
        }


        public override string ToString()
        {
            return "CartoonID: " + Id
                + ",\nLayers packed: " + LayeredProducts.Keys.Count
                + ",\nProducts Count: " + ProductsInCarton.Count
                + ",\nUsed Space: " + PackedVolume
                + ",\nWasteVolume: " + WasteVolume
                + ",\nSurfaceAreaUsedPerLevel:" + SurfaceAreaUsedPerLevel();
        }


        private string SurfaceAreaUsedPerLevel()
        {
            StringBuilder stringBuilder = new System.Text.StringBuilder();

            double surfaceArea = Width * Length;

            foreach(int level in LayeredProducts.Keys)
            {
                stringBuilder.AppendFormat("\nlevelId: {0}, surfaceAreaWaste: {1}", level, surfaceArea - LevelSurfaceArea(level));
            }

            return stringBuilder.ToString();
        }
    }
}
