using Firebase.Database;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tour_agency.Helper;
using Tour_agency.Models;
using Tour_agency.Aditional_windows;
using System;
using System.IO;
using System.Drawing;

namespace Tour_agency.Frame
{
    /// <summary>
    /// Interaction logic for Travelpage.xaml
    /// </summary>
    /// 
   
    public partial class TravelPage : Page
    {
        TourHelper tourHelper = new TourHelper();
        static FirebaseClient tour = new FirebaseClient("https://traver-agency.firebaseio.com/");
        static bool executed = true;
        public Tour TourOne { get; set; }
        public List<Tour> toursList { get; set; }
        public TravelPage()
        {
            InitializeComponent();
        }

        private void Grid_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            toursList = await tourHelper.GetAllAsync();
            foreach(var t in toursList)
            {
                if (t.image != null)
                {
                    byte[] b = Convert.FromBase64String(t.image);

                    using (var ms = new MemoryStream(b))
                    {
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.CacheOption = BitmapCacheOption.OnLoad; // з кожного запису турів переводимо масив байтів в картинку і ..........
                        image.StreamSource = ms;
                        image.EndInit();

                        t.tourImage = image;// .............. Присвоюємо цю картинку локальному полю в класі Tour і здійснюємо байндинг до цього поля з елементу зображення
                    }
                }


            }

            ListViewTravel.ItemsSource = toursList;
        }
        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
