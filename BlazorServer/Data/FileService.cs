using Microsoft.Net.Http.Headers;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorServer.Data
{
    public class FileService
    {
        private readonly HttpClient httpClient;
        private string urlBytes = "https://localhost:44329/api/files/bytes";
        private string urlFormFile = "https://localhost:44329/api/files/formfile";

        public FileService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task SendImageBase64(string base64)
        {
            if (base64 == null) { return; }
            var data = new { TestProperty = "This is a test", FileBase64 = base64 };
            var dataJson = JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(urlBytes, stringContent);
            Console.WriteLine($"Sucess: {response.IsSuccessStatusCode}");
            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine(body);
        }

        public async Task SendImageIFormFile(string base64)
        {
            if (base64 == null) { return; }
            var requestContent = new MultipartFormDataContent();
            
            var fileBytes = Convert.FromBase64String(base64);
            var imageContent = new ByteArrayContent(fileBytes);
            imageContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("image/jpeg");

            var testNameContent = new StringContent("this is a test value");

            requestContent.Add(testNameContent, "TestProperty");
            requestContent.Add(imageContent, "file", "image.jpg");

            var response = await httpClient.PostAsync(urlFormFile, requestContent);
            Console.WriteLine($"Sucess: {response.IsSuccessStatusCode}");
            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine(body);
        }
    }
}
