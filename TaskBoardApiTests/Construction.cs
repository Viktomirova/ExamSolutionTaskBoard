using System;

namespace ExamApiTests
{
    internal class Construction
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string message { get; set; }
        public Board board { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateModified { get; set; }

    }
}
