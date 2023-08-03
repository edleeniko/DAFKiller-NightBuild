using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAFKiller.Core
{
    internal class EcuManipulator
    {
        public EcuParameter AddParameter(Ecu ecu)
        {
            var ecuParameters = new EcuParameter() { };
            ecu.Add(ecuParameters);
            return ecuParameters;
        }

        public void RemoveParameters(Ecu ecu, EcuParameter ecuParameters)
        {
            ecu.Remove(ecuParameters);
        }
    }
}
