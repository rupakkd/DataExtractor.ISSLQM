using System;
using System.IO;
using System.Linq;
using DataExtractor.ISSLQM.CsvParser.Implementation.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataExtractor.ISSLQM.CsvParser.Implementation.Tests
{
    [TestClass]
    public class CsvReaderNugetTests
    {
        #region Declarations
        private CsvReaderNuget csvReader;
        #endregion Declarations

        #region Test Initialisation
        [TestInitialize]
        public void Initialise()
        {
            csvReader = new CsvReaderNuget();
        }
        #endregion Test Initialisation

        #region Test Methods
        #region -- ReadFromFile ----
        [TestMethod]
        public void ReadFromFileTest_ThrowsError_WhenFilePathNull()
        {
            //--- Act / Assert
            Assert.ThrowsException<ArgumentNullException>(() => csvReader.ReadFromFile<TestModel>(null));
        }

        [TestMethod]
        public void ReadFromFileTest_ThrowsError_WhenFilePathEmpty()
        {
            //--- Act / Assert
            Assert.ThrowsException<ArgumentException>(() => csvReader.ReadFromFile<TestModel>(String.Empty));
        }

        [TestMethod]
        public void ReadFromFileTest_ThrowsError_WhenFileDoesNotExists()
        {
            //--- Act / Assert
            Assert.ThrowsException<ArgumentException>(() => csvReader.ReadFromFile<TestModel>("abcdefgh.csv"));
        }

        [TestMethod]
        public void ReadFromFileTest_ReturnsEmptyList_WhenEmptyCsv()
        {
            //--- Arrange
            var expectedCount = 0;

            //--- Act
            var actual = csvReader.ReadFromFile<TestModel>(@"Resources\EmptyCsv.csv");

            //--- Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual.Any());
            Assert.AreEqual(expectedCount, actual.Count);
        }

        [TestMethod]
        public void ReadFromFileTest_ReturnsEmptyList_WhenMandatoryHeadersMissing()
        {
            //--- Act
            var actual = csvReader.ReadFromFile<TestModel>(@"Resources\MissingMandatoryHeader.csv");

            //--- Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual.Any());
        }

        [TestMethod]
        public void ReadFromFileTest_ReturnsList_WhenOptionalHeadersMissing()
        {
            //--- Arrange
            var expectedCount = 1;

            //--- Act
            var actual = csvReader.ReadFromFile<TestModel>(@"Resources\MissingOptionalHeader.csv");

            //--- Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Any());
            Assert.AreEqual(expectedCount, actual.Count);
        }

        [TestMethod]
        public void ReadFromFileTest_ReturnsEmptyList_WhenNoData()
        {
            //--- Arrange
            var expectedCount = 0;

            //--- Act
            var actual = csvReader.ReadFromFile<TestModel>(@"Resources\EmptyData.csv");

            //--- Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual.Any());
            Assert.AreEqual(expectedCount, actual.Count);
        }

        [TestMethod]
        public void ReadFromFileTest_ReadSuccess_WhenDirtyCsv()
        {
            //--- Arrange
            var expectedCount = 2;

            //--- Act
            var actual = csvReader.ReadFromFile<TestModel>(@"Resources\DirtyCsv.csv");

            //--- Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Any());
            Assert.AreEqual(expectedCount, actual.Count);
        }

        [TestMethod]
        public void ReadFromFileTest_ReadSuccess_WhenValidData()
        {
            //--- Arrange
            var expectedCount = 2;

            //--- Act
            var actual = csvReader.ReadFromFile<TestModel>(@"Resources\ValidData.csv");

            //--- Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Any());
            Assert.AreEqual(expectedCount, actual.Count);

            // Validate time + Property/Field name mapping
            Assert.IsNotNull(actual[0].FirstFillTimeTradeTime);
            Assert.AreEqual("23:22", actual[0].FirstFillTimeTradeTime?.ToString("mm:ss"));

            Assert.IsNotNull(actual[1].FirstFillTimeTradeTime);
            Assert.AreEqual("05:07:37", actual[1].FirstFillTimeTradeTime?.ToString("HH:mm:ss"));

            // Validate bool
            Assert.IsTrue(actual[0].IsMultiFill); // TRUE
            Assert.IsFalse(actual[1].IsMultiFill); // FALSE

            Assert.IsTrue(actual[0].FlowType); // Y
            Assert.IsFalse(actual[1].FlowType); // N

            // Validate int
            Assert.AreEqual(1, actual[0].OrderRef);
            Assert.AreEqual(2, actual[1].OrderRef);

            // Validate double
            Assert.AreEqual(1239.5, actual[0].Price);
            Assert.AreEqual(1552, actual[1].Price);

            // Validate enum
            Assert.AreEqual(eCurrency.EUR, actual[0].Currency);
            Assert.AreEqual(eCurrency.PLN, actual[1].Currency);
        }
        #endregion -- ReadFromFile ----

        #region -- ReadFromCsv ----
        [TestMethod]
        public void ReadFromCsvTest_ThrowsError_WhenValueIsNull()
        {
            //--- Act / Assert
            Assert.ThrowsException<ArgumentNullException>(() => csvReader.ReadFromCsv<TestModel>(null));
        }

        [TestMethod]
        public void ReadFromCsvTest_ThrowsError_WhenFilePathEmpty()
        {
            //--- Act / Assert
            Assert.ThrowsException<ArgumentException>(() => csvReader.ReadFromCsv<TestModel>(String.Empty));
        }

        [TestMethod]
        public void ReadFromCsvTest_ReturnsEmptyList_WhenEmptyCsv()
        {
            //--- Arrange
            var expectedCount = 0;

            //--- Act
            var actual = csvReader.ReadFromCsv<TestModel>(File.ReadAllText(@"Resources\EmptyCsv.csv"));

            //--- Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual.Any());
            Assert.AreEqual(expectedCount, actual.Count);
        }

        [TestMethod]
        public void ReadFromCsvTest_ReturnsEmptyList_WhenMandatoryHeadersMissing()
        {
            //--- Act
            var actual = csvReader.ReadFromCsv<TestModel>(File.ReadAllText(@"Resources\MissingMandatoryHeader.csv"));

            //--- Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual.Any());
        }

        [TestMethod]
        public void ReadFromCsvTest_ReturnsList_WhenOptionalHeadersMissing()
        {
            //--- Arrange
            var expectedCount = 1;

            //--- Act
            var actual = csvReader.ReadFromCsv<TestModel>(File.ReadAllText(@"Resources\MissingOptionalHeader.csv"));

            //--- Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Any());
            Assert.AreEqual(expectedCount, actual.Count);
        }

        [TestMethod]
        public void ReadFromCsvTest_ReturnsEmptyList_WhenNoData()
        {
            //--- Arrange
            var expectedCount = 0;

            //--- Act
            var actual = csvReader.ReadFromCsv<TestModel>(File.ReadAllText(@"Resources\EmptyData.csv"));

            //--- Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual.Any());
            Assert.AreEqual(expectedCount, actual.Count);
        }

        [TestMethod]
        public void ReadFromCsvTest_ReadSuccess_WhenDirtyCsv()
        {
            //--- Arrange
            var expectedCount = 2;

            //--- Act
            var actual = csvReader.ReadFromCsv<TestModel>(File.ReadAllText(@"Resources\DirtyCsv.csv"));

            //--- Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Any());
            Assert.AreEqual(expectedCount, actual.Count);
        }

        [TestMethod]
        public void ReadFromCsvTest_ReadSuccess_WhenValidData()
        {
            //--- Arrange
            var expectedCount = 2;

            //--- Act
            var actual = csvReader.ReadFromCsv<TestModel>(File.ReadAllText(@"Resources\ValidData.csv"));

            //--- Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Any());
            Assert.AreEqual(expectedCount, actual.Count);

            // Validate time + Property/Field name mapping
            Assert.IsNotNull(actual[0].FirstFillTimeTradeTime);
            Assert.AreEqual("23:22", actual[0].FirstFillTimeTradeTime?.ToString("mm:ss"));

            Assert.IsNotNull(actual[1].FirstFillTimeTradeTime);
            Assert.AreEqual("05:07:37", actual[1].FirstFillTimeTradeTime?.ToString("HH:mm:ss"));

            // Validate bool
            Assert.IsTrue(actual[0].IsMultiFill); // TRUE
            Assert.IsFalse(actual[1].IsMultiFill); // FALSE

            Assert.IsTrue(actual[0].FlowType); // Y
            Assert.IsFalse(actual[1].FlowType); // N

            // Validate int
            Assert.AreEqual(1, actual[0].OrderRef);
            Assert.AreEqual(2, actual[1].OrderRef);

            // Validate double
            Assert.AreEqual(1239.5, actual[0].Price);
            Assert.AreEqual(1552, actual[1].Price);

            // Validate enum
            Assert.AreEqual(eCurrency.EUR, actual[0].Currency);
            Assert.AreEqual(eCurrency.PLN, actual[1].Currency);
        }
        #endregion -- ReadFromCsv ----
        #endregion Test Methods
    }
}
