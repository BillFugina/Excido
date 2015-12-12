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
using Moq;
using MSTestExtensions;
using System.Linq.Expressions;

namespace BamApps.Excido.Service.Tests {
    [TestClass()]
    public class BaseServiceTests : BaseTest{
        
        private IDataContext _dataContext;
        private IServiceValidator<SharedContentUnit> _validator;
        private Guid id1 = Guid.NewGuid();
        private Guid id2 = Guid.NewGuid();
        private SharedContentUnit sharedContentUnit1;
        private SharedContentUnit sharedContentUnit2;

        private bool _validate = true;

        [TestInitialize]
        public void initialize() {
        sharedContentUnit1 = new SharedContentUnit { Id = id1, Name = "one", Content = "One Content", Created = DateTime.Now, ExpireCount = 0, ExpireDate = DateTime.Now.AddDays(1), Slug = "Slug One" };
        sharedContentUnit2 = new SharedContentUnit { Id = id2, Name = "two", Content = "Two Content", Created = DateTime.Now, ExpireCount = 0, ExpireDate = DateTime.Now.AddDays(1), Slug = "Slug Two" };

        List<SharedContentUnit> list = new List<SharedContentUnit> { sharedContentUnit1, sharedContentUnit2 };
            IQueryable<SharedContentUnit> queryable = list.AsQueryable();

            Mock<IDataContext> dataContextMock = new Mock<IDataContext>();
            dataContextMock.Setup(m => m.BeginTransaction()).Returns(Mock.Of<IContextTransaction>());
            dataContextMock.Setup(m => m.GetAll<SharedContentUnit>()).Returns(queryable);
            dataContextMock.Setup(m => m.GetAllWhere<SharedContentUnit>(It.IsAny<Expression<Func<SharedContentUnit, bool>>>()))
                .Returns<Expression<Func<SharedContentUnit, bool>>>(p => queryable.Where(p));
            dataContextMock.Setup(m => m.GetById<SharedContentUnit>(It.IsAny<Guid>()))
                .Returns<Guid>(g => queryable.Where(x => x.Id == g).Single());
            dataContextMock.Setup(m => m.AddEntity(It.IsAny<SharedContentUnit>()))
                .Returns<SharedContentUnit>(u => u.Id);
            dataContextMock.Setup(m => m.SaveChanges()).Returns(2);
            _dataContext = dataContextMock.Object;

            Mock<IServiceValidator<SharedContentUnit>> validatorMock = new Mock<IServiceValidator<SharedContentUnit>>();
            validatorMock.Setup(m => m.ValidateRead()).Returns(_validate);
            validatorMock.Setup(m => m.ValidateWrite()).Returns(_validate);
            validatorMock.Setup(m => m.ValidateAdd(It.IsAny<SharedContentUnit>())).Returns(_validate);
            validatorMock.Setup(m => m.ValidateUpdate(It.IsAny<SharedContentUnit>())).Returns(_validate);
            validatorMock.Setup(m => m.ValidateDelete(It.IsAny<SharedContentUnit>())).Returns(_validate);
            _validator = validatorMock.Object;
        }

        [TestCleanup]
        public void cleanUp() {
            _dataContext = null;
            _validator = null;
            _validate = true;
            sharedContentUnit1 = null;
            sharedContentUnit2 = null;
        }

        [TestMethod()]
        public void BaseServiceTest() {
            BaseService<SharedContentUnit> baseService = new BaseService<SharedContentUnit>(_dataContext, _validator);
            Assert.IsNotNull(baseService);
        }

        [TestMethod()]
        public void BaseServiceNullDataContextTest() {
            
            Assert.Throws<ArgumentNullException>(() => {
                BaseService<SharedContentUnit> baseService = new BaseService<SharedContentUnit>(null, _validator);
            });
        }

        [TestMethod()]
        public void BaseServiceNullDataValidatorTest() {

            Assert.Throws<ArgumentNullException>(() => {
                BaseService<SharedContentUnit> baseService = new BaseService<SharedContentUnit>(_dataContext, null);
            });
        }

        [TestMethod()]
        public void BeginTransactionTest() {
            BaseService<SharedContentUnit> baseService = new BaseService<SharedContentUnit>(_dataContext, _validator);
            var target = baseService.BeginTransaction();
            Assert.IsInstanceOfType(target, typeof (IContextTransaction));
        }

        [TestMethod()]
        public void GetAllTest() {
            BaseService<SharedContentUnit> baseService = new BaseService<SharedContentUnit>(_dataContext, _validator);
            var target = baseService.GetAll()?.Count() ?? -1;

            Assert.AreEqual(2, target);
        }

        [TestMethod()]
        public void GetAllWhereTest() {
            BaseService<SharedContentUnit> baseService = new BaseService<SharedContentUnit>(_dataContext, _validator);
            var target = baseService.GetAllWhere(x => x.Name == "one").Single();

            Assert.AreEqual("one", target.Name);
        }

