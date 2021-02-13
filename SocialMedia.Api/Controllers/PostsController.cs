﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Responses;
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
        private readonly IPostService _service;
        private readonly IMapper _mapper;

        public PostsController(IPostService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _service.GetPost();
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
            //return Ok(postDto);
            var response = new ApiResponse<IEnumerable<PostDto>>(postsDto);
            return Ok(response);
        }

        //[HttpGet("{id}, Name=GetPostById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _service.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }

            var postDto = _mapper.Map<PostDto>(post);
            //return Ok(postDto);

            var response = new ApiResponse<PostDto>(postDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            await _service.CreatePost(post);

            var newPostDto = _mapper.Map<PostDto>(post);
            //return CreatedAtRoute(nameof(GetPostById), new {Id = newPostDto.PostId }, newPostDto);
            var response = new ApiResponse<PostDto>(newPostDto);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, PostDto postDto)
        {
            var post = await _service.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }

            _mapper.Map(postDto, post);
            var result = await _service.UpdatePost(post);
            //return NoContent();
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _service.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }

            var result = await _service.DeletePost(post);
            //return NoContent();
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
