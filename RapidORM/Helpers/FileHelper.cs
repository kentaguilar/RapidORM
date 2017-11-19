using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Configuration;

namespace RapidORM.Helpers
{
    public class FileHelper
    {
        public enum FileType
        {
            CSV,
            XLSX,
            TXT
        }

        public static void WriteFileToFolder(string content, string folder, string fileExtension = "log")
        {
            string currentDate = DateTime.Now.ToString("MMddyyyy");
            string path = folder + @"\" + currentDate + "." + fileExtension;

            if (!File.Exists(path))
            {
                File.WriteAllText(path, content + "\r\n");
            }
            else
            {
                File.AppendAllLines(path, new[] { content });
            }
        }

        public static void WriteToFile(string content)
        {
            string directory = @"logs\";
            string time = DateTime.Now.ToString("MMddyyyy");
            string path = directory + time + ".log";
            string contentToAppend = string.Format("\n[{0}] - {1}", DateTime.Now.ToString("HH:mm:ss"), content);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(path))
            {
                File.WriteAllText(path, contentToAppend);
            }
            else
            {
                File.AppendAllLines(path, new[] { contentToAppend });
            }
        }

        public static void CreateCsvFile(string filePath, List<string> csvContentArr)
        {
            var csv = new StringBuilder();
            for (var i = 0; i < csvContentArr.Count; i++)
            {
                csv.AppendLine(csvContentArr[i]);
            }

            File.WriteAllText(filePath, csv.ToString());
        }

        public static string GetFileChecksum(string path)
        {
            using (var stream = new BufferedStream(File.OpenRead(path), 1200000))
            {
                SHA256Managed sha = new SHA256Managed();
                byte[] checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty);
            }
        }

        public static void GenerateCsvFromDataGridView(DataGridView gridResult, string outputDirectory, string filename)
        {
            string outputFile = outputDirectory + filename;
            try
            {
                if (!Directory.Exists(outputDirectory))
                {
                    Directory.CreateDirectory(outputDirectory);
                }

                if (gridResult.RowCount > 0)
                {
                    string value = string.Empty;
                    DataGridViewRow gridRow = new DataGridViewRow();

                    StreamWriter streamWriterOut = new StreamWriter(outputFile, false, Encoding.Unicode);

                    //write header rows to csv
                    for (int i = 0; i <= gridResult.Columns.Count - 1; i++)
                    {
                        if (i > 0)
                        {
                            streamWriterOut.Write("\t");
                        }
                        streamWriterOut.Write(gridResult.Columns[i].HeaderText);
                    }

                    streamWriterOut.WriteLine();

                    //write DataGridView rows to csv
                    for (int j = 0; j <= gridResult.Rows.Count - 1; j++)
                    {
                        if (j > 0)
                        {
                            streamWriterOut.WriteLine();
                        }

                        gridRow = gridResult.Rows[j];
                        for (int i = 0; i <= gridResult.Columns.Count - 1; i++)
                        {
                            if (i > 0)
                            {
                                streamWriterOut.Write("\t");
                            }

                            value = gridRow.Cells[i].Value.ToString();
                            value = value.Replace(',', ' ');
                            value = value.Replace(Environment.NewLine, " ");

                            streamWriterOut.Write(value);
                        }
                    }
                    streamWriterOut.Close();
                }
            }
            catch (Exception exceptionObj)
            {
                throw new Exception(exceptionObj.ToString());
            }
        }

        public static List<string> ReadCsvFile(string sourceCsvFile)
        {            
            using (var reader = new StreamReader(sourceCsvFile))
            {
                List<string> urlList = new List<string>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    urlList.Add(values[0]);
                }

                return urlList;
            }
        }
    }
}
