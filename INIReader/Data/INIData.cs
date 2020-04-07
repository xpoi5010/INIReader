using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INIReader.Data
{
    public class INIData : ICollection<INISection>
    {
        private INISection[] INISections = new INISection[0];

        private INICommand[] BaseCommands = new INICommand[0];

        public int Count => INISections.Length;

        public bool IsReadOnly => false;

        public INISection this[string Name]
        {
            get
            {
                int index = Array.FindIndex(INISections, x => x.Name == Name);
                if (index == -1)
                {
                    this.Add(new INISection(Name));
                    index = INISections.Length-1;
                }
                return INISections[index];
            }
            set
            {
                int index = Array.FindIndex(INISections, x => x.Name == Name);
                if (index == -1)
                {
                    this.Add(value);
                    index = INISections.Length-1;
                }
                INISections[index] = value;
            }
        }

        

        public INIData()
        {

        }

        public INIData(INIFile file)
        {
            INIParse Parser = new INIParse();
            ConvertFromINICommands();
        }

        public override string ToString()
        {
            string[] con = Array.ConvertAll(INISections, x => x.ToString());
            string output = string.Join("\r\n", con);
            return output;
        }

        private void ConvertFromINICommands()
        {
            string SECTION = "";
            string NAME = "";
            string VALUE = "";
            foreach(INICommand command in BaseCommands)
            {
                switch (command.Opcode)
                {
                    case INICommandOpcode.SECTION:
                        SECTION = command.Value;
                        break;
                    case INICommandOpcode.NAME:
                        NAME = command.Value;
                        break;
                    case INICommandOpcode.VALUE:
                        VALUE = command.Value;
                        this[SECTION][NAME].Value = VALUE;
                        break;
                }
            }
        }

        public void Add(INISection item)
        {
            Array.Resize(ref INISections, INISections.Length + 1);
            INISections[INISections.Length - 1] = item;
        }

        public void Clear()
        {
            INISections = new INISection[0];
        }

        public bool Contains(INISection item)
        {
            return Array.Exists(INISections, x => x.Name == item.Name);
        }

        public void CopyTo(INISection[] array, int arrayIndex)
        {
            INISections.CopyTo(array, arrayIndex);
        }

        public IEnumerator<INISection> GetEnumerator()
        {
            return new INISectionEnumerator(INISections);
        }

        public bool Remove(INISection item)
        {
            return RemoveAt(Array.FindIndex(INISections, x => x.Name == item.Name));
        }

        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= INISections.Length)
                return false;
            if (index == INISections.Length - 1)
            {
                Array.Resize(ref INISections, INISections.Length - 1);
                return true;
            }
            Array.Copy(INISections, index + 1, INISections, index, INISections.Length - index - 1);
            Array.Resize(ref INISections, INISections.Length - 1);
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new INISectionEnumerator(INISections);
        }
    }

    public class INISectionEnumerator : IEnumerator<INISection>
    {
        public INISectionEnumerator(INISection[] items)
        {
            this.items = items;
        }

        private INISection[] items = new INISection[0];

        private int Position = -1;

        public INISection Current => items[Position];

        object IEnumerator.Current => items[Position];

        public void Dispose()
        {
            items = new INISection[0];
        }

        public bool MoveNext()
        {
            if (Position < items.Length)
                Position++;
            return Position < items.Length;
        }

        public void Reset()
        {
            Position = -1;
        }
    }
}

