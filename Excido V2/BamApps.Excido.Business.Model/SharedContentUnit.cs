using BamApps.Excido.Interface.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BamApps.Excido.Business.Model
{
    public class SharedContentUnit : ISharedContentUnit {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }
        public DateTime Created { get; set; }
        public DateTime? ExpireDate { get; set; }
        public int ExpireCount { get; set; }

    }
}
