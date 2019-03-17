using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleESTest.Api.Models;

namespace SimpleESTest.Api.Data
{
    public interface IRepository
    {
         Task<List<Candidate>> GetCandidates();
    }
}