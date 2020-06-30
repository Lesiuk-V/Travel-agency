using Firebase.Database;
using MaterialDesignThemes.Wpf;
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
using System.Windows.Shapes;
using Tour_agency.Frame;
using Tour_agency.Helper;
using Tour_agency.Models;

namespace Tour_agency.Aditional_windows
{
    /// <summary>
    /// Interaction logic for Costomers.xaml
    /// </summary>
    /// 
    
    public partial class Costomers : Window
    {
       
        string tourId;
        bool edited = true;
        ClientHelper clientHelper = new ClientHelper();
        Client Client = new Client();
        TourHelper tourHelper = new TourHelper();
        List<Tour> ToursList; 
        public Costomers(Client client = null, in List<Tour> toursList = null)
        {
            InitializeComponent();
            if (toursList != null)
                ToursList = toursList;

            getTours();
            Client = client;
            if (client == null)
            {
                Button_delete.Visibility = Visibility.Hidden;
                Client = new Client();
                edited = false;
            }
            DataContext = Client;
            FillTourComboBox();
           
        }

        void getTours()
        {
            if(ToursList != null)
            tour.ItemsSource = ToursList;
           
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private async void Button_add_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Close();
                if (edited == false)
                {
                    await clientHelper.AddClient(
                        name.Text,
                        surname.Text,
                        patronymic.Text,
                        phone.Text,
                        tourId, // !!!!!!!!!!!!!!!!
                        dateOfDeparture.Text,
                        returnDate.Text);
                }
                else if (edited == true)
                {
                    await clientHelper.UpdateClient(
                        Client.id,
                        name.Text,
                        surname.Text,
                        patronymic.Text,
                        phone.Text,
                        tourId, // !!!!!!!!!!!!!!!!
                        dateOfDeparture.Text,
                        returnDate.Text
                        );

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void Button_delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Видалити дані клієнта?", "Видалення клієнта", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (res)
            {
                case MessageBoxResult.Yes:
                    Close();
                    await clientHelper.DeleteClient(Client.id);
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        async void FillTourComboBox()
        {
           
            if (edited == true)
            {
                Tour specificTour = await tourHelper.GetTourIdAndName(Client.idTour);
                if (specificTour != null)
                {
                    string tourName = specificTour.name;
                   
                    tour.Text = tourName;
                }
            }
        }

        private void tour_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tour.SelectedItem != null)
            {
                Tour selectedTour = tour.SelectedItem as Tour;
                tourId = selectedTour.id;
            }
        }
    }
}
