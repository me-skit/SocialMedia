using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly SocialMediaContext _context;
        private readonly DbSet<TEntity> _entity;

        public Repository(SocialMediaContext context)
        {
            _context = context;
            _entity = context.Set<TEntity>();
        }
        public async Task Add(TEntity entity)
        {
            _entity.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(TEntity entity)
        {
            _entity.Remove(entity);
            //await _context.SaveChangesAsync();
            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _entity.ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _entity.FindAsync(id);
        }

        public async Task<bool> Update(TEntity entity)
        {
            _entity.Update(entity);
            var rows = await _context.SaveChangesAsync();
            return rows > 0;
            //await _context.SaveChangesAsync();
        }
    }
}
