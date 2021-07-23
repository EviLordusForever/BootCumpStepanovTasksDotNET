﻿using ConsoleAppTask1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ConsoleAppTask1.Services
{
    public class SerializationService
    {
        private static int id;
        public string FilePath { get; set; } = "text.json";

        public void SaveMessage(string encryptedMessage)
        {
            id++;

            var message = new Message
            {
                Id = id,
                Data = encryptedMessage,
                DateUpdated = DateTime.Now,
            };
            var messages = GetMessages();
            messages.Add(message);
            var json = JsonSerializer.Serialize(messages);

            File.WriteAllText(FilePath, json);
        }

        public List<Message> GetMessages()
        {
            CreateFile();

            string json;

            tryAgain:
            try
            {
                json = File.ReadAllText(FilePath);
            }
            catch
            {
                goto tryAgain;
            }

            if (string.IsNullOrEmpty(json))
            {
                return new List<Message>();
            }
            return JsonSerializer.Deserialize<List<Message>>(json);
        }

        private void CreateFile()
        {
            if (!File.Exists(FilePath))
            {
                File.Create(FilePath);
            }

            while (!File.Exists(FilePath)) { };
        }

        public SerializationService()
        {
            id = Convert.ToInt32(File.ReadAllText(Environment.CurrentDirectory + "\\lastId.txt"));
        }
    }
}
