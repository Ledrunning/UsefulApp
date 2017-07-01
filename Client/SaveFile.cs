using Microsoft.Win32;
using GeneralContract;
using System.Collections.Generic;
using System.IO;

namespace Client
{
    /// <summary>
    /// Class for saving files and openfile dialog
    /// </summary>
    public class SaveFile
    {
        private string CurrentFile { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SaveFile()
        {

        }

        /// <summary>
        /// Constructor get the string value with file path
        /// </summary>
        /// <param name="currentFile"></param>
        public SaveFile(string currentFile)
        {
            this.CurrentFile = currentFile;
        }

        /// <summary>
        /// Get path from dialog
        /// </summary>
        /// <returns></returns>
        public string GetPath()
        {
            if (string.IsNullOrEmpty(CurrentFile))
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "CSV Files(*.csv)|*.csv|All(*.*)|*";
                dialog.RestoreDirectory = true;
                dialog.InitialDirectory = dialog.FileName;
                if (dialog.ShowDialog() == true)
                    return dialog.FileName;
                return null;
            }

            return CurrentFile;
        }

        /// <summary>
        /// OpenFile diolog method
        /// </summary>
        /// <param name="a"></param>
        /// <param name="d"></param>
        public void OpenFileDialog(NotesData[] a, SaveFileDialog d)
        {

            if (d.ShowDialog() == true)
            {

                string path = d.FileName;
                StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.GetEncoding(1251));
                using (sw)
                {
                    foreach (var item in a)
                    {
                        sw.Write(item.Id + ",");
                        sw.Write(item.Header + ",");
                        sw.Write(item.Content + ",");
                        sw.Write(item.Time + ",");
                        sw.WriteLine();
                    }

                    d.RestoreDirectory = true;
                    sw.Close();
                }

            }
        }
    }
}
