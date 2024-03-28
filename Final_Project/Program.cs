using System;
using System.IO;
using System.Xml;
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
        public void Add(string xmlFilePath)
        {
        }
        //TODO: public void Remove() { }
        //TODO: public void Edit() { }
    }
    public class Project
    {
        public void Main()
        {
            Diary diary = new Diary
            {
                Title = "Заголовок",
                Text = "Сделать дз",
                DateOfReg = new DateTime(2006, 04, 25),
            };
        }
    }
}