        [TestMethod()]
        public void GetAllWhereFailTest() {
            Mock<IServiceValidator<SharedContentUnit>> validatorMock = new Mock<IServiceValidator<SharedContentUnit>>();
            validatorMock.Setup(m => m.ValidateRead()).Returns(false);
            validatorMock.Setup(m => m.ValidateWrite()).Returns(false);
            validatorMock.Setup(m => m.ValidateAdd(It.IsAny<SharedContentUnit>())).Returns(false);
            validatorMock.Setup(m => m.ValidateUpdate(It.IsAny<SharedContentUnit>())).Returns(false);
            validatorMock.Setup(m => m.ValidateDelete(It.IsAny<SharedContentUnit>())).Returns(false);
            _validator = validatorMock.Object;

            BaseService<SharedContentUnit> baseService = new BaseService<SharedContentUnit>(_dataContext, _validator);
            var target = baseService.GetAllWhere(x => x.Name == "one").SingleOrDefault();

            Assert.IsNull(target);
        }

        [TestMethod()]
        public void GetByIdTest() {
            BaseService<SharedContentUnit> baseService = new BaseService<SharedContentUnit>(_dataContext, _validator);
            var target = baseService.GetById(id1);


            Assert.AreEqual(id1, target.Id);
            Assert.AreEqual(sharedContentUnit1.Name, target.Name);
            Assert.AreEqual(sharedContentUnit1.Content, target.Content);
            Assert.AreEqual(sharedContentUnit1.Created, target.Created);
            Assert.AreEqual(sharedContentUnit1.ExpireCount, target.ExpireCount);
            Assert.AreEqual(sharedContentUnit1.ExpireDate, target.ExpireDate);
            Assert.AreEqual(sharedContentUnit1.Slug, target.Slug);
        }

        [TestMethod()]
        public void GetByIdFailTest() {
            Mock<IServiceValidator<SharedContentUnit>> validatorMock = new Mock<IServiceValidator<SharedContentUnit>>();
            validatorMock.Setup(m => m.ValidateRead()).Returns(false);
            validatorMock.Setup(m => m.ValidateWrite()).Returns(false);
            validatorMock.Setup(m => m.ValidateAdd(It.IsAny<SharedContentUnit>())).Returns(false);
            validatorMock.Setup(m => m.ValidateUpdate(It.IsAny<SharedContentUnit>())).Returns(false);
            validatorMock.Setup(m => m.ValidateDelete(It.IsAny<SharedContentUnit>())).Returns(false);
            _validator = validatorMock.Object;

            BaseService<SharedContentUnit> baseService = new BaseService<SharedContentUnit>(_dataContext, _validator);
            var target = baseService.GetById(id1);

            Assert.IsNull(target);
        }

        [TestMethod()]
        public void AddEntityTest() {
            BaseService<SharedContentUnit> baseService = new BaseService<SharedContentUnit>(_dataContext, _validator);
            var target = baseService.AddEntity(sharedContentUnit1);

            Assert.AreEqual(id1, target);
        }

        [TestMethod()]
        public void UpdateEntityTest() {
            BaseService<SharedContentUnit> baseService = new BaseService<SharedContentUnit>(_dataContext, _validator);
            baseService.UpdateEntity(sharedContentUnit1);
        }

        [TestMethod()]
        public void DeleteEntityTest() {
            BaseService<SharedContentUnit> baseService = new BaseService<SharedContentUnit>(_dataContext, _validator);
            baseService.DeleteEntity(sharedContentUnit1);
        }

        [TestMethod()]
        public void SaveChangesTest() {
            BaseService<SharedContentUnit> baseService = new BaseService<SharedContentUnit>(_dataContext, _validator);
            var target = baseService.SaveChanges();

            Assert.AreEqual(2, target);
        }

        [TestMethod()]
        public void SaveChangesFailTest() {

            Mock<IServiceValidator<SharedContentUnit>> validatorMock = new Mock<IServiceValidator<SharedContentUnit>>();
            validatorMock.Setup(m => m.ValidateRead()).Returns(false);
            validatorMock.Setup(m => m.ValidateWrite()).Returns(false);
            validatorMock.Setup(m => m.ValidateAdd(It.IsAny<SharedContentUnit>())).Returns(false);
            validatorMock.Setup(m => m.ValidateUpdate(It.IsAny<SharedContentUnit>())).Returns(false);
            validatorMock.Setup(m => m.ValidateDelete(It.IsAny<SharedContentUnit>())).Returns(false);
            _validator = validatorMock.Object;

            BaseService<SharedContentUnit> baseService = new BaseService<SharedContentUnit>(_dataContext, _validator);
            var target = baseService.SaveChanges();

            Assert.AreEqual(-1, target);
        }
    }
}