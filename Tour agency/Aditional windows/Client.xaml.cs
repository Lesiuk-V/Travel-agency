﻿using Firebase.Database;
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
    
    public partial class Customers : Window
    {
       
        private string tourId;
        private readonly bool edited = true;
        private readonly ClientHelper clientHelper = new ClientHelper();
        readonly Client Client = new Client();
        private readonly TourHelper tourHelper = new TourHelper();
        private readonly List<Tour> ToursList; 
        public Customers(Client client = null, in List<Tour> toursList = null)
        {
            InitializeComponent();
            if (toursList != null)
                ToursList = toursList;

            GetTours();
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

        private void GetTours()
        {
            if(ToursList != null)
            tour.ItemsSource = ToursList;
           
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) => Close();

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e) => DragMove();

        private async void Button_add_Click(object sender, RoutedEventArgs e)
        {
            
            if (name.Text != "" && surname.Text != "" && patronymic.Text != "" && phone.Text != "" && tourId != "" && dateOfDeparture.Text != null && returnDate.Text != null )
            {
                Client client = new Client
                {
                    Name = name.Text,
                    Surname = surname.Text,
                    Patronymic = patronymic.Text,
                    Phone = phone.Text,
                    IdTour = tourId,
                    DateOFDeparture = dateOfDeparture.Text,
                    ReturnDate = returnDate.Text
                };
                try
                {
                    if (edited == false)
                    {
                        await clientHelper.AddAsync(client);
                    }
                    else if (edited == true)
                    {
                        client.Id = Client.Id;
                        await clientHelper.UpdateAsync(client);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Close();
            }
            else
                MessageBox.Show("Заповніть всі поля");
        }

        private async void Button_delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Видалити дані клієнта?", "Видалення клієнта", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (res)
            {
                case MessageBoxResult.Yes:
                    Close();
                    await clientHelper.DeleteAsync(Client.Id);
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        async void FillTourComboBox()
        {
           
            if (edited == true)
            {
                Tour specificTour = await tourHelper.GetTourName(Client.IdTour);
                if (specificTour != null)
                {
                    string tourName = specificTour.Name;
                   
                    tour.Text = tourName;
                }
            }
        }

        private void Tour_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tour.SelectedItem != null)
            {
                Tour selectedTour = tour.SelectedItem as Tour;
                tourId = selectedTour.Id;
            }
        }
    }
}
