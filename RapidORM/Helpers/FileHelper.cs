using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
    }
}
