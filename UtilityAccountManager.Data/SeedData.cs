using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UtilityAccountManager.Data.Models;

namespace UtilityAccountManager.Data;

public static class SeedData
{
    public static void EnsurePopulated(IApplicationBuilder app)
    {
        var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UtilityAccountContext>();
        bool wasEmpty = false;

        if (context.Database.GetPendingMigrations().Any())
            context.Database.Migrate();

        if (!context.Residents.Any())
        {
            wasEmpty = true;
            AddResidents(context);
            context.SaveChanges();
        }

        if (!context.Addresses.Any())
        {
            wasEmpty = true;
            AddAddresses(context);
            context.SaveChanges();
        }

        if (!context.UtilityAccounts.Any())
        {
            wasEmpty = true;
            AddUtilityAccounts(context);
            context.SaveChanges();
        }

        if (wasEmpty)
            ConnectResidentsAndAccounts(context);

        context.SaveChanges();
    }

    private static void AddAddresses(UtilityAccountContext context)
    {
        context.Addresses.AddRange(
            new AddressModel { City = "Казань", Street = "Кирова", HouseNumber = "25", FlatNumber = 9, Zipcode = 420001 },
            new AddressModel { City = "Волгоград", Street = "Ленина", HouseNumber = "30", FlatNumber = 18, Zipcode = 400001 },
            new AddressModel { City = "Самара", Street = "Гагарина", HouseNumber = "35", FlatNumber = 6, Zipcode = 443001 },
            new AddressModel { City = "Ульяновск", Street = "Ломоносова", HouseNumber = "40", FlatNumber = 21, Zipcode = 432001 },
            new AddressModel { City = "Иркутск", Street = "Свердлова", HouseNumber = "45", FlatNumber = 16, Zipcode = 664001 },
            new AddressModel { City = "Хабаровск", Street = "Ленина", HouseNumber = "50", FlatNumber = 28, Zipcode = 680001 },
            new AddressModel { City = "Тюмень", Street = "Пушкина", HouseNumber = "55", FlatNumber = 13, Zipcode = 625001 },
            new AddressModel { City = "Саратов", Street = "Гагарина", HouseNumber = "60", FlatNumber = 5, Zipcode = 410001 },
            new AddressModel { City = "Владимир", Street = "Ленина", HouseNumber = "65", FlatNumber = 12, Zipcode = 600001 },
            new AddressModel { City = "Тольятти", Street = "Кирова", HouseNumber = "70", FlatNumber = 8, Zipcode = 445001 },
            new AddressModel { City = "Астрахань", Street = "Советский", HouseNumber = "75", FlatNumber = 17, Zipcode = 414001 },
            new AddressModel { City = "Кемерово", Street = "Мира", HouseNumber = "80", FlatNumber = 20, Zipcode = 650001 },
            new AddressModel { City = "Рязань", Street = "Ленина", HouseNumber = "85", FlatNumber = 4, Zipcode = 390001 },
            new AddressModel { City = "Липецк", Street = "Гагарина", HouseNumber = "90", FlatNumber = 9, Zipcode = 398001 },
            new AddressModel { City = "Тверь", Street = "Победы", HouseNumber = "95", FlatNumber = 14, Zipcode = 170001 }

            );
    }

    private static void ConnectResidentsAndAccounts(UtilityAccountContext context)
    {
        var residents = context.Residents;

        var utilityAccounts = context.UtilityAccounts.ToList();
        int utilityAccountsNumber = utilityAccounts.Count();

        foreach (var resident in residents)
        {
            var randomUtilAcc = utilityAccounts[new Random().Next(utilityAccountsNumber)];

            context.ResidentUtilityAccounts.Add(new ResidentUtilityAccountModel
            {
                ResidentId = resident.Id,
                Resident = resident,
                UtilityAccountNumber = randomUtilAcc.Id,
                UtilityAccount = randomUtilAcc
            });
        }
    }

