using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace W1688589_EAD_PMS.Client.Services
{
    public class UpdateLayoutService:IUpdateLayoutService
    {
        public event Action UpdateRequested;

        public void CallUpdateRequestRefresh()
        {
            UpdateRequested?.Invoke();
        }
    }
}
