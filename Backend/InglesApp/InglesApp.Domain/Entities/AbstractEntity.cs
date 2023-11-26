namespace InglesApp.Domain.Entities
{
    public abstract class AbstractEntity
    {
        public AbstractEntity()
        {
            if (Id == 0)
            {
                CreatedAt = DateTime.Now;
            }
        }

        public int Id;
        public DateTime CreatedAt { get; set; }
    }
}
