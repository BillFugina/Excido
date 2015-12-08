using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BamApps.Excido.Service.Validation;
using Moq;
using BamApps.Excido.Interface.Data;
using BamApps.Excido.Interface.Service;

namespace BamApps.Excido.Service.Tests {
    using System.Diagnostics.CodeAnalysis;
    using static Utils;

    [TestClass]
    public class SpecificationTests {
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

            ExpectException<ArgumentNullException>(() => {
                trueSpecification.And(null);
            });
        }

        [TestMethod]
        public void NullAndTrueSpecificationTest() {
            var mock = Mock.Of<IEntity>();
            var trueSpecification = new TrueSpecification<IEntity>();

            ExpectException<ArgumentNullException>(() => {
                new BamApps.Excido.Service.Validation.AndSpecification<IEntity>(null, trueSpecification);
            });
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

            ExpectException<ArgumentNullException>(() => {
                trueSpecification.Or(nullSpecification);
            });
        }

        [TestMethod]
        public void NullOrTrueSpecificationTest() {
            var mock = Mock.Of<IEntity>();
            var trueSpecification = new TrueSpecification<IEntity>();
            ISpecification<IEntity> nullSpecification = null;

            ExpectException<ArgumentNullException>(() => {
                new OrSpecification<IEntity>(nullSpecification, trueSpecification);
            });
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
        [ExcludeFromCodeCoverage]
        public void NullNotSpecificationTest() {
            var mock = Mock.Of<IEntity>();
            ISpecification<IEntity> nullSpecification = null;

            var argumentNullException = ExpectException<ArgumentNullException>(() => {
                new NotSpecification<IEntity>(nullSpecification);
            });

            Assert.IsNotNull(argumentNullException);
        }


    }
}
