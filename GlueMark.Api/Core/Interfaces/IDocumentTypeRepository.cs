using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IDocumentTypeRepository
    {
        Task<bool> ExistsAsync(string description, string codeSunat, int businessId, long? excludeId = null);
        Task AddAsync(DocumentType entity);
        Task<List<DocumentType>> GetAllAsync(int businessId);
        Task<DocumentType> GetByIdAsync(long documentTypeId);
        Task<bool> UpdateAsync(DocumentType document);
        Task<bool> PatchStatusAsync(long documentTypeId, string status, int UsersBy, long businessId);
    }
}
