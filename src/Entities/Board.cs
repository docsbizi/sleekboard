namespace SleekBoard.Entities
{
    public class Board : Entity
    {
        public string Name { get; protected set; }
        public string? Description { get; protected set; }

        public Board(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }

        public Board UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Board name cannot be empty.");
            }

            Name = name;
            return this;
        }

        public Board UpdateDescription(string description)
        {
            Description = description;
            return this;
        }
    }
}