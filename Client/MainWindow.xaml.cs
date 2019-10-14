using System;
using System.IO;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Client.ExchangeService;
using Client.Helpers;
using Client.NotesService;
using Client.RbcService;
using Client.WeatherService;
using Microsoft.Win32;
using TotalContract;

// Это для OpenFileDialog; 

namespace Client
{
    /// <summary>
    ///     Логика взаимодействия для MainWindow.xaml
    ///     For wcf please include link to System.ServiceModel
    ///     GeneralContract is a class library .dll
    ///     To use migration use enable-migrations
    ///     command in Packet Manager Consol
    ///     Use command: Enable-Migrations -ProjectName "DAL" -StartUpProjectName "NoteService"
    ///     Then put this command in console line Add-Migration FirstMigration -ProjectName "DAL" -StartUpProjectName
    ///     "NoteService"
    ///     And Update-Database -ProjectName "DAL" -StartUpProjectName "NoteService"
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        ///     Static var for NotesWindow
        /// </summary>
        public static Guid test;

        private decimal money;

        //Needed when I use GeneralCOntracts without link to service;
        //FactoryAndChannels factory = new FactoryAndChannels();


        /// <summary>
        ///     Main Window constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            LoadCurrency();
            GetTime();
        }


        #region Service methods

        /// <summary>
        ///     Method working with RBC Api to get actual rates
        /// </summary>
        private async void GetRbcExchangeRates()
        {
            //Vname - Название валюты
            //Vnom - Номинал
            //Vcurs - Курс
            //Vcode - ISO Цифровой код валюты
            //VchCode - ISO Символьный код валюты
            var client = new DailyInfoSoapClient();

            try
            {
                var response = await client.GetCursOnDateAsync(DateTime.Now);
                rbcCurrency.DataContext = response.Tables["ValuteCursOnDate"];
            }
            catch (Exception err)
            {
                ShowError("Ошибка подключения" + err.Message);
            }
        }

        /// <summary>
        ///     Simple currency converter
        /// </summary>
        private async void CurrencyCalculate()
        {
            decimal tempMoney = 0;
            ResetCurrencyToZero();
            try
            {
                var isMoneyValueRigth = decimal.TryParse(moneyValue.Text, out tempMoney);
                if (isMoneyValueRigth) money = tempMoney;

                //decimal result = factory.CreateExchangeFactory().Get(money, chooseCurrency.Text, toCurrency.Text);
                using (var currencyService = new ExchangeServiceContractClient())
                {
                    var result = await currencyService.GetAsync(money, chooseCurrency.Text, toCurrency.Text);
                    if (toCurrency.Text == "Рубль")
                        currency.Text = result + Constants.Ru;
                    else if (toCurrency.Text == "Доллар")
                        currency.Text = result + Constants.Us;
                    else
                        currency.Text = result.ToString();
                }
            }
            catch (Exception err)
            {
                ShowError(err.Message);
            }
        }

        private void ResetCurrencyToZero()
        {
            if (string.IsNullOrWhiteSpace(moneyValue.Text) || string.IsNullOrEmpty(moneyValue.Text))
            {
                currency.Text = Constants.ZeroValue;
                money = 0;
            }
        }

        /// <summary>
        ///     Test method
        /// </summary>
        private void LoadCurrency()
        {
            if (string.IsNullOrWhiteSpace(moneyValue.Text) || string.IsNullOrEmpty(moneyValue.Text))
                currency.Text = Constants.ZeroValue;
        }

        /// <summary>
        ///     Get weather from API
        /// </summary>
        private async void GetWeather()
        {
            try
            {
                //string result = factory.CreateWeatherFactory().GetWeatherFromOpenWeatherApi();
                using (var weatherService = new WeatherServiceContractClient())
                {
                    var getWeather = await weatherService.GetWeatherFromOpenWeatherApiAsync();
                    //weather.Text = result.ToString();
                    weather.Text = getWeather;
                }
            }
            catch (Exception err)
            {
                ShowError(err.Message);
            }
        }

        /// <summary>
        ///     Get notes from Notes service
        /// </summary>
        private async void GetNotes()
        {
            try
            {
                //ListOfNotes.ItemsSource = factory.CreateNotesFactory().GetAll();
                using (var notesService = new NoteServiceContractClient())
                {
                    ListOfNotes.ItemsSource = await notesService.GetAllAsync();
                }
            }
            catch (Exception err)
            {
                ShowError(err.Message);
            }
        }

        private void OpenNotesDialog()
        {
            var notes = new NotesWindow();
            notes.Owner = this;
            //notes.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            notes.ShowDialog();
        }

        #endregion

        #region Service methods

        /// <summary>
        ///     Method for Warnings and errors to show to users
        /// </summary>
        /// <param name="err"></param>
        public void ShowError(string err)
        {
            MessageBox.Show(err, UserNotifications.Error, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        ///     Getting time from system
        /// </summary>
        private void GetTime()
        {
            var timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                time.Content = DateTime.Now.ToString("HH:mm:ss");
                // this.
            }, Dispatcher);

            date.Content = DateTime.Now.ToShortDateString();
        }

        #endregion

        #region TabControl, buttons etc handlers

        /// <summary>
        ///     Test Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void NotesRepository_Changed(object sender, EventArgs e)
        {
            using (var notesService = new NoteServiceContractClient())
            {
                try
                {
                    //ListOfNotes.ItemsSource = factory.CreateNotesFactory().GetAll();
                    ListOfNotes.ItemsSource = await notesService.GetAllAsync();
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
        }


        private void chooseCurrency_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrencyCalculate();
        }

        private void toCurrency_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrencyCalculate();
        }

        private void moneyValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            CurrencyCalculate();
        }

        private void chooseCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetWeather();
        }


        private void addNote_Click(object sender, RoutedEventArgs e)
        {
            OpenNotesDialog();
        }

        private void getRbc_Click(object sender, RoutedEventArgs e)
        {
            GetRbcExchangeRates();
        }

        private void rbcCurrency_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void ImportCsvMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var of = new OpenFileDialog();
            var nd = new NotesDataModel();
            //CSVFileParser parser = new CSVFileParser(path);
            of.Filter = "CSV Files(*.csv)|*.csv|All(*.*)|*";

            if (of.ShowDialog() == true)
                //return;
            {
                var filename = of.FileName;
                var fileText = File.ReadAllText(filename);
                var csParse = new CSVFileParser();

                try
                {
                    csParse.ReadData(filename);
                }
                catch (FaultException fex)
                {
                    ShowError(fex.Message);
                }
                catch (Exception err)
                {
                    ShowError(err.Message);
                }
            }
        }

        private async void ExportCsvMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //var rdDataFromDB = factory.CreateNotesFactory().GetAll();
            //List<NotesData> rdDataFromDB = new List<NotesData>();

            NotesData[] dataModel;

            using (var notesService = new NoteServiceContractClient())
            {
                dataModel = await notesService.GetAllAsync();
            }

            var saveFile = new SaveFile();
            var safeFile = new SaveFileDialog();
            safeFile.Filter = "CSV Files(*.csv)|*.csv|All(*.*)|*";
            safeFile.RestoreDirectory = true;
            safeFile.InitialDirectory = "c:\\";

            try
            {
                saveFile.OpenFileDialog(dataModel, safeFile);
            }
            catch (Exception ex)
            {
                ShowError("Ошибка сохранения " + ex.Message);
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var searchPage = new SearchPage();
            searchPage.Owner = this;
            searchPage.ShowDialog();
        }

        #endregion


        #region DataGrid Handlers and methods

        private int selectedColumn;
        private bool selectTrue;

        private void grid_Loaded(object sender, RoutedEventArgs e)
        {
            GetNotes();
        }

        private void ListOfNotes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedColumn = ListOfNotes.CurrentCell.Column.DisplayIndex;
            selectTrue = true;
        }

        private void ListOfNotes_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        #endregion

        #region Menu item handlers

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedId = ((NotesDataModel) ListOfNotes.SelectedItem).Id;
            //factory.CreateNotesFactory().DeleteNote(selectedId);
            using (var notesService = new NoteServiceContractClient())
            {
                try
                {
                    notesService.DeleteNoteAsync(selectedId);
                }
                catch (Exception err)
                {
                    ShowError(err.Message);
                }
            }
        }

        private void DeleteAllMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //factory.CreateNotesFactory().DeleteAll();
            using (var notesService = new NoteServiceContractClient())
            {
                try
                {
                    notesService.DeleteAllAsync();
                }
                catch (Exception err)
                {
                    ShowError(err.Message);
                }
            }
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectTrue)
                {
                    var notesWindow = new NotesWindow();

                    var selectedCell = ListOfNotes.SelectedCells[selectedColumn];
                    var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
                    var notesData = new NotesDataModel();

                    for (var i = 0; i < Constants.DataGridSize; i++)
                    {
                        selectedCell = ListOfNotes.SelectedCells[i];
                        cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);

                        switch (i)
                        {
                            case 0:
                                notesData.Id = Guid.Parse((cellContent as TextBlock).Text);
                                test = notesData.Id;
                                break;
                            case 1:
                                notesWindow.header.Text = (cellContent as TextBlock).Text;
                                break;

                            case 2:
                                notesWindow.content.AppendText((cellContent as TextBlock).Text);
                                break;
                        }
                    }

                    notesWindow.Show();
                }
                else
                {
                    ShowError(UserNotifications.NoEntrySelected);
                }
            }
            catch (Exception err)
            {
                ShowError("Таблица пуста! " + err.Message);
            }
        }

        #endregion
    }
}