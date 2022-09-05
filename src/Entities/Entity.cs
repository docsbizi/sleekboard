namespace SleekBoard.Entities
{
    public class Entity : IEntity
    {
        public Guid Id { get; protected set; }
        public DateTime CreationTime { get; protected set; }
        public DateTime? ModificationTime { get; protected set; }
        public bool isDeleted { get; protected set; }
        public DateTime? DeletionTime { get; protected set; }

        public void Create(DateTime creationTime)
        {
            CreationTime = creationTime;
        }

        public void Modify(DateTime modificationTime)
        {
            ModificationTime = modificationTime;
        }

        public void SoftDelete(DateTime deletionTime)
        {
            isDeleted = true;
            DeletionTime = deletionTime;
        }
    }
}