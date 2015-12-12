using Microsoft.VisualStudio.TestTools.UnitTesting;
using BamApps.Excido.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BamApps.Excido.Data.Model;

namespace BamApps.Excido.Service.Tests {
    [TestClass()]
    public class NullServiceValidatorTests {
        [TestMethod()]
        public void InstanceTest() {
            var target = NullServiceValidator<SharedContentUnit>.Instance();
            Assert.IsNotNull(target);
        }

        [TestMethod()]
        public void ValidateAddTest() {
            var validator = NullServiceValidator<SharedContentUnit>.Instance();
            var target = validator.ValidateAdd(new SharedContentUnit());

            Assert.IsTrue(target);
        }

        [TestMethod()]
        public void ValidateDeleteTest() {
            var validator = NullServiceValidator<SharedContentUnit>.Instance();
            var target = validator.ValidateDelete(new SharedContentUnit());

            Assert.IsTrue(target);
        }

        [TestMethod()]
        public void ValidateGetTest() {
            var validator = NullServiceValidator<SharedContentUnit>.Instance();
            var target = validator.ValidateGet(new SharedContentUnit());

            Assert.IsTrue(target);
        }

        [TestMethod()]
        public void ValidateReadTest() {
            var validator = NullServiceValidator<SharedContentUnit>.Instance();
            var target = validator.ValidateRead();

            Assert.IsTrue(target);
        }

        [TestMethod()]
        public void ValidateUpdateTest() {
            var validator = NullServiceValidator<SharedContentUnit>.Instance();
            var target = validator.ValidateUpdate(new SharedContentUnit());

            Assert.IsTrue(target);
        }

        [TestMethod()]
        public void ValidateWriteTest() {
            var validator = NullServiceValidator<SharedContentUnit>.Instance();
            var target = validator.ValidateWrite();

            Assert.IsTrue(target);
        }
    }
}