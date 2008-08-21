using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace TetrisGame.Drawing
{
    public class CellTheme:IEnumerable<Stream>
    {
        const int BufferLength = 4096;
        const string ThemeDirectoryName = "Theme";

        private List<Stream> imageStreams = new List<Stream>();
        private string fileName;
        public string Name { get; private set; }

        public CellTheme(string fileName)
        {
            Load(fileName);
        }

        public void Load(string fileName)
        {
            if (imageStreams.Count > 0)
            {
                imageStreams.Clear();
            }

            this.fileName = fileName;

            byte[] buffer = new byte[BufferLength];
            int length;
            using (XmlReader reader = XmlReader.Create(fileName))
            {
                while (reader.Read())
                {
                    if (reader.Name == "name")
                    {
                        Name = reader.Value;
                    }

                    if (reader.Name == "image")
                    {
                        reader.Read();
                        MemoryStream ms = new MemoryStream();
                        while ((length = reader.ReadContentAsBase64(buffer, 0, BufferLength)) > 0)
                        {
                            ms.Write(buffer, 0, length);
                        }
                        imageStreams.Add(ms);
                    }
                }
            }
        }

        public void Save(string fileName)
        {
            byte[] buffer = new byte[BufferLength];
            int length;
            using (XmlWriter writer = XmlWriter.Create(fileName))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("theme");
                writer.WriteAttributeString("name", Name);
                foreach (Stream stream in imageStreams)
                {
                    writer.WriteStartElement("image");
                    while ((length = stream.Read(buffer, 0, BufferLength)) > 0)
                    {
                        writer.WriteBase64(buffer, 0, length);
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public void Save()
        {
            Save(fileName);
        }

        public void Add(Stream stream)
        {
            imageStreams.Add(stream);
        }

        public void Remove(int index)
        {
            imageStreams.RemoveAt(index);
        }

        public static string[] GetAllThemeFileNames()
        {
            return Directory.GetFiles(GetThemePath());
        }

        private static string GetThemePath()
        {
#if DEBUG
            string path = TetrisUtility.GetTetrisPath() + @"\..\..\" + ThemeDirectoryName;
#else
            string path=TetrisUtility.GetTetrisPath()+"\\"+ThemeDirectoryName;
#endif
            return path;
        }

        public IEnumerator<Stream> GetEnumerator()
        {
            return imageStreams.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
