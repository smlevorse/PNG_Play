using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PNG_parser
{
    class PNGParser
    {
        //fields
        private string filePath;    //The path to the PNG file
        private byte[] fileData;    //all of the bytes in the file
        
        //properties
        public byte[] FileData { get { return fileData; } }
        public string FilePath { 
            get { return filePath; } 
            set {
                filePath = value;
                LoadPNG(filePath);
            } 
        }

        //Constructors
        public PNGParser(string path = "")
        {
            filePath = path;
            LoadPNG(filePath);
        }

        //methods
        private void LoadPNG(string path)
        {
            //Open the file
            Stream fileStream = null;
            try
            {
                fileStream = File.OpenRead(filePath);
                BinaryReader input = new BinaryReader(fileStream);

                //create a new byte array the size of the file
                FileInfo fileInfo = new FileInfo(filePath);

                //read the data in the file
                fileData = input.ReadBytes((int)fileInfo.Length);

                //print the file
                PrintData();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error loading the PNG: " + e.Message);
            }
            finally
            {
                if(fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }

        public void PrintData()
        {
            Console.WriteLine("~~~~~~~~~~~Next Chunk~~~~~~~~~~~");
            //print the header
            int index = 0;
            Console.WriteLine("Header: should be 137 80 78 71 13 10 26 10");
            for (int i = 0; i < 8; i++)
            {
                Console.Write(fileData[index++] + " ");
            }
            Console.WriteLine("\n");

            while (index < fileData.Length)
            {
                //print the first chunk
                int length = 0;
                Console.WriteLine("Length:");
                for (int i = 0; i < 4; i++)
                {
                    length += (int)Math.Pow(256, (3 - i)) * fileData[index];
                    Console.Write(fileData[index++] + " ");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n" + length);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Type:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                for (int i = 0; i < 4; i++)
                {
                    Console.Write((char)fileData[index++]);
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\nData:");
                for (int i = 0; i < length; i++)
                {
                    Console.Write(String.Format("{0,4}", (fileData[index++] + " ")));
                    if ((i + 1) % 8 == 0)
                        Console.Write("  ");
                    if ((i + 1) % 16 == 0)
                        Console.WriteLine();
                }
                Console.WriteLine("\nCRC:");
                int crc = 0;
                for (int i = 0; i < 4; i++)
                {
                    crc += (int)Math.Pow(256, (3 - i)) * fileData[index];
                    Console.Write(fileData[index++] + " ");
                }
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\n" + crc);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine();
            }
        }
        
    }
}
