using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using DAL;
using TotalContract;

namespace NoteService
{
    public class Notes : INoteServiceContract
    {
        public void Add(NotesDataModel dataModel)
        {
            var notesRepository = new NotesRepository();
            try
            {
                notesRepository.Add(new NoteEntity
                    {Id = dataModel.Id, Header = dataModel.Header, Content = dataModel.Content, Time = dataModel.Time});
            }
            catch (Exception err)
            {
                throw new FaultException(err.Message);
            }
        }

        public void GetById(Guid id)
        {
        }

        public List<NotesDataModel> GetAll()
        {
            var notesRepository = new NotesRepository();

            try
            {
                var result = notesRepository.GetAll().Select(e => new NotesDataModel
                    {Id = e.Id, Header = e.Header, Content = e.Content, Time = e.Time}).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public string SearchBy()
        {
            return "";
        }

        public void DeleteNote(Guid id)
        {
            var notesRepository = new NotesRepository();
            try
            {
                notesRepository.DeleteNote(id);
            }
            catch (Exception err)
            {
                throw new FaultException(err.Message);
            }
        }

        public void Edit(NotesDataModel dataModel)
        {
            var notesRepository = new NotesRepository();
            try
            {
                notesRepository.Edit(new NoteEntity {Id = dataModel.Id, Header = dataModel.Header, Content = dataModel.Content});
            }
            catch (Exception err)
            {
                throw new FaultException(err.Message);
            }
        }

        public void DeleteAll()
        {
            var notesRepository = new NotesRepository();
            try
            {
                notesRepository.DeleteAll();
            }
            catch (Exception err)
            {
                throw new FaultException(err.Message);
            }
        }
    }
}