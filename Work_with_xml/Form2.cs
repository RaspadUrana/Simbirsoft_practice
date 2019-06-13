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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        [Serializable]
        public class Book
        {
            [XmlAttribute]
            public string id { get; set; }               //без паблика не уверен робит ли 
            public string Author { get; set; }
            public string Title { get; set; }
            public string Genre { get; set; }
            public string Price { get; set; }
            public string PublishDate { get; set; }
            public string Description { get; set; }
            public string InStorage { get; set; }
            public Book()
            { }

            public Book(string key, string author, string title, string genre, string price, string publish_date, string description, string in_storage)
            {
                id = key;
                Author = author;
                Title = title;
                Genre = genre;
                Price = price;
                PublishDate = publish_date;
                Description = description;
                InStorage = in_storage;
            }
            // book[] books = new book[10] ;//10 элементов макс
            /*   [Serializable()]
               [System.Xml.Serialization.XmlRoot("Catalog")]
               public class Catalog
               {
                   [XmlArray("Catalog")]
                   [XmlArrayItem("book", typeof(book))]
                   public book[] book_ { get; set; }
               }*/
        }
        public Book[] newbook;//изначально всё строилость под массив, переделывалось под список, но десериализация в список не работала так как надо.
                            //в связи с этим было принято решение отказаться от добавления строк.

        private void Form2_Load(object sender, EventArgs e)
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Catalog";
            xRoot.Namespace = null;
            xRoot.IsNullable = true;
            XmlSerializer formatter = new XmlSerializer(typeof(Book[]), xRoot);
            using (FileStream fs = new FileStream("BooksCatalog.xml", FileMode.OpenOrCreate))
            {
                newbook = (Book[])formatter.Deserialize(fs);
                dataGridView1.DataSource = newbook;

            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Rows.Add();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Редактировать")
            {
                button2.Text = "Закончить редактирование";//А "редактирование" то где?
                dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystroke;

            }
            else
            {
                //Создание аттрибута для указания новых свойств сериализации
                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "Catalog";
                xRoot.Namespace = null;
                xRoot.IsNullable = true;
                XmlSerializer formatter = new XmlSerializer(typeof(Book[]), xRoot);
                button2.Text = "Редактировать";
                using (FileStream fs = new FileStream("BooksCatalog.xml", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, newbook);
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {

        }
    }
}
