using HW_16_09_21.Context;
using HW_16_09_21.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW_16_09_21
{
    class Program
    {
        static async Task Main(string[] args)
        {
            
            Console.WriteLine("Выберите команду - \n1: Добавить пользователья" +
                "\n2: Посмотреть список пользователей" +
                "\n3: Найти пользователья по ID" +
                "\n4: Обновить данные пользователья по ID" +
                "\n5: Удалить пользователья(по ID)");
            switch (Console.ReadLine())
            {
                case "1":
                    {
                        var createUserResult = await CreateUserAsync(new Users() { LastName = "Iqbol", FirstName = "Hasanzoda", MiddleName = "Umedjon", BirthDate = new DateTime(1999, 5, 24), CreatedAt = DateTime.Now });
                        if (!createUserResult)
                        {
                            Console.WriteLine("User not Created!!!");
                        }
                    }
                    break;
                case "2":
                    {
                        var selectUsersResult = await SelectUsersAsync();
                        foreach (var u in selectUsersResult)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"ID: {u.Id} - {u.LastName}, {u.FirstName}, {u.MiddleName} - {u.BirthDate}, {u.CreatedAt}");
                        }
                        if (selectUsersResult.Count == 0)
                        {
                            Console.WriteLine("Users not found");
                        }
                    }break;
                case "3":
                    {
                        Console.WriteLine("Введите ID: "); var a = int.Parse(Console.ReadLine());
                        var selectUserById = await SelectUserByIdAsync(a);
                        if (selectUserById == null)
                        {
                            Console.WriteLine("User not found!!!");
                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"ID: {selectUserById.Id} - {selectUserById.LastName}, {selectUserById.FirstName}, {selectUserById.MiddleName} - {selectUserById.BirthDate}, {selectUserById.CreatedAt}");
                        
                    }break;
                case "4":
                    {
                        Console.WriteLine("Введите ID: "); var a = int.Parse(Console.ReadLine());
                        var updateUserById = await UpdateUserByIdAsync(a, new Users() { LastName = "Aziz", FirstName = "Bobokhujaev", MiddleName = "F", BirthDate = new DateTime(1999, 5, 14), CreatedAt = DateTime.Now });
                        if (!updateUserById)
                        {
                            Console.WriteLine("User not updated");
                        }
                    }break;
                case "5":
                    {
                        Console.WriteLine("Введите ID: "); var a = int.Parse(Console.ReadLine());
                        var removeUserResult = await RemoveUserByIdAsync(a);
                        if (!removeUserResult)
                        {
                            Console.WriteLine("User not deleted");
                        }
                    }
                    break;
                default:
                    break;
            }
           
            Console.ReadLine();
        }

        private static async Task<bool> RemoveUserByIdAsync(int id)
        {
            using var usersDbContext = new UsersDbContext();
            var deleteUserResult = await usersDbContext.Users.FindAsync(id);

            if (deleteUserResult == null)
            {
                return false;
            }
            usersDbContext.Remove(deleteUserResult);
            var result = await usersDbContext.SaveChangesAsync();
            return result > 0;

        }

        private static async Task<bool> UpdateUserByIdAsync(int id, Users users)
        {
            using var usersDbContext = new UsersDbContext();
            var updateUserResult = await usersDbContext.Users.FindAsync(id);
            if (updateUserResult == null)
            {
                return false;
            }
            updateUserResult.LastName = users.LastName;
            updateUserResult.FirstName = users.FirstName;
            updateUserResult.MiddleName = users.MiddleName;
            updateUserResult.BirthDate = users.BirthDate;
            updateUserResult.CreatedAt = users.CreatedAt;

            var saveChanges = await usersDbContext.SaveChangesAsync();
            return saveChanges > 0;
        }

        private static async Task<Users> SelectUserByIdAsync(int id)
        {
            using var usersDbContext = new UsersDbContext();
            var user = await usersDbContext.Users.FindAsync(id);
            return user;
        }

        private static async Task<List<Users>> SelectUsersAsync()
        {
            using var usersDbContext = new UsersDbContext();
            var users = await usersDbContext.Users.ToListAsync();
            return users;
        }

        private static async Task<bool> CreateUserAsync(Users users)
        {
            using var usersDbContext = new UsersDbContext();
            usersDbContext.Users.Add(users);

            var result = await usersDbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
