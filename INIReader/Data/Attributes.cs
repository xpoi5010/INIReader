using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INIReader.Data
{
    public class INISectionAttribute
    {
        public string Name { get; set; }
        public INISectionAttribute(string Name)
        {
            this.Name = Name;
        }
    }

    public class INIItemAttribute
    {
        public string Name { get; set; }
        public INIItemAttribute(string Name)
        {
            this.Name = Name;
        }
    }

    public class INICommentAttribute
    {
        public string Content { get; set; }
        public INICommentAttribute(string Content)
        {
            this.Content = Content;
        }
    }
}
