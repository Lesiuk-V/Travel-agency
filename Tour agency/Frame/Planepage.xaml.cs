using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for Planepage.xaml
    /// </summary>
    public partial class Planepage : Page
    {
        private static ObservableCollection<Tour> tourCollection = new ObservableCollection<Tour>();
        private readonly TourHelper tourHelper = new TourHelper();
        private static readonly FirebaseClient tour = new FirebaseClient("https://traver-agency.firebaseio.com/");
        private static bool executed = true;
        public Planepage()
        {
            InitializeComponent();
            if (executed)
            {
                GetTour();
                executed = false;
            }

            DataGrid.ItemsSource = tourCollection;
        }

        void GetTour()
        {
            tourCollection = tour
            .Child("Tour")
            .AsObservable<Tour>().ObserveOnDispatcher().AsObservableCollection();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Travel t = new Travel();
            t.ShowDialog();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e) => Edit();

        private void Edit()
        {
            Tour specificTour = DataGrid.SelectedItem as Tour;
            Travel costomers = new Travel(specificTour); ;
            costomers.Show();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteTour();
        }

        async void DeleteTour()
        {
            MessageBoxResult res = MessageBox.Show("Видалити дані про тур?", "Видалення туру", MessageBoxButton.YesNo);
            switch (res)
            {
                case MessageBoxResult.Yes:
                    if (DataGrid.SelectedItem != null)
                    {
                        Tour toDeleteTour = DataGrid.SelectedItem as Tour;
                        await tourHelper.DeleteAsync(toDeleteTour.Id);
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

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var tourList = tourCollection;
            if (DataGrid.SelectedIndex >= 0 && DataGrid.SelectedIndex < tourList.Count)
            {
                Tour specificTour = DataGrid.SelectedItem as Tour;
                Travel tour = new Travel(specificTour);
                tour.ShowDialog();
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchByCombobox.SelectedIndex == 0)
            {
                var SearchedList = (from tour in tourCollection
                                    where tour.Name.ToLower().StartsWith(SearchTextBox.Text.ToLower())
                                    select tour).ToList();
                DataGrid.ItemsSource = SearchedList;
            }
            if (SearchByCombobox.SelectedIndex == 1)
            {
                var SearchedList = (from tour in tourCollection
                                    where tour.Price.ToLower().StartsWith(SearchTextBox.Text.ToLower())
                                    select tour).ToList();
                DataGrid.ItemsSource = SearchedList;
            }
            if (SearchByCombobox.SelectedIndex == 2)
            {
                var SearchedList = (from tour in tourCollection
                                    where tour.Country.ToLower().StartsWith(SearchTextBox.Text.ToLower())
                                    select tour).ToList();
                DataGrid.ItemsSource = SearchedList;
            }
            if (SearchByCombobox.SelectedIndex == 3)
            {
                var SearchedList = (from tour in tourCollection
                                    where tour.Hotel.ToLower().StartsWith(SearchTextBox.Text.ToLower())
                                    select tour).ToList();
                DataGrid.ItemsSource = SearchedList;
            }
        }

        private void SearchByCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
        private void Edit_Click(object sender, RoutedEventArgs e) => Edit();
        private void Delete_Click(object sender, RoutedEventArgs e) => DeleteTour();

        private void ButtonExport_Click(object sender, RoutedEventArgs e)
        {
            TextWriter tw = new StreamWriter("TourList.txt");

            foreach (Tour s in tourCollection)
                tw.WriteLine("Tour name: " + s.Name + " Price: " + s.Price + " Country: " + s.Country + " Hotel: " + s.Hotel + "Description: " + s.Description);

            tw.Close();
            MessageBox.Show("Дані успішно збережені");
        }
    }
}
