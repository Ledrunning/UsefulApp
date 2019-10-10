using System;
using System.IO;
using Client.NotesService;
using TotalContract;

namespace Client
{
    internal class CSVFileParser
    {
        public async void ReadData(string path)
        {
            string[] read;
            char[] separators = {',', '\t', '\n', '\r', '_'};

            using (var reader = new StreamReader(path))
            {
                var data = reader.ReadLine();
                var nd = new NotesDataModel();

                try
                {
                    while ((data = reader.ReadLine()) != null)
                    {
                        read = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        nd.Id = Guid.Parse(read[0]);
                        nd.Header = read[1];
                        nd.Content = read[2];
                        nd.Time = int.Parse(read[3]);
                        using (var notesService = new NoteServiceContractClient())
                        {
                            await notesService.AddAsync(nd);
                        }
                    }
                }
                catch (Exception err)
                {
                    throw new Exception("В базе уже существуют данные записи!" + err.Message);
                }
            }
        }
    }
}