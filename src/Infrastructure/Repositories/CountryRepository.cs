namespace Infrastructure.Repositories
{
    using Domain.Entities;
    using Domain.Interfaces;

    public sealed class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Country?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return await this.FindOneAsync(x => x.ISOCode == code, cancellationToken);
        }
    }
}
