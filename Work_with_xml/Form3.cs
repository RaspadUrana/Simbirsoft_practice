using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using NLog;

namespace Work_with_xml
{
    public partial class Form3 : Form
    {
        Logger logErr = LogManager.GetLogger("Errors");
        Logger logInfo = LogManager.GetLogger("Info");

        public Form3()
        {
            InitializeComponent();
        }

        [Serializable]
        public class Book
        {
            public string Author { get; set; }
            [XmlAttribute]
            public Int32 id { get; set; }               //без паблика не уверен робит ли 
            public string Title { get; set; }
            public string Genre { get; set; }
            public string Price { get; set; }
            public string PublishDate { get; set; }
            public string Description { get; set; }
            public bool InStorage { get; set; }
            public Book()
            { }

            public Book(Int32 Id, string author, string title, string genre, string price, string publish_date, string description, bool in_storage) 
            {
                id = Id;
                Author = author;
                Title = title;
                Genre = genre;
                Price = price;
                PublishDate = publish_date;
                Description = description;
                InStorage = in_storage;
            }

        }
        public Book[] newbook;
        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox8.Enabled = true;
            }
            else
            {
                textBox8.Enabled = false;
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Catalog";
            xRoot.Namespace = null;
            xRoot.IsNullable = true;
            XmlSerializer formatter = new XmlSerializer(typeof(Book[]), xRoot);
            using (FileStream fs = new FileStream("BooksCatalog.xml", FileMode.OpenOrCreate))
            {
                newbook = (Book[])formatter.Deserialize(fs);
            }
            
            Book[] newbook1 = new Book[newbook.Length+1];
            newbook1[newbook.Length] = new Book();

            for (int j = 0; j < newbook.Length; j++)
            {
                newbook1[j] = newbook[j];
            }
            
            if (checkBox1.Checked)
            {

                //Последнее= наибольшее ? нет ну и ладно...
                newbook1[newbook.Length ].id = Convert.ToInt32(textBox8.Text);
                newbook1[newbook.Length ].Author = textBox1.Text;
                newbook1[newbook.Length ].Title = textBox2.Text;
                newbook1[newbook.Length ].Genre = textBox3.Text;
                newbook1[newbook.Length ].Price = textBox4.Text;
                newbook1[newbook.Length ].PublishDate = textBox5.Text;
                newbook1[newbook.Length ].Description = textBox6.Text;
                newbook1[newbook.Length ].InStorage = checkBox2.Checked;

            }
            else
            {
                if (newbook.Length != 0)
                {
                    newbook1[newbook.Length].id = newbook[newbook.Length-1].id + 1;
                    newbook1[newbook.Length].Author = textBox1.Text;
                    newbook1[newbook.Length].Title = textBox2.Text;
                    newbook1[newbook.Length].Genre = textBox3.Text;
                    newbook1[newbook.Length].Price = textBox4.Text;
                    newbook1[newbook.Length].PublishDate = textBox5.Text;
                    newbook1[newbook.Length].Description = textBox6.Text;
                    newbook1[newbook.Length].InStorage = checkBox2.Checked;
                }
                else
                {
                    newbook1[newbook.Length + 1].id = 1;
                    newbook1[newbook.Length + 1].Author = textBox1.Text;
                    newbook1[newbook.Length + 1].Title = textBox2.Text;
                    newbook1[newbook.Length + 1].Genre = textBox3.Text;
                    newbook1[newbook.Length + 1].Price = textBox4.Text;
                    newbook1[newbook.Length + 1].PublishDate = textBox5.Text;
                    newbook1[newbook.Length + 1].Description = textBox6.Text;
                    newbook1[newbook.Length + 1].InStorage = checkBox2.Checked;
                }
            }
            try
            {

                using (FileStream fs = new FileStream("BooksCatalog.xml", FileMode.Create))
                {
                    formatter.Serialize(fs, newbook1);
                }
                logInfo.Info("Добавлена новая запись");
            }
            catch
            {
                logErr.Error("Ошибка создания новой записи");
            }
            this.Close();
        }

        private void TextBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 45 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void TextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 45)
            {
                e.Handled = true;
            }
        }

        private void Form3_Activated(object sender, EventArgs e)
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Catalog";
            xRoot.Namespace = null;
            xRoot.IsNullable = true;
            XmlSerializer formatter = new XmlSerializer(typeof(Book[]), xRoot);
            using (FileStream fs = new FileStream("BooksCatalog.xml", FileMode.OpenOrCreate))
            {
                newbook = (Book[])formatter.Deserialize(fs);
            }
            if (newbook.Length != 0)
            {
                textBox8.Text = (newbook[newbook.Length - 1].id + 1).ToString();
            }
            else
            {
                textBox8.Text = "1";
            }
        }

        private void TextBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }
    }
}

