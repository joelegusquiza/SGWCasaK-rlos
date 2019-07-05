using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAL.Interfaces
{
    public interface IEnvironmentContext
    {
        string BaseUrl();
        string Environment();
    }
}
