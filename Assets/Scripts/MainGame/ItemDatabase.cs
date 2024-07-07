using System.Collections.Generic;
namespace MainGame
{
    public static class ItemDatabase
    {
        // Dictionary to hold item names and their quantities
        public static List<string> Items = new List<string>();

        // Method to add an item or increase its quantity
        public static void GetItem(string itemName)
        {
            Items.Add(itemName);
            
        }
        public static void RemoveItem(string itemName)
        {
            Items.Remove(itemName);

        }
        public static bool CheckItem(string itemName)
        {
            return Items.Contains(itemName);
        }
    }

}