using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Programatica.Framework.Data.Models;
using Programatica.Framework.Data.Repository;
using Programatica.Framework.Services;
using Programatica.Framework.Services.Injector;
using System;

namespace Programatica.Framework.Tests
{
    [TestClass]
    public class AuditService_Tests
    {
        [TestMethod]
        public void Service_Given_AuditId_Should_Get_Audit()
        {
            // Arrange
            var id = 1;
            var expected = typeof(Audit);
            var audit = new Audit() { SystemId = Guid.NewGuid(), Id = id };

            var auditRepositoryMock = new Mock<IRepository<Audit>>();
            auditRepositoryMock.Setup(m => m.GetData(id)).Returns(audit).Verifiable();

            var injectorMock = new Mock<IInjector<Audit>>();
            injectorMock.SetupGet(m => m.TRepository).Returns(auditRepositoryMock.Object);

            IService<Audit> sut = new Service<Audit>(injectorMock.Object);

            //Act
            var actual = sut.Get(id);

            // Assert
            auditRepositoryMock.Verify();
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual.GetType());
        }
    }
}
