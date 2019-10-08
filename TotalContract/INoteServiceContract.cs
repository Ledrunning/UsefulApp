using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace TotalContract
{
    [ServiceContract]
    public interface INoteServiceContract
    {
        [OperationContract]
        void Add(NotesData nd);

        [OperationContract]
        void GetById(Guid id);

        [OperationContract]
        List<NotesData> GetAll();

        [OperationContract]
        string SearchBy();

        [OperationContract]
        void DeleteNote(Guid id);

        [OperationContract]
        void Edit(NotesData nt);

        [OperationContract]
        void DeleteAll();
    }
}