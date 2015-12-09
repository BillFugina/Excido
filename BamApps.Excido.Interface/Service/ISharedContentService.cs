using BamApps.Excido.Interface.Data;
using BamApps.Excido.Interface.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BamApps.Excido.Interface.Service {
    public interface ISharedContentService<T> where T : ISharedContentUnit{
        string GetSlugContent(string slug);

        T GetSharedContentUnit(string slug);
    }
        
}
