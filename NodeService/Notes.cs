using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralContract;
using DAL;
using System.ServiceModel;

namespace NoteService
{
    public class Notes : INoteServiceContract
    {
        
        public void Add(NotesData nd)
        {
            NotesRepository nr = new NotesRepository();
            try
            {
                nr.Add(new NoteEntity() { Id = nd.Id, Header = nd.Header, Content = nd.Content, Time = nd.Time });
            }
            catch(Exception err)
            {
                throw new FaultException(err.Message);
            }
        }

        public void GetById(Guid id)
        {

        }

        public List<NotesData> GetAll()
        {
            NotesRepository nr = new NotesRepository();
            
            try
            {
                var result = nr.GetAll().Select(e => new NotesData()
                { Id = e.Id, Header = e.Header, Content = e.Content, Time = e.Time}).ToList();
                return result;
            }
            catch(Exception ex)
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
            NotesRepository nr = new NotesRepository();
            //DAL.NotesRepository.DeleteNote(id);
            try
            {
                nr.DeleteNote(id);
            }
            catch(Exception err)
            {
                throw new FaultException(err.Message);
            }

        }

        public void Edit(NotesData nd)
        {
            NotesRepository nr = new NotesRepository();
            //DAL.NotesRepository.Edit(new DAL.NoteEntity() { Id = nd.Id, Header = nd.Header, Content = nd.Content });
            try
            {
                nr.Edit(new DAL.NoteEntity() { Id = nd.Id, Header = nd.Header, Content = nd.Content });
            }
            catch(Exception err)
            {
                throw new FaultException(err.Message);
            }
        }

        public void DeleteAll()
        {
            //DAL.NotesRepository.DeleteAll();
            NotesRepository nr = new NotesRepository();
            try
            {
               nr.DeleteAll();
            }
            catch(Exception err)
            {
                throw new FaultException(err.Message);
            }
        }
    }
}
