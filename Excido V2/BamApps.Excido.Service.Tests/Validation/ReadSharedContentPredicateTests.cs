using Microsoft.VisualStudio.TestTools.UnitTesting;
using BamApps.Excido.Service.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BamApps.Excido.Service.Validation.Tests {
    [TestClass()]
    public class ReadSharedContentPredicateTests {
        [TestMethod()]
        public void TestPassTest() {
            ReadSharedContentPredicate predicate = new ReadSharedContentPredicate();

            var target = predicate.Test();

            Assert.IsTrue(target);
        }

        [TestMethod()]
        public void TestNotPassTest() {
            ReadSharedContentPredicate predicate = new ReadSharedContentPredicate();

            var target = predicate.Not().Test();

            Assert.IsFalse(target);
        }

    }
}