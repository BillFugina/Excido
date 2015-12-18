using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BamApps.Excido.Service.Validation;
using Moq;
using BamApps.Excido.Interface.Data;
using BamApps.Excido.Interface.Service;
using MSTestExtensions;
using System.Diagnostics.CodeAnalysis;

namespace BamApps.Excido.Service.Tests {
    [TestClass]
    public class SpecificationTests : BaseTest {
        [TestMethod]
        public void TrueSpecificationTest() {
            var mock = Mock.Of<IEntity>();
            var trueSpecification = Specification<IEntity>.True;

            var result = trueSpecification.IsSatisfiedBy(mock);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FalseSpecificationTest() {
            var mock = Mock.Of<IEntity>();
            var falseSpecification = Specification<IEntity>.False;

            var result = falseSpecification.IsSatisfiedBy(mock);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TrueAndTrueSpecificationTest() {
            var mock = Mock.Of<IEntity>();
            var trueSpecification = new TrueSpecification<IEntity>();
            var alsoTrueSpecification = new TrueSpecification<IEntity>();
            var testSpecification = trueSpecification.And(alsoTrueSpecification);

            var result = testSpecification.IsSatisfiedBy(mock);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TrueAndFalseSpecificationTest() {
            var mock = Mock.Of<IEntity>();
            var trueSpecification = new TrueSpecification<IEntity>();
            var falseSpecification = new FalseSpecification<IEntity>();
            var testSpecification = trueSpecification.And(falseSpecification);

            var result = testSpecification.IsSatisfiedBy(mock);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TrueAndNullSpecificationTest() {
            var mock = Mock.Of<IEntity>();
            var trueSpecification = new TrueSpecification<IEntity>();

            Assert.Throws<ArgumentNullException>(() => trueSpecification.And(null).IsSatisfiedBy(mock));
        }

        [TestMethod]
        public void NullAndTrueSpecificationTest() {
            var mock = Mock.Of<IEntity>();
            var trueSpecification = new TrueSpecification<IEntity>();

            Assert.Throws<ArgumentNullException>(() => new AndSpecification<IEntity>(null, trueSpecification).IsSatisfiedBy(mock));
        }

        [TestMethod]
        public void TrueOrTrueSpecificationTest() {
            var mock = Mock.Of<IEntity>();
            var trueSpecification = new TrueSpecification<IEntity>();
            var alsoTrueSpecification = new FalseSpecification<IEntity>();
            var testSpecification = trueSpecification.Or(alsoTrueSpecification);

            var result = testSpecification.IsSatisfiedBy(mock);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TrueOrFalseSpecificationTest() {
            var mock = Mock.Of<IEntity>();
            var trueSpecification = new TrueSpecification<IEntity>();
            var falseSpecification = new FalseSpecification<IEntity>();
            var testSpecification = trueSpecification.Or(falseSpecification);

            var result = testSpecification.IsSatisfiedBy(mock);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FalseOrTrueSpecificationTest() {
            var mock = Mock.Of<IEntity>();
            var trueSpecification = new TrueSpecification<IEntity>();
            var falseSpecification = new FalseSpecification<IEntity>();
            var testSpecification = falseSpecification.Or(trueSpecification);

            var result = testSpecification.IsSatisfiedBy(mock);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FalseOrFalseSpecificationTest() {
            var mock = Mock.Of<IEntity>();
            var falseSpecification = new FalseSpecification<IEntity>();
            var alsoFalseSpecification = new FalseSpecification<IEntity>();
            var testSpecification = falseSpecification.Or(alsoFalseSpecification);

            var result = testSpecification.IsSatisfiedBy(mock);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TrueOrNullSpecificationTest() {
            var mock = Mock.Of<IEntity>();
            var trueSpecification = new TrueSpecification<IEntity>();
            FalseSpecification<IEntity> nullSpecification = null;

            Assert.Throws<ArgumentNullException>(() => trueSpecification.Or(nullSpecification).IsSatisfiedBy(mock));
        }

        [TestMethod]
        public void NullOrTrueSpecificationTest() {
            var mock = Mock.Of<IEntity>();
            var trueSpecification = new TrueSpecification<IEntity>();
            ISpecification<IEntity> nullSpecification = null;

            Assert.Throws<ArgumentNullException>(() => new OrSpecification<IEntity>(nullSpecification, trueSpecification).IsSatisfiedBy(mock));
        }

        [TestMethod]
        public void NotTrueSpecificationTest() {
            var mock = Mock.Of<IEntity>();
            var trueSpecification = new TrueSpecification<IEntity>();
            var testSpecification = trueSpecification.Not();

            var result = testSpecification.IsSatisfiedBy(mock);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void NotFalseSpecificationTest() {
            var mock = Mock.Of<IEntity>();
            var falseSpecification = new FalseSpecification<IEntity>();
            var testSpecification = falseSpecification.Not();

            var result = testSpecification.IsSatisfiedBy(mock);

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void NullNotSpecificationTest() {
            var mock = Mock.Of<IEntity>();
            ISpecification<IEntity> nullSpecification = null;

            Assert.Throws<ArgumentNullException>(() => new NotSpecification<IEntity>(nullSpecification).IsSatisfiedBy(mock));
        }


    }
}
