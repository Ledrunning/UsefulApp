using System;
using System.Runtime.Serialization;

// Для DataContract;

namespace TotalContract
{
    [DataContract]
    public class NotesDataModel
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember] public string Header { get; set; }

        [DataMember] public string Content { get; set; }

        [DataMember] public int Time { get; set; }
    }
}