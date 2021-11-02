using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tour_agency.Helper
{
    public interface IHelper<T>
    {
        public Task<List<T>> GetAllAsync();

        public Task AddAsync(T t);

        public Task UpdateAsync(T t);

        public Task DeleteAsync(string ID);
    }
}
