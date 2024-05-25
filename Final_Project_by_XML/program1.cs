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
using System.Net.Http.Headers;
namespace final_project
{
    public class Note
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DateOfReg { get; set; }
    }
    public class Diary
    {
        public List<Note> notes { get; set; }
        public Diary()
        {
            notes = new List<Note>();
        }
    }
    public class NoteManager
    {
        private string FilePath;
        public NoteManager(string filePath)
        {
            this.FilePath = filePath;
        }
        public void AddNote(Diary diary, Note note)
        {
            diary.notes.Add(note);
        }
        public void RemoveByIndex(Diary diary, int index)
        {
            if (index >= 0 && index < diary.notes.Count)
            {
                diary.notes.RemoveAt(index);
            }
            else
            {
                Console.WriteLine("По этому индексу нету ничего");
            }
        }

        public void Load(Diary diary)
        {
            if (File.Exists(FilePath))
            {
                XDocument doc = XDocument.Load(FilePath);
                foreach (XElement noteElement in doc.Root.Elements("Note"))
                {
                    Note note = new Note
                    {
                        Title = noteElement.Element("Title")?.Value,
                        Text = noteElement.Element("Text")?.Value,
                        DateOfReg = DateTime.Parse(noteElement.Element("DateOfReg")?.Value)
                    };
                    diary.notes.Add(note);
                }
            }
        }
        public void Save(Diary diary)
        {
            XDocument doc = new XDocument(new XElement("Notes"));
            foreach (Note note in diary.notes)
            {
                XElement noteElement = new XElement("Note",
                    new XElement("Title", note.Title),
                    new XElement("Text", note.Text),
                    new XElement("DateOfReg", note.DateOfReg.ToString("yyyy-MM-dd HH:mm:ss"))
                );
                doc.Root.Add(noteElement);
            }
            doc.Save(FilePath);
        }
        public void EditByIndex(Diary diary, int index, Note editnote)
        {
            if (index >= 0 && index < diary.notes.Count)
            {
                diary.notes[index] = editnote;
            }
            else
            {
                Console.WriteLine("Index didnt find");
            }
        }/*
        public void EditNoteByTitle(Diary diary, string title, Note editedNote)
        {
            Note noteToEdit = diary.notes.FirstOrDefault(n => n.Title == title);
            if (noteToEdit != null)
            {
                noteToEdit.Title = editedNote.Title;
                noteToEdit.Text = editedNote.Text;
            }
            else
            {
                Console.WriteLine("Index didnt find");
            }
        }
        */
    }
    public class Project
    {
        public static void Main()
        {
            NoteManager noteManager = new NoteManager("data.xml");
            Note note = new Note
            {
                Title = "ДЗ",
                Text = "Сделать дз"
            };
            Diary diary = new Diary();
            noteManager.Load(diary);
            //noteManager.AddNote(diary, note);
            Note newnote = new Note
            {
                Title = "New title",
                Text = "new text"
            };
            noteManager.EditByIndex(diary, 0, newnote);
            noteManager.Save(diary);
        }
    }
}