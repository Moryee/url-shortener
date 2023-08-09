using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Core;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using webapi.Controllers;
using webapi.Data;
using webapi.Models.DTO;
using webapi.Models.Entities;
using Xunit.Sdk;

namespace webapi.tests.Controller
{
    public class ShortUrlsControllerTests
    {
        private readonly IShortUrlRepo _repository;
        private readonly IMapper _mapper;

        public ShortUrlsControllerTests()
        {
            _repository = A.Fake<IShortUrlRepo>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public async void ShortUrlController_GetShortUrls_ReturnOK()
        {
            // Arrange
            var shortUrls = A.Fake<IEnumerable<ShortUrl>>();
            var shortUrlsReadDto = A.Fake<IEnumerable<ShortUrlReadDto>>();

            A.CallTo(() => _repository.GetAllShortUrls()).Returns(shortUrls);
            A.CallTo(() => _mapper.Map<IEnumerable<ShortUrlReadDto>>(shortUrls)).Returns(shortUrlsReadDto);
            var controller = new ShortUrlsController(_repository, _mapper);

            // Act
            var result = await controller.GetAllShortUrls();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void ShortUrlController_GetShortUrlById_ReturnOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            var shortUrl = A.Fake<ShortUrl>();
            var shortUrlReadDto = A.Fake<ShortUrlReadDto>();

            A.CallTo(() => _repository.GetShortUrlById(id)).Returns(shortUrl);
            A.CallTo(() => _mapper.Map<ShortUrlReadDto>(shortUrl)).Returns(shortUrlReadDto);
            var controller = new ShortUrlsController(_repository, _mapper);

            // Act
            var result = await controller.GetShortUrlById(id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void ShortUrlController_CreateShortUrl_ReturnOK()
        {
            // Arrange
            var shortUrlModel = A.Fake<ShortUrl>();
            var shortUrlCreateDto = A.Fake<ShortUrlCreateDto>();

            A.CallTo(() => _mapper.Map<ShortUrl>(shortUrlCreateDto)).Returns(shortUrlModel);
            A.CallTo(() => _repository.IsShortenedUrlExist(shortUrlModel)).Returns(false);
            A.CallTo(() => _repository.CreateShortUrl(shortUrlModel)).Returns(Task.CompletedTask);
            A.CallTo(() => _repository.SaveChanges()).Returns(true);
            var controller = new ShortUrlsController(_repository, _mapper);

            // Act
            var result = await controller.CreateShortUrl(shortUrlCreateDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(CreatedAtActionResult));
        }

        [Fact]
        public async void ShortUrlController_UpdateShortUrl_ReturnOK()
        {
            // Arrange
            var id = Guid.NewGuid();
            var shortUrlModel = A.Fake<ShortUrl>();
            var shortUrlUpdateDto = A.Fake<ShortUrlUpdateDto>();
            var shortUrlReadDto = A.Fake<ShortUrlReadDto>();

            A.CallTo(() => _repository.GetShortUrlById(id)).Returns(shortUrlModel);
            A.CallTo(() => _mapper.Map<ShortUrl>(shortUrlUpdateDto)).Returns(shortUrlModel);
            A.CallTo(() => _repository.IsShortenedUrlExist(shortUrlModel)).Returns(false);
            A.CallTo(() => _mapper.Map(shortUrlUpdateDto, shortUrlModel));
            A.CallTo(() => _repository.SaveChanges()).Returns(true);
            A.CallTo(() => _mapper.Map<ShortUrlReadDto>(shortUrlModel)).Returns(shortUrlReadDto);
            var controller = new ShortUrlsController(_repository, _mapper);

            // Act
            var result = await controller.UpdateShortUrl(id, shortUrlUpdateDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void ShortUrlController_DeleteShortUrl_ReturnOK()
        {
            // Arrange
            var id = Guid.NewGuid();
            var shortUrlModel = A.Fake<ShortUrl>();
            var shortUrlReadDto = A.Fake<ShortUrlReadDto>();

            A.CallTo(() => _repository.GetShortUrlById(id)).Returns(shortUrlModel);
            A.CallTo(() => _repository.DeleteShortUrlById(id)).Returns(Task.CompletedTask);
            A.CallTo(() => _repository.SaveChanges()).Returns(true);
            A.CallTo(() => _mapper.Map<ShortUrlReadDto>(shortUrlModel)).Returns(shortUrlReadDto);
            var controller = new ShortUrlsController(_repository, _mapper);

            // Act
            var result = await controller.DeleteShortUrl(id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void ShortUrlController_GetShortUrlByShortUrl_ReturnOK()
        {
            // Arrange
            var url = "test";
            var shortUrlModel = A.Fake<ShortUrl>();
            var shortUrlReadDto = A.Fake<ShortUrlReadDto>();

            A.CallTo(() => _repository.GetShortUrlByShortUrl(url)).Returns(shortUrlModel);
            A.CallTo(() => _mapper.Map<ShortUrlReadDto>(shortUrlModel)).Returns(shortUrlReadDto);
            var controller = new ShortUrlsController(_repository, _mapper);

            // Act
            var result = await controller.GetShortUrlByShortUrl(url);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
