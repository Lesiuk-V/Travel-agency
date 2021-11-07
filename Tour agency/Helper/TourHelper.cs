﻿using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour_agency.Models;

namespace Tour_agency.Helper
{
    public class TourHelper : IHelper<Tour>
    {
        FirebaseClient tour = new FirebaseClient("https://traver-agency.firebaseio.com/");//поле для зв'язку з віддаленим сервером Firebase

        public async Task<List<Tour>> GetAllAsync()
        {
            return (await tour
                .Child("Tour")
                .OnceAsync<Tour>()).Select(item => new Tour
                {
                    Id = item.Object.Id,
                    Name = item.Object.Name,
                    Price = item.Object.Price,
                    Country = item.Object.Country,
                    Hotel = item.Object.Hotel,
                    Description = item.Object.Description,
                    Image = item.Object.Image
                }).ToList();
        }

        public async Task<List<Tour>> GetTourIdsAndNames()
        {
            return (await tour
                .Child("Tour")
                .OnceAsync<Tour>()).Select(item => new Tour
                {
                    Id = item.Object.Id,
                    Name = item.Object.Name
             
                }).ToList();
        }

        public async Task<Tour> GetTourIdAndName(string ID)
        {
            var allTours = await GetTourIdsAndNames();
            await tour
                .Child("Tour")
                .OnceAsync<Tour>();

            return allTours.Where(t => t.Id == ID).FirstOrDefault();
        }

        public async Task<Tour> GetTour(string ID)
        {
            var allTours = await GetAllAsync();
            await tour
                .Child("Tour")
                .OnceAsync<Tour>();

            return allTours.Where(t => t.Id == ID).FirstOrDefault();
        }
        public async Task AddAsync(Tour newTour)
        {
            await tour
                .Child("Tour")
                .PostAsync(new Tour()
                {
                    Id = GetRandomId(),//отримання нового згенерованого айді
                    Name = newTour.Name,
                    Price = newTour.Price,
                    Country = newTour.Country,
                    Hotel = newTour.Hotel,
                    Description = newTour.Description,
                    Image = newTour.Image
                }) ;
        }

        //Метод оновлення даних конкретного продукту
        public async Task UpdateAsync(Tour updateTour)
        {
            var toUpdateProduct = (await tour
                .Child("Tour")
                .OnceAsync<Tour>()).Where(a => a.Object.Id == updateTour.Id).FirstOrDefault(); //шукаємо продукт за переданим в метод айді

            await tour
                .Child("Tour")
                .Child(toUpdateProduct.Key)//звертаємося до конкретного запису в сервері за ключем
                .PutAsync(new Tour { Id = updateTour.Id, Name = updateTour.Name, Price = updateTour.Price, Country = updateTour.Country, Hotel = updateTour.Hotel, Description = updateTour.Description, Image = updateTour.Image });
        }

        //Метод видалення конкретного продукту
        public async Task DeleteAsync(string ID)
        {
            var toDeleteProduct = (await tour
                .Child("Tour")
                .OnceAsync<Tour>()).Where(a => a.Object.Id == ID).FirstOrDefault();//шукаємо продукт за переданим в метод айді
            await tour.Child("Tour").Child(toDeleteProduct.Key).DeleteAsync();
        }
        //Метод генерування нового айді
        #region Random ID FOR Tour
        string GetRandomId()
        {
            Random rnd = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string x = new string(Enumerable.Repeat(chars, 4)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());
            const string nums = "123456789";
            string y = new string(Enumerable.Repeat(nums, 4)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());

            string sDate = DateTime.Now.ToString();
            DateTime value = (Convert.ToDateTime(sDate.ToString()));

            return x + y +
                value.Day.ToString() +
                value.Month.ToString() +
                value.Year.ToString() +
                value.Minute.ToString() +
                value.Hour.ToString() +
                value.Second.ToString() +
                "P";
        }

        
        #endregion
    }
}