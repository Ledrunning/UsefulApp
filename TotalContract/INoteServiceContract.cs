using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace TotalContract
{
    [ServiceContract]
    public interface INoteServiceContract
    {
        [OperationContract]
        void Add(NotesDataModel dataModel);

        [OperationContract]
        void GetById(Guid id);

        [OperationContract]
        List<NotesDataModel> GetAll();

        [OperationContract]
        string SearchBy();

        [OperationContract]
        void DeleteNote(Guid id);

        [OperationContract]
        void Edit(NotesDataModel dataModel);

        [OperationContract]
        void DeleteAll();
    }
}