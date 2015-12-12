using Microsoft.VisualStudio.TestTools.UnitTesting;
using BamApps.Excido.Service.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BamApps.Excido.Data.Model;
using Moq;
using BamApps.Excido.Interface.Data;

namespace BamApps.Excido.Service.Validation.Tests {
    [TestClass()]
    public class SharedContentServiceValidatorTests {
        [TestMethod()]
        public void SharedContentServiceValidatorTest() {
            IReadSharedContentPredicate<SharedContentUnit> readSharedContentPredicate = Mock.Of<IReadSharedContentPredicate<SharedContentUnit>>();
            IWriteSharedContentPredicate<SharedContentUnit> writeSharedContentPredicate = Mock.Of<IWriteSharedContentPredicate<SharedContentUnit>>();
            IGetSharedContentValidator<SharedContentUnit> getSharedContentValidator = Mock.Of<IGetSharedContentValidator<SharedContentUnit>>();
            IAddSharedContentValidator<SharedContentUnit> addSharedContentValidator = Mock.Of<IAddSharedContentValidator<SharedContentUnit>>();
            IUpdateSharedContentValidator<SharedContentUnit> updateSharedContentValidator = Mock.Of<IUpdateSharedContentValidator<SharedContentUnit>>();
            IDeleteSharedContentValidator<SharedContentUnit> deleteSharedContentValidator = Mock.Of<IDeleteSharedContentValidator<SharedContentUnit>>();

            SharedContentServiceValidator target = new SharedContentServiceValidator(
                readSharedContentPredicate,
                writeSharedContentPredicate,
                getSharedContentValidator,
                addSharedContentValidator,
                updateSharedContentValidator,
                deleteSharedContentValidator);

            Assert.IsNotNull(target);
        }

        [TestMethod()]
        public void ValidateAddPassTest() {
            IReadSharedContentPredicate<SharedContentUnit> readSharedContentPredicate = Mock.Of<IReadSharedContentPredicate<SharedContentUnit>>();
            IWriteSharedContentPredicate<SharedContentUnit> writeSharedContentPredicate = Mock.Of<IWriteSharedContentPredicate<SharedContentUnit>>();
            IGetSharedContentValidator<SharedContentUnit> getSharedContentValidator = Mock.Of<IGetSharedContentValidator<SharedContentUnit>>();
            IAddSharedContentValidator<SharedContentUnit> addSharedContentValidator = Mock.Of<IAddSharedContentValidator<SharedContentUnit>>(x => x.IsSatisfiedBy(It.IsAny<SharedContentUnit>()) == true);
            IUpdateSharedContentValidator<SharedContentUnit> updateSharedContentValidator = Mock.Of<IUpdateSharedContentValidator<SharedContentUnit>>();
            IDeleteSharedContentValidator<SharedContentUnit> deleteSharedContentValidator = Mock.Of<IDeleteSharedContentValidator<SharedContentUnit>>();

            SharedContentServiceValidator validator = new SharedContentServiceValidator(
                readSharedContentPredicate,
                writeSharedContentPredicate,
                getSharedContentValidator,
                addSharedContentValidator,
                updateSharedContentValidator,
                deleteSharedContentValidator);


            const string slug = "slug";
            const string content = "content";
            DateTime now = DateTime.Now;
            SharedContentUnit sharedContentUnit = new SharedContentUnit() { Slug = slug, Content = content, Created = now };
            var list = new List<SharedContentUnit> { sharedContentUnit };
            var queryable = list.AsQueryable();

            var target = validator.ValidateAdd(sharedContentUnit);


            Assert.IsTrue(target);
        }

