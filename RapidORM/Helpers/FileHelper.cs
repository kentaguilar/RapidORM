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
        /// <summary>
        /// Log a message to a user-defined directory. Default file extension is .log
        /// Folder will be generated on the build directory of the app
        /// </summary>
        /// <param name="content"></param>
        /// <param name="folder"></param>
        /// <param name="fileExtension"></param>
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

        /// <summary>
        /// Log a message to the log folder. Somewhat similar to LogHelper.Log
        /// </summary>
        /// <param name="content"></param>
        public static void WriteToFile(string content)
        {
            string time = DateTime.Now.ToString("MMddyyyy");
            string path = @"logs\" + time + ".log";
            string contentToAppend = string.Format("\n[{0}] - {1}", DateTime.Now.ToString("HH:mm:ss"), content);

            if (!File.Exists(path))
            {
                File.WriteAllText(path, contentToAppend);
            }
            else
            {
                File.AppendAllLines(path, new[] { contentToAppend });
            }
        }

        /// <summary>
        /// Create CSV file using a user-define list of string
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="contentList"></param>
        public static void CreateCsvFile(string filePath, List<string> contentList)
        {
            var csv = new StringBuilder();
            for (var i = 0; i < contentList.Count; i++)
            {
                csv.AppendLine(contentList[i]);
            }

            File.WriteAllText(filePath, csv.ToString());
        }

        /// <summary>
        /// Get file checksum based on a given path
        /// </summary>
        /// <param name="path"></param>
        public static string GetFileChecksum(string path)
        {
            using (var stream = new BufferedStream(File.OpenRead(path), 1200000))
            {
                SHA256Managed sha = new SHA256Managed();
                byte[] checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty);
            }
        }

        /// <summary>
        /// Generates a CSV file from windows form gridview
        /// </summary>
        /// <param name="gridResult"></param>
        /// <param name="outputDirectory"></param>
        /// <param name="filename"></param>
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

        /// <summary>
        /// Reads a given CSV file
        /// </summary>
        /// <param name="sourceCsvFile"></param>
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
