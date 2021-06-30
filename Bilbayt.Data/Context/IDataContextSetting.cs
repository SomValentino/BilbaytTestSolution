using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilbayt.Data.Context
{
    public interface IDataContextSetting
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
