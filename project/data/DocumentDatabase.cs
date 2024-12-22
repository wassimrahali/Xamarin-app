using System.Collections.Generic;
using System.Threading.Tasks;
using project.model;
using SQLite;

namespace project.data
{
    public class DocumentDatabase
    {
        readonly SQLiteAsyncConnection _database;

        // Constructor to initialize SQLite connection
        public DocumentDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Document>().Wait(); // Create the documents table
        }

        public DocumentDatabase()
        {
        }

        // Get all documents
        public Task<List<Document>> GetDocumentsAsync()
        {
            return _database.Table<Document>().ToListAsync();
        }

        // Get a document by its ID
        public Task<Document> GetDocumentAsync(int id)
        {
            return _database.Table<Document>()
                             .Where(d => d.ID == id)
                             .FirstOrDefaultAsync();
        }

        // Save or update a document
        public Task<int> SaveDocumentAsync(Document document)
        {
            if (document.ID != 0)
            {
                return _database.UpdateAsync(document); // Update if document exists
            }
            else
            {
                return _database.InsertAsync(document); // Insert if document is new
            }
        }

        // Delete a document
        public Task<int> DeleteDocumentAsync(Document document)
        {
            return _database.DeleteAsync(document);
        }
    }
}
