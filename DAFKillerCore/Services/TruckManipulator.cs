using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAFKiller.Core
{
    internal class TruckManipulator
    {
        public Ecu AddEcu(Truck truck) 
        { 
            var ecu = new Ecu() { };
            truck.Add(ecu);
            return ecu;
        }

        public void RemoveEcu(Truck truck, Ecu ecu)
        {
            truck.Remove(ecu);
        }
    }
}
