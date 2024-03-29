﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tour_agency.Helper;
using Tour_agency.Models;

namespace Tour_agency.Aditional_windows
{
    /// <summary>
    /// Interaction logic for Travel.xaml
    /// </summary>
    public partial class Travel : Window
    {
        readonly bool edited = true;
        readonly TourHelper tourHelper = new TourHelper();
        readonly Tour Tour = new Tour();
        private byte[] tourImageToFirebase;
        public Travel(Tour tour = null)
        {
            InitializeComponent();
            Tour = tour;

            if (tour == null)
            {
                Button_delete.Visibility = Visibility.Hidden;
                Tour = new Tour();
                edited = false;
            }
            else
            {
                ConvertByteToImage();
            }
            DataContext = Tour;
        }

        private void Button_close_Click(object sender, RoutedEventArgs e) => Close();
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e) => DragMove();
        private void ConvertByteToImage()
        { 
            byte[] b = Convert.FromBase64String(Tour.Image);

            using (var ms = new MemoryStream(b))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; //Переводимо масив байтів в картинку
                image.StreamSource = ms;
                image.EndInit();

                TourImage.Source = image;
            }
            DataContext = Tour;
        }
        private async void Button_add_Click(object sender, RoutedEventArgs e)
        {
            bool close = true;
            if (name.Text != "" && price.Text != "" && country.Text !="" && hotel.Text != "" && description.Text != "")
            {
                Tour tour = new Tour
                {
                    Name = name.Text,
                    Price = price.Text,
                    Country = country.Text,
                    Hotel = hotel.Text,
                    Description = description.Text
                };


                if (edited == false)
                {
                    if (tourImageToFirebase != null)
                    {
                        tour.Image = Convert.ToBase64String(tourImageToFirebase);
                        await tourHelper.AddAsync(tour);
                        close = true;
                    }
                    else
                    {
                        MessageBox.Show("Виберіть зображення");
                        close = false;
                    }
                }

                else if (edited)
                {
                    Close();
                    if (tourImageToFirebase == null)
                    {

                        tour.Id = Tour.Id;
                        tour.Image = Tour.Image;
                        await tourHelper.UpdateAsync(tour);
                    }
                    else
                    {
                        tour.Id = Tour.Id;
                        tour.Image = Convert.ToBase64String(tourImageToFirebase);
                        await tourHelper.UpdateAsync(tour);
                    }
                }
                
            }
            else
            {
                MessageBox.Show("Заповніть всі поля");
                close = false;
            }
                
            if (close == true)
                Close();
        }

        private async void Button_delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Видалити дані туру?", "Видалення туру", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (res)
            {
                case MessageBoxResult.Yes:
                    Close();
                    await tourHelper.DeleteAsync(Tour.Id);
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void Addimage_Click(object sender, RoutedEventArgs e) => AddImageFromDialogWindow();

        private void TourImage_MouseDown(object sender, MouseButtonEventArgs e) => AddImageFromDialogWindow();

        void AddImageFromDialogWindow()
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Select Image",
                Filter = "JPG Files(*.jpg) | *.jpg"
            };



            if (ofd.ShowDialog() == true)
            {
                var img = new BitmapImage(new Uri(ofd.FileName));
                TourImage.Source = img;

                MemoryStream memStream = new MemoryStream();
                JpegBitmapEncoder encoder = new JpegBitmapEncoder(); //Закидаємо картинку в масив байтів і надсилаємо цей масив на сервер
                encoder.Frames.Add(BitmapFrame.Create(img));
                encoder.Save(memStream);
                tourImageToFirebase = memStream.ToArray();
            }

        }
    }      
}
  

