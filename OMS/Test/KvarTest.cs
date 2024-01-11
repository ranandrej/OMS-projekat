using NUnit.Framework;
using OMS.DAO;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.Klase;
using OMS;

namespace Test
{
    [TestFixture]
    public class KvarTest
    {
        [Test]
        public void FindKvarovi_VratiListuKvarova()
        {
            // Arrange
            KvarDAO kvarDAO = new KvarDAO();

            // Act
            var result = kvarDAO.FindKvarovi();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<List<OMS.Klase.Akcija>>());

        }

        [Test]
        public void BrKvarZaDatum_VratiBr()
        {
            // Arrange
            KvarDAO kvarDAO = new KvarDAO();
            DateTime currentDate = DateTime.Now;

            // Act
            var result = kvarDAO.brKvarZaDatum(currentDate);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<int>());
        }

        [Test]
        public void UnesiKvar_ShouldNotThrowException()
        {
            // Arrange
            KvarDAO kvarDAO = new KvarDAO();

            // Act & Assert
            Assert.That(() => kvarDAO.UnesiKvar(), Throws.Nothing);
        }

        [Test]
        public void KvaroviUOpsegu_VratiListuKvarova()
        {
            // Arrange
            KvarDAO kvarDAO = new KvarDAO();
          

            // Act
            var result = kvarDAO.KvaroviUOpsegu();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<List<Kvar>>());
        }

        [Test]
        public void PrikazPoId_KvarSaId()
        {
            // Arrange
            KvarDAO kvarDAO = new KvarDAO();
            string id = "20030102112012_02"; // Provide a valid test id

            // Act
            var result = kvarDAO.PrikazPoId(id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<KvarDAO>());
            Assert.That(result.IdKv, Is.EqualTo(id));
        }

        [Test]
        public void AzurirajKvarove_Test()
        {
            // Arrange
            KvarDAO kvarDAO = new KvarDAO();
            string id = "20030102112012_02"; // Provide a valid test id

            // Act & Assert
            Assert.That(() => kvarDAO.AzurirajKvarove(id), Throws.Nothing);
        }

        [Test]
        public void SaveExcel_ShouldCreateExcelFile()
        {
            // Arrange
            KvarDAO kvarDAO = new KvarDAO();
            string filePath = "test_oms.xlsx";

            // Act
            Assert.That(() => kvarDAO.SaveExcel(), Throws.Nothing);

            // Assert
            Assert.That(File.Exists(filePath), Is.True);

            // Clean up
            File.Delete(filePath);
        }

        [Test]
        public void IzracunajPrioritetZaDane_ShouldReturnDouble()
        {
            // Arrange
            KvarDAO kvarDAO = new KvarDAO();
            string id = "20030102112012_02"; // Provide a valid test id

            // Act
            var result = kvarDAO.IzracunajPrioritetZaDane(id);

            // Assert
            Assert.That(result, Is.InstanceOf<double>());
        }
    }


}


