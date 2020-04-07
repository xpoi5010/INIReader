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
    public partial class TextBoxDialog : Form
    {
        public TextBoxDialog(string Title,string Name)
        {
            InitializeComponent();
            this.Text = Title;
            this.label1.Text = Name;
        }

        private string Message
        {
            get
            {
                return textBox1.Text;
            }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void TextBoxDialog_Load(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
