using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ValidationHandling
{
    public class BuildingValidation
    {
        public string CheckCreateNewBuildingWithValidation(Building building)
        {
            if (string.IsNullOrEmpty(building.Name))
                return "Hãy nhập name của building";
            if (string.IsNullOrEmpty(building.AreaId.ToString()))
                return "Hãy nhập Area Id";
            return "ok";
        }
    }
}
