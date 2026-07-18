
namespace Forge.Domain.Services.Promotions
{
    using Forge.Domain.Entities;

    public class PromotionService {
        public void Promote(Employee employee, Position position)
        {
            position.EnsureNotRetired();
            employee.Promote(position.Id);
        }
    }
}