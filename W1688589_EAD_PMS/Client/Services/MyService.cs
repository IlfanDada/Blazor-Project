using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Client.Services
{
    public class MyService:IMyService
    {
        public event Action<string> RefreshRequested;

        public string CallRequestRefresh(string a)
        {
            RefreshRequested?.Invoke(a);
            return null;
        }
    }
}
