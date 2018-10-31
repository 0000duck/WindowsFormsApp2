using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace WindowsFormsApp2
{
    public partial class Form2 : Form
    {
        
        public static String[] def=new String[] {
            "ns=2;s=OP050PC.S.AI2-01",
            "ns=2;s=OP050PC.S.AI2-02",
            "ns=2;s=OP050PC.S.AI2-03",
            "ns=2;s=CNC.CNC.PROJECT.OP050_CNC.X_AXIS1_A",
            "ns=2;s=CNC.CNC.PROJECT.OP050_CNC.Y_AXIS1_A",
            "ns=2;s=CNC.CNC.PROJECT.OP050_CNC.Z_AXIS1_A",
            "ns=2;s=OP050PC.S.T1"
    };
        public List<String> nodeList = new List<String>(def);
        //public String[] nodeList = def;

        public Form2()
        {
            InitializeComponent();
            //dic.Add("1", "ns=2;s=OP030PC.S.AI1-01");
            //dic.Add("2", "ns=2;s=OP030PC.S.AI1-02");
            //dic.Add("3", "ns=2;s=OP030PC.S.AI1-03");
            //this.dataGridView1.DataSource = dic;
            for (int i = 0; i < def.Length; i++) {
                this.dataGridView1.Rows.Add(def[i]);
            }

            //this.dataGridView1.Rows.Add(def[0]);
            //this.dataGridView1.Rows.Add(def[1]);
            //this.dataGridView1.Rows.Add(def[2]);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int rows = dataGridView1.Rows.Count;
            button2.Text = rows.ToString();
            if (rows > 1)
            {
                nodeList = new List<String>();
                try {
                    for (int i = 0; i < rows; i++)
                    {
                        //MessageBox.Show(i.ToString()+"xunhuan");
                        if (dataGridView1.Rows[i].Cells[0].Value != null
                 && dataGridView1.Rows[i].Cells[0].Value.ToString().Trim().Length != 0) {
                            //MessageBox.Show("ok");
                            String s = dataGridView1.Rows[i].Cells[0].Value.ToString();
                            nodeList.Add(s);
                        }
                          
                    }
                    MessageBox.Show(nodeList.Count().ToString());

                }
                catch (Exception e2) {
                    button1.Text = "error";
                    MessageBox.Show(e2.Message);
                }

            }
           
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
           this.Hide();
            e.Cancel = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
