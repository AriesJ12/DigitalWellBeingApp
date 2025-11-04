using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DigitalWellBeingApp.FakeServices
{
    class AppCategoryService
    {
        public List<string> GetCategories()
        {
            return new List<string>
            {
                "Uncategorized",
                "Productive",
                "Entertainment"
            };
        }

        public List<string> GetAppsInCategory(string category)
        {
            return new List<string>
            {
                "Discord",
                "Chrome",
                "Roblox"
            };
        }
    }
}
