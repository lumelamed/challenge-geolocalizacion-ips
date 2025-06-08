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
    }
}
