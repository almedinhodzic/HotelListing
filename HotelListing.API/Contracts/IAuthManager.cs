﻿using HotelListing.API.Data;
using HotelListing.API.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto);
        Task<AuthResponseDto?> Login(LoginDto loginDto);
        Task<string?> CreateRefreshToken(ApiUser user);
        Task<AuthResponseDto?> VerifyRefreshToken(AuthResponseDto request);
    }
}
