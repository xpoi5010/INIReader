using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INIReader.Data
{
    public class INISection : ICollection<INIItem>
    {
        public INISection()
        {

        }

        public INISection(string Name)
        {
            this.Name = Name;
        }
        public string Name { get; set; }

        //private INIItem[] INIItems = new INIItem[0];

        private List<INIItem> baseItem = new List<INIItem>();

        public int Count => baseItem.Count();

        public bool IsReadOnly => false;

        public INIItem this[string Key]
        {
            get
            {
                int index = Array.FindIndex(baseItem.ToArray(), x => x.Name == Key);
                if (index == -1)
                {
                    this.Add(new INIItem(Key,""));
                    index = baseItem.Count-1;
                }
                return baseItem[index];
            }
            set
            {
                int index = Array.FindIndex(baseItem.ToArray(), x => x.Name == Key);
                if(index == -1)
                {
                    this.Add(value);
                    index = baseItem.Count - 1;
                }
                baseItem[index] = value;
            }
        }

        public void Add(INIItem item)
        {
            baseItem.Add(item);
        }

        public void Clear()
        {
            baseItem.Clear();
        }

        public bool Contains(INIItem item)
        {
            return baseItem.Exists(x=>x==item);
        }

        public void CopyTo(INIItem[] array, int arrayIndex)
        {
            baseItem.CopyTo(array, arrayIndex);
        }

        public IEnumerator<INIItem> GetEnumerator()
        {
            return baseItem.GetEnumerator();
        }

        public bool Remove(INIItem item)
        {
            baseItem.Remove(item);
            return true;
        }

        public bool RemoveAt(int index)
        {
            baseItem.RemoveAt(index);
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return baseItem.GetEnumerator();
        }

        public override string ToString()
        {
            string[] con = Array.ConvertAll(baseItem.ToArray(), x => x.ToString());
            string output = $"[{Name}]\r\n" + string.Join("\r\n", con);
            return output;
        }
    }

    public class INIItemEnumerator : IEnumerator<INIItem>
    {
        public INIItemEnumerator(INIItem[] items)
        {
            this.items = items;
        }

        private INIItem[] items = new INIItem[0];

        private int Position = -1;

        public INIItem Current => items[Position];

        object IEnumerator.Current => items[Position];

        public void Dispose()
        {
            items = new INIItem[0];
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
