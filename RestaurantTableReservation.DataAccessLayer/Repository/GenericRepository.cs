using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RestaurantTableReservation.DataAccessLayer.Repository.IGenericRepository;

namespace RestaurantTableReservation.DataAccessLayer.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private ApplicationDbContext _context = null;
        private DbSet<T> table = null;
    
        public GenericRepository(ApplicationDbContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await table.ToListAsync();
        }
        
        public async Task<T> GetById(object id)
        {
            return await table.FindAsync(id);
        }
        
        public async Task Insert(T obj)
        {
           await table.AddAsync(obj);
        }
        public async Task Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public async Task Delete(object id)
        {
            T existing = await table.FindAsync(id);
            table.Remove(existing);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
