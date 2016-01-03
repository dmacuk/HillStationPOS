using System;

namespace HillStationPOS.Exceptions
{
    [Serializable]
    internal class DatabaseException : Exception
    {
        public DatabaseException(string msg) : base(msg)
        {
        }
    }
}