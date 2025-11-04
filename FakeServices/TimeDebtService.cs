using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWellBeingApp.Models;
using DigitalWellBeingApp.Models.Enums;

/*
FUNCTIONS:
FUNC 1
- MUST BE ABLE TO GET THE TIME DEBT BETWEEN 2 CATEGORIES

FUNC 2
- MUST BE ABLE TO CHECK WHEN THE APP IS BLOCKED OR NOT
 */

namespace DigitalWellBeingApp.FakeServices
{
   
    class TimeDebtService
    {
        public CategoryDebt GetCategoryDebt() {
            return new CategoryDebt
            {
                Id = 1,
                SourceCategory = AppCategory.Entertainment,
                TargetCategory = AppCategory.Productive,
                Ratio = 2.0,
                TriggerHours = 1.5
            };
        }

        public bool IsCategoryBlocked(AppCategory category)
        {
            return true;
        }
    }
}
