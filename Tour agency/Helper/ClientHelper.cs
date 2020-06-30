using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour_agency.Models;

namespace Tour_agency.Helper
{
    public class ClientHelper
    {
        FirebaseClient client = new FirebaseClient("https://traver-agency.firebaseio.com/");
        public async Task<List<Client>> GetAllClientsAsync()
        {
            return (await client
                .Child("Client")
                .OnceAsync<Client>()).Select(item => new Client
                {
                    id = item.Object.id,
                    name = item.Object.name,
                    surname = item.Object.surname,
                    patronymic = item.Object.patronymic,
                    phone = item.Object.phone,
                    idTour = item.Object.idTour,
                    returnDate = item.Object.returnDate,
                    dateOFDeparture = item.Object.dateOFDeparture
                }).ToList();
        }

        public async Task AddClient(string Name, string Surname, string Patronymic, string Phone, string IdTour, string DateOfDeparture, string ReturnDate)
        {
            await client
                .Child("Client")
                .PostAsync(new Client()
                {
                    id = GetRandomId(),
                    name = Name,
                    surname = Surname,
                    patronymic = Patronymic,
                    phone = Phone,
                    idTour = IdTour,
                    dateOFDeparture = DateOfDeparture,
                    returnDate = ReturnDate
                });
        }

        public async Task UpdateClient(string ID, string Name, string Surname, string Patronymic, string Phone, string IdTour, string DateOfDeparture, string ReturnDate)
        {
            var toUpdateClient = (await client
                .Child("Client")
                .OnceAsync<Client>()).Where(a => a.Object.id == ID).FirstOrDefault();

            await client
                .Child("Client")
                .Child(toUpdateClient.Key)
                .PutAsync(new Client { id = ID, name = Name, surname = Surname, patronymic = Patronymic, phone = Phone, idTour = IdTour, dateOFDeparture = DateOfDeparture, returnDate = ReturnDate });
        }

        public async Task DeleteClient(string ID)
        {
            var toDeleteProduct = (await client
                .Child("Client")
                .OnceAsync<Client>()).Where(a => a.Object.id == ID).FirstOrDefault();
            await client.Child("Client").Child(toDeleteProduct.Key).DeleteAsync();
        }

        public async Task<Client> GetClient(string ID)
        {
            var allClients = await GetAllClientsAsync();
            await client
                .Child("client")
                .OnceAsync<Client>();

            return allClients.Where(c => c.id == ID).FirstOrDefault();
        }

        #region Random ID FOR Clients
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
