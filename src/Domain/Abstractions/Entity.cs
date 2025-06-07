namespace Domain.Abstractions
{
    public abstract class Entity
    {
        protected Entity()
        {
        }

        protected Entity(int id)
        {
            this.Id = id;
        }

        public int Id { get; init; }
    }
}
