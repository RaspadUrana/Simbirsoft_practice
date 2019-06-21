using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
//using System.Runtime.Serialization;
using System.Windows.Forms;
using NLog;

/*Снизу миллиард комментариев с кодом, в большинстве своём он бесполезен,
 * т.к был взят исклюительно ради теста */


namespace Work_with_xml
{

	public partial class Form1 : Form
	{
        Logger logErr = LogManager.GetLogger("Errors");
        Logger logInfo = LogManager.GetLogger("Info");
        int i = 0;
        bool flag_izm =false;




        public Form1()
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
             
         /*   [Serializable()]
            [System.Xml.Serialization.XmlRoot("Catalog")]
            public class Catalog
            {
                [XmlArray("Catalog")]
                [XmlArrayItem("book", typeof(book))]
                public book[] book_ { get; set; }
            }*/
        }
        public Book[] newbook;
        private void Button1_Click(object sender, EventArgs e)
        {

            //Вперёд
            try
            {
                i = (i + 1);//мотаем счётчик вперёд

                dataGridView1.DataSource = newbook;
                textBox1.Text = newbook[i].Author;
                textBox2.Text = newbook[i].Title;
                textBox8.Text = newbook[i].id.ToString();
                textBox3.Text = newbook[i].Genre;
                textBox4.Text = newbook[i].Price;
                textBox5.Text = newbook[i].PublishDate;
                textBox6.Text = newbook[i].Description;
                checkBox1.Checked = newbook[i].InStorage;
                if (i + 1 == newbook.Length)
                {
                    button1.Enabled = false;
                };
                if (i - 1 != -1)
                {
                    button2.Enabled = true;
                };
            }
            catch
            {
                logErr.Error("Ошибка перехода на следующую запись, на запись {String}",i);
            }

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //Назад
            try
            {
                i = --i;//мотаем счётчик назад на для того чтобы показалась пред. запись

                dataGridView1.DataSource = newbook;
                textBox1.Text = newbook[i].Author;
                textBox2.Text = newbook[i].Title;
                textBox8.Text = newbook[i].id.ToString();
                textBox3.Text = newbook[i].Genre;
                textBox4.Text = newbook[i].Price;
                textBox5.Text = newbook[i].PublishDate;
                textBox6.Text = newbook[i].Description;
                checkBox1.Checked = newbook[i].InStorage;
                if (i + 1 == newbook.Length)
                {
                    button1.Enabled = false;
                }
                else
                {
                    button1.Enabled = true;
                };
                if (i - 1 != -1)
                {
                    button2.Enabled = true;
                }
                else
                {
                    button2.Enabled = false;
                };
            }
            catch
            {
                logErr.Error("Ошибка перехода на предыдущую запись, на запись {String}", i);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            { 
            //logInfo.Trace("trace message");
            //logInfo.Debug("debug message");
            logInfo.Info("Программа запущена");
            //logErr.Warn("warn message");
            //logErr.Error("error message");
            //logErr.Fatal("fatal message");


            //Создание аттрибута для указания новых свойств сериализации
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Catalog";
            xRoot.Namespace = null;
            xRoot.IsNullable = true;
            XmlSerializer formatter = new XmlSerializer(typeof(Book[]), xRoot);

                // десериализация
                // XmlSerializer formatter = new XmlSerializer(typeof(book[]));

                using (FileStream fs = new FileStream("BooksCatalog.xml", FileMode.OpenOrCreate))
                {
                    newbook = (Book[])formatter.Deserialize(fs);
                    dataGridView1.DataSource = newbook;
                    textBox1.Text = newbook[i].Author;
                    textBox8.Text = newbook[i].id.ToString();
                    textBox2.Text = newbook[i].Title;
                    textBox3.Text = newbook[i].Genre;
                    textBox4.Text = newbook[i].Price;
                    textBox5.Text = newbook[i].PublishDate;
                    textBox6.Text = newbook[i].Description;
                    checkBox1.Checked = newbook[i].InStorage;
                    if (newbook.Length == i)
                    {
                        button1.Enabled = false;
                    };
                }

            }
            catch
            {
                logErr.Error("Ошибка чтения файла");
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            //TODO: создать иерархию
            //TODO: найти как это сделать
            Form2 newForm = new Form2();
            newForm.ShowDialog();

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                //if (button4.Text == "Изменить")  //не работает, почему не знаю, добавлен костыль в иф ниже
                if (flag_izm == false)
                {
                    button4.Text = "Записать";
                    textBox1.Enabled = true;
                    textBox2.Enabled = true;
                    textBox3.Enabled = true;
                    textBox4.Enabled = true;
                    textBox5.Enabled = true;
                    textBox6.Enabled = true;
                    checkBox1.Enabled = true;
                    //button1.Enabled = false; //в случае если мы выключаем кнопки переключения записей нужно будет делать несколько не  нужных проверок
                    //button2.Enabled = false; //а в случае если не выключаем на функционал повлиять не должно
                    button3.Enabled = false;
                    flag_izm = true;
                }
                //if (button4.Text == "Записать")
                else
                {
                    button4.Text = "Изменить";
                    textBox1.Enabled = false;
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    textBox4.Enabled = false;
                    textBox5.Enabled = false;
                    textBox6.Enabled = false;
                    checkBox1.Enabled = false;
                    //button1.Enabled = true;
                    //button2.Enabled = true;
                    button3.Enabled = true;
                    //Создание аттрибута для указания новых свойств сериализации
                    XmlRootAttribute xRoot = new XmlRootAttribute();
                    xRoot.ElementName = "Catalog";
                    xRoot.Namespace = null;
                    xRoot.IsNullable = true;
                    //Примечание: изменение свойств выше жрёт оперативку и не отдаёт её при обратном изменении lul
                    XmlSerializer formatter = new XmlSerializer(typeof(Book[]), xRoot);

                    // сериализация
                    //Ломает файл 
                    //Через раз?
                    newbook[i].Author = textBox1.Text;
                    newbook[i].Title = textBox2.Text;
                    newbook[i].Genre = textBox3.Text;
                    newbook[i].Price = textBox4.Text;
                    newbook[i].PublishDate = textBox5.Text;
                    newbook[i].Description = textBox6.Text;
                    newbook[i].InStorage = checkBox1.Checked;
                    using (FileStream fs = new FileStream("BooksCatalog.xml", FileMode.OpenOrCreate))
                    {
                        formatter.Serialize(fs, newbook);
                        /*newbook[i].Author = textBox1.Text;
                        newbook[i].Title = textBox2.Text;
                        newbook[i].Genre = textBox3.Text;
                        newbook[i].Price = textBox4.Text;
                        newbook[i].PublishDate = textBox5.Text;
                        newbook[i].Description = textBox6.Text;
                        newbook[i].InStorage = textBox7.Text;*/
                        /*using (FileStream fs1 = new FileStream("BooksCatalog1.xml", FileMode.OpenOrCreate))
                        {
                            formatter.Serialize(fs, newbook);
                        }*/


                    }
                    flag_izm = false;
                    logInfo.Info("Изменения в файле записаны в строку {String}",i.ToString());
                };
            }
            catch
            {
                logErr.Error("Ошибка при попытке изменения файла (форма), в запись {String}", i);
            }

        }


        private void Button3_Click(object sender, EventArgs e)
        {
            Form3 newForm = new Form3();
            newForm.ShowDialog();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            //Создание аттрибута для указания новых свойств сериализации
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Catalog";
            xRoot.Namespace = null;
            xRoot.IsNullable = true;
            XmlSerializer formatter = new XmlSerializer(typeof(Book[]), xRoot);

            // десериализация
            // XmlSerializer formatter = new XmlSerializer(typeof(book[]));
            try
            {
                using (FileStream fs = new FileStream("BooksCatalog.xml", FileMode.OpenOrCreate))
                {
                    newbook = (Book[])formatter.Deserialize(fs);
                    dataGridView1.DataSource = newbook;
                    textBox1.Text = newbook[i].Author;
                    textBox8.Text = newbook[i].id.ToString();
                    textBox2.Text = newbook[i].Title;
                    textBox3.Text = newbook[i].Genre;
                    textBox4.Text = newbook[i].Price;
                    textBox5.Text = newbook[i].PublishDate;
                    textBox6.Text = newbook[i].Description;
                    checkBox1.Checked = newbook[i].InStorage;
                    if (newbook.Length == i)
                    {
                        button1.Enabled = false;
                    };
                }
            }
            catch
            {
                logErr.Error("Ошибка открытия файла");
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            logInfo.Info("Закрытие программы");
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            logInfo.Info("Удалена запись № {String}",i);
            Book[] newbook1 = new Book[newbook.Length];
            bool fl = false;
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Catalog";
            xRoot.Namespace = null;
            xRoot.IsNullable = false;
            XmlSerializer formatter = new XmlSerializer(typeof(Book[]), xRoot);
            /*for (int j = 0; j < newbook.Length - 1; j++)
            {
                newbook1[j] = newbook[j];
            }*/

            try
            {
                for (int j = 0; j < newbook.Length; j++)
                {
                    if (i == j)
                    {
                        fl = true;
                    }
                    else
                    {
                        if (fl)
                        {
                            newbook1[j - 1] = newbook[j];
                        }
                        else
                        {
                            newbook1[j] = newbook[j];
                        }
                    }
                }

                using (FileStream fs = new FileStream("BooksCatalog.xml", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, newbook1);

                }//
                if (i == newbook.Length)
                {
                    i = i - 1;
                    textBox1.Text = newbook[i].Author;
                    textBox8.Text = newbook[i].id.ToString();
                    textBox2.Text = newbook[i].Title;
                    textBox3.Text = newbook[i].Genre;
                    textBox4.Text = newbook[i].Price;
                    textBox5.Text = newbook[i].PublishDate;
                    textBox6.Text = newbook[i].Description;
                    checkBox1.Checked = newbook[i].InStorage;
                }
                else
                {
                    textBox1.Text = newbook[i + 1].Author;
                    textBox8.Text = newbook[i + 1].id.ToString();
                    textBox2.Text = newbook[i + 1].Title;
                    textBox3.Text = newbook[i + 1].Genre;
                    textBox4.Text = newbook[i + 1].Price;
                    textBox5.Text = newbook[i + 1].PublishDate;
                    textBox6.Text = newbook[i + 1].Description;
                    checkBox1.Checked = newbook[i + 1].InStorage;
                }
            }
            catch
            {
                logErr.Error("Ошибка повторной записи при удалении записи");
            }
            try
            { 
                using (FileStream fs = new FileStream("BooksCatalog.xml", FileMode.OpenOrCreate))
                {
                    newbook = (Book[])formatter.Deserialize(fs);

                }
            }
            catch
            {
                logErr.Error("Ошибка чтения изменений после удалении записи");
            }
        }
    }
}
