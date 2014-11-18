using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Data_Access
{
    public class ProductRepository : ConnectionClass
    {

        public ProductRepository() : base() { }

        public IEnumerable<Product> GetAllProducts()
        {

            return entities.Products.AsEnumerable();

        }

        public Product GetProductByID(int id)
        {

            return entities.Products.SingleOrDefault(x => x.ProductID == id);

        }

        public void AddProduct(Product newProd)
        {

            entities.AddToProducts(newProd);
            entities.SaveChanges();

        }

        public void DeleteProductByID(int id)
        {

            entities.DeleteObject(GetProductByID(id));
            entities.SaveChanges();

        }

        public void UpdateProduct(Product updateProd)
        {

            entities.Products.Attach(GetProductByID(updateProd.ProductID));
            entities.Products.ApplyCurrentValues(updateProd);
            entities.SaveChanges();

        }





    }
}
