using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Dtos;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;

        public PostsController(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _repository.GetPost();
            return Ok(_mapper.Map<IEnumerable<PostDto>>(posts));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _repository.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PostDto>(post));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            await _repository.CreatePost(post);
            //var newPostDto = _mapper.Map<Post>(postDto);
            return Ok(post);
        }
    }
}
