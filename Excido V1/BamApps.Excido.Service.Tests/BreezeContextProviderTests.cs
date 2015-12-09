using Microsoft.VisualStudio.TestTools.UnitTesting;
using BamApps.Excido.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using MSTestExtensions;
using BamApps.Excido.Interface.Service;
using BamApps.Excido.Interface.Data;
using BamApps.Excido.Data.Model;

namespace BamApps.Excido.Service.Tests {
    [TestClass()]
    public class BreezeContextProviderTests : BaseTest {


        [TestMethod()]
        public void BreezeContextProviderTest() {
            ISharedContentService<SharedContentUnit> sharedContentService = Mock.Of<ISharedContentService<SharedContentUnit>>();
            IReadRepository <SharedContentUnit> readRepository = Mock.Of<IReadRepository<SharedContentUnit>>();
            IWriteRepository<SharedContentUnit> writeRepository = Mock.Of<IWriteRepository<SharedContentUnit>>();

            var target = new BreezeContextProvider(sharedContentService, readRepository, writeRepository);

            Assert.IsNotNull(target);
        }

        [TestMethod()]
        public void BreezeContextProviderSharedContentServiceNullTest() {
            ISharedContentService<SharedContentUnit> sharedContentService = Mock.Of<ISharedContentService<SharedContentUnit>>();
            IReadRepository<SharedContentUnit> readRepository = Mock.Of<IReadRepository<SharedContentUnit>>();
            IWriteRepository<SharedContentUnit> writeRepository = Mock.Of<IWriteRepository<SharedContentUnit>>();

            Assert.Throws<ArgumentNullException>(() => new BreezeContextProvider(null, readRepository, writeRepository));

        }

        [TestMethod()]
        public void BreezeContextProviderReadRepositoryNullTest() {
            ISharedContentService<SharedContentUnit> sharedContentService = Mock.Of<ISharedContentService<SharedContentUnit>>();
            IReadRepository<SharedContentUnit> readRepository = Mock.Of<IReadRepository<SharedContentUnit>>();
            IWriteRepository<SharedContentUnit> writeRepository = Mock.Of<IWriteRepository<SharedContentUnit>>();

            Assert.Throws<ArgumentNullException>(() => new BreezeContextProvider(sharedContentService, null, writeRepository));
        }

        [TestMethod()]
        public void BreezeContextProviderWriteRepositoryNullTest() {
            ISharedContentService<SharedContentUnit> sharedContentService = Mock.Of<ISharedContentService<SharedContentUnit>>();
            IReadRepository<SharedContentUnit> readRepository = Mock.Of<IReadRepository<SharedContentUnit>>();
            IWriteRepository<SharedContentUnit> writeRepository = Mock.Of<IWriteRepository<SharedContentUnit>>();

            Assert.Throws<ArgumentNullException>(() => new BreezeContextProvider(sharedContentService, readRepository, null));
        }

        [TestMethod()]
        public void SharedContentUnitsTest() {
            ISharedContentService<SharedContentUnit> sharedContentService = Mock.Of<ISharedContentService<SharedContentUnit>>();
            Guid unitId = Guid.NewGuid();
            IReadRepository<SharedContentUnit> readRepository = Mock.Of<IReadRepository<SharedContentUnit>>(r => r.GetAll() == new List<SharedContentUnit> {
                new SharedContentUnit() { Id =unitId }
            }.AsQueryable());
            IWriteRepository<SharedContentUnit> writeRepository = Mock.Of<IWriteRepository<SharedContentUnit>>();
            BreezeContextProvider breezeContextProvider = new BreezeContextProvider(sharedContentService, readRepository, writeRepository);

            IQueryable<SharedContentUnit> target = breezeContextProvider.SharedContentUnits();

            Assert.AreEqual(target.First().Id, unitId);
        }

        [TestMethod()]
        public void GetDbConnectionTest() {
            ISharedContentService<SharedContentUnit> sharedContentService = Mock.Of<ISharedContentService<SharedContentUnit>>();
            IReadRepository<SharedContentUnit> readRepository = Mock.Of<IReadRepository<SharedContentUnit>>();
            IWriteRepository<SharedContentUnit> writeRepository = Mock.Of<IWriteRepository<SharedContentUnit>>();
            var bcp = new BreezeContextProvider(sharedContentService, readRepository, writeRepository);

            var target = bcp.GetDbConnection();

            Assert.IsNull(target);
        }
    }
}