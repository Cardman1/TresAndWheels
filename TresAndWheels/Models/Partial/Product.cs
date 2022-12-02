using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TresAndWheels.Models
{
    public partial class Product
    {
        public string ImagePath { get {
                if (Image == null)
                {
                    return "..\\..\\Assets\\Images\\picture.png";
                }
                else { 
                    return "..\\..\\Assets\\Images" + Image;
                }
             }
        }
        public string MaterialList
        {
            get
            {
                string material = "";
                return material;
            }
        }public string CostProduct 
        {
            get
            {
                string material = "Материалы: ";
                List<string> arrayMaterials = new List<string> { };
                List<ProductMaterial> arrayActiveProduct = ProductMaterial.Where(x => x.ProductID == ID).ToList();
                foreach (var item in arrayActiveProduct)
                {
                    arrayMaterials.Add(item.Material.Title.ToString());
                }
                material += String.Join(",", values: arrayMaterials);
                return material;

            }
        }
    }
}
