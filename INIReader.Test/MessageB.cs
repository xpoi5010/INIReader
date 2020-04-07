using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INIReader.Test
{
    public partial class MessageB : Form
    {
        public MessageB()
        {
            InitializeComponent();
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void Print(string a)
        {
            listBox1.Items.Add(a);
        }
    }
}
