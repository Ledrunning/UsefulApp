using System;
using System.Collections.Generic;
using DAL;
using GeneralContract;
using LumenWorks.Framework;
using LumenWorks.Framework.IO.Csv;
using System.IO;

namespace Client
{
    class CSVFileParser
    {
        List<NotesData> lst = new List<NotesData>();

        public CachedCsvReader ReadCsv(string path)
        {
            // open the file "data.csv" which is a CSV file with headers
            using (var csv = new CachedCsvReader(new StreamReader(path), true))
            {
                // Field headers will automatically be used as column names
                return csv;
            }
        }


        public IEnumerable<NotesData> GetAllNotes()
        {
            foreach (var item in lst)
            {
                yield return item;
            }
        }
    }
}
