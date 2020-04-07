using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INIReader.Serialization
{
    public class ININameAttribute:Attribute
    {
        public string Name { get; set; }

        public ININameAttribute(string Name)
        {
            this.Name = Name;
        }
    }

    public class INISectionAttribute : Attribute
    {
        public string Name { get; set; }

        public INISectionAttribute(string Name)
        {
            this.Name = Name;
        }
    }

    public class INICommentAttribute : Attribute
    {
        public string Content { get; set; }

        public CommendPosition Position { get; set; }


        /*
             BehindItem:
             item = value;Comment
             FrontOfItem:
             ;Comment
             item = value
        */
        public enum CommendPosition
        {
            BehindItem,FrontOfItem
        }

        public INICommentAttribute(string Content, CommendPosition Position)
        {
            this.Content = Content;

            this.Position = Position;

        }
    }
}
