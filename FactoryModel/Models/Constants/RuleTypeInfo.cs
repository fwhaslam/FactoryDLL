using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryModel.Models.Constants {

    
    public enum RuleType {
        Make,       // product enter system
        Vend,       // products exits system for money
        Edit        // product changes from one to another
    }

    public class RuleTypeInfo {
    }

}
