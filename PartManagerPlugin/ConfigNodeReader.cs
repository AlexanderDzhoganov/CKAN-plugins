using System;
using System.IO;
using System.Text;

namespace PartManagerPlugin
{
    public class ConfigNodeReader
    {
        public static ConfigNode StringToConfigNode(string inputString)
        {
            ConfigNode returnNode = new ConfigNode();
            using (StringReader sr = new StringReader(inputString))
            {
                int objectLevel = 0;
                string passName = "";
                StringBuilder passData = null;
                string previousLine = null;
                string currentLine = null;

                bool partStringFound = false;

                while ((currentLine = sr.ReadLine()) != null)
                {
                    string trimmedLine = currentLine.TrimStart(); //Take note of depth
                    if (trimmedLine.Contains("//"))
                    {
                        trimmedLine = trimmedLine.Substring(0, trimmedLine.IndexOf("//"));
                    }

                    if (trimmedLine.Length == 0)
                    {
                        continue;
                    }

                    if (trimmedLine == "{")
                    {
                        if (!partStringFound)
                        {
                            return null;
                        }
                    }
                    if (trimmedLine == "}")
                    {
                      
                    }
                  
                    if (trimmedLine == "PART")
                    {
                        partStringFound = true;
                    }

                    //We are reading a config node at our depth
                    if (trimmedLine.Contains(" = "))
                    {
                        string pairKey = trimmedLine.Substring(0, trimmedLine.IndexOf(" = "));
                        string pairValue = trimmedLine.Substring(trimmedLine.IndexOf(" = ") + 3);
                        returnNode.AddValue(pairKey, pairValue);
                    }
               
                    previousLine = trimmedLine;
                }
            }
            return returnNode;
        }

        public static ConfigNode FileToConfigNode(string inputFile)
        {
            string configNodeText = File.ReadAllText(inputFile);
            return StringToConfigNode(configNodeText);
        }
    }
}
