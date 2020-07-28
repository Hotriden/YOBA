using System.Reflection;
using YOBA_LibraryData.BLL.Entities.Supply;

namespace YOBA_LibraryData.DAL.Mapper
{
    public static class CustomMapper
    {
        public static void ChangeWareHouse(this WareHouse wareHouse, WareHouse putWareHouse)
        {
            PropertyInfo[] properties = typeof(WareHouse).GetProperties();
            for(int i=0; i<properties.Length; i++)
            {
                object propValue = properties[i].GetValue(putWareHouse);
                if (propValue != null)
                {
                    properties[i].SetValue(wareHouse, propValue);
                }
            }
        }
    }
}
