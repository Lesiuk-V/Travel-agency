using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

            ClientsDataGrid.ItemsSource = ClientCollection;
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

        private void ButtonEdit_Click(object sender, RoutedEventArgs e) => Edit();
        private void Edit()
        {
            Client specificclient = ClientsDataGrid.SelectedItem as Client;
            Costomers costomers = new Costomers(specificclient); ;
            costomers.Show();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Costomers c = new Costomers(null, in ToursList);
            c.ShowDialog();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e) => DeleteClient();
        async void DeleteClient()
        {
            MessageBoxResult res = MessageBox.Show("Видалити дані про клієнта?", "Видалення Клієнта", MessageBoxButton.YesNo);
            switch (res)
            {
                case MessageBoxResult.Yes:
                    if (ClientsDataGrid.SelectedItem != null)
                    {
                        Client toDeleteWorker = ClientsDataGrid.SelectedItem as Client;
                        await clientHelper.DeleteAsync(toDeleteWorker.id);
                    }
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (ClientsDataGrid.SelectedItem != null)
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
                ClientsDataGrid.ItemsSource = SearchedList;
            }
            if (SearchByCombobox.SelectedIndex == 1)
            {
                var SearchedList = (from worker in ClientCollection
                                    where worker.surname.ToLower().StartsWith(SearchTextBox.Text.ToLower())
                                    select worker).ToList();
                ClientsDataGrid.ItemsSource = SearchedList;
            }
            if (SearchByCombobox.SelectedIndex == 2)
            {
                var SearchedList = (from worker in ClientCollection
                                    where worker.patronymic.ToLower().StartsWith(SearchTextBox.Text.ToLower())
                                    select worker).ToList();
                ClientsDataGrid.ItemsSource = SearchedList;
            }
            if (SearchByCombobox.SelectedIndex == 3)
            {
                var SearchedList = (from worker in ClientCollection
                                    where worker.phone.ToLower().StartsWith(SearchTextBox.Text.ToLower())
                                    select worker).ToList();
                ClientsDataGrid.ItemsSource = SearchedList;
            }
            if (SearchByCombobox.SelectedIndex == 4)
            {
                var SearchedList = (from worker in ClientCollection
                                    where worker.dateOFDeparture.ToLower().StartsWith(SearchTextBox.Text.ToLower())
                                    select worker).ToList();
                ClientsDataGrid.ItemsSource = SearchedList;
            }
            if (SearchByCombobox.SelectedIndex == 5)
            {
                var SearchedList = (from worker in ClientCollection
                                    where worker.returnDate.ToLower().StartsWith(SearchTextBox.Text.ToLower())
                                    select worker).ToList();
                ClientsDataGrid.ItemsSource = SearchedList;
            }
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var clientList = ClientCollection;
            if (ClientsDataGrid.SelectedIndex >= 0 && ClientsDataGrid.SelectedIndex < clientList.Count)
            {
                Client specificClient = ClientsDataGrid.SelectedItem as Client;
                Costomers client = new Costomers(specificClient, in ToursList);
                client.ShowDialog();
            }
        }
        private void Edit_Click(object sender, RoutedEventArgs e) => Edit();
        private void Delete_Click(object sender, RoutedEventArgs e) =>  DeleteClient();




        private async void ButtonExport_Click(object sender, RoutedEventArgs e)
        {

            TextWriter tw = new StreamWriter("ClientList.txt");
            tw.WriteLine("Ім'я \t Прізвище \t По батькові \t Тур\n");
            foreach (Client s in ClientCollection)
                tw.WriteLine(s.name + '\t' + s.surname + '\t' + s.patronymic + '\t' + await tourHelper.GetTour(s.idTour));
            
            tw.Close();
            MessageBox.Show("Дані успішно збережені");
        }
    }
}
