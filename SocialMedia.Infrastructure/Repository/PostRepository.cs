using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly SocialMediaContext _context;

        public PostRepository(SocialMediaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Post>> GetPost()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<Post> GetPostById(int id)
        {
            return await _context.Posts.FirstOrDefaultAsync(item => item.PostId == id);
        }

        public async Task CreatePost(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePost(Post post)
        {
            //var currentPost = await GetPostById(post.PostId);
            //currentPost.Date = post.Date;
            //currentPost.Description = post.Description;
            //currentPost.Image = post.Image;

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> DeletePost(Post post)
        {
            _context.Posts.Remove(post);

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
