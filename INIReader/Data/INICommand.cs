using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INIReader.Data
{
    public class INICommand
    {
        public INICommandOpcode Opcode { get; set; }

        public string Value { get; set; }

        public INICommand(INICommandOpcode Opcode, string Value)
        {
            this.Opcode = Opcode;
            this.Value = Value;
        }

        public static implicit operator string(INICommand command)
        {
            return $"{command.Opcode.ToString()} \"{command.Value}\"";
        }
    }
    public enum INICommandOpcode
    {
        SECTION = 0,
        NAME = 1,
        VALUE = 2,
        None =-1,
        STRING = 3,
        COMMENT = 4,
        ESCAPE = 5,
        WAITINGVALUE = 6
    }
}
