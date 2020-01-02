using System;
using System.Collections.Generic;
using System.Text;
using YOBA_LibraryData.BLL.Entities.Products;
using YOBA_LibraryData.BLL.Entities.Staff;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.Entities.User;
using YOBA_LibraryData.BLL.UOF.Interfaces;

namespace YOBA_BLL.Catalogue
{
    public class WareHouseCatalogue
    {
        private readonly IWareHouseRepository db;

        public WareHouseCatalogue(IWareHouseRepository _repo)
        {
            db = _repo;
        }
        public string CreateWareHouse(string name, string address, Client client, Employee wareHouseKeeper, List<Product> products )
        {
            var result = db.GetByName(name);
            if (result != null)
            {
                return "WareHouse with such name already exist";
            }
            else
            {
                db.Add(new WareHouse() 
                { 
                    Address = address, 
                    Created = DateTime.Now, 
                    CreatedBy=client.Login, 
                    LastModified=DateTime.Now, 
                    WareHouseName=name,
                    StockMan=wareHouseKeeper,
                    Products=products
                });
            }
            return $"WareHouse {name} successfull created";
        }
    }
}
