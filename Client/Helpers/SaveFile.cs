using System.IO;
using System.Text;
using Client.Service_References.NotesService;
using Microsoft.Win32;

namespace Client.Helpers
{
    /// <summary>
    ///     Class for saving files and openfile dialog
    /// </summary>
    public class SaveFile
    {
        /// <summary>
        ///     Default constructor
        /// </summary>
        public SaveFile()
        {
        }

        /// <summary>
        ///     Constructor get the string value with file path
        /// </summary>
        /// <param name="currentFile"></param>
        public SaveFile(string currentFile)
        {
            CurrentFile = currentFile;
        }

        private string CurrentFile { get; }

        /// <summary>
        ///     Get path from dialog
        /// </summary>
        /// <returns></returns>
        public string GetPath()
        {
            if (string.IsNullOrEmpty(CurrentFile))
            {
                var dialog = new SaveFileDialog();
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
        ///     OpenFile diolog method
        /// </summary>
        /// <param name="dataModel"></param>
        /// <param name="dialog"></param>
        public void OpenFileDialog(NotesData[] dataModel, SaveFileDialog dialog)
        {
            if (dialog.ShowDialog() == true)
            {
                var path = dialog.FileName;
                var streamWriter = new StreamWriter(path, true, Encoding.GetEncoding(1251));
                using (streamWriter)
                {
                    foreach (var item in dataModel)
                    {
                        streamWriter.Write(item.Id + ",");
                        streamWriter.Write(item.Header + ",");
                        streamWriter.Write(item.Content + ",");
                        streamWriter.Write(item.Time + ",");
                        streamWriter.WriteLine();
                    }

                    dialog.RestoreDirectory = true;
                    streamWriter.Close();
                }
            }
        }
    }
}