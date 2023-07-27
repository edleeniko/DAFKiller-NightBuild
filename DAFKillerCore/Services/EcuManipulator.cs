using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAFKiller.Core
{
    internal class EcuManipulator
    {
        public EcuParameters AddParameters(Ecu ecu)
        {
            var ecuParameters = new EcuParameters() { };
            ecu.Add(ecuParameters);
            return ecuParameters;
        }

        public void RemoveParameters(Ecu ecu, EcuParameters ecuParameters)
        {
            ecu.Remove(ecuParameters);
        }
    }
}
