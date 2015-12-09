using Microsoft.VisualStudio.TestTools.UnitTesting;
using BamApps.Excido.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BamApps.Excido.Interface.Data;
using BamApps.Excido.Interface.Service;
using BamApps.Excido.Data.Model;
using MSTestExtensions;
using Moq;
using System.Linq.Expressions;

namespace BamApps.Excido.Service.Tests {
    [TestClass()]
    public class SharedContentServiceTests : BaseTest {
        [TestMethod()]
        public void SharedContentServiceTest() {
            IDataContext dataContext = Moq.Mock.Of<IDataContext>();
            IServiceValidator<SharedContentUnit> serviceValidator = Moq.Mock.Of<IServiceValidator<SharedContentUnit>>();
            ISharedContentServiceValidator<SharedContentUnit> sharedContentServiceValidator = Moq.Mock.Of<ISharedContentServiceValidator<SharedContentUnit>>();

            SharedContentService target = new SharedContentService(dataContext, serviceValidator, sharedContentServiceValidator);

            Assert.IsNotNull(target);
        }

        [TestMethod()]
        public void SharedContentServiceDataContextNullTest() {
            IDataContext dataContext = Moq.Mock.Of<IDataContext>();
            IServiceValidator<SharedContentUnit> serviceValidator = Moq.Mock.Of<IServiceValidator<SharedContentUnit>>();
            ISharedContentServiceValidator<SharedContentUnit> sharedContentServiceValidator = Moq.Mock.Of<ISharedContentServiceValidator<SharedContentUnit>>();

            Assert.Throws<ArgumentNullException>(() => new SharedContentService(null, serviceValidator, sharedContentServiceValidator));
        }

        [TestMethod()]
        public void SharedContentServiceServiceValidatorNullTest() {
            IDataContext dataContext = Moq.Mock.Of<IDataContext>();
            IServiceValidator<SharedContentUnit> serviceValidator = Moq.Mock.Of<IServiceValidator<SharedContentUnit>>();
            ISharedContentServiceValidator<SharedContentUnit> sharedContentServiceValidator = Moq.Mock.Of<ISharedContentServiceValidator<SharedContentUnit>>();

            Assert.Throws<ArgumentNullException>(() => new SharedContentService(dataContext, null, sharedContentServiceValidator));
        }
        [TestMethod()]
        public void SharedContentServiceSharedContentServiceValidatorNullTest() {
            IDataContext dataContext = Moq.Mock.Of<IDataContext>();
            IServiceValidator<SharedContentUnit> serviceValidator = Moq.Mock.Of<IServiceValidator<SharedContentUnit>>();
            ISharedContentServiceValidator<SharedContentUnit> sharedContentServiceValidator = Moq.Mock.Of<ISharedContentServiceValidator<SharedContentUnit>>();

            Assert.Throws<ArgumentNullException>(() => new SharedContentService(dataContext, serviceValidator, null));
        }

        [TestMethod()]
        public void GetSlugContentTest() {
            const string slug = "slug";
            const string content = "content";
            SharedContentUnit sharedContentUnit = new SharedContentUnit() { Slug = slug, Content = content };
            var list = new List<SharedContentUnit> { sharedContentUnit };
            var queryable = list.AsQueryable();

            IDataContext dataContext = 
                Mock.Of<IDataContext>(x => x.QueryAllWhere<SharedContentUnit>(It.IsAny<Expression<Func<SharedContentUnit, bool>>>()) == queryable);

            IServiceValidator<SharedContentUnit> serviceValidator = Moq.Mock.Of<IServiceValidator<SharedContentUnit>>(x => x.ValidateRead() == true);

            ISharedContentServiceValidator<SharedContentUnit> sharedContentServiceValidator = 
                Moq.Mock.Of<ISharedContentServiceValidator<SharedContentUnit>>(x => x.ValidateGetSlug(sharedContentUnit) == true);

            SharedContentService service = new SharedContentService(dataContext, serviceValidator, sharedContentServiceValidator);

            var target = service.GetSlugContent(slug);

            Assert.AreEqual(content, target);
        }


        [TestMethod()]
        public void GetSlugContentBlockedReadTest() {
            const string slug = "slug";
            const string content = "content";
            SharedContentUnit sharedContentUnit = new SharedContentUnit() { Slug = slug, Content = content };
            var list = new List<SharedContentUnit> { sharedContentUnit };
            var queryable = list.AsQueryable();

            IDataContext dataContext =
                Mock.Of<IDataContext>(x => x.QueryAllWhere<SharedContentUnit>(It.IsAny<Expression<Func<SharedContentUnit, bool>>>()) == queryable);

            IServiceValidator<SharedContentUnit> serviceValidator = Moq.Mock.Of<IServiceValidator<SharedContentUnit>>(x => x.ValidateRead() == false);

            ISharedContentServiceValidator<SharedContentUnit> sharedContentServiceValidator =
                Moq.Mock.Of<ISharedContentServiceValidator<SharedContentUnit>>(x => x.ValidateGetSlug(sharedContentUnit) == true);

            SharedContentService service = new SharedContentService(dataContext, serviceValidator, sharedContentServiceValidator);

            var target = service.GetSlugContent(slug);

            Assert.AreEqual("", target);
        }


        [TestMethod()]
        public void GetSlugContentInvalidTest() {
            const string slug = "slug";
            const string content = "content";
            SharedContentUnit sharedContentUnit = new SharedContentUnit() { Slug = slug, Content = content };
            var list = new List<SharedContentUnit> { sharedContentUnit };
            var queryable = list.AsQueryable();

            IDataContext dataContext =
                Mock.Of<IDataContext>(x => x.QueryAllWhere<SharedContentUnit>(It.IsAny<Expression<Func<SharedContentUnit, bool>>>()) == queryable);

            IServiceValidator<SharedContentUnit> serviceValidator = Moq.Mock.Of<IServiceValidator<SharedContentUnit>>(x => x.ValidateRead() == true);

            ISharedContentServiceValidator<SharedContentUnit> sharedContentServiceValidator =
                Moq.Mock.Of<ISharedContentServiceValidator<SharedContentUnit>>(x => x.ValidateGetSlug(sharedContentUnit) == false);

            SharedContentService service = new SharedContentService(dataContext, serviceValidator, sharedContentServiceValidator);

            var target = service.GetSlugContent(slug);

            Assert.AreEqual("", target);
        }

    }
}