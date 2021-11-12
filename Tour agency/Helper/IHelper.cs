using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tour_agency.Helper
{
    /// <summary>
    /// Інтерфейс для роботи із бд
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHelper<T>
    {
        /// <summary>
        /// Метод для отримання всіх записів із бд
        /// </summary>
        /// <returns></returns>
        public Task<List<T>> GetAllAsync();
        /// <summary>
        ///  Метод для додавання запису в бд
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Task AddAsync(T t);
        /// <summary>
        /// Метод для оновлення даних 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Task UpdateAsync(T t);
        /// <summary>
        /// Метод для видалення запису по id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Task DeleteAsync(string ID);
    }
}
