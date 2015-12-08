using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BamApps.Excido.Service.Validation;

namespace BamApps.Excido.Service.Tests {

    [TestClass]
    public class PredicateTests {

        [TestMethod]
        public void TestTruePredicate() {
            //Arrange
            var truePredicate = Predicate.True;

            //Act
            var result = truePredicate.Test();

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestFalsePredicate() {
            //Arrange
            var falsePredicate = Predicate.False;

            //Act
            var result = falsePredicate.Test();

            //Assert
            Assert.IsFalse(result);
        }
    }
}
