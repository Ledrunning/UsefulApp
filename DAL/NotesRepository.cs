using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public class NotesRepository
    {
        /// <summary>
        ///     Add data to the default database
        /// </summary>
        /// <param name="noteEntity"></param>
        public void Add(NoteEntity noteEntity)
        {
            using (var dbcon = new DataBaseContext())
            {
                try
                {
                    dbcon.Notes.Add(noteEntity);
                    dbcon.SaveChanges();
                }
                catch (Exception err)
                {
                    throw new Exception(err.Message);
                }
            }
        }

        /// <summary>
        ///     Method where note gets by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<NoteEntity> GetById(Guid id)
        {
            using (var dbcon = new DataBaseContext())
            {
                var nt = new NoteEntity();
                var getNote = dbcon.Notes.Where(i => i.Id == nt.Id);
                return dbcon.Notes.ToList();
            }
        }

        /// <summary>
        ///     Get all data from database table
        /// </summary>
        /// <returns></returns>
        public List<NoteEntity> GetAll()
        {
            using (var dbcon = new DataBaseContext())
            {
                try
                {
                    return dbcon.Notes.ToList();
                }
                catch (Exception err)
                {
                    throw new Exception(err.Message);
                }
            }
        }

        /// <summary>
        ///     Search by Id and content
        /// </summary>
        /// <param name="notesEntity"></param>
        /// <returns></returns>
        public List<NoteEntity> SearchBy(NoteEntity notesEntity)
        {
            using (var dbcon = new DataBaseContext())
            {
                var searchdNote = dbcon.Notes.Where(i => i.Id == notesEntity.Id).OrderBy(i => i.Content);
                dbcon.SaveChanges();
                return dbcon.Notes.ToList();
            }
        }

        /// <summary>
        ///     Edit selected entry
        /// </summary>
        /// <param name="notesEntity"></param>
        public void Edit(NoteEntity notesEntity)
        {
            using (var dbcon = new DataBaseContext())
            {
                var noteToUpdate = dbcon.Notes.FirstOrDefault(n => n.Id == notesEntity.Id);

                if (noteToUpdate != null)
                {
                    //noteToUpdate.Id = Guid.Parse("689f5ab7-f779-4df3-bece-1aaaa64f8ddf");
                    noteToUpdate.Header = notesEntity.Header;
                    noteToUpdate.Content = notesEntity.Content;
                    dbcon.Entry(noteToUpdate).State = EntityState.Modified;
                    dbcon.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("Заметка с Id = '{0}' отсутствует {1}!", notesEntity.Id, notesEntity.Header));
                }
            }
        }

        /// <summary>
        ///     Delete selected note
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception"></exception>
        public void DeleteNote(Guid id)
        {
            using (var dbcon = new DataBaseContext())
            {
                try
                {
                    var noteToRemove = new NoteEntity
                    {
                        Id = id
                    };
                    dbcon.Notes.Attach(noteToRemove);
                    dbcon.Notes.Remove(noteToRemove);
                    dbcon.SaveChanges();
                }
                catch (Exception err)
                {
                    throw new Exception(string.Format("Заметка не существует или удалена!" + err.Message));
                }

                //OnChanged.Invoke(null, new EventArgs());
            }
        }

        /// <summary>
        ///     Delete all entries in database table
        /// </summary>
        public void DeleteAll()
        {
            using (var dbcon = new DataBaseContext())
            {
                var all = dbcon.Notes;
                try
                {
                    dbcon.Notes.RemoveRange(all);
                    dbcon.SaveChanges();
                }
                catch (Exception err)
                {
                    throw new Exception(string.Format("Заметки пусты!" + err.Message));
                }

                //OnChanged.Invoke(null, new EventArgs());
            }
        }
    }
}