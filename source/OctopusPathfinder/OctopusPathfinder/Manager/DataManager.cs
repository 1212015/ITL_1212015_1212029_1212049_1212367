using OctopusPathfinder.Screens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OctopusPathfinder.Manager
{
    class DataManager
    {
        public static string sFileName = Environment.CurrentDirectory + "\\Level.txt";

        public static void ReadFile()
        {
            try
            {
                using (StreamReader streamInput = new StreamReader(sFileName))
                {
                    string strLevels = streamInput.ReadToEnd();

                    string[] arrStringLevels = strLevels.Split(' ', '\n', '\r');

                    for (int i = 0; i < arrStringLevels.Length; i++)
                    {
                        if (arrStringLevels[i] != "")
                        {
                            LevelMenu.lLevelsStar.Add(sbyte.Parse(arrStringLevels[i]));
                        }
                    }

                    streamInput.Close();
                }
            }
            catch (IOException)
            {
                StreamWriter streamInput = new StreamWriter(sFileName);

                streamInput.Close();
            }
        }

        public static void WriteToFile()
        {
            using (StreamWriter streamOutput = new StreamWriter(sFileName))
            {
                for (int i = 0; i < LevelMenu.lLevelsStar.Count - 1; i++)
                {
                    streamOutput.WriteLine(LevelMenu.lLevelsStar[i]);
                }

                streamOutput.Close();
            }
        }
    }
}
