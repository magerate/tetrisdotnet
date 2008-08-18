using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using System.IO;

namespace TetrisGame.Drawing
{
    public class ImageBrushScheme
    {
        const int BufferLength = 4096;

        public IEnumerable<Image> Load(string fileName)
        {
            byte[] buffer = new byte[BufferLength];
            MemoryStream ms;
            int length;
            using (XmlReader reader = XmlReader.Create(fileName))
            {
                while (reader.Read())
                {
                    if (reader.Name == "image")
                    {
                        reader.Read();
                        ms = new MemoryStream();
                        while ((length = reader.ReadContentAsBase64(buffer, 0, BufferLength)) > 0)
                        {
                            ms.Write(buffer, 0, length);
                        }
                        yield return new Bitmap(ms);
                    }
                }
            }
        }

        public void Save(IEnumerable<Image> images)
        {
        }

        public void Save(IEnumerable<string> files, string schemeName)
        {
            byte[] buffer = new byte[BufferLength];
            int length;
            using (XmlWriter writer = XmlWriter.Create(schemeName))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("ImageBrushScheme");
                foreach (string file in files)
                {
                    writer.WriteStartElement("image");
                    using (FileStream fs = File.OpenRead(file))
                    {
                        while ((length = fs.Read(buffer, 0, BufferLength)) > 0)
                        {
                            writer.WriteBase64(buffer, 0, length);
                        }
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public void Convert(string directory, string schemeName)
        {
            Save(Directory.GetFiles(directory), schemeName);
        }
    }
}
