using SleekBoard.Enums;

namespace SleekBoard.Entities
{
    public class BoardItem : Entity
    {
        public string Name { get; protected set; }
        public string? Description { get; protected set; }
        public DateTime? DueDate { get; protected set; }
        public Statuses Status { get; protected set; }
        public DateTime? CompletionTime { get; protected set; }
        public Guid BoardId { get; protected set; }

        public BoardItem(
            string name,
            Guid boardId,
            string? description = null,
            DateTime? dueDate = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Name cannot be blank.");
            }

            Name = name;
            Description = description;
            DueDate = dueDate;
            BoardId = boardId;
        }

        public BoardItem UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Name cannot be blank.");
            }

            Name = name;
            return this;
        }

        public BoardItem UpdateDescription(string description)
        {
            Description = description;
            return this;
        }

        public BoardItem UpdateDueDate(DateTime? dueDate)
        {
            DueDate = dueDate;
            return this;
        }

        public BoardItem UpdateStatus(Statuses status)
        {
            switch (status)
            {
                case Statuses.Pending:
                    if (Status != Statuses.Pending)
                    {
                        Pending();
                    }
                    break;
                case Statuses.Completed:
                    if (Status != Statuses.Completed)
                    {
                        Complete();
                    }
                    break;
            }
            return this;
        }

        private void Complete()
        {
            Status = Statuses.Completed;
            CompletionTime = DateTime.UtcNow;
        }

        private void Pending()
        {
            Status = Statuses.Pending;
            CompletionTime = null;
        }
    }
}