using Agilis.Core.Domain.Models.Entities.Tags;
using Agilis.Infra.Data.SqlServer.Abstractions;

namespace Agilis.Infra.Data.SqlServer.Repositories.Tags
{
    public class TagRepository : EntityFrameworkRepository<Tag>
    {
        public TagRepository(AgilisDbContext agilisDbContext)
            : base(agilisDbContext)
        {
        }
    }
}