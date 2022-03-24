// Author = MyGuy

using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;

namespace ProxChatAssistant
{
    /// <summary>
    /// Writes values to a text document asynchronously
    /// </summary>
    public class ProxWriter
    {

        /// <summary>
        /// Inserts the <paramref name="value"/> of <paramref name="label"/> at the specified <paramref name="row"/> in the text file at <paramref name="path"/>.
        /// <paramref name="row"/> starts from 0. -1 as <paramref name="row"/> value places new value at the end of the file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="label"></param>
        /// <param name="value"></param>
        /// <param name="row"></param>
        public static async Task NewValueAsync(string path, string label, dynamic value, int row)
        {
            List<string> text = new();

            string[] temp = await File.ReadAllLinesAsync(path, Encoding.UTF8);
            foreach (string str in temp)
            {
                if (str != "\n") { text.Add(str); }
            }

            if (row == -1) { text.Add(label + " = " + value); }
            else { text.Insert(row, label + " = " + value); }

            using (StreamWriter writer = File.CreateText(path))
            {
                foreach (string line in text)
                {
                    await writer.WriteLineAsync(line);
                }
                writer.Close();
            }

            await Task.Delay(0);
        }

        /// <summary>
        /// Edits the <paramref name="value"/> of the given <paramref name="label"/> in the text file at <paramref name="path"/>.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="label"></param>
        /// <param name="value"></param>
        public static async Task EditValueAsync(string path, string label, dynamic value)
        {
            List<string> text = new();
            int index = -1;

            string[] temp = await File.ReadAllLinesAsync(path, Encoding.UTF8);
            foreach (string str in temp)
            {
                if (str != "\n") { text.Add(str); }
            }

            for (int i = 0; i < text.Count; i++)
            {
                if (text[i].Contains(label))
                {
                    index = i;
                    break;
                }
            }

            if (index == -1) { text.Add(label + " = " + value); }
            else
            {
                string tempLine = text[index];
                string labelString = tempLine.Substring(0, tempLine.IndexOf("=") + 2);
                text[index] = labelString + value.ToString();
            }

            using (StreamWriter writer = File.CreateText(path))
            {
                foreach (string line in text)
                {
                    await writer.WriteLineAsync(line).ConfigureAwait(false);
                }
                writer.Close();
            }
            await Task.Delay(0);
        }

        /// <summary>
        /// Edits the <paramref name="values"/> of the given <paramref name="labels"/> in the text file at <paramref name="path"/>.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="labels"></param>
        /// <param name="values"></param>
        public static async Task EditValuesAsync(string path, List<string> labels, List<dynamic> values)
        {
            List<string> text = new();
            int index = -1;

            string[] temp = await File.ReadAllLinesAsync(path, Encoding.UTF8);
            foreach (string str in temp)
            {
                if (str != "\n") { text.Add(str); }
            }

            for (int j = 0; j < labels.Count; j++)
            {
                string label = labels[j];
                dynamic value = values[j];
                index = -1;

                for (int i = 0; i < text.Count; i++)
                {
                    if (text[i].Contains(label))
                    {
                        index = i;
                        break;
                    }
                }

                if (index == -1) { text.Add(label + " = " + value.ToString()); }
                else
                {
                    string tempLine = text[index];
                    string labelString = tempLine.Substring(0, tempLine.IndexOf('=') + 2);
                    text[index] = labelString + value.ToString();
                }
            }

            using (StreamWriter writer = File.CreateText(path))
            {
                foreach (string line in text)
                {
                    await writer.WriteLineAsync(line);
                }
                writer.Close();
            }
            await Task.Delay(0);
        }

        public static List<string> GetLabels(List<string> text)
        {
            List<string> temp = new();
            int equalIndex;
            string label;

            foreach (string line in text)
            {
                equalIndex = line.IndexOf('=');
                label = line.Substring(0, equalIndex - 2);
                Console.WriteLine("Label of [" + line + "]: " + label);
                temp.Add(label);
            }

            return temp;
        }

        public static List<dynamic> GetValues(List<string> text)
        {
            List<dynamic> temp = new();
            int equalIndex;
            dynamic value;

            foreach (string line in text)
            {
                equalIndex = line.IndexOf('=');
                value = line.Substring(equalIndex + 2, line.Length - 1);
                Console.WriteLine("Value of [" + line + "]: " + value);
                temp.Add(value);
            }

            return temp;
        }
    }
}


