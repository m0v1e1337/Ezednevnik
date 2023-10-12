using System;
using System.Collections.Generic;

namespace PracticalWork4
{
    internal class Program
    {
        static List<Note> notes = new List<Note>();
        static DateTime currentDate = DateTime.Now;
        static int cursorPosition = 1;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Выбрана дата: {currentDate.ToString("dd.MM.yyyy")}\t\tF1 для добавления заметки");

                int i = 1;
                foreach (Note note in notes)
                {
                    if (note.Date.Date == currentDate.Date)
                    {
                        Console.Write("  " + i + ". ");
                        Console.WriteLine(note.Name);
                        i++;
                    }
                }

                Console.SetCursorPosition(0, cursorPosition);
                Console.Write("->");

                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.F1:
                        AddNote();
                        break;
                    case ConsoleKey.Enter:
                        if (notes.Count > 0)
                        {
                            Note selectedNote = GetSelectedNote();
                            if (selectedNote != null)
                            {
                                DisplayNoteInfo(selectedNote);
                                Console.ReadKey();
                            }
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (cursorPosition > 1)
                            cursorPosition--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (cursorPosition < i - 1)
                            cursorPosition++;
                        break;
                    case ConsoleKey.LeftArrow:
                        currentDate = ChangeDate(-1);
                        cursorPosition = 1;
                        break;
                    case ConsoleKey.RightArrow:
                        currentDate = ChangeDate(1);
                        cursorPosition = 1;
                        break;
                    default:
                        Console.WriteLine("Ошибка ввода");
                        break;
                }
            }
        }

        static void AddNote()
        {
            Console.InputEncoding = System.Text.Encoding.GetEncoding("utf-16");
            Console.Clear();
            Note note = new Note();
            Console.WriteLine("Введите название");
            note.Name = Console.ReadLine();
            Console.WriteLine("Введите описание");
            note.Description = Console.ReadLine();
            Console.WriteLine("Введите дату (в формате день.месяц.год)");
            string dateString = Console.ReadLine();
            DateTime date;
            if (DateTime.TryParseExact(dateString, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out date))
            {
                note.Date = date;
            }
            else
            {
                Console.WriteLine("Неверный формат даты");
                return;
            }
            notes.Add(note);
        }

        static void DisplayNoteInfo(Note note)
        {
            Console.Clear();
            Console.WriteLine("Название: " + note.Name);
            Console.WriteLine("Описание: " + note.Description);
            Console.WriteLine("Дата: " + note.Date.ToString("dd.MM.yyyy"));
        }

 static Note GetSelectedNote()
        {
            int i = 1;
            foreach (Note note in notes)
            {
                if (note.Date.Date == currentDate.Date)
                {
                    if (i == cursorPosition)
                    {
                        return note;
                    }
                    i++;
                }
            }
            return null;
        }
        static DateTime ChangeDate(int change)
        {
            DateTime newDate = currentDate.AddDays(change);
            if (newDate.Date >= DateTime.Today)
            {
                return newDate;
            }
            return currentDate;
        }
    }

    public class Note
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}