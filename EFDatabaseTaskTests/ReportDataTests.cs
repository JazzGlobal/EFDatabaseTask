using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using EFDatabaseTask;
using Model;

namespace EFDatabaseTaskTests
{
    [TestClass]
    public class ReportDataTests
    {
        private U07lyXEntities dbcontext
        {
            get { return new U07lyXEntities(); }
        }
        /// <summary>
        /// Executes stored procedure "clean_database". Truncates the PET, customer, and appointment tables.
        /// </summary>
        private void CleanDatabase()
        {
            dbcontext.clean_database();
        }
        /// <summary>
        /// Adds 2 customers and 2 PETs. Queries the database using ReportData.GetAllActivePets(). Should Assert query row (count = 2) = true.
        /// </summary>
        [TestMethod]
        public void GetAllActivePets_ResetDBAdd2Pets_Return2()
        {
            // Create test data.  
            dbcontext.unit_test_1(); // Stored Procedure.

            // Query database
            ReportData r_data = new ReportData();
            var results = r_data.GetAllActivePets();

            // Assert database results.
            Assert.AreEqual(results.Count, 2);

            // Cleanup
            CleanDatabase();
        }
        /// <summary>
        /// Adds 2 customers and 2 PETs. Queries the database using ReportData.GetAllActiveCustomers(). Should Assert query row (count = 2) = true.
        /// </summary>
        [TestMethod]
        public void GetAllActiveCustomers_ResetDBAdd2Customers_Return2() 
        {
            // Create test data.
            dbcontext.unit_test_1();

            // Query database
            ReportData r_data = new ReportData();
            var results = r_data.GetAllActiveCustomers();

            // Assert database results. 
            Assert.AreEqual(results.Count, 2);

            // Cleanup
            CleanDatabase();
        }
    }
}
