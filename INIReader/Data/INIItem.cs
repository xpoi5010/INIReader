using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INIReader.Data
{
    public class INIItem
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public INIItem(string Name,string Value)
        {
            this.Name = Name;
            this.Value = Value;
        }

        public INIItem()
        {

        }

        public override string ToString()
        {
            return $"{Name} = {Value}";
        }
    }
}
