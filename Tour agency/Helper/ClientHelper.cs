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
    public class ClientHelper : IHelper<Client>
    {
        FirebaseClient client = new FirebaseClient("https://traver-agency.firebaseio.com/");

        public async Task<List<Client>> GetAllAsync()
        {
            return (await client
                .Child("Client")
                .OnceAsync<Client>()).Select(item => new Client
                {
                    Id = item.Object.Id,
                    Name = item.Object.Name,
                    Surname = item.Object.Surname,
                    Patronymic = item.Object.Patronymic,
                    Phone = item.Object.Phone,
                    IdTour = item.Object.IdTour,
                    ReturnDate = item.Object.ReturnDate,
                    DateOFDeparture = item.Object.DateOFDeparture
                }).ToList();
        }

        public async Task AddAsync(Client newClient)
        {
            await client
                .Child("Client")
                .PostAsync(new Client()
                {
                    Id = GetRandomId(),
                    Name = newClient.Name,
                    Surname = newClient.Surname,
                    Patronymic = newClient.Patronymic,
                    Phone = newClient.Phone,
                    IdTour = newClient.IdTour,
                    DateOFDeparture = newClient.DateOFDeparture,
                    ReturnDate = newClient.ReturnDate
                });
        }
        public async Task UpdateAsync(Client updateClient)
        {
            var toUpdateClient = (await client
                .Child("Client")
                .OnceAsync<Client>()).Where(a => a.Object.Id == updateClient.Id).FirstOrDefault();

            await client
                .Child("Client")
                .Child(toUpdateClient.Key)
                .PutAsync(new Client { Id = updateClient.Id, Name = updateClient.Name, Surname = updateClient.Surname,
                 Patronymic = updateClient.Patronymic, Phone = updateClient.Phone, IdTour = updateClient.IdTour, DateOFDeparture = updateClient.DateOFDeparture,
                 ReturnDate = updateClient.ReturnDate});
        }

        public async Task DeleteAsync(string ID)
        {
            var toDeleteProduct = (await client
                .Child("Client")
                .OnceAsync<Client>()).Where(a => a.Object.Id == ID).FirstOrDefault();
            await client.Child("Client").Child(toDeleteProduct.Key).DeleteAsync();
        }

        public async Task<Client> GetClient(string ID)
        {
            var allClients = await GetAllAsync();
            await client
                .Child("client")
                .OnceAsync<Client>();

            return allClients.Where(c => c.Id == ID).FirstOrDefault();
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
