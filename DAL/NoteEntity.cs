using System;

namespace DAL
{
    public class NoteEntity
    {
        public Guid Id { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public int Time { get; set; }
    }
}