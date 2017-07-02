using System;
using System.Collections.Generic;
using DAL;
using GeneralContract;
using LumenWorks.Framework;
using LumenWorks.Framework.IO.Csv;
using System.IO;
using Client.NotesService;

namespace Client
{
    class CSVFileParser
    {
        public async void ReadData(string path)
        {
            string[] read;
            char[] separators = { ',', '\t', '\n', '\r', '_' };

            using (StreamReader sr = new StreamReader(path))
            {
                string data = sr.ReadLine();
                NotesData nd = new NotesData();

                try
                {
                    while ((data = sr.ReadLine()) != null)
                    {
                        read = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        nd.Id = Guid.Parse(read[0]);
                        nd.Header = read[1].ToString();
                        nd.Content = read[2].ToString();
                        nd.Time = int.Parse(read[3]);
                        using (NoteServiceContractClient notesService = new NoteServiceContractClient())
                        {
                            await notesService.AddAsync(nd);
                        }
                    }
                }
                catch(Exception err)
                {
                    throw new Exception("В базе уже существуют данные записи!" + err.Message);
                }
               
            }

            
        }
   
    }
}
