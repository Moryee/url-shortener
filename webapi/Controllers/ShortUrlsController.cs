﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using webapi.Data;
using webapi.Models.DTO;
using webapi.Models.Entities;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortUrlsController : ControllerBase
    {
        private readonly IShortUrlRepo _repository;
        private readonly IMapper _mapper;
        private readonly ShortUrlService _service;

        public ShortUrlsController(IShortUrlRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _service = new ShortUrlService(repository);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShortUrls()
        {
            var shortUrlsModel = await _repository.GetAllShortUrls();
            return Ok(_mapper.Map<IEnumerable<ShortUrlReadDto>>(shortUrlsModel));
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetShortUrlById")]
        public async Task<IActionResult> GetShortUrlById([FromRoute] Guid id)
        {
            var shortUrlModel = await _repository.GetShortUrlById(id);
            if (shortUrlModel != null)
            {
                return Ok(_mapper.Map<ShortUrlReadDto>(shortUrlModel));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateShortUrl(ShortUrlCreateDto shortUrlCreateDto)
        {
            var shortUrlModel = _mapper.Map<ShortUrl>(shortUrlCreateDto);

            var errors = await _service.ValidateShortenedUrl(shortUrlCreateDto.ShortenedUrl);
            if (errors.Capacity > 0)
            {
                return BadRequest(errors);
            }

            await _repository.CreateShortUrl(shortUrlModel);
            await _repository.SaveChanges();

            var shortUrlReadDto = _mapper.Map<ShortUrlReadDto>(shortUrlModel);
            return CreatedAtAction(nameof(GetShortUrlById), new { id = shortUrlReadDto.Id }, shortUrlReadDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateShortUrl([FromRoute] Guid id, ShortUrlUpdateDto shortUrlUpdateDto)
        {
            var shortUrlModel = await _repository.GetShortUrlById(id);

            if (shortUrlModel == null)
            {
                return NotFound();
            }

            var errors = await _service.ValidateShortenedUrl(shortUrlUpdateDto.ShortenedUrl);
            if (errors.Capacity > 0)
            {
                return BadRequest(errors);
            }

            _mapper.Map(shortUrlUpdateDto, shortUrlModel);
            await _repository.SaveChanges();

            var shortUrlReadDto = _mapper.Map<ShortUrlReadDto>(shortUrlModel);
            return Ok(shortUrlReadDto);
        }

        //[Authorize(Policy = IdentityData.SuperUserPolicyName)]
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteShortUrl([FromRoute] Guid id)
        {
            var shortUrlModel = await _repository.GetShortUrlById(id);

            if (shortUrlModel == null)
            {
                return NotFound();
            }

            await _repository.DeleteShortUrlById(id);
            await _repository.SaveChanges();

            var shortUrlReadDto = _mapper.Map<ShortUrlReadDto>(shortUrlModel);
            return Ok(shortUrlReadDto);
        }

        [HttpGet]
        [Route("Short/{url}")]
        public async Task<IActionResult> GetShortUrlByShortUrl([FromRoute] string url)
        {
            var shortUrlModel = await _repository.GetShortUrlByShortUrl(url);

            if (shortUrlModel == null)
            {
                return Ok();
            }

            var shortUrlReadDto = _mapper.Map<ShortUrlReadDto>(shortUrlModel);
            return Ok(shortUrlReadDto);
        }
    }
}
