﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Client.Service_References.NotesService;
using TotalContract;

//using GeneralContract;

namespace Client
{
    /// <summary>
    ///     Логика взаимодействия для Notes.xaml
    /// </summary>
    /// To use services, please delete the link from references!!!
    public partial class NotesWindow : Window
    {
        //FactoryAndChannels factory = new FactoryAndChannels();

        private readonly NotesData notesData = new NotesData();

        /// <summary>
        /// </summary>
        public NotesWindow()
        {
            InitializeComponent();
        }

        private void confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MainWindow.test != Guid.Empty)
                {
                    //notesData.Id = Guid.Parse("689f5ab7-f779-4df3-bece-1aaaa64f8ddf"); // Проверка;
                    Dispatcher.Invoke(delegate
                    {
                        notesData.Id = MainWindow.test;
                        notesData.Header = header.Text;
                        notesData.Content = GetStringFromRtb(content);
                        //factory.CreateNotesFactory().Edit(notesData);
                        using (var notesService = new NoteServiceContractClient())
                        {
                            notesService.Edit(notesData);
                        }
                    });

                    Close();
                }

                else //if (notesData.Id == Guid.Empty && MainWindow.test == Guid.Empty)
                {
                    if (string.IsNullOrWhiteSpace(header.Text) && string.IsNullOrWhiteSpace(GetStringFromRtb(content)))
                    {
                        ShowError(UserNotifications.FILL_ALL_FIELDS);
                    }
                    else
                    {
                        Dispatcher.Invoke(delegate
                        {
                            notesData.Id = Guid.NewGuid();
                            notesData.Time = (int) (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalHours;
                            notesData.Header = header.Text;
                            notesData.Content = GetStringFromRtb(content);
                            //factory.CreateNotesFactory().Add(notesData);
                            using (var notesService = new NoteServiceContractClient())
                            {
                                notesService.Add(notesData);
                            }
                        });

                        Close();
                    }
                }
            }

            catch (Exception err)
            {
                ShowError(err, UserNotifications.ERROR);
            }
        }

        private void fill_Click(object sender, RoutedEventArgs e)
        {
            FillNotes();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     This method gets text content from RichTextBox
        /// </summary>
        /// <param name="rtb"></param>
        /// <returns></returns>
        private string GetStringFromRtb(RichTextBox rtb)
        {
            var textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            return textRange.Text;
        }

        /// <summary>
        ///     Method to show errors for users
        /// </summary>
        /// <param name="err"></param>
        public void ShowError(string err)
        {
            MessageBox.Show(err, UserNotifications.NO_ENTRY_SELECTED, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        ///     Overloaded method for error
        /// </summary>
        /// <param name="err"></param>
        /// <param name="msg"></param>
        public void ShowError(Exception err, string msg)
        {
            MessageBox.Show(err.Message, msg, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        ///     Random fillin notes to database
        /// </summary>
        private async void FillNotes()
        {
            NotesData notesObj;
            for (var i = 0; i < 600; i++)
            {
                notesObj = new NotesData();
                notesObj.Id = Guid.NewGuid();
                notesObj.Header = "Header " + i;
                notesObj.Content = "Content " + i;
                notesObj.Time = (int) (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalHours;
                //factory.CreateNotesFactory().Add(notesObj);\
                using (var notesService = new NoteServiceContractClient())
                {
                    await notesService.AddAsync(notesObj);
                }
            }
        }
    }
}