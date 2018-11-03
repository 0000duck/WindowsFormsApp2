using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Client.Controls;
using OpcUaHelper;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        OpcUaClient m_OpcUaClient;
        private String[] MonitorNodeTags = null;
        SqlConnection conn;
        Form2 f2;
        String database;
        public Form1()
        {
       
            InitializeComponent();
            f2 = new Form2();
            comboBox1.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;

            textBox4.Text = "opc.tcp://172.17.0.18:49320";

            textBox1.Text = "ns=2;s=OP030PC.S.AI1-01";
            textBox2.Text = "ns=2;s=OP030PC.S.AI1-01";
            textBox3.Text = "ns=2;s=OP030PC.S.AI1-01";
            database = this.textBox8.Text;
        }
        ~Form1() {
            if (conn != null) {
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = this.button1.Text;
            this.button1.Text ="ing...";
            

            m_OpcUaClient = new OpcUaClient();
            m_OpcUaClient.UserIdentity = new UserIdentity(new AnonymousIdentityToken());
           
             m_OpcUaClient.ConnectServer(textBox4.Text);
            //m_OpcUaClient.ConnectServer("opc.tcp://172.17.0.18:49320");
            //int value = m_OpcUaClient.ReadNode<int>("ns=2;s=Machines/Machine A/TestValueInt");
     
            this.button1.Text = "connected";
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;



        }

        private void SubCallback(string key, MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs args)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string, MonitoredItem, MonitoredItemNotificationEventArgs>(SubCallback), key, monitoredItem, args);
                return;
            }

            if (key == "A")
            {
                // 如果有多个的订阅值都关联了当前的方法，可以通过key和monitoredItem来区分
                MonitoredItemNotification notification = args.NotificationValue as MonitoredItemNotification;
                for (int i=0;i<MonitorNodeTags.Length;i++)
                if (monitoredItem.StartNodeId.ToString() == MonitorNodeTags[i])
                {
                    SqlCommand com = new SqlCommand();
                    com.Connection = conn;
                    com.CommandType = CommandType.Text;
                        com.CommandText = "INSERT INTO dbo."+database+" (t1,n1,tag) VALUES('" +
                            DateTime.Now.ToString() + "'," +
                            notification.Value.WrappedValue.Value.ToString() +
                            ",'" 
                        +MonitorNodeTags[i].Split(';')[1]+"')";
                    SqlDataReader dr = com.ExecuteReader();//执行SQL语句
                    dr.Close();//关闭执行

                    label5.Text = notification.Value.WrappedValue.Value.ToString();
                    label3.Text = DateTime.Now.ToString();
                }
                //else if (monitoredItem.StartNodeId.ToString() == MonitorNodeTags[1])
                //{
                //    label6.Text = notification.Value.WrappedValue.Value.ToString();
                //    label10.Text = DateTime.Now.ToString();
                //}
                //else if (monitoredItem.StartNodeId.ToString() == MonitorNodeTags[2])
                //{
                //    label7.Text = notification.Value.WrappedValue.Value.ToString();
                //    label11.Text = DateTime.Now.ToString();
                //}

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // MonitorNodeTags = new String[]
            //{
            //           textBox1.Text,
            //            textBox2.Text,
            //            textBox3.Text,
            //};
    

            MonitorNodeTags = f2.nodeList.ToArray();
            m_OpcUaClient.AddSubscription("A", MonitorNodeTags, SubCallback);
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            database = textBox8.Text;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            m_OpcUaClient.RemoveSubscription("A");
            textBox1.Enabled=true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text.Equals("Anonymous")) {
                label13.Text = "User Name:";
                label14.Text = "Password:";
                textBox5.Enabled = false;
                textBox6.Enabled = false;
            }
            if (comboBox1.Text.Equals("Username"))
            {
                label13.Text = "User Name:";
                label14.Text = "Password:";
                textBox5.Enabled = true;
                textBox6.Enabled = true;
            }
            if (comboBox1.Text.Equals("Certifacate"))
            {
                label13.Text = "Certifacate:";
                label14.Text = "Password:";
                textBox5.Enabled = true;
                textBox6.Enabled = false;
            }
        }


        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text.Equals("Windows"))
            {
                textBox9.Enabled = false;
                textBox10.Enabled = false;
            }
            if (comboBox3.Text.Equals("SqlServer"))
            {
                textBox9.Enabled = true;
                textBox10.Enabled = true;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection("Server=WIN-MULARBQUR6O\\SQLEXPRESS;DataBase=OPCdata;Trusted_Connection=SSPI");
            //打开连接
            try {
                conn.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = conn;
                com.CommandType = CommandType.Text;
                com.CommandText = "INSERT INTO dbo.Table1 (num,money) VALUES(200,200)";
                //com.CommandText = "INSERT INTO dbo.Data2 (t1,n1) VALUES('"+ DateTime.Now.ToString()+"',0.00236)";
                //com.CommandText = "INSERT INTO dbo.Data2 (t1,n1) VALUES('2018/10/11 12:00:01',200)";
                SqlDataReader dr = com.ExecuteReader();//执行SQL语句
                dr.Close();//关闭执行
                //conn.Close();//关闭数据库
                label20.Text = "Success";
                button5.Enabled = true;

            }
            catch (Exception e2) {
                label20.Text = "Failed";
                label3.Text = DateTime.Now.ToString();

            }
  
            
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (f2 == null)
            {
                f2 = new Form2();
                f2.ShowDialog();
            }
            else {
                f2.Show();
            }

        }
    }
}
