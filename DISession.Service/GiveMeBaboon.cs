using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace DISession.Service
{
    public class GiveMeBaboon : IGiveMeBaboon
    {
        private readonly ISessionCompatibilization _httpContextAccessor;

        public GiveMeBaboon(ISessionCompatibilization pHttpContextAccessor)
        {
            _httpContextAccessor = pHttpContextAccessor;
        }

        public string GiveBaboon()
        {
            return $"OE OE AA AA {_httpContextAccessor.GetString("Baboon")}";
        }
    }
}
