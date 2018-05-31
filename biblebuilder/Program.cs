using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsQuery;

namespace BibleBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var versions = new string[] {
                "NIV"
            };

            var books = new Dictionary<string, int>()
            {
                {"Genesis", 50},
                {"Exodus", 40},
                {"Leviticus", 27},
                {"Numbers", 36},
                {"Deuteronomy", 34},
                {"Joshua", 24},
                {"Judges", 21},
                {"Ruth", 4},
                {"1 Samuel", 31},
                {"2 Samuel", 24},
                {"1 Kings", 22},
                {"2 Kings", 25},
                {"1 Chronicles", 29},
                {"2 Chronicles", 36},
                {"Ezra", 10},
                {"Nehemiah", 13},
                {"Esther", 10},
                {"Job", 42},
                {"Psalms", 150},
                {"Proverbs", 31},
                {"Ecclesiastes", 12},
                {"Song of Solomon", 8},
                {"Isaiah", 66},
                {"Jeremiah", 52},
                {"Lamentations", 5},
                {"Ezekiel", 48},
                {"Daniel", 12},
                {"Hosea", 14},
                {"Joel", 3},
                {"Amos", 9},
                {"Obadiah", 1},
                {"Jonah", 4},
                {"Micah", 7},
                {"Nahum", 3},
                {"Habakkuk", 3},
                {"Zephaniah", 3},
                {"Haggai", 2},
                {"Zechariah", 14},
                {"Malachi", 4},
                {"Matthew", 28},
                {"Mark", 16},
                {"Luke", 24},
                {"John", 21},
                {"Acts", 28},
                {"Romans", 16},
                {"1 Corinthians", 16},
                {"2 Corinthians", 13},
                {"Galatians", 6},
                {"Ephesians", 6},
                {"Philippians", 4},
                {"Colossians", 4},
                {"1 Thessalonians", 5},
                {"2 Thessalonians", 3},
                {"1 Timothy", 6},
                {"2 Timothy", 4},
                {"Titus", 3},
                {"Philemon", 1},
                {"Hebrews", 13},
                {"James", 5},
                {"1 Peter", 5},
                {"2 Peter", 3},
                {"1 John", 5},
                {"2 John", 1},
                {"3 John", 1},
                {"Jude", 1},
                {"Revelation", 22}
            };

            books = new Dictionary<string, int>() { { "Isaiah", 66 } };

            foreach (var version in versions)
            {
                string folderPath = string.Format(@"F:/Documents/Bible/{0}", version);
                Console.WriteLine("Downloading {0} to {1}...", version, folderPath);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                foreach (var entry in books)
                {
                    string book = entry.Key;
                    int chapters = entry.Value;

                    string bookPath = string.Format(@"{0}/{1}", folderPath, book);

                    if (!Directory.Exists(bookPath))
                    {
                        Directory.CreateDirectory(bookPath);
                    }

                    for (var chapter = 1; chapter <= chapters; chapter++)
                    {
                        string chapterPath = string.Format(@"{0}/{1}.html", bookPath, chapter);
                        string uri = @"http://biblegateway.com/passage/?search={0} {1}&version={2}";
                        string html = "<html></html>";
                        Console.Write("Downloading {0} chapter {1}...", book, chapter);

                        try
                        {
                            uri = string.Format(uri, book, chapter, version);
                            var cq = CQ.CreateFromUrl(uri);
                            html = cq[".passage"].Html();
                        }
                        catch
                        {
                            Console.WriteLine(
                                "{0} Chapter {1} could not be downloaded from {2}.",
                                book, chapter, uri);
                            continue;
                        }

                        File.WriteAllText(chapterPath, html, Encoding.UTF8);
                        Console.WriteLine("[DONE]");
                    }
                }
            }
        }
    }
}
