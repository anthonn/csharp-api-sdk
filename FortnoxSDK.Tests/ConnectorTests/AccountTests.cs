using System.Linq;
using Fortnox.SDK;
using Fortnox.SDK.Connectors;
using Fortnox.SDK.Entities;
using Fortnox.SDK.Exceptions;
using Fortnox.SDK.Interfaces;
using Fortnox.SDK.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FortnoxSDK.Tests.ConnectorTests
{
    [TestClass]
    public class AccountTests
    {
        [TestInitialize]
        public void Init()
        {
            //Set global credentials for SDK
            //--- Open 'TestCredentials.resx' to edit the values ---\\
            ConnectionCredentials.AccessToken = TestCredentials.Access_Token;
            ConnectionCredentials.ClientSecret = TestCredentials.Client_Secret;
        }

        [TestMethod]
        public void Test_Account_CRUD()
        {
            IAccountConnector connector = new AccountConnector();

            #region CREATE
            var newAccount = new Account()
            {
                Description = "Test Account",
                Active = false,
                Number = TestUtils.GetUnusedAccountNumber(),
                CostCenterSettings = CostCenterSettings.Allowed,
                ProjectSettings = ProjectSettings.Allowed,
                SRU = 123
            };

            var createdAccount = connector.Create(newAccount);
            Assert.AreEqual("Test Account", createdAccount.Description);

            #endregion CREATE

            #region UPDATE

            createdAccount.Description = "Updated Test Account";

            var updatedAccount = connector.Update(createdAccount); 
            Assert.AreEqual("Updated Test Account", updatedAccount.Description);

            #endregion UPDATE

            #region READ / GET

            var retrievedAccount = connector.Get(createdAccount.Number);
            Assert.AreEqual("Updated Test Account", retrievedAccount.Description);

            #endregion READ / GET

            #region DELETE

            connector.Delete(createdAccount.Number);

            Assert.ThrowsException<FortnoxApiException>(
                () => connector.Get(createdAccount.Number),
                "Entity still exists after Delete!");

            #endregion DELETE

            #region Delete arranged resources
            //Add code to delete temporary resources
            #endregion Delete arranged resources
        }

        [TestMethod]
        public void Test_Account_CRUD_SpecificFinYear()
        {
            var specificFinYear = new FinancialYearConnector().Find(null).Entities.First(y => y.FromDate?.Year == 2018); //get fin year 2018

            IAccountConnector connector = new AccountConnector();

            #region CREATE
            var newAccount = new Account()
            {
                Description = "Test Account",
                Active = false,
                Number = TestUtils.GetUnusedAccountNumber(),
                CostCenterSettings = CostCenterSettings.Allowed,
                ProjectSettings = ProjectSettings.Allowed,
                SRU = 123
            };

            var createdAccount = connector.Create(newAccount, specificFinYear.Id);
            Assert.AreEqual(specificFinYear.Id, createdAccount.Year);
            Assert.AreEqual("Test Account", createdAccount.Description);

            #endregion CREATE

            #region UPDATE

            createdAccount.Description = "Updated Test Account";

            var updatedAccount = connector.Update(createdAccount, specificFinYear.Id);
            Assert.AreEqual("Updated Test Account", updatedAccount.Description);
            Assert.AreEqual(specificFinYear.Id, updatedAccount.Year);

            #endregion UPDATE

            #region READ / GET

            var retrievedAccount = connector.Get(createdAccount.Number, specificFinYear.Id);
            Assert.AreEqual("Updated Test Account", retrievedAccount.Description);
            Assert.AreEqual(specificFinYear.Id, retrievedAccount.Year);

            #endregion READ / GET

            #region DELETE

            connector.Delete(createdAccount.Number, specificFinYear.Id);

            Assert.ThrowsException<FortnoxApiException>(
                () => connector.Get(createdAccount.Number, specificFinYear.Id),
                "Entity still exists after Delete!");

            #endregion DELETE

            #region Delete arranged resources
            //Add code to delete temporary resources
            #endregion Delete arranged resources
        }

        [TestMethod]
        public void Test_Find()
        {
            #region Arrange
            //Add code to create required resources
            #endregion Arrange

            var testKeyMark = TestUtils.RandomInt();

            IAccountConnector connector = new AccountConnector();
            var newAccount = new Account()
            {
                Description = "Test Account",
                Active = false,
                CostCenterSettings = CostCenterSettings.Allowed,
                ProjectSettings = ProjectSettings.Allowed,
                SRU = testKeyMark
            };

            //Add entries
            for (var i = 0; i < 5; i++)
            {
                newAccount.Number = TestUtils.GetUnusedAccountNumber();
                connector.Create(newAccount);
            }

            //Apply base test filter
            var searchSettings = new AccountSearch();
            searchSettings.SRU = testKeyMark.ToString();

            var fullCollection = connector.Find(searchSettings);

            Assert.AreEqual(5, fullCollection.TotalResources);
            Assert.AreEqual(5, fullCollection.Entities.Count);
            Assert.AreEqual(1, fullCollection.TotalPages);

            //Apply Limit
            searchSettings.Limit = 2;
            var limitedCollection = connector.Find(searchSettings);

            Assert.AreEqual(5, limitedCollection.TotalResources);
            Assert.AreEqual(2, limitedCollection.Entities.Count);
            Assert.AreEqual(3, limitedCollection.TotalPages);

            //Delete entries
            foreach (var entry in fullCollection.Entities)
            {
                connector.Delete(entry.Number);
            }
            #region Delete arranged resources

            //Add code to delete temporary resources

            #endregion Delete arranged resources
        }
    }
}