        [TestMethod()]
        public void ValidateAddFailTest() {
            IReadSharedContentPredicate<SharedContentUnit> readSharedContentPredicate = Mock.Of<IReadSharedContentPredicate<SharedContentUnit>>();
            IWriteSharedContentPredicate<SharedContentUnit> writeSharedContentPredicate = Mock.Of<IWriteSharedContentPredicate<SharedContentUnit>>();
            IGetSharedContentValidator<SharedContentUnit> getSharedContentValidator = Mock.Of<IGetSharedContentValidator<SharedContentUnit>>();
            IAddSharedContentValidator<SharedContentUnit> addSharedContentValidator = Mock.Of<IAddSharedContentValidator<SharedContentUnit>>(x => x.IsSatisfiedBy(It.IsAny<SharedContentUnit>()) == false);
            IUpdateSharedContentValidator<SharedContentUnit> updateSharedContentValidator = Mock.Of<IUpdateSharedContentValidator<SharedContentUnit>>();
            IDeleteSharedContentValidator<SharedContentUnit> deleteSharedContentValidator = Mock.Of<IDeleteSharedContentValidator<SharedContentUnit>>();

            SharedContentServiceValidator validator = new SharedContentServiceValidator(
                readSharedContentPredicate,
                writeSharedContentPredicate,
                getSharedContentValidator,
                addSharedContentValidator,
                updateSharedContentValidator,
                deleteSharedContentValidator);


            const string slug = "slug";
            const string content = "content";
            DateTime now = DateTime.Now;
            SharedContentUnit sharedContentUnit = new SharedContentUnit() { Slug = slug, Content = content, Created = now };

            var target = validator.ValidateAdd(sharedContentUnit);


            Assert.IsFalse(target);
        }

        [TestMethod()]
        public void ValidateDeletePassTest() {
            IReadSharedContentPredicate<SharedContentUnit> readSharedContentPredicate = Mock.Of<IReadSharedContentPredicate<SharedContentUnit>>();
            IWriteSharedContentPredicate<SharedContentUnit> writeSharedContentPredicate = Mock.Of<IWriteSharedContentPredicate<SharedContentUnit>>();
            IGetSharedContentValidator<SharedContentUnit> getSharedContentValidator = Mock.Of<IGetSharedContentValidator<SharedContentUnit>>();
            IAddSharedContentValidator<SharedContentUnit> addSharedContentValidator = Mock.Of<IAddSharedContentValidator<SharedContentUnit>>();
            IUpdateSharedContentValidator<SharedContentUnit> updateSharedContentValidator = Mock.Of<IUpdateSharedContentValidator<SharedContentUnit>>();
            IDeleteSharedContentValidator<SharedContentUnit> deleteSharedContentValidator = Mock.Of<IDeleteSharedContentValidator<SharedContentUnit>>(x => x.IsSatisfiedBy(It.IsAny<SharedContentUnit>()) == true);

            SharedContentServiceValidator validator = new SharedContentServiceValidator(
                readSharedContentPredicate,
                writeSharedContentPredicate,
                getSharedContentValidator,
                addSharedContentValidator,
                updateSharedContentValidator,
                deleteSharedContentValidator);


            const string slug = "slug";
            const string content = "content";
            DateTime now = DateTime.Now;
            SharedContentUnit sharedContentUnit = new SharedContentUnit() { Slug = slug, Content = content, Created = now };

            var target = validator.ValidateDelete(sharedContentUnit);

            Assert.IsTrue(target);
        }

        [TestMethod()]
        public void ValidateDeleteFailTest() {
            IReadSharedContentPredicate<SharedContentUnit> readSharedContentPredicate = Mock.Of<IReadSharedContentPredicate<SharedContentUnit>>();
            IWriteSharedContentPredicate<SharedContentUnit> writeSharedContentPredicate = Mock.Of<IWriteSharedContentPredicate<SharedContentUnit>>();
            IGetSharedContentValidator<SharedContentUnit> getSharedContentValidator = Mock.Of<IGetSharedContentValidator<SharedContentUnit>>();
            IAddSharedContentValidator<SharedContentUnit> addSharedContentValidator = Mock.Of<IAddSharedContentValidator<SharedContentUnit>>();
            IUpdateSharedContentValidator<SharedContentUnit> updateSharedContentValidator = Mock.Of<IUpdateSharedContentValidator<SharedContentUnit>>();
            IDeleteSharedContentValidator<SharedContentUnit> deleteSharedContentValidator = Mock.Of<IDeleteSharedContentValidator<SharedContentUnit>>(x => x.IsSatisfiedBy(It.IsAny<SharedContentUnit>()) == false);

            SharedContentServiceValidator validator = new SharedContentServiceValidator(
                readSharedContentPredicate,
                writeSharedContentPredicate,
                getSharedContentValidator,
                addSharedContentValidator,
                updateSharedContentValidator,
                deleteSharedContentValidator);


            const string slug = "slug";
            const string content = "content";
            DateTime now = DateTime.Now;
            SharedContentUnit sharedContentUnit = new SharedContentUnit() { Slug = slug, Content = content, Created = now };

            var target = validator.ValidateDelete(sharedContentUnit);

            Assert.IsFalse(target);
        }

