﻿using System;
using System.IO;
using System.Collections.Generic;

namespace Rubix
{
    class Config
    {
        public static Dictionary<string, string[]> options;
        public static string configFilePath;

        public static void Load(string _configFilePath)
        {
            configFilePath = _configFilePath;
            options = new Dictionary<string, string[]>();

            if (!File.Exists(configFilePath)) // Create an empty config file if none is found
            {
                File.Create(configFilePath);
                return;
            }

            string[] configCommands = File.ReadAllLines(configFilePath);

            foreach (string command in configCommands)
            {
                if (command.Length < 2) // Skip any invalid config commands
                    continue;

                List<string> args = new List<string>(command.Split(' '));
                string option = args[0];
                args.RemoveAt(0);
                options[option] = args.ToArray();
            }
        }

        public static bool Exists(string option)
        {
            return options.ContainsKey(option);
        }

        public static void SetOption(string option, string[] value)
        {
            options[option] = value;
        }

        public static string[] GetOption(string option)
        {
            return options[option];
        }

        public static void Save()
        {
            string result = "";
            foreach (KeyValuePair<string, string[]> configCommand in options)
            {
                result += configCommand.Key + " ";
                foreach (string argument in configCommand.Value)
                {
                    result += argument + " ";
                }
                result += "\n";
            }

        }
    }
}