using Microsoft.VisualStudio.TestTools.UnitTesting;
using BamApps.Excido.Service.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BamApps.Excido.Service.Validation.Tests {
    [TestClass()]
    public class WriteSharedContentPredicateTests {
        [TestMethod()]
        public void TestWriteSharedContentPredicateTrueTest() {
            WriteSharedContentPredicate predicate = new WriteSharedContentPredicate();

            var target = predicate.Test();

            Assert.IsTrue(target);
        }


        [TestMethod()]
        public void TestWriteSharedContentPredicateNotTest() {
            WriteSharedContentPredicate predicate = new WriteSharedContentPredicate();

            var target = predicate.Not().Test();

            Assert.IsFalse(target);
        }
    }
}