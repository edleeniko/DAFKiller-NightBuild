using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAFKiller.Core
{
    [Serializable]
    public class Truck : List<Ecu>
    {
        public string chassisNumber { get; set; }
        public List<Ecu> ecuList = new List<Ecu>();
    }

    [Serializable]
    public class Ecu : List<EcuParameters>
    {
        public string ecuName { get; set; }
        public string ecuDescription { get; set; }
        public int ecuInstallationVariant { get; set; }
        public int ecuDafGroup { get; set; }
        public List<EcuParameters> ecuParameters = new List<EcuParameters>();
    }

    [Serializable]
    public class EcuParameters
    {
        public int parameterId { get; set; }
        public string parameterName { get; set; } 
        public string stringParameterValue { get; set; }
        public int intParameterValue { get; set; }
        public double doubleParameterValue { get; set; }

    }
}
