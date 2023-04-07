using Core.Configuration;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace Infrastructure.DAL
{
    public class DbContext : IAsyncDisposable, IDisposable
    {
        private readonly ConnectionSettings _connectionSettings;
        private readonly MySqlConnection _connection;
        private bool _isConnectionOpen;

        public DbContext(IOptions<ConnectionSettings> connectionSettings)
        {
            _connectionSettings = connectionSettings.Value;
            _connection = GetConn(_connectionSettings);
        }

        public async Task<T> ExecuteQueryAsync<T>(Func<MySqlConnection, Task<T>> query)
        {
            await OpenConnectionAsync();

            return await query(_connection);
        }

        public async Task ExecuteQueryAsync(Func<MySqlConnection, Task> query)
        {
            await OpenConnectionAsync();

            await query(_connection);
        }

        private async Task OpenConnectionAsync()
        {
            if (!_isConnectionOpen)
            {
                await _connection.OpenAsync();

                _isConnectionOpen = true;
            }
        }

        private MySqlConnection GetConn(ConnectionSettings settings)
        {
            string connString = $"Server={_connectionSettings.Host};Database={_connectionSettings.Database};Port=3306;User ID=root;Password={_connectionSettings.Password};ConnectionReset=True;ConnectionLifeTime=100;";

            return new MySqlConnection(connString);
        }

        public async ValueTask DisposeAsync()
        {
            if (_isConnectionOpen) await _connection.CloseAsync().ConfigureAwait(false);            
        }

        public void Dispose()
        {
            DisposeAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
