﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.API;

    [Route("api/users")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UsersController(
            IUserRepository userRepository,
            IMapper mapper)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            //var claims = User.Claims.ToList();

            var usersFromDb = await userRepository.GetUsersAsync();

            var usersForResult = mapper.Map<IEnumerable<UserDto>>(usersFromDb);

            return Ok(usersForResult);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(string id)
        {
            var userFromDb = await userRepository.GetUserAsync(id);

            if (userFromDb == null)
            {
                return NotFound();
            }

            var gamesUserFromDb = await userRepository.GetUserGamesAsync(userFromDb.Id);

            var userForResult = mapper.Map<UserDto>(userFromDb);
            userForResult.GamesLibrary = mapper.Map<ICollection<GameDto>>(gamesUserFromDb);

            return Ok(userForResult);
        }
    }
