using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotinoDotNetHW.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NotinoDotNetHW.Tests
{
    [TestClass]
    public class DocumentRepositoryTests : BaseTest
    {
        [TestMethod]
        public async void TestCreateDocuments()
        {
            Assert.IsFalse(this.DocumentsRepository.FindAll().Any());

            this.DocumentsRepository.Create(new Models.Document()
            {
                Id = Guid.Parse("2e3f16ec-dd64-4dda-bb9d-285454fc3d6e"),
                Tags = null,
                Data = null
            });
            await this.DocumentsRepository.SaveChangesAsync();

            Assert.IsTrue(this.DocumentsRepository.FindAll().Any());
            Assert.IsTrue(this.DocumentsRepository.FindAll().Count() == 1);

            this.DocumentsRepository.Create(new Models.Document()
            {
                Id = Guid.Parse("17cf7b50-0861-48a1-ad3b-6327701d8686"),
                Tags = new List<string>() { "test"},
                Data = null
            });
            await this.DocumentsRepository.SaveChangesAsync();

            Assert.IsTrue(this.DocumentsRepository.FindAll().Count() == 2);
        }

        [TestMethod]
        public async void TestUpdateDocuments()
        {
            var docId = Guid.Parse("d5f1f544-07a4-4998-9623-5219015d291e");

            this.DocumentsRepository.Create(new Models.Document()
            {
                Id =docId,
                Tags = new List<string>() { "testing", "tags"},
                Data = null
            });
            await this.DocumentsRepository.SaveChangesAsync();

            var document = this.DocumentsRepository.FindById(docId);
            document.Tags = new List<string>() { "changed", "tags" };
            this.DocumentsRepository.Update(document);
            await this.DocumentsRepository.SaveChangesAsync();

            document = this.DocumentsRepository.FindById(docId);
            Assert.IsTrue(document.Tags.First() == "changed");
            Assert.IsTrue(document.Tags.Last() == "tags");
        }

    }
}