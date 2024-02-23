using NetDevPack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Domain.Models
{
    public class FileUpload : Entity, IAggregateRoot
    {
        public FileUpload(string? name, string? path, long? size)
        {
            Name = name;
            Path = path;
            Size = size;
        }

        public string? Name { get; private set; }

        public string? Path { get; private set;}

        public long? Size{ get; private set; }
    }
}
