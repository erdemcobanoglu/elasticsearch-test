using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;
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
            var enResult = await GetCandidatesForTurkishSearch("Banka kredi analist");
            var trResult = await GetCandidatesForEnglishSearch("Bank credits analyst");

            var result = enResult.Concat(trResult).ToList();

            return result;

        }


        private async Task<List<Candidate>> GetCandidatesForTurkishSearch(string term)
        {
            var res = await _elasticProvider.TurkishLanguageClient
             .SearchAsync<Candidate>(c => c
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
             .Match(mpp => mpp.Field(p => p.Title).Query(term).Operator(Operator.And))
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
        private async Task<List<Candidate>> GetCandidatesForEnglishSearch(string term)
        {
            var res = await _elasticProvider.EnglishLanguageClient
             .SearchAsync<Candidate>(c => c
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
             .Match(mpp => mpp.Field(p => p.Title).Query(term).Operator(Operator.And))
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