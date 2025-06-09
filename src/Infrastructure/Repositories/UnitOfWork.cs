namespace Infrastructure.Repositories
{
    using System.Threading.Tasks;
    using Domain.Abstractions;
    using Domain.Interfaces;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        private IIpInfoRepository? ipInfoRepository = null;
        private ICountryRepository? countryRepository = null;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IIpInfoRepository IpInfoRepository => this.ipInfoRepository ??= new IpInfoRepository(this.context);

        public ICountryRepository CountryRepository => this.countryRepository ??= new CountryRepository(this.context);

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return await this.context.SaveChangesAsync(cancellationToken);
        }
    }
}
