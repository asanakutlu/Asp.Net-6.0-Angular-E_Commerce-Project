using Azure.Core;
using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.DTOs.User;
using E_CommerceAPI.Application.Exceptions;
using E_CommerceAPI.Application.Helpers;
using E_CommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using E_CommerceAPI.Application.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceAPI.Domain.Entities;
using System.Security.Cryptography;

namespace E_CommerceAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly IEndpointReadRepository _endpointReadRepository;


        public int TotalUsersCount => throw new NotImplementedException();

        public UserService(UserManager<AppUser> userManager, IEndpointReadRepository endpointReadRepository)
        {
            _userManager = userManager;
            _endpointReadRepository = endpointReadRepository;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                NameSurname = model.NameSurname,
                UserName = model.UserName,
                Email = model.Email
            }, model.Password);
            CreateUserResponse response = new() { Succeeded = result.Succeeded };
            if (result.Succeeded)
                response.Message = "kullanıcı başaralı oluştu";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code}-{error.Description}<br>";
            return response;
        }

        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAcsessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAcsessTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new NotFoundException();

        }
        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                //byte[] tokenBytes = WebEncoders.Base64UrlDecode(resetToken);
                //resetToken = Encoding.UTF8.GetString(tokenBytes);
                resetToken = resetToken.UrlDecode();
                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (result.Succeeded)
                    await _userManager.UpdateSecurityStampAsync(user);
                else
                    throw new PasswordChangeFailedExcepiton();

            }
        }

        public async Task<List<ListUser>> GetAllUsersAsync(int page, int size)
        {
            var users = await _userManager.Users.Skip(page * size).Take(size).ToListAsync();
            return users.Select(user=>new ListUser
            {
                Id=user.Id,
                Email=user.Email,
                NameSurname=user.NameSurname,
                TwoFactoryEnabled=user.TwoFactorEnabled,
                UserName=user.UserName
            }).ToList();    
        }

        public int TotalUserCount()
        {
           return _userManager.Users.Count();
        }

        public async Task AssignRoleToUserAsync(string userId, string[] roles)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if(user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);
                await _userManager.AddToRolesAsync(user, userRoles);
            }
        }

        public async Task<string[]> GetRolesToUserAsync(string userIdOrName)
        {
            AppUser user=await _userManager.FindByIdAsync(userIdOrName);
            if(user ==null)
                user=await _userManager.FindByNameAsync(userIdOrName);
            if(user!=null)
            {
               var userRoles=await _userManager.GetRolesAsync(user);
                return userRoles.ToArray();
            }
            return new string[] { };
        }

        public async Task<bool> HasRolePermissionToEndpointAsync(string name, string code)
        {
            var userRoles = await GetRolesToUserAsync(name);
            if(!userRoles.Any()) 
                return false;
           Endpoint? endpoint=await _endpointReadRepository.Table.Include(o=>o.Roles).FirstOrDefaultAsync(e=>e.Code == code);
            if (endpoint == null)
                return false;
            var hasRole = false;
            var endpointRoles = endpoint.Roles.Select(r => r.Name);
            //foreach(var userRole in userRoles)
            //{
            //    if (!hasRole)
            //    {

            //        foreach (var endpointRole in endpointRoles)
            //        {
            //            hasRole = true;
            //            break;
            //        }
            //    }
            //    else break;
            //}
            //return hasRole;
            foreach(var userRole in userRoles)
            {
                foreach (var endpointRole in endpointRoles)
                    if (userRole == endpointRole)
                        return true;
            }
            return hasRole;
        }

        //public int TotalUsersCount => _userManager.Users.Count();
    }
}
