using Sport_Match.Dtos;
using Sport_Match.Models;
using System.Threading.Tasks;

namespace Sport_Match.Services
{
    public class PenaltyRuleService : IPenaltyRuleService
    {
        
        private static PenaltyRule _rule = new PenaltyRule
        {
            LateCancellationPenalty = 10,
            NoShowEnabled = true
        };

        public Task<PenaltyRule> GetAsync()
        {
            return Task.FromResult(_rule);
        }

        public Task UpdateAsync(PenaltyRuleDto dto)
        {
            _rule.LateCancellationPenalty = dto.LateCancellationPenalty;
            _rule.NoShowEnabled = dto.NoShowEnabled;

            return Task.CompletedTask;
        }
    }
}