        [TestMethod()]
        public void ValidateGetSlugPassTest() {
            IReadSharedContentPredicate<SharedContentUnit> readSharedContentPredicate = Mock.Of<IReadSharedContentPredicate<SharedContentUnit>>();
            IWriteSharedContentPredicate<SharedContentUnit> writeSharedContentPredicate = Mock.Of<IWriteSharedContentPredicate<SharedContentUnit>>();
            IGetSharedContentValidator<SharedContentUnit> getSharedContentValidator = Mock.Of<IGetSharedContentValidator<SharedContentUnit>>(x => x.IsSatisfiedBy(It.IsAny<SharedContentUnit>()) == true);
            IAddSharedContentValidator<SharedContentUnit> addSharedContentValidator = Mock.Of<IAddSharedContentValidator<SharedContentUnit>>();
            IUpdateSharedContentValidator<SharedContentUnit> updateSharedContentValidator = Mock.Of<IUpdateSharedContentValidator<SharedContentUnit>>();
            IDeleteSharedContentValidator<SharedContentUnit> deleteSharedContentValidator = Mock.Of<IDeleteSharedContentValidator<SharedContentUnit>>();

            SharedContentServiceValidator validator = new SharedContentServiceValidator(
                readSharedContentPredicate,
                writeSharedContentPredicate,
                getSharedContentValidator,
                addSharedContentValidator,
                updateSharedContentValidator,
                deleteSharedContentValidator);


            const string slug = "slug";
            const string content = "content";
            DateTime now = DateTime.Now;
            SharedContentUnit sharedContentUnit = new SharedContentUnit() { Slug = slug, Content = content, Created = now };

            var target = validator.ValidateGetSlug(sharedContentUnit);

            Assert.IsTrue(target);
        }

        [TestMethod()]
        public void ValidateGetSlugFailTest() {
            IReadSharedContentPredicate<SharedContentUnit> readSharedContentPredicate = Mock.Of<IReadSharedContentPredicate<SharedContentUnit>>();
            IWriteSharedContentPredicate<SharedContentUnit> writeSharedContentPredicate = Mock.Of<IWriteSharedContentPredicate<SharedContentUnit>>();
            IGetSharedContentValidator<SharedContentUnit> getSharedContentValidator = Mock.Of<IGetSharedContentValidator<SharedContentUnit>>(x => x.IsSatisfiedBy(It.IsAny<SharedContentUnit>()) == false);
            IAddSharedContentValidator<SharedContentUnit> addSharedContentValidator = Mock.Of<IAddSharedContentValidator<SharedContentUnit>>();
            IUpdateSharedContentValidator<SharedContentUnit> updateSharedContentValidator = Mock.Of<IUpdateSharedContentValidator<SharedContentUnit>>();
            IDeleteSharedContentValidator<SharedContentUnit> deleteSharedContentValidator = Mock.Of<IDeleteSharedContentValidator<SharedContentUnit>>();

            SharedContentServiceValidator validator = new SharedContentServiceValidator(
                readSharedContentPredicate,
                writeSharedContentPredicate,
                getSharedContentValidator,
                addSharedContentValidator,
                updateSharedContentValidator,
                deleteSharedContentValidator);


            const string slug = "slug";
            const string content = "content";
            DateTime now = DateTime.Now;
            SharedContentUnit sharedContentUnit = new SharedContentUnit() { Slug = slug, Content = content, Created = now };

            var target = validator.ValidateGetSlug(sharedContentUnit);

            Assert.IsFalse(target);
        }

