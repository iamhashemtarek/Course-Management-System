using CourseManagement.BLL.Interfaces;
using CourseManagement.DAL.Data;
using CourseManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly CourseManagementDbContext _courseManagementDbContext;

        public GenericRepository(CourseManagementDbContext courseManagementDbContext)
        {
            _courseManagementDbContext = courseManagementDbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _courseManagementDbContext.Set<T>().AddAsync(entity);
            await _courseManagementDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _courseManagementDbContext.Set<T>().Update(entity);
            await _courseManagementDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _courseManagementDbContext.Set<T>().Remove(entity);
            await _courseManagementDbContext.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _courseManagementDbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.FromResult(_courseManagementDbContext.Set<T>().AsEnumerable());
        }

    }
}
