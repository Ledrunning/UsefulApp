using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization; // Для DataContract;

namespace GeneralContract
{
    [DataContract]
    public class NotesData
    {
        [DataMember]
        // Гуид нужен что бы генерировать автоматически значения, в методе примениить Guid.NewGuid();
        public Guid Id { get; set; }
        [DataMember]
        public string Header { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public int Time { get; set; }

    }
}
