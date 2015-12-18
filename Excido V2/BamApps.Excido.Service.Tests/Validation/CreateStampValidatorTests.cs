using Microsoft.VisualStudio.TestTools.UnitTesting;
using BamApps.Excido.Service.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BamApps.Excido.Interface.Data;
using BamApps.Excido.Data.Model;
using Moq;

namespace BamApps.Excido.Service.Validation.Tests {
    [TestClass()]
    public class CreateStampValidatorTests {
        [TestMethod()]
        public void CreateStampValidatorTest() {
            IReadRepository<SharedContentUnit> readRepository = Mock.Of<IReadRepository<SharedContentUnit>>();
            CreateStampValidator<SharedContentUnit> target = new CreateStampValidator<SharedContentUnit>(readRepository);
            Assert.IsNotNull(target);
        }

        [TestMethod()]
        public void ChecksWithPassTest() {
            IReadRepository<SharedContentUnit> readRepository = Mock.Of<IReadRepository<SharedContentUnit>>();
            CreateStampValidator<SharedContentUnit> validator = new CreateStampValidator<SharedContentUnit>(readRepository);
            DateTime now = DateTime.Now;
            SharedContentUnit a = new SharedContentUnit { Id = Guid.NewGuid(), Created = now };
            SharedContentUnit b = new SharedContentUnit { Id = Guid.NewGuid(), Created = now };

            var target = validator.ChecksWith(a, b);

            Assert.IsTrue(target);
        }

        [TestMethod()]
        public void ChecksWithFailTest() {
            IReadRepository<SharedContentUnit> readRepository = Mock.Of<IReadRepository<SharedContentUnit>>();
            CreateStampValidator<SharedContentUnit> validator = new CreateStampValidator<SharedContentUnit>(readRepository);
            DateTime now = DateTime.Now;
            DateTime yesterday = now.AddDays(-1);
            SharedContentUnit a = new SharedContentUnit { Id = Guid.NewGuid(), Created = now };
            SharedContentUnit b = new SharedContentUnit { Id = Guid.NewGuid(), Created = yesterday };

            var target = validator.ChecksWith(a, b);

            Assert.IsFalse(target);
        }

        [TestMethod()]
        public void IsSatisfiedByPassTest() {
            const string slug = "slug";
            const string content = "content";
            DateTime now = DateTime.Now;
            SharedContentUnit sharedContentUnit = new SharedContentUnit() { Slug = slug, Content = content, Created = now };
            var list = new List<SharedContentUnit> { sharedContentUnit };
            var queryable = list.AsQueryable();
            IReadRepository<SharedContentUnit> readRepository = Mock.Of<IReadRepository<SharedContentUnit>>(x => x.GetById(It.IsAny<Guid>()) == sharedContentUnit);

            CreateStampValidator<SharedContentUnit> validator = new CreateStampValidator<SharedContentUnit>(readRepository);


            var target = validator.IsSatisfiedBy(sharedContentUnit);

            Assert.IsTrue(target);
        }

        [TestMethod()]
        public void IsSatisfiedByFailTest() {
            const string slug = "slug";
            const string content = "content";
            DateTime now = DateTime.Now;
            DateTime yesterday = now.AddDays(-1);
            SharedContentUnit a = new SharedContentUnit() { Slug = slug, Content = content, Created = now };
            SharedContentUnit b = new SharedContentUnit() { Slug = slug, Content = content, Created = yesterday };
            var list = new List<SharedContentUnit> { a };
            var queryable = list.AsQueryable();
            IReadRepository<SharedContentUnit> readRepository = Mock.Of<IReadRepository<SharedContentUnit>>(x => x.GetById(It.IsAny<Guid>()) == a);

            CreateStampValidator<SharedContentUnit> validator = new CreateStampValidator<SharedContentUnit>(readRepository);


            var target = validator.IsSatisfiedBy(b);

            Assert.IsFalse(target);
        }
    }
}