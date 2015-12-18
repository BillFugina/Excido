using Microsoft.VisualStudio.TestTools.UnitTesting;
using BamApps.Excido.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BamApps.Excido.Interface.Data;
using Moq;
using MSTestExtensions;

namespace BamApps.Excido.Service.Tests {
    [TestClass()]
    public class SharedContentRepositoryTests : BaseTest{
        [TestMethod()]
        public void SharedContentRepositoryTest() {
            IDataContext dataContext = Mock.Of<IDataContext>();
            SharedContentRepository repo = new SharedContentRepository(dataContext);
            Assert.IsNotNull (repo);
        }

        [TestMethod()]
        public void SharedContentRepositoryNullTest() {
            Assert.Throws<ArgumentNullException>(() => {
                SharedContentRepository repo = new SharedContentRepository(null);
            });

        }
    }
}