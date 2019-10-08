using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace TotalContract
{
    [ServiceContract]
    public interface INoteServiceContract
    {
        [OperationContract]
        void Add(NotesData data);

        [OperationContract]
        void GetById(Guid id);

        [OperationContract]
        List<NotesData> GetAll();

        [OperationContract]
        string SearchBy();

        [OperationContract]
        void DeleteNote(Guid id);

        [OperationContract]
        void Edit(NotesData data);

        [OperationContract]
        void DeleteAll();
    }
}