using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Models.Dto.Account
{
    public class ListError:Exception
    {
        public List<string> Errors { get; }
    }
}
