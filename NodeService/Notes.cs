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
            var nr = new NotesRepository();
            try
            {
                nr.Add(new NoteEntity {Id = dataModel.Id, Header = dataModel.Header, Content = dataModel.Content, Time = dataModel.Time});
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
            var nr = new NotesRepository();

            try
            {
                var result = nr.GetAll().Select(e => new NotesDataModel
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
            var nr = new NotesRepository();
            try
            {
                nr.DeleteNote(id);
            }
            catch (Exception err)
            {
                throw new FaultException(err.Message);
            }
        }

        public void Edit(NotesDataModel dataModel)
        {
            var nr = new NotesRepository();
            try
            {
                nr.Edit(new NoteEntity {Id = dataModel.Id, Header = dataModel.Header, Content = dataModel.Content});
            }
            catch (Exception err)
            {
                throw new FaultException(err.Message);
            }
        }

        public void DeleteAll()
        {
            var nr = new NotesRepository();
            try
            {
                nr.DeleteAll();
            }
            catch (Exception err)
            {
                throw new FaultException(err.Message);
            }
        }
    }
}