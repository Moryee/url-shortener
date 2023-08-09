# Url Shortener
This url shortener fullstack app allows you to shorten your url address. All CRUD operations created for ShortUrls. There are also Login and Register endpoints but only on backend.

## Url shortener algorithm
* When user creating short url frontend checks if url is unique using custom validator
```ts
angularapp/src/app/components/short-urls-create/short-urls-create.component.ts

  createUniqueShortUrlValidator() {
    return (control: AbstractControl) => {
      let value = this.shortUrlForm.controls['shortenedUrl'].value
      if (value && value.length > 0) {
        this.shortUrlsService.getShortUrlByShortUrl(value).subscribe(response => {
          console.log(response);
          if (response) {
            this.shortUrlForm.controls['shortenedUrl'].setErrors({ 'unique_error': 'Short url must be unique' });
          }
        });
      }
    }
  }
```
* After submitting frontend sends create request to backend and creates it if it's unique

```c#
webapi/Controllers/ShortUrlsController.cs

    [HttpPost]
    public async Task<IActionResult> CreateShortUrl(ShortUrlCreateDto shortUrlCreateDto)
    {
        var shortUrlModel = _mapper.Map<ShortUrl>(shortUrlCreateDto);

        if (await _repository.IsShortenedUrlExist(shortUrlModel))
        {
            return BadRequest("This short url already exist");
        }

        await _repository.CreateShortUrl(shortUrlModel);
        await _repository.SaveChanges();

        var shortUrlReadDto = _mapper.Map<ShortUrlReadDto>(shortUrlModel);
        return CreatedAtAction(nameof(GetShortUrlById), new { id = shortUrlReadDto.Id }, shortUrlReadDto);
    }
```

* Then user can go to his short url by entering https://localhost:4200/short/{short url}

## Authorization
The implementation of JWT authentication exists only on the backend.
There is claims to identify if user is admin. They are used in the creation of a jwt token

```c#
webapi/Identity/IdentityData.cs

    public class IdentityData
    {
        public const string SuperUserClaimName = "superUser";
        public const string SuperUserPolicyName = "SuperUser";

        public const string EmailClaimName = "email";
        public const string EmailPolicyName = "Email";
    }
```

Also user model has `IsSuperUser` field.

```c#
webapi/Models/Entities/User.cs

    [Required]
    public bool IsSuperUser { get; set; }
```

And when the user wants to log in, the system checks if the password is correct and returns a jwt token.

```c#
webapi/Controllers/UsersController.cs
    
    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> LoginUser(UserLoginDto userLoginDto)
    {
        var userModel = await _repository.GetUserByEmail(userLoginDto.Email);

        if (userModel == null)
        {
            return NotFound();
        }

        if (userModel.Password != userLoginDto.Password)
        {
            return BadRequest("Wrong password");
        }

        var secretToken = _configuration.GetSection("JwtSettings:Key").ToString();
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretToken));

        var issuer = _configuration.GetSection("JwtSettings:Issuer").ToString();
        var audience = _configuration.GetSection("JwtSettings:Audience").ToString();

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(IdentityData.SuperUserClaimName, userModel.IsSuperUser.ToString(), ClaimValueTypes.Boolean),
                new Claim(IdentityData.EmailClaimName, userModel.Email, ClaimValueTypes.String),
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var data = new { accessToken = tokenHandler.WriteToken(token) };
        return Ok(data);
    }
```

## Unit tests
Unit tests are made only for ShortUrlsController. I used `FakeItEasy` and `FluentAssertion` packages. Here is one of the 6 tests
```c#
webapi.tests/Controller/ShortUrlsControllerTests.cs

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
```
