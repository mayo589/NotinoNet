using NotinoDotNetHW.Data;
using NotinoDotNetHW.Models;

namespace NotinoDotNetHW.Repositories
{
    /// <summary>
    /// Documents repository for accessing Documents data
    /// </summary>
    public class DocumentsRepository : RepositoryBase<Document>
    {
        public DocumentsRepository(DocumentsDb repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
