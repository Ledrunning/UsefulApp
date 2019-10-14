using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Client
{
    /// <summary>
    ///     Логика взаимодействия для SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Window
    {
        private FactoryAndChannels fc = new FactoryAndChannels();
        private List<object> myList = new List<object>();
        private int numberOfRecPerPage;
        private int pageIndex = 1;

        /// <summary>
        ///     Search page main constructor
        /// </summary>
        public SearchPage()
        {
            InitializeComponent();
            cbNumberOfRecords.Items.Add("10");
            cbNumberOfRecords.Items.Add("20");
            cbNumberOfRecords.Items.Add("30");
            cbNumberOfRecords.Items.Add("50");
            cbNumberOfRecords.Items.Add("100");
            cbNumberOfRecords.SelectedItem = 10;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            myList = GetData();
            //GetNotes();
            dataGrid.ItemsSource = myList.Take(numberOfRecPerPage);
            var count = myList.Take(numberOfRecPerPage).Count();
            lblpageInformation.Content = count + " of " + myList.Count;
        }

        private void GetNotes()
        {
            //myList.Add(fc.CreateNotesFactory().GetAll());
            //dataGrid.ItemsSource = fc.CreateNotesFactory().GetAll();
        }

        private List<object> GetData()
        {
            var genericList = new List<object>();
            Student studentObj;
            var randomObj = new Random();
            for (var i = 0; i < 1000; i++)
            {
                studentObj = new Student();
                studentObj.FirstName = "First " + i;
                studentObj.MiddleName = "Middle " + i;
                studentObj.LastName = "Last " + i;
                studentObj.Age = (uint) randomObj.Next(1, 100);

                genericList.Add(studentObj);
            }

            return genericList;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //To check the paging direction according to use selection.
        private enum PagingMode
        {
            First = 1,
            Next = 2,
            Previous = 3,
            Last = 4,
            PageCountChange = 5
        }

        #region Pagination 

        private void btnFirst_Click(object sender, EventArgs e)
        {
            Navigate((int) PagingMode.First);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Navigate((int) PagingMode.Next);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            Navigate((int) PagingMode.Previous);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            Navigate((int) PagingMode.Last);
        }

        private void cbNumberOfRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Navigate((int) PagingMode.PageCountChange);
        }

        private void Navigate(int mode)
        {
            int count;
            switch (mode)
            {
                case (int) PagingMode.Next:
                    btnPrev.IsEnabled = true;
                    btnFirst.IsEnabled = true;
                    if (myList.Count >= pageIndex * numberOfRecPerPage)
                    {
                        if (myList.Skip(pageIndex *
                                        numberOfRecPerPage).Take(numberOfRecPerPage).Count() == 0)
                        {
                            dataGrid.ItemsSource = null;
                            dataGrid.ItemsSource = myList.Skip(pageIndex *
                                                               numberOfRecPerPage - numberOfRecPerPage)
                                .Take(numberOfRecPerPage);
                            count = pageIndex * numberOfRecPerPage +
                                    myList.Skip(pageIndex *
                                                numberOfRecPerPage).Take(numberOfRecPerPage).Count();
                        }
                        else
                        {
                            dataGrid.ItemsSource = null;
                            dataGrid.ItemsSource = myList.Skip(pageIndex * numberOfRecPerPage).Take(numberOfRecPerPage);
                            count = pageIndex * numberOfRecPerPage + myList.Skip(pageIndex * numberOfRecPerPage)
                                        .Take(numberOfRecPerPage).Count();
                            pageIndex++;
                        }

                        lblpageInformation.Content = count + " of " + myList.Count;
                    }

                    else
                    {
                        btnNext.IsEnabled = false;
                        btnLast.IsEnabled = false;
                    }

                    break;

                case (int) PagingMode.Previous:
                    btnNext.IsEnabled = true;
                    btnLast.IsEnabled = true;
                    if (pageIndex > 1)
                    {
                        pageIndex -= 1;
                        dataGrid.ItemsSource = null;
                        if (pageIndex == 1)
                        {
                            dataGrid.ItemsSource = myList.Take(numberOfRecPerPage);
                            count = myList.Take(numberOfRecPerPage).Count();
                            lblpageInformation.Content = count + " of " + myList.Count;
                        }
                        else
                        {
                            dataGrid.ItemsSource = myList.Skip
                                (pageIndex * numberOfRecPerPage).Take(numberOfRecPerPage);
                            count = Math.Min(pageIndex * numberOfRecPerPage, myList.Count);
                            lblpageInformation.Content = count + " of " + myList.Count;
                        }
                    }
                    else
                    {
                        btnPrev.IsEnabled = false;
                        btnFirst.IsEnabled = false;
                    }

                    break;

                case (int) PagingMode.First:
                    pageIndex = 2;
                    Navigate((int) PagingMode.Previous);
                    break;
                case (int) PagingMode.Last:
                    pageIndex = myList.Count / numberOfRecPerPage;
                    Navigate((int) PagingMode.Next);
                    break;

                case (int) PagingMode.PageCountChange:
                    pageIndex = 1;
                    numberOfRecPerPage = Convert.ToInt32(cbNumberOfRecords.SelectedItem);
                    dataGrid.ItemsSource = null;
                    dataGrid.ItemsSource = myList.Take(numberOfRecPerPage);
                    count = myList.Take(numberOfRecPerPage).Count();
                    lblpageInformation.Content = count + " of " + myList.Count;
                    btnNext.IsEnabled = true;
                    btnLast.IsEnabled = true;
                    btnPrev.IsEnabled = true;
                    btnFirst.IsEnabled = true;
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //GetNotes(); 
        }
    }
}

#endregion