        [TestMethod()]
        public void ValidateReadPassTest() {
            IReadSharedContentPredicate<SharedContentUnit> readSharedContentPredicate = Mock.Of<IReadSharedContentPredicate<SharedContentUnit>>(x => x.Test() == true);
            IWriteSharedContentPredicate<SharedContentUnit> writeSharedContentPredicate = Mock.Of<IWriteSharedContentPredicate<SharedContentUnit>>();
            IGetSharedContentValidator<SharedContentUnit> getSharedContentValidator = Mock.Of<IGetSharedContentValidator<SharedContentUnit>>();
            IAddSharedContentValidator<SharedContentUnit> addSharedContentValidator = Mock.Of<IAddSharedContentValidator<SharedContentUnit>>();
            IUpdateSharedContentValidator<SharedContentUnit> updateSharedContentValidator = Mock.Of<IUpdateSharedContentValidator<SharedContentUnit>>();
            IDeleteSharedContentValidator<SharedContentUnit> deleteSharedContentValidator = Mock.Of<IDeleteSharedContentValidator<SharedContentUnit>>();

            SharedContentServiceValidator validator = new SharedContentServiceValidator(
                readSharedContentPredicate,
                writeSharedContentPredicate,
                getSharedContentValidator,
                addSharedContentValidator,
                updateSharedContentValidator,
                deleteSharedContentValidator);

            var target = validator.ValidateRead();

            Assert.IsTrue(target);
        }

        [TestMethod()]
        public void ValidateReadFailTest() {
            IReadSharedContentPredicate<SharedContentUnit> readSharedContentPredicate = Mock.Of<IReadSharedContentPredicate<SharedContentUnit>>(x => x.Test() == false);
            IWriteSharedContentPredicate<SharedContentUnit> writeSharedContentPredicate = Mock.Of<IWriteSharedContentPredicate<SharedContentUnit>>();
            IGetSharedContentValidator<SharedContentUnit> getSharedContentValidator = Mock.Of<IGetSharedContentValidator<SharedContentUnit>>();
            IAddSharedContentValidator<SharedContentUnit> addSharedContentValidator = Mock.Of<IAddSharedContentValidator<SharedContentUnit>>();
            IUpdateSharedContentValidator<SharedContentUnit> updateSharedContentValidator = Mock.Of<IUpdateSharedContentValidator<SharedContentUnit>>();
            IDeleteSharedContentValidator<SharedContentUnit> deleteSharedContentValidator = Mock.Of<IDeleteSharedContentValidator<SharedContentUnit>>();

            SharedContentServiceValidator validator = new SharedContentServiceValidator(
                readSharedContentPredicate,
                writeSharedContentPredicate,
                getSharedContentValidator,
                addSharedContentValidator,
                updateSharedContentValidator,
                deleteSharedContentValidator);

            var target = validator.ValidateRead();

            Assert.IsFalse(target);
        }

        [TestMethod()]
        public void ValidateUpdatePassTest() {
            IReadSharedContentPredicate<SharedContentUnit> readSharedContentPredicate = Mock.Of<IReadSharedContentPredicate<SharedContentUnit>>();
            IWriteSharedContentPredicate<SharedContentUnit> writeSharedContentPredicate = Mock.Of<IWriteSharedContentPredicate<SharedContentUnit>>();
            IGetSharedContentValidator<SharedContentUnit> getSharedContentValidator = Mock.Of<IGetSharedContentValidator<SharedContentUnit>>();
            IAddSharedContentValidator<SharedContentUnit> addSharedContentValidator = Mock.Of<IAddSharedContentValidator<SharedContentUnit>>();
            IUpdateSharedContentValidator<SharedContentUnit> updateSharedContentValidator = Mock.Of<IUpdateSharedContentValidator<SharedContentUnit>>(x => x.IsSatisfiedBy(It.IsAny<SharedContentUnit>()) == true);
            IDeleteSharedContentValidator<SharedContentUnit> deleteSharedContentValidator = Mock.Of<IDeleteSharedContentValidator<SharedContentUnit>>();

            SharedContentServiceValidator validator = new SharedContentServiceValidator(
                readSharedContentPredicate,
                writeSharedContentPredicate,
                getSharedContentValidator,
                addSharedContentValidator,
                updateSharedContentValidator,
                deleteSharedContentValidator);


            const string slug = "slug";
            const string content = "content";
            DateTime now = DateTime.Now;
            SharedContentUnit sharedContentUnit = new SharedContentUnit() { Slug = slug, Content = content, Created = now };

            var target = validator.ValidateUpdate(sharedContentUnit);

            Assert.IsTrue(target);
        }

