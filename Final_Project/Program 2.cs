using System;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using System.IO;
using System.Xml;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using System.Linq;
using System.Reflection.Metadata;
/*
Щоденник заміток:
Створіть клас Diary для представлення кожної замітки з властивостями, такими як заголовок, текст, дата створення тощо.
Реалізуйте клас NoteManager, який буде відповідати за додавання, видалення та редагування записів у щоденнику.
Використовуйте серіалізацію XML для зберігання та завантаження списку заміток.
*/
namespace Final_project
{
    public class Diary
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DateOfReg { get; set; }
    }
    public class NoteManager
    {
        private readonly string filePath;
        private XDocument xDocument;
        public NoteManager(string filePath)
        {
            this.filePath = filePath;
            if (File.Exists(filePath))
            {
                xDocument = XDocument.Load(filePath);
            }
            else
            {
                xDocument = new XDocument(new XElement("users"));
            }
        }
        public void Add(Diary diary)
        {
            XElement xElement = new XElement("user",
                new XElement("Title", diary.Title),
                new XElement("Text", diary.Text),
                new XElement("DateOfRegister", diary.DateOfReg.ToString("yyyy-MM-dd")));
            xDocument.Element("users").Add(xElement);
            xDocument.Save(filePath);
        }
        public void Remove(string name)
        {
            XElement user = FindByTitle(name);
            if (user != null)
            {
                user.Remove();
                xDocument.Save(filePath);
            }
        }
        public void Edit(string name, string newTitle, string newTxt)
        {
            XElement xElement = FindByTitle(name);
            if (xElement != null)
            {
            }
            /*
                XElement xElement1 = new XElement("user",
                    new XElement("Text", newTxt),
                    new XElement("Title", newTitle));
                xDocument.Element("users").Add(xElement1);
                xDocument.Save(filePath);
            */
        }
        private XElement FindByTitle(string name)
        {
            return xDocument.Descendants("user")// щоб знайти всі елементи <user> у документі XML, а потім застосовуємо метод FirstOrDefault
                .FirstOrDefault(x => (string)x.Element("name") == name);//щоб знайти перший елемент, для якого ім'я користувача співпадає з переданим іменем
        }
        public class Project
        {
            public static void Main()
            {
                NoteManager noteManager = new NoteManager("data.xml");
                Diary diary = new Diary
                {
                    Title = "1",
                    Text = "Сделать дз 1",
                    DateOfReg = new DateTime(2006, 04, 25),
                };
                //noteManager.Add(diary);
                //noteManager.Remove("1");
                //noteManager.Remove("2");
                //noteManager.Edit("Сделаь дз 2", "2");
            }
        }
    }
}