using System;
using System.Linq;
using BamApps.Excido.Interface.Service;

namespace BamApps.Excido.Service.Validation {

    

    public class FalsePredicate : IPredicate {
        public bool Test() {
            return false;
        }
    }


}