        [TestMethod()]
        public void ValidateUpdateFailTest() {
            IReadSharedContentPredicate<SharedContentUnit> readSharedContentPredicate = Mock.Of<IReadSharedContentPredicate<SharedContentUnit>>();
            IWriteSharedContentPredicate<SharedContentUnit> writeSharedContentPredicate = Mock.Of<IWriteSharedContentPredicate<SharedContentUnit>>();
            IGetSharedContentValidator<SharedContentUnit> getSharedContentValidator = Mock.Of<IGetSharedContentValidator<SharedContentUnit>>();
            IAddSharedContentValidator<SharedContentUnit> addSharedContentValidator = Mock.Of<IAddSharedContentValidator<SharedContentUnit>>();
            IUpdateSharedContentValidator<SharedContentUnit> updateSharedContentValidator = Mock.Of<IUpdateSharedContentValidator<SharedContentUnit>>(x => x.IsSatisfiedBy(It.IsAny<SharedContentUnit>()) == false);
            IDeleteSharedContentValidator<SharedContentUnit> deleteSharedContentValidator = Mock.Of<IDeleteSharedContentValidator<SharedContentUnit>>();

            SharedContentServiceValidator validator = new SharedContentServiceValidator(
                readSharedContentPredicate,
                writeSharedContentPredicate,
                getSharedContentValidator,
                addSharedContentValidator,
                updateSharedContentValidator,
                deleteSharedContentValidator);


            const string slug = "slug";
            const string content = "content";
            DateTime now = DateTime.Now;
            SharedContentUnit sharedContentUnit = new SharedContentUnit() { Slug = slug, Content = content, Created = now };

            var target = validator.ValidateUpdate(sharedContentUnit);

            Assert.IsFalse(target);
        }

        [TestMethod()]
        public void ValidateWritePassTest() {
            IReadSharedContentPredicate<SharedContentUnit> readSharedContentPredicate = Mock.Of<IReadSharedContentPredicate<SharedContentUnit>>();
            IWriteSharedContentPredicate<SharedContentUnit> writeSharedContentPredicate = Mock.Of<IWriteSharedContentPredicate<SharedContentUnit>>(x => x.Test() == true);
            IGetSharedContentValidator<SharedContentUnit> getSharedContentValidator = Mock.Of<IGetSharedContentValidator<SharedContentUnit>>();
            IAddSharedContentValidator<SharedContentUnit> addSharedContentValidator = Mock.Of<IAddSharedContentValidator<SharedContentUnit>>();
            IUpdateSharedContentValidator<SharedContentUnit> updateSharedContentValidator = Mock.Of<IUpdateSharedContentValidator<SharedContentUnit>>();
            IDeleteSharedContentValidator<SharedContentUnit> deleteSharedContentValidator = Mock.Of<IDeleteSharedContentValidator<SharedContentUnit>>();

            SharedContentServiceValidator validator = new SharedContentServiceValidator(
                readSharedContentPredicate,
                writeSharedContentPredicate,
                getSharedContentValidator,
                addSharedContentValidator,
                updateSharedContentValidator,
                deleteSharedContentValidator);

            var target = validator.ValidateWrite();

            Assert.IsTrue(target);
        }

        [TestMethod()]
        public void ValidateWriteFailTest() {
            IReadSharedContentPredicate<SharedContentUnit> readSharedContentPredicate = Mock.Of<IReadSharedContentPredicate<SharedContentUnit>>();
            IWriteSharedContentPredicate<SharedContentUnit> writeSharedContentPredicate = Mock.Of<IWriteSharedContentPredicate<SharedContentUnit>>(x => x.Test() == false);
            IGetSharedContentValidator<SharedContentUnit> getSharedContentValidator = Mock.Of<IGetSharedContentValidator<SharedContentUnit>>();
            IAddSharedContentValidator<SharedContentUnit> addSharedContentValidator = Mock.Of<IAddSharedContentValidator<SharedContentUnit>>();
            IUpdateSharedContentValidator<SharedContentUnit> updateSharedContentValidator = Mock.Of<IUpdateSharedContentValidator<SharedContentUnit>>();
            IDeleteSharedContentValidator<SharedContentUnit> deleteSharedContentValidator = Mock.Of<IDeleteSharedContentValidator<SharedContentUnit>>();

            SharedContentServiceValidator validator = new SharedContentServiceValidator(
                readSharedContentPredicate,
                writeSharedContentPredicate,
                getSharedContentValidator,
                addSharedContentValidator,
                updateSharedContentValidator,
                deleteSharedContentValidator);

            var target = validator.ValidateWrite();

            Assert.IsFalse(target);
        }

