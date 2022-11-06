using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NotinoDotNetHW.Data;
using NotinoDotNetHW.Models;
using NotinoDotNetHW.Repositories;

namespace NotinoDotNetHW
{
    // api/Documents
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly DocumentsRepository documentsRepo;

        public DocumentsController(DocumentsDb context, DocumentsRepository documentsRepo)
        {
            this.documentsRepo = documentsRepo;
        }

        // GET: api/Documents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocument()
        {
            return await this.documentsRepo.FindAll()
                .ToListAsync();
        }

        // GET: api/Documents/67afb3fb-8454-4f30-b274-d3b44fc76858
        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> GetDocument(Guid id)
        {

            var document = this.documentsRepo.FindById(id);

            if (document == null)
            {
                return NotFound();
            }

            return document;
        }

        // PUT: api/Documents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocument(Guid id, Document document)
        {
            if (id != document.Id)
            {
                return BadRequest();
            }

            this.documentsRepo.Update(document);
            await this.documentsRepo.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Documents
        [HttpPost]
        public async Task<ActionResult<Document>> PostDocument(Document document)
        {
            this.documentsRepo.Create(document);
            await this.documentsRepo.SaveChangesAsync();

            return CreatedAtAction("GetDocument", new { id = document.Id }, document);
        }


        // POST: api/Documents
        [Route("generateDocuments")]
        [HttpPost]
        public async Task<IActionResult> GenerateDocuments(int count)
        {
            var listTags = new List<string>()
            {
                "one","two","three","four","five","six","seven","eight","nine"
            };

            for (var i = 0; i < count; i++)
            {
                Dictionary<string,string> dataDict = null;

                if (i % 2 == 0)
                {
                    dataDict = new Dictionary<string, string>()
                    {
                        { "some", "data"},
                        { "optional", "fields"}
                    };
                }

                this.documentsRepo.Create(new Document()
                {
                    Id = Guid.NewGuid(),
                    Tags = listTags.Take(i%10).ToList(),
                    Data = dataDict
                });
                await this.documentsRepo.SaveChangesAsync();
            }

            return NoContent();
        }
    }
}
