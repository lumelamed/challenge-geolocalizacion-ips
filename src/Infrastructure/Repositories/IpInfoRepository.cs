namespace Infrastructure.Repositories
{
    using Domain.Entities;
    using Domain.Interfaces;

    public sealed class IpInfoRepository : BaseRepository<IpInfo>, IIpInfoRepository
    {
        public IpInfoRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
