using PrefinalMobSys1.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Data
{
    /// <summary>
    /// Centralized Class for handing local SQLite Database things for the App
    /// Loaded in MauiProgram as Singleton (one instance only within the App)
    /// </summary>
    public class DatabaseContext
    {
        SQLiteAsyncConnection database;
        public static DatabaseContext Instance { set; get; }
        public DatabaseContext()
        {
            //init from constructor
            DatabaseContext.Instance = this;
        }

        /// <summary>
        /// Initialize Database Availability
        /// </summary>
        /// <returns></returns>
        public async Task Init()
        {
            if (database is not null)
                return;

            database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            //Create tables
            await database.CreateTableAsync<Product>();
            await database.CreateTableAsync<User>();
            await database.CreateTableAsync<Cart>();
            await database.CreateTableAsync<CartItem>();
        }

        /// <summary>
        /// Get All Products
        /// </summary>
        /// <returns></returns>
        public async Task<List<Product>> Products()
        {
            await Init();
            return await database.Table<Product>().ToListAsync();
        }

        /// <summary>
        /// Insert or  Update a Product record
        /// </summary>
        /// <param name="incoming"></param>
        /// <returns></returns>
        public async Task<int> SaveProduct(Product incoming)
        {
            await Init();
            if (incoming.ID != 0)
                return await database.UpdateAsync(incoming);//update existing
            else
                return await database.InsertAsync(incoming);//insert new
        }

        public async Task<int> DeleteProduct(Product incoming)
        {
            await Init();
            return await database.DeleteAsync(incoming);
        }

        public async Task<List<User>> Users()
        {
            await Init();
            return await database.Table<User>().ToListAsync();
        }

        public async Task<int> SaveUser(User incoming)
        {
            await Init();
            if (incoming.ID != 0)
                return await database.UpdateAsync(incoming);//update existing
            else
                return await database.InsertAsync(incoming);//insert new
        }

        public async Task<int> DeleteUser(User incoming)
        {
            await Init();
            return await database.DeleteAsync(incoming);
        }

        public async Task<List<Cart>> Carts()
        {
            await Init();
            return await database.Table<Cart>().ToListAsync();
        }

        public async Task<int> SaveCart(Cart incoming)
        {
            await Init();
            if (incoming.ID != 0)
                return await database.UpdateAsync(incoming);//update existing
            else
                return await database.InsertAsync(incoming);//insert new
        }

        public async Task<int> DeleteCart(Cart incoming)
        {
            await Init();
            return await database.DeleteAsync(incoming);
        }

        public async Task<List<CartItem>> CartItems()
        {
            await Init();
            return await database.Table<CartItem>().ToListAsync();
        }

        public async Task<int> SaveCartItem(CartItem incoming)
        {
            await Init();
            if (incoming.ID != 0)
                return await database.UpdateAsync(incoming);//update existing
            else
                return await database.InsertAsync(incoming);//insert new
        }

        public async Task<int> DeleteCartItem(CartItem incoming)
        {
            await Init();
            return await database.DeleteAsync(incoming);
        }
    }
}
