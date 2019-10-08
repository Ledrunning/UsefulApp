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
        public void Add(NotesData data)
        {
            var nr = new NotesRepository();
            try
            {
                nr.Add(new NoteEntity {Id = data.Id, Header = data.Header, Content = data.Content, Time = data.Time});
            }
            catch (Exception err)
            {
                throw new FaultException(err.Message);
            }
        }

        public void GetById(Guid id)
        {
        }

        public List<NotesData> GetAll()
        {
            var nr = new NotesRepository();

            try
            {
                var result = nr.GetAll().Select(e => new NotesData
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
            //var a = new DAL.NoteEntity() { Id = id };
            var nr = new NotesRepository();
            //DAL.NotesRepository.DeleteNote(id);
            try
            {
                nr.DeleteNote(id);
            }
            catch (Exception err)
            {
                throw new FaultException(err.Message);
            }
        }

        public void Edit(NotesData data)
        {
            var nr = new NotesRepository();
            //DAL.NotesRepository.Edit(new DAL.NoteEntity() { Id = nd.Id, Header = nd.Header, Content = nd.Content });
            try
            {
                nr.Edit(new NoteEntity {Id = data.Id, Header = data.Header, Content = data.Content});
            }
            catch (Exception err)
            {
                throw new FaultException(err.Message);
            }
        }

        public void DeleteAll()
        {
            //DAL.NotesRepository.DeleteAll();
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