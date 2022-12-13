using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Text;
using System.IO;
using MongoDB.Driver;
using WebApplication10.Models;
using System.Configuration;
using System.Web.Http.Cors;

namespace WebApplication10.Controllers
{
    [EnableCors(origins: "https://localhost:5001,http://localhost:4200", headers: "*", methods: "*")]



    public class ClientController : ApiController
    {
        //public int GetConfigurationUrl()
        //{
        //    // var myConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"];

        //    var myConnectionString = int.Parse(ConfigurationManager.AppSettings["myConnectionString"]);


        //    return myConnectionString;
        //}
        // GET: api/Client
        public string Get(string value1, string value2)
        {
            string value3 = DateTime.Now.ToString();
            String[] message = new string[] { value1, value2, value3 };
            string valid = Validation(value1, value2);
            try
            {


                //List<MessageLog> result = new List<MessageLog>();
                if (valid == "Success")
                {
                    WriteToFile(message);
                    MessageLog ClientMessage = new MessageLog()
                    {
                        name = message[0],
                        message = message[1],
                        LogTime = DateTime.Now
                    };
                    var client = new MongoClient(ConfigurationManager.AppSettings["myConnectionString"]);

                    var database = client.GetDatabase("Client");
                    var table = database.GetCollection<MessageLog>("MessageLog");
                    // string connStr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

                    table.InsertOne(ClientMessage);
                    // result = table.Find(name => true).ToList();
                    return valid;
                    //return new string[] { "Invalid ClientName and Message " };
                }
                // return valid;
            }

            catch (Exception ex)
            {

            }
            return valid;

        }
       
        [Route("api/client/GetAllData")]
        public List<MessageLog> GetAllData()
        {
            List<MessageLog> result = new List<MessageLog>();
            try
            {

                var client = new MongoClient(ConfigurationManager.AppSettings["myConnectionString"]);
                var database = client.GetDatabase("Client");
                var table = database.GetCollection<MessageLog>("MessageLog");
                result = table.Find(name => true).ToList();
            }
            catch (Exception ex)
            {
                string[] ErrorMessage = new string[] { ex.Message };
                WriteToErrotLog(ErrorMessage);
                result = null;
            }
            return result;
        }

        private string Validation(string str1, string str2)
        {
            string val = "Success";
            string val1 = string.Empty;
            string val2 = string.Empty;
            // Regex objNotNaturalPattern = new Regex("[^0-9]");
            if (!Regex.Match(str1, @"^\d*[a-zA-Z]{1,}\d*$").Success)
            {
                val1 = "Invalid Client Name";
            }
            if (!Regex.Match(str2.Replace("<EOF>", ""), @"^[a-zA-Z]+$").Success)
            {
                val2 = "Invaid Message";
            }

            if (val1.Length > 0 || val2.Length > 0)
            {
                val = val1 + "  " + val2;
            }

            return val;
        }

        // GET: api/Client/5
        public string Get(int id)
        {
             return "value";
        }

        // POST: api/Client
        public void Post([FromBody]string value)
        {

        }

        // PUT: api/Client/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Client/5
        public void Delete(int id)
        {
        }
        public void WriteToFile(String[] Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Database";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Database\\Data Storage" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine("Client Name: " + Message[0]);
                    sw.WriteLine("Message: " + Message[1]);
                    sw.WriteLine("Date: " + Message[2]);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine("Client Name: " + Message[0]);
                    sw.WriteLine("Message: " + Message[1]);
                    sw.WriteLine("Date: " + Message[2]);
                }
            }
        }
        public void WriteToErrotLog(String[] Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Database";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Database\\Error Log" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine("Error Message:  " + Message[0]);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine("Error Message:  " + Message[0]);
                }
            }
        }
    }
}
