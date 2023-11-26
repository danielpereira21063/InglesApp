using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InglesApp.Application.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GerarTokenAsync(string userName);
    }
}
