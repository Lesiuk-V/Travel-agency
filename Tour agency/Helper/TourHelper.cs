using Firebase.Database;
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
    /// <summary>
    /// Клас для роботи із бд Турів
    /// </summary>
    public class TourHelper : IHelper<Tour>
    {
        private readonly FirebaseClient tour = new FirebaseClient("https://traver-agency.firebaseio.com/");//поле для зв'язку з віддаленим сервером Firebase

        /// <summary>
        /// Метод для отримання всіх турів із бд
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        ///  Метод для отримання всіх id та імен туру
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        ///  Метод для отримання  назву туру по id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task<Tour> GetTourName(string ID)
        {
            var allTours = await GetTourIdsAndNames();
            await tour
                .Child("Tour")
                .OnceAsync<Tour>();

            return allTours.Where(t => t.Id == ID).FirstOrDefault();
        }
        /// <summary>
        /// Метод для отриманння всіх даних про тур за його ід
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task<Tour> GetTour(string ID)
        {
            var allTours = await GetAllAsync();
            await tour
                .Child("Tour")
                .OnceAsync<Tour>();

            return allTours.Where(t => t.Id == ID).FirstOrDefault();
        }
        /// <summary>
        /// Метод для додавання даних про тур в бд
        /// </summary>
        /// <param name="newTour"></param>
        /// <returns></returns>
        public async Task AddAsync(Tour newTour)
        {
            await tour
                .Child("Tour")
                .PostAsync(new Tour()
                {
                    Id = GetRandomId(),//отримання нового згенерованого ід
                    Name = newTour.Name,
                    Price = newTour.Price,
                    Country = newTour.Country,
                    Hotel = newTour.Hotel,
                    Description = newTour.Description,
                    Image = newTour.Image
                }) ;
        }

        /// <summary>
        /// Метод оновлення даних туру
        /// </summary>
        /// <param name="updateTour"></param>
        /// <returns></returns>
        public async Task UpdateAsync(Tour updateTour)
        {
            var toUpdateProduct = (await tour
                .Child("Tour")
                .OnceAsync<Tour>()).Where(a => a.Object.Id == updateTour.Id).FirstOrDefault(); //шукаємо тур за переданим в метод айді

            await tour
                .Child("Tour")
                .Child(toUpdateProduct.Key)//звертаємося до конкретного запису в сервері за ключем
                .PutAsync(new Tour { Id = updateTour.Id, Name = updateTour.Name, Price = updateTour.Price, Country = updateTour.Country, Hotel = updateTour.Hotel, Description = updateTour.Description, Image = updateTour.Image });
        }

        /// <summary>
        /// Метод видалення туру із бд за його ід
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task DeleteAsync(string ID)
        {
            var toDeleteProduct = (await tour
                .Child("Tour")
                .OnceAsync<Tour>()).Where(a => a.Object.Id == ID).FirstOrDefault();//шукаємо тур за переданим в метод ід
            await tour.Child("Tour").Child(toDeleteProduct.Key).DeleteAsync();
        }
        //Метод генерування нового айді
        #region Random ID FOR Tour
        private string GetRandomId()
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