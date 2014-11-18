using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Data_Access;


namespace Business_Logic
{
    public class ProductsBL 
    {

        public IEnumerable<Product> GetAllProducts()
        {

            return new ProductRepository().GetAllProducts();

        }

        public Product GetProductByID(int id)
        {

            return new ProductRepository().GetProductByID(id);

        }

        public void AddProduct(Product newProd)
        {

            new ProductRepository().AddProduct(newProd);

        }

        public void DeleteProductByID(int id)
        {

            new ProductRepository().DeleteProductByID(id);

        }

        public void UpdateProduct(Product updateProd)
        {

            new ProductRepository().UpdateProduct(updateProd);

        }


    }
}
