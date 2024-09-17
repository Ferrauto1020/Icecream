using IcecreamApp.Data;
using IcecreamApp.Models;
using SQLite;
namespace IcecreamApp.Services

{
    public class DatabaseService : IAsyncDisposable
    {
        private const string DatabaseName = "IcecreamDB";
        private static readonly string _databasePath = Path.Combine(FileSystem.AppDataDirectory, DatabaseName);
        private SQLiteAsyncConnection _connection;
        private SQLiteAsyncConnection Database =>
         _connection ??= new SQLiteAsyncConnection(
            _databasePath,
          SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);


        private async Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> actionOnDb)
        {

            await Database.CreateTableAsync<CartItemEntity>();
            return await actionOnDb?.Invoke();
        }
        public async Task<int> AddCartItemAsync(CartItemEntity entity)
        => await ExecuteAsync(async () =>
                        await Database.InsertAsync(entity)
                        );

        public async Task UpdateCartItemAsync(CartItemEntity entity)
                => await ExecuteAsync(async () =>
            await Database.UpdateAsync(entity)
            );
        //var existingCartItem

        public async Task DeleteCartItemAsync(CartItemEntity entity)
            => await ExecuteAsync(async () =>
            await Database.DeleteAsync(entity));





        public async Task<CartItemEntity> GetCartItemAsync(int id)
            => await ExecuteAsync(async () =>
             await Database.GetAsync<CartItemEntity>(id));



        public async Task<List<CartItemEntity>> GetAllItemCartItemsAsync()
            => await ExecuteAsync(async () =>
             await Database.Table<CartItemEntity>().ToListAsync());


        public async Task<int> ClearCartAsync() => await ExecuteAsync(async () => await Database.DeleteAllAsync<CartItemEntity>());

        public async ValueTask DisposeAsync()
            =>
              await _connection?.CloseAsync();


    }
}