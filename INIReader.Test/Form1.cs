using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using INIReader.Data;


namespace INIReader.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       string test = "";

        private INISection Current { get; set; }

        private INIData Data { get; set; }

        private string SavePath { get; set; }

        private bool Changed { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            Changed = false;
        }

        private void DataGridView1_Resize(object sender, EventArgs e)
        {

        }

        private void Form1_Resize(object sender)
        { 

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "INIFile|*.ini|AllFile|*.*";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                OpenFile(ofd.FileName);
            }
        }

        public void OpenFile(string Path)
        {
            treeView1.Nodes.Clear();
            INIFile file = INIFile.ReadFile(Path);
            INIParse parse = new INIParse();
            Data = parse.ParseINI(file.Content, true);
            List<TreeNode> temp = new List<TreeNode>();
            foreach (INISection section in Data)
            {
                TreeNode node = new TreeNode(section.Name);
                temp.Add(node);
            }
            treeView1.Nodes.AddRange(temp.ToArray());
            
        }

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;
            if (treeView1.SelectedNode.Tag != null && treeView1.SelectedNode.Tag == "inited")
                goto updateContent;
            string SectionName = treeView1.SelectedNode.Text;
            List<TreeNode> temp = new List<TreeNode>();
            INISection section = Data[SectionName];
            Current = section;
            foreach (INIItem item in section)
            {
                temp.Add(new TreeNode(item.Name));
            }
            treeView1.SelectedNode.Nodes.AddRange(temp.ToArray());
            treeView1.SelectedNode.Tag = "inited";
            goto updateContent;
            updateContent:
            {
                /*
                List<DataGridViewRow> temp_dgv = new List<DataGridViewRow>();
                foreach(INIItem item in Current)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
                    cell1.Value = item.Name;
                    cell2.Value = item.Value;
                    row.Cells.Add(cell1);
                    row.Cells.Add(cell2);
                    temp_dgv.Add(row);
                }
                dataGridView1.Rows.AddRange(temp_dgv.ToArray());
                */
                //dataGridView1.DataSource = Current.ToArray();
            }
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;
            if (treeView1.SelectedNode.Tag != null && treeView1.SelectedNode.Tag == "inited")
                goto updateContent;
            string SectionName = treeView1.SelectedNode.Text;
            List<TreeNode> temp = new List<TreeNode>();
            INISection section = Data[SectionName];
            Current = section;
            foreach (INIItem item in section)
            {
                temp.Add(new TreeNode(item.Name));
            }
            treeView1.SelectedNode.Nodes.AddRange(temp.ToArray());
            treeView1.SelectedNode.Tag = "inited";
            goto updateContent;
        updateContent:
            {
                dataGridView1.Rows.Clear();
                List<DataGridViewRow> temp_ = new List<DataGridViewRow>();
                foreach(INIItem Item in Current)
                {
                    DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
                    DataGridViewRow row = new DataGridViewRow();
                    cell1.Value = Item.Name;
                    cell2.Value = Item.Value;
                    row.Cells.Add(cell1);
                    row.Cells.Add(cell2);
                    temp_.Add(row);
                }
                dataGridView1.Rows.AddRange(temp_.ToArray());
             }
        }

        private void SectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBoxDialog textBoxDialog = new TextBoxDialog("Create Section","Name");
            if(textBoxDialog.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void ApplyItemsChange()
        {
            List<INIItem> items = new List<INIItem>();
            
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void FileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void AAAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("output.ini", FileMode.Create);
            for (int s = 0; s < 1234; s++)
            {
                fs.Write(new byte[] { 0x5B }, 0, 1);
                fs.Write(getRandomText(7), 0, 7);
                fs.Write(new byte[] { 0x5D }, 0, 1);
                fs.Write(new byte[] { 0x0D, 0x0A }, 0, 1);
                for (int i = 0; i < 1321; i++)
                {
                    fs.Write(getRandomText(8), 0, 8);
                    fs.Write(new byte[] { 0x3D }, 0, 1);
                    fs.Write(getRandomText(13), 0, 13);
                    fs.Write(new byte[] { 0x0D,0x0A }, 0, 1);
                }
            }
            fs.Close();
        }

        private void DebugToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        Random rd = new Random();
        private byte[] getRandomText(int size)
        {
            using(MemoryStream ms = new MemoryStream())
                using(BinaryWriter bw = new BinaryWriter(ms))
            {
                for (int i = 0; i < size; i++)
                {
                    int a = rd.Next(0, 0xFF);
                    a &= 1;
                    bw.Write((byte)(a == 0 ? rd.Next(0x61, 0x7A) : rd.Next(0x41, 0x5A)));
                }
                return ms.ToArray();
            }
        }

        private void PerformanceTestrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("output.ini", FileMode.Open);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, data.Length);
            string Text = Encoding.ASCII.GetString(data);
            MessageB message = new MessageB();
            List<double> d = new List<double>();
            for(int i = 0; i < 10; i++)
                {
                    INIParse parse = new INIParse();
                    DateTime dt = DateTime.Now;
                    parse.ParseINI(Text, false);
                    TimeSpan ts = DateTime.Now - dt;
                    d.Add(ts.TotalMilliseconds);
                }
            double a = 0;

                foreach(double da in d)
            {
                message.Print(da.ToString());
                a += da;
            }
            message.Print("avg:" + (a/10).ToString());
            message.Show();

        }
    }
}