        [TestMethod()]
        public void ValidateGetPassTest() {
            IReadSharedContentPredicate<SharedContentUnit> readSharedContentPredicate = Mock.Of<IReadSharedContentPredicate<SharedContentUnit>>();
            IWriteSharedContentPredicate<SharedContentUnit> writeSharedContentPredicate = Mock.Of<IWriteSharedContentPredicate<SharedContentUnit>>();
            IGetSharedContentValidator<SharedContentUnit> getSharedContentValidator = Mock.Of<IGetSharedContentValidator<SharedContentUnit>>(x => x.IsSatisfiedBy(It.IsAny<SharedContentUnit>()) == true);
            IAddSharedContentValidator<SharedContentUnit> addSharedContentValidator = Mock.Of<IAddSharedContentValidator<SharedContentUnit>>();
            IUpdateSharedContentValidator<SharedContentUnit> updateSharedContentValidator = Mock.Of<IUpdateSharedContentValidator<SharedContentUnit>>();
            IDeleteSharedContentValidator<SharedContentUnit> deleteSharedContentValidator = Mock.Of<IDeleteSharedContentValidator<SharedContentUnit>>();

            SharedContentServiceValidator validator = new SharedContentServiceValidator(
                readSharedContentPredicate,
                writeSharedContentPredicate,
                getSharedContentValidator,
                addSharedContentValidator,
                updateSharedContentValidator,
                deleteSharedContentValidator);


            const string slug = "slug";
            const string content = "content";
            DateTime now = DateTime.Now;
            SharedContentUnit sharedContentUnit = new SharedContentUnit() { Slug = slug, Content = content, Created = now };

            var target = validator.ValidateGet(sharedContentUnit);

            Assert.IsTrue(target);
        }

        [TestMethod()]
        public void ValidateGetFailTest() {
            IReadSharedContentPredicate<SharedContentUnit> readSharedContentPredicate = Mock.Of<IReadSharedContentPredicate<SharedContentUnit>>();
            IWriteSharedContentPredicate<SharedContentUnit> writeSharedContentPredicate = Mock.Of<IWriteSharedContentPredicate<SharedContentUnit>>();
            IGetSharedContentValidator<SharedContentUnit> getSharedContentValidator = Mock.Of<IGetSharedContentValidator<SharedContentUnit>>(x => x.IsSatisfiedBy(It.IsAny<SharedContentUnit>()) == false);
            IAddSharedContentValidator<SharedContentUnit> addSharedContentValidator = Mock.Of<IAddSharedContentValidator<SharedContentUnit>>();
            IUpdateSharedContentValidator<SharedContentUnit> updateSharedContentValidator = Mock.Of<IUpdateSharedContentValidator<SharedContentUnit>>();
            IDeleteSharedContentValidator<SharedContentUnit> deleteSharedContentValidator = Mock.Of<IDeleteSharedContentValidator<SharedContentUnit>>();

            SharedContentServiceValidator validator = new SharedContentServiceValidator(
                readSharedContentPredicate,
                writeSharedContentPredicate,
                getSharedContentValidator,
                addSharedContentValidator,
                updateSharedContentValidator,
                deleteSharedContentValidator);


            const string slug = "slug";
            const string content = "content";
            DateTime now = DateTime.Now;
            SharedContentUnit sharedContentUnit = new SharedContentUnit() { Slug = slug, Content = content, Created = now };

            var target = validator.ValidateGet(sharedContentUnit);

            Assert.IsFalse(target);
        }
        
    }
}