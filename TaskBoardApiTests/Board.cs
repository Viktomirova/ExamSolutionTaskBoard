namespace ExamApiTests
{
    internal class Board
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Board(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
