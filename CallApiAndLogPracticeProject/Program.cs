using System;
using System.Net.Http;
using System.IO;

namespace CallApiAndLogPracticeProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = args[0];
            string outputFilePath = args[1];
            int numberToSubmit;

            // read file
            StreamReader reader = new StreamReader(inputFilePath);
            string inputFileText = reader.ReadToEnd();
            reader.Close();
            if (!int.TryParse(inputFileText, out numberToSubmit))
            {
                throw new Exception($"Number could not be read from file at {inputFilePath}");
            }

            // call API
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://numbersapi.com");

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"/{numberToSubmit}").Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("API failed to return a response");
            }

            var responseText = response.Content.ReadAsStringAsync().Result;

            // write file
            StreamWriter writer = new StreamWriter(outputFilePath);
            writer.WriteLine(responseText);
            writer.Close();

            Console.WriteLine("Success");
        }
    }
}
