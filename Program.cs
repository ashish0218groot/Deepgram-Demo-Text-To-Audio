using System;
using System.IO;
using System.Threading.Tasks;
using Deepgram;
using Deepgram.Models.Speak.v1;
using Deepgram.Logger;

namespace SampleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Deepgram.Library.Initialize();

            string apiKey = "3c485e2536882943816647a068cef6821e141dc";
            var deepgramClient = new SpeakClient(apiKey);

            // Specify the desired directory to save the audio file
            string outputDirectory = @"D:\Ashish\POC\DeepgramPOCwithDotNet\1Oct24\DeepgramDemoApp\audio";
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string outputFilePath = Path.Combine(outputDirectory, $"test_{timestamp}.mp3");

            var response = await deepgramClient.ToFile(
               new TextSource("Hi, I am Parav, I am your virtual assitant,How may i help you today!"),
               outputFilePath,
               new SpeakSchema()
               {
                   Model = "aura-asteria-en",
               });

            if (response != null)
            {
                Console.WriteLine($"Audio file created successfully: {response.Filename}");
                Console.WriteLine($"File saved at: {outputFilePath}");
            }
            else
            {
                Console.WriteLine("Failed to create audio file.");
            }

            Console.WriteLine($"Current Directory: {Directory.GetCurrentDirectory()}");

            Console.ReadKey();

            Library.Terminate();
        }
    }
}
