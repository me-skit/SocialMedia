using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitofwork;

        public PostService(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public async Task CreatePost(Post post)
        {
            var user = await _unitofwork.UserRepository.GetById(post.UserId);
            if (user == null)
            {
                throw new Exception("User doesn't exist");
            }

            if (post.Description.Contains("terrorism"))
            {
                throw new Exception("Content not allowed");
            }

            await _unitofwork.PostRepository.Add(post);
        }

        public async Task<bool> DeletePost(Post post)
        {
            return await _unitofwork.PostRepository.Delete(post);
        }

        public async Task<IEnumerable<Post>> GetPost()
        {
            return await _unitofwork.PostRepository.GetAll();
        }

        public async Task<Post> GetPostById(int id)
        {
            return await _unitofwork.PostRepository.GetById(id);
        }

        public async Task<bool> UpdatePost(Post post)
        {
            return await _unitofwork.PostRepository.Update(post);
        }
    }
}
