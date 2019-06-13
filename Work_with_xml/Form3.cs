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

namespace Work_with_xml
{
    public partial class Form3 : Form
    {

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
            public string InStorage { get; set; }
            public Book()
            { }

            public Book(Int32 Id, string author, string title, string genre, string price, string publish_date, string description, string in_storage) 
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
            
            List<Book> books = new List<Book>();
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Catalog";
            xRoot.Namespace = null;
            xRoot.IsNullable = true;
            XmlSerializer formatter = new XmlSerializer(typeof(List<Book>), xRoot);
            using (FileStream fs = new FileStream("BooksCatalog.xml", FileMode.OpenOrCreate))
            {
                books = (List<Book>)formatter.Deserialize(fs);
            }
            if (checkBox1.Checked)
            {
                books.Add(new Book()
                {
                    //Последнее= наибольшее ? нет ну и ладно...
                    id = Convert.ToInt32(textBox8.Text),
                    Author = textBox1.Text,
                    Title = textBox2.Text,
                    Genre = textBox3.Text,
                    Price = textBox4.Text,
                    PublishDate = textBox5.Text,
                    Description = textBox6.Text,
                    InStorage = textBox7.Text
                });
            }
            else
            {
                books.Add(new Book()
                {
                    id = books[books.Count-1].id+1,
                    Author = textBox1.Text,
                    Title = textBox2.Text,
                    Genre = textBox3.Text,
                    Price = textBox4.Text,
                    PublishDate = textBox5.Text,
                    Description = textBox6.Text,
                    InStorage = textBox7.Text
                });
            }
            using (FileStream fs = new FileStream("BooksCatalog.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, books);
            }
            this.Close();
        }
    }
}

