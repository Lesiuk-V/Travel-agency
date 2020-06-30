using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tour_agency.Aditional_windows;
using Tour_agency.Helper;
using Tour_agency.Models;

namespace Tour_agency.Frame
{
    /// <summary>
    /// Interaction logic for Costomers.xaml
    /// </summary>
    public partial class CostomersPage : Page
    {
        static ObservableCollection<Client> ClientCollection = new ObservableCollection<Client>();
        ClientHelper clientHelper = new ClientHelper();
        TourHelper tourHelper = new TourHelper();
        static FirebaseClient client = new FirebaseClient("https://traver-agency.firebaseio.com/");
        static bool executed = true;
        List<Tour> ToursList;
        public CostomersPage()
        {
            InitializeComponent();
            if (executed)
            {
                GetClient();
                executed = false;
            }

            DataGrid.ItemsSource = ClientCollection;
        }

        void GetClient()
        {
            ClientCollection = client
            .Child("Client")
            .AsObservable<Client>().ObserveOnDispatcher().AsObservableCollection();
        }
        async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ToursList = new List<Tour>();
            ToursList = await tourHelper.GetTourIdsAndNames();

        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            Edit();
        }
        private void Edit()
        {
            Client specificclient = DataGrid.SelectedItem as Client;
            Costomers costomers = new Costomers(specificclient); ;
            costomers.Show();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Costomers c = new Costomers(null, in ToursList);
            c.ShowDialog();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteClient();
        }
        async void DeleteClient()
        {
            MessageBoxResult res = MessageBox.Show("Видалити дані про клієнта?", "Видалення Клієнта", MessageBoxButton.YesNo);
            switch (res)
            {
                case MessageBoxResult.Yes:
                    if (DataGrid.SelectedItem != null)
                    {
                        Client toDeleteWorker = DataGrid.SelectedItem as Client;
                        await clientHelper.DeleteClient(toDeleteWorker.id);
                    }
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                ButtonDelete.IsEnabled = true;
                ButtonEdit.IsEnabled = true;
            }
            else
            {
                ButtonDelete.IsEnabled = false;
                ButtonEdit.IsEnabled = false;
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchByCombobox.SelectedIndex == 0)
            {
                var SearchedList = (from client in ClientCollection
                                    where client.name.ToLower().StartsWith(SearchTextBox.Text.ToLower())
                                    select client).ToList();
                DataGrid.ItemsSource = SearchedList;
            }
            if (SearchByCombobox.SelectedIndex == 1)
            {
                var SearchedList = (from worker in ClientCollection
                                    where worker.surname.ToLower().StartsWith(SearchTextBox.Text.ToLower())
                                    select worker).ToList();
                DataGrid.ItemsSource = SearchedList;
            }
            if (SearchByCombobox.SelectedIndex == 2)
            {
                var SearchedList = (from worker in ClientCollection
                                    where worker.patronymic.ToLower().StartsWith(SearchTextBox.Text.ToLower())
                                    select worker).ToList();
                DataGrid.ItemsSource = SearchedList;
            }
            if (SearchByCombobox.SelectedIndex == 3)
            {
                var SearchedList = (from worker in ClientCollection
                                    where worker.phone.ToLower().StartsWith(SearchTextBox.Text.ToLower())
                                    select worker).ToList();
                DataGrid.ItemsSource = SearchedList;
            }
            if (SearchByCombobox.SelectedIndex == 4)
            {
                var SearchedList = (from worker in ClientCollection
                                    where worker.dateOFDeparture.ToLower().StartsWith(SearchTextBox.Text.ToLower())
                                    select worker).ToList();
                DataGrid.ItemsSource = SearchedList;
            }
            if (SearchByCombobox.SelectedIndex == 5)
            {
                var SearchedList = (from worker in ClientCollection
                                    where worker.returnDate.ToLower().StartsWith(SearchTextBox.Text.ToLower())
                                    select worker).ToList();
                DataGrid.ItemsSource = SearchedList;
            }
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var clientList = ClientCollection;
            if (DataGrid.SelectedIndex >= 0 && DataGrid.SelectedIndex < clientList.Count)
            {
                Client specificClient = DataGrid.SelectedItem as Client;
                Costomers client = new Costomers(specificClient, in ToursList);
                client.ShowDialog();
            }
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Edit();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteClient();
        }

    }
}
