using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Microsoft.Win32; // Это для OpenFileDialog; 
using DAL;
using System.Collections.Generic;
using System.Data;
using Client.RbcService;
using System.Windows.Threading;
using Client.ExchangeService;
using Client.WeatherService;
using Client.NotesService;
using GeneralContract;

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// For wcf please include link to System.ServiceModel
    /// GeneralContract is a class library .dll
    /// To use migration use enable-migrations 
    /// command in Packet Manager Consol 
    /// Use command: Enable-Migrations -ProjectName "DAL" -StartUpProjectName "NoteService"
    /// Then put this command in console line Add-Migration FirstMigration -ProjectName "DAL" -StartUpProjectName "NoteService"  
    /// And Update-Database -ProjectName "DAL" -StartUpProjectName "NoteService" 
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private decimal money = 0;
       
        /// <summary>
        /// Static var for NotesWindow
        /// </summary>
        public static Guid test;

        //FactoryAndChannels factory = new FactoryAndChannels();
        //ExServiceContractClient currencyService = new ExServiceContractClient();
        //WtServiceContractClient weatherService = new WtServiceContractClient();
        NoteServiceContractClient notesService = new NoteServiceContractClient();

        /// <summary>
        /// Main Window constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            LoadCurrency();
            GetTime();
        }


        #region Service methods

        /// <summary>
        /// Method working with RBC Api to get actual rates
        /// </summary>
        private async void GetRbcExchangeRates()
        {
            //Vname - Название валюты
            //Vnom - Номинал
            //Vcurs - Курс
            //Vcode - ISO Цифровой код валюты
            //VchCode - ISO Символьный код валюты
            List<string> lst = new List<string>();
            DailyInfoSoapClient client = new DailyInfoSoapClient();

            try
            {
                DataSet response = await client.GetCursOnDateAsync(DateTime.Now);
                rbcCurrency.DataContext = response.Tables["ValuteCursOnDate"];
            }
            catch (Exception err)
            {
                ShowError("Ошибка подключения" + err.Message);
            }
        }

        /// <summary>
        /// Simple currency converter
        /// </summary>
        private async void CurrencyCalculate()
        {
            decimal tempMoney = 0;
            ResetCurrencyToZero();
            try
            {
                bool isMoneyValueRigth = Decimal.TryParse(moneyValue.Text, out tempMoney);
                if (isMoneyValueRigth)
                {
                    money = tempMoney;
                }

                //decimal result = factory.CreateExchangeFactory().Get(money, chooseCurrency.Text, toCurrency.Text);
                using (ExServiceContractClient currencyService = new ExServiceContractClient())
                {
                    decimal result = await currencyService.GetAsync(money, chooseCurrency.Text, toCurrency.Text);
                    if (toCurrency.Text == "Рубль")
                        currency.Text = result.ToString() + Constants.RU;
                    else if (toCurrency.Text == "Доллар")
                        currency.Text = result.ToString() + Constants.US;
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
                currency.Text = Constants.PRINT_ZERO_VALUE;
                money = 0;
            }
        }

        /// <summary>
        /// Test method
        /// </summary>
        private void LoadCurrency()
        {
            if (string.IsNullOrWhiteSpace(moneyValue.Text) || string.IsNullOrEmpty(moneyValue.Text))
                currency.Text = Constants.PRINT_ZERO_VALUE;
        }

        /// <summary>
        /// Get weather from API
        /// </summary>
        private async void GetWeather()
        {
            try
            {
                //string result = factory.CreateWeatherFactory().GetWeatherFromOpenWeatherApi();
                using (WtServiceContractClient weatherService = new WtServiceContractClient())
                {
                    string getWeather = await weatherService.GetWeatherFromOpenWeatherApiAsync();
                    //weather.Text = result.ToString();
                    weather.Text = getWeather.ToString();
                }
                   
            }
            catch (Exception err)
            {
                ShowError(err.Message);
            }
        }

        /// <summary>
        /// Get notes from Notes service
        /// </summary>
        private async void GetNotes()
        {
            try
            {
                //ListOfNotes.ItemsSource = factory.CreateNotesFactory().GetAll();
                using (NoteServiceContractClient notesService1 = new NoteServiceContractClient())
                {
                    ListOfNotes.ItemsSource = await notesService1.GetAllAsync();
                }
            }
            catch (Exception err)
            {
                ShowError(err.Message);
            }
        }

        private void OpenNotesDialog()
        {
            NotesWindow notes = new NotesWindow();
            notes.Owner = this;
            //notes.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            notes.ShowDialog();
        }
        #endregion

        #region Service methods
        /// <summary>
        /// Method for Warnings and errors to show to users
        /// </summary>
        /// <param name="err"></param>
        public void ShowError(string err)
        {
            MessageBox.Show(err, UserNotifications.ERROR, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        /// Getting time from system
        /// </summary>
        private void GetTime()
        {
            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                this.time.Content = DateTime.Now.ToString("HH:mm:ss");
                // this.
            }, this.Dispatcher);

            date.Content = DateTime.Now.ToShortDateString();
        }
        #endregion

        #region TabControl, buttons etc handlers
        /// <summary>
        /// Test Event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void NotesRepository_Changed(object sender, EventArgs e)
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
            OpenFileDialog of = new OpenFileDialog();
            NotesData nd = new NotesData();
            //CSVFileParser parser = new CSVFileParser(path);
            of.Filter = "CSV Files(*.csv)|*.csv|All(*.*)|*";

            if (of.ShowDialog() == true)
                return;

            string filename = of.FileName;
            string fileText = System.IO.File.ReadAllText(filename);
            CSVFileParser csParse = new CSVFileParser();
            //ListOfNotes.DataContext = csParse.ReadCsv(filename);
            
        }

        private async void ExportCsvMenuItem_Click(object sender, RoutedEventArgs e)
        {

            //var rdDataFromDB = factory.CreateNotesFactory().GetAll();
            //List<NotesData> rdDataFromDB = new List<NotesData>();

            NotesData[] rdDataFromDB = await notesService.GetAllAsync();

            SaveFile sf = new SaveFile();
            SaveFileDialog safeFile = new SaveFileDialog();
            safeFile.Filter = "CSV Files(*.csv)|*.csv|All(*.*)|*";
            safeFile.RestoreDirectory = true;
            safeFile.InitialDirectory = "c:\\";

            try
            {
                sf.OpenFileDialog(rdDataFromDB, safeFile);
            }
            catch (Exception ex)
            {
                ShowError("Ошибка сохранения " + ex.Message);
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            SearchPage searchPage = new SearchPage();
            searchPage.Owner = this;
            searchPage.ShowDialog();
        }

        #endregion


        #region DataGrid Handlers and methods

        int selectedColumn = 0;
        bool selectTrue = false;

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
            Guid selectedId = ((NotesData)ListOfNotes.SelectedItem).Id;
            //factory.CreateNotesFactory().DeleteNote(selectedId);
            try
            {
                notesService.DeleteNoteAsync(selectedId);
            }
            catch(Exception err)
            {
                ShowError(err.Message);
            }
        }

        private void DeleteAllMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //factory.CreateNotesFactory().DeleteAll();
            try
            {
                notesService.DeleteAllAsync();
            }
            catch(Exception err)
            {
                ShowError(err.Message);
            }
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectTrue)
                {
                    NotesWindow notesWindow = new NotesWindow();

                    var selectedCell = ListOfNotes.SelectedCells[selectedColumn];
                    var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
                    NotesData notesData = new NotesData();

                    for (int i = 0; i < Constants.DATAGRID_SIZE; i++)
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
                    ShowError(UserNotifications.NO_ENTRY_SELECTED);
            }
           catch(Exception err)
            {
                ShowError("Таблица пуста! " + err.Message);
            }
        }



        #endregion

    }
}
