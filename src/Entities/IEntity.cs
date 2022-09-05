namespace SleekBoard.Entities
{
    public interface IEntity
    {
        Guid Id { get; }
        DateTime CreationTime { get; }
        DateTime? ModificationTime { get; }
        bool isDeleted { get; }
        DateTime? DeletionTime { get; }

        void Create(DateTime creationTime);
        void Modify(DateTime modificationTime);
        void SoftDelete(DateTime deletionTime);
    }
}