    private static void AddResidents(UtilityAccountContext context)
    {
        context.Residents.AddRange(
            new ResidentModel { FirstName = "John", LastName = "Doe", Age = 30 },
            new ResidentModel { FirstName = "Jane", LastName = "Smith", Age = 25 },
            new ResidentModel { FirstName = "Иван", LastName = "Иванов", Patronymic = "Иванович", Age = 35 },
            new ResidentModel { FirstName = "Мария", LastName = "Петрова", Patronymic = "Ивановна", Age = 28 },
            new ResidentModel { FirstName = "Александр", LastName = "Смирнов", Patronymic = "Александрович", Age = 40 },
            new ResidentModel { FirstName = "Michael", LastName = "Johnson", Age = 45 },
            new ResidentModel { FirstName = "Emily", LastName = "Brown", Age = 32 },
            new ResidentModel { FirstName = "Ольга", LastName = "Новикова", Patronymic = "Александровна", Age = 50 },
            new ResidentModel { FirstName = "Сергей", LastName = "Кузнецов", Patronymic = "Иванович", Age = 27 },
            new ResidentModel { FirstName = "Анастасия", LastName = "Соловьева", Patronymic = "Петровна", Age = 34 },
            new ResidentModel { FirstName = "Ирина", LastName = "Смирнова", Patronymic = "Андреевна", Age = 45 },
            new ResidentModel { FirstName = "Павел", LastName = "Волков", Patronymic = "Сергеевич", Age = 32 },
            new ResidentModel { FirstName = "Наталья", LastName = "Козлова", Patronymic = "Владимировна", Age = 28 },
            new ResidentModel { FirstName = "Андрей", LastName = "Павлов", Patronymic = "Игоревич", Age = 35 },
            new ResidentModel { FirstName = "Екатерина", LastName = "Никитина", Patronymic = "Алексеевна", Age = 40 },
            new ResidentModel { FirstName = "Максим", LastName = "Соколов", Patronymic = "Дмитриевич", Age = 25 },
            new ResidentModel { FirstName = "Ольга", LastName = "Иванова", Patronymic = "Петровна", Age = 38 },
            new ResidentModel { FirstName = "Сергей", LastName = "Кузнецов", Patronymic = "Александрович", Age = 42 },
            new ResidentModel { FirstName = "Александра", LastName = "Королева", Patronymic = "Сергеевна", Age = 30 },
            new ResidentModel { FirstName = "Дмитрий", LastName = "Александров", Patronymic = "Михайлович", Age = 33 }
            );
    }

    private static void AddUtilityAccounts(UtilityAccountContext context)
    {
        context.UtilityAccounts.AddRange(
            new UtilityAccountModel
            {
                Id = "51ВИ260391",
                StartDate = new DateTime(2022, 1, 1, 10, 30, 0),
                EndDate = new DateTime(2022, 12, 31, 15, 45, 0),
                AddressId = context.Addresses.ElementAt(2).Id,
                Area = 150.0f,
            },
            new UtilityAccountModel
            {
                Id = "37ЛМ170592",
                StartDate = new DateTime(2022, 2, 5, 9, 15, 0),
                EndDate = new DateTime(2022, 11, 30, 14, 20, 0),
                AddressId = context.Addresses.ElementAt(3).Id,
                Area = 200.0f,
            },
            new UtilityAccountModel
            {
                Id = "64ИО310893",
                StartDate = new DateTime(2022, 3, 10, 8, 45, 0),
                EndDate = new DateTime(2022, 10, 31, 13, 30, 0),
                AddressId = context.Addresses.ElementAt(4).Id,
                Area = 180.0f,
            },
            new UtilityAccountModel
            {
                Id = "89ГА010394",
                StartDate = new DateTime(2022, 4, 15, 7, 20, 0),
                EndDate = new DateTime(2022, 9, 30, 12, 10, 0),
                AddressId = context.Addresses.ElementAt(5).Id,
                Area = 220.0f,
            },
            new UtilityAccountModel
            {
                Id = "22ЕУ050595",
                StartDate = new DateTime(2022, 5, 20, 6, 55, 0),
                EndDate = new DateTime(2022, 8, 31, 11, 5, 0),
                AddressId = context.Addresses.ElementAt(6).Id,
                Area = 190.0f,
            },
            new UtilityAccountModel
            {
                Id = "18РЕ230796",
                StartDate = new DateTime(2022, 6, 25, 5, 40, 0),
                EndDate = new DateTime(2022, 7, 31, 10, 25, 0),
                AddressId = context.Addresses.ElementAt(7).Id,
                Area = 170.0f,
            },
            new UtilityAccountModel
            {
                Id = "57ФУ120497",
                StartDate = new DateTime(2022, 7, 30, 4, 25, 0),
                EndDate = new DateTime(2023, 8, 30, 9, 15, 0),
                AddressId = context.Addresses.ElementAt(8).Id,
                Area = 210.0f,
            },
            new UtilityAccountModel
            {
                Id = "10ИЕ170999",
                StartDate = new DateTime(2022, 9, 15, 2, 30, 0),
                EndDate = new DateTime(2023, 4, 30, 7, 50, 0),
                AddressId = context.Addresses.ElementAt(9).Id,
                Area = 180.0f,
            },
            new UtilityAccountModel
            {
                Id = "83ТО260993",
                StartDate = new DateTime(2022, 10, 10, 12, 0, 0),
                EndDate = new DateTime(2023, 3, 31, 18, 0, 0),
                AddressId = context.Addresses.ElementAt(10).Id,
                Area = 170.0f,
            });
    }
}
