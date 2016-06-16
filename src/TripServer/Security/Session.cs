using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace TripServer.Security
{
    public static class Session
    {
        public static Dictionary<Guid, Guid> TokenDictionary{ get; set; } = new Dictionary<Guid, Guid>();
    }
}