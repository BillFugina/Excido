using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BamApps.Excido.Service.Validation;
using MSTestExtensions;

namespace BamApps.Excido.Service.Tests {

    [TestClass]
    public class PredicateTests : BaseTest {

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

        [TestMethod]
        public void TestAndBothTruePredicate() {
            var pred1 = Predicate.True;
            var pred2 = Predicate.True;
            var andPredicate = pred1.And(pred2);

            var target = andPredicate.Test();

            Assert.IsTrue(target);
        }

        [TestMethod]
        public void TestAndBothFalsePredicate() {
            var pred1 = Predicate.False;
            var pred2 = Predicate.False;
            var andPredicate = pred1.And(pred2);

            var target = andPredicate.Test();

            Assert.IsFalse(target);
        }

        [TestMethod]
        public void TestAndFirstPredTrueSecondPredFalsePredicate() {
            var pred1 = Predicate.True;
            var pred2 = Predicate.False;
            var andPredicate = pred1.And(pred2);

            var target = andPredicate.Test();

            Assert.IsFalse(target);
        }

        [TestMethod]
        public void TestAndFirstPredFalseSecondPredTruePredicate() {
            var pred1 = Predicate.True;
            var pred2 = Predicate.False;
            var andPredicate = pred1.And(pred2);

            var target = andPredicate.Test();

            Assert.IsFalse(target);
        }

        [TestMethod]
        public void TestAndFirstPredNullSecondPredFalsePredicate() {
            TruePredicate pred1 = null;
            FalsePredicate pred2 = Predicate.False;

            Assert.Throws<ArgumentNullException>(() => { pred1.And(pred2); });
        }


        [TestMethod]
        public void TestAndFirstPredTrueSecondPredNullPredicate() {
            TruePredicate pred1 = Predicate.True;
            FalsePredicate pred2 = null;

            Assert.Throws<ArgumentNullException>(() => { pred1.And(pred2); });
        }

        [TestMethod]
        public void TestOrBothTruePredicate() {
            var pred1 = Predicate.True;
            var pred2 = Predicate.True;
            var orPredicate = pred1.Or(pred2);

            var target = orPredicate.Test();

            Assert.IsTrue(target);
        }

        [TestMethod]
        public void TestOrBothFalsePredicate() {
            var pred1 = Predicate.False;
            var pred2 = Predicate.False;
            var orPredicate = pred1.Or(pred2);

            var target = orPredicate.Test();

            Assert.IsFalse(target);
        }

        [TestMethod]
        public void TestOrFirstPredTrueSecondPredFalsePredicate() {
            var pred1 = Predicate.True;
            var pred2 = Predicate.False;
            var orPredicate = pred1.Or(pred2);

            var target = orPredicate.Test();

            Assert.IsTrue(target);
        }

        [TestMethod]
        public void TestOrFirstPredFalseSecondPredTruePredicate() {
            var pred1 = Predicate.False;
            var pred2 = Predicate.True;
            var orPredicate = pred1.Or(pred2);

            var target = orPredicate.Test();

            Assert.IsTrue(target);
        }


        [TestMethod]
        public void TestOrFirstPredNullSecondPredFalsePredicate() {
            TruePredicate pred1 = null;
            FalsePredicate pred2 = Predicate.False;

            Assert.Throws<ArgumentNullException>(() => { pred1.Or(pred2); });
        }

        [TestMethod]
        public void TestOrFirstPredTrueSecondPredNullPredicate() {
            TruePredicate pred1 = Predicate.True;
            FalsePredicate pred2 = null;

            Assert.Throws<ArgumentNullException>(() => { pred1.Or(pred2); });
        }

        [TestMethod]
        public void TestNotPredTruePredicate() {
            var pred1 = Predicate.True;
            var notPredicate = pred1.Not();

            var target = notPredicate.Test();

            Assert.IsFalse(target);
        }

        [TestMethod]
        public void TestNotPredFalsePredicate() {
            var pred1 = Predicate.False;
            var notPredicate = pred1.Not();

            var target = notPredicate.Test();

            Assert.IsTrue(target);
        }

        [TestMethod]
        public void TestNotPredNullPredicate() {
            TruePredicate pred1 = null;
            Assert.Throws<ArgumentNullException>(() => { pred1.Not(); });
        }

    }
}
