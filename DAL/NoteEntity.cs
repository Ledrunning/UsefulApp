using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NoteEntity
    {
        // Гуид нужен что бы генерировать автоматически значения, в методе примениить Guid.NewGuid();
        public Guid Id { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public int Time { get; set; }
    }
}
