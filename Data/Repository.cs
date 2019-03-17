using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleESTest.Api.Helpers;
using SimpleESTest.Api.Models;

namespace SimpleESTest.Api.Data
{
    public class Repository : IRepository
    {
        private readonly ElasticClientProvider _elasticProvider;
        public Repository(ElasticClientProvider elasticProvider)
        {
            _elasticProvider = elasticProvider;

        }
        public async Task<List<Candidate>> GetCandidates()
        {
             var res = await _elasticProvider.Client.SearchAsync<Candidate>(c => c
            .Query(q => q
            .Bool(b => b
            .Filter(f => f
            .Bool(fb => fb
            .Must(fbm => fbm
            .Range(fbmr => fbmr
           .Field(p => p.Experience)
           .GreaterThan(5) 
            ))))
            .Must(m => m
             .MatchPhrasePrefix(mpp => mpp.Field(p => p.Title).Query("Banka kredi analist"))
            )

            ))
              .Sort(ss => ss
                 .Descending(p => p.ResumeScore)
                 )
            );

           var result = new List<Candidate>();
            foreach (var document in res.Documents)
            {
                result.Add(document);
            }
            return result.Distinct().ToList();
        }
    }
}