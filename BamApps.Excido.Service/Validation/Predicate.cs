using System;
using System.Linq;

namespace BamApps.Excido.Service.Validation {
    public static class Predicate {
        public static readonly TruePredicate True = new TruePredicate();
        public static readonly FalsePredicate False = new FalsePredicate();
    }

}
