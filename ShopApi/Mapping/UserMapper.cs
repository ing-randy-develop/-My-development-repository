using ShopApi.Context;
using ShopApi.Dtos;
using ShopApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Mapping
{
    public class UserMapper
    {
        private readonly ShopContext context;
        public UserMapper(ShopContext context)
        {
            this.context = context;
        }

        public static UserDto UserToDto(User entity)
        {
            var userDto = new UserDto()
            {
                username = entity.UserName,
                email = entity.Email,
                name = entity.Name,
                lastname = entity.Lastname,
                password = entity.PasswordHash 
            };
            return userDto;
        }
        public static User UserDtoToEntity(UserDto dto)
        {
            var user = new User()
            {
                Name = dto.name,
                Lastname = dto.lastname,
                UserName = dto.username,
                PasswordHash = dto.password,
                Email = dto.email

            };
            return user;
        }
    }
}

