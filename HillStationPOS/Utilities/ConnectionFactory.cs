using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Timers;
using HillStationPOS.Exceptions;

namespace HillStationPOS.Utilities
{
    public static class ConnectionFactory
    {
        #region Private Members

        private const double ConnectionInterval = 5*60*1000; // 5 Minutes

        private static DbConnection _connection;
        private static Timer _connectionTimer;

        #endregion Private Members

        #region Public Members

        /// <summary>
        ///     Name of provider of the database
        /// </summary>
        public static string ProviderName { private get; set; }

        /// <summary>
        ///     Database connection string
        /// </summary>
        public static string ConnectionString { private get; set; }

        static ConnectionFactory()
        {
            const string menuFile = "Data\\HillStation.vdb5";
            ProviderName = "System.Data.VistaDB5";
            ConnectionString = $@"Data Source='|DataDirectory|\{menuFile}'";
        }

        /// <summary>
        ///     Return an open database connection
        /// </summary>
        [SuppressMessage("Microsoft.Design",
            "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
        public static DbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    if (string.IsNullOrEmpty(ProviderName) || string.IsNullOrEmpty(ConnectionString))
                    {
                        var msg =
                            $"ProviderName or ConnectionString not set, ProviderName: {ProviderName}, ConnectionString: {ConnectionString}";
                        throw new DatabaseException(msg);
                    }

                    var providerFactory = DbProviderFactories.GetFactory(ProviderName);
                    _connection = providerFactory.CreateConnection();
                    if (_connection != null)
                    {
                        _connection.ConnectionString = ConnectionString;
                        _connection.StateChange += ConnectionStateChange;
                    }
                    else
                    {
                        throw new DatabaseException(
                            $"Unable to connect to ProviderName: {ProviderName}, ConnectionString: {ConnectionString} ");
                    }
                }
                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }
                _connectionTimer.Stop();
                _connectionTimer.Start();
                return _connection;
            }
        }

        public static bool SuspendClose { get; set; }

        /// <summary>
        ///     Close and dispose of the current connection
        /// </summary>
        public static void Dispose()
        {
            if (_connection == null) return;
            if (_connection.State == ConnectionState.Open)
            {
                Console.Out.WriteLine("Closing cxonnection");
                _connection.Close();
            }
            _connection = null;
        }

        #endregion Public Members

        #region Private Methods

        private static void ConnectionStateChange(object sender, StateChangeEventArgs e)
        {
            if (e.CurrentState != ConnectionState.Open) return;
            _connectionTimer = new Timer(ConnectionInterval);
            _connectionTimer.Elapsed += ConnectionTimerElapsed;
        }

        private static void ConnectionTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (SuspendClose) return;
            if (_connection.State != ConnectionState.Open) return;
            _connectionTimer = null;
            _connection.Close();
        }

        #endregion Private Methods
    }
}