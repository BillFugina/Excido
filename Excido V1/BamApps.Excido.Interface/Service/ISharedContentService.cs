using BamApps.Excido.Interface.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BamApps.Excido.Interface.Service {
    public interface ISharedContentService {
        string GetSlugContent(string slug);
    }
}
