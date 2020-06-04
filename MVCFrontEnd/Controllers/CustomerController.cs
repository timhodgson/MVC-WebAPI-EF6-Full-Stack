using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IO;
using System.Text.Json;
using System.Text;
using MVCFrontEnd.App_Code;
using DataModel;

namespace MVCFrontEnd.Controllers
{
    public class CustomerController : Controller
    {
        // GET: List of Customers
        public ActionResult Index()
        {
            List<Customer> customers = null;

            try
            {
                string jsonString = string.Empty;
                string url = $"{Globals.BASE_URL}customers";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    jsonString = reader.ReadToEnd();
                }

                customers = JsonSerializer.Deserialize<List<Customer>>(jsonString);
                return View(customers);
            }
            catch
            {
                // Offline or error
            }
            finally
            {
            }
            return View();
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            #region Initialize
            string ResponseString = "";
            HttpWebResponse response = null;
            Customer customer = new Customer()
            {
                FirstName = collection["FirstName"],
                LastName = collection["LastName"],
                EmailAddress = collection["EmailAddress"],
                PhoneNumber = collection["PhoneNumber"]
            };
            #endregion

            if (ModelState.IsValid == false)
            {
                return View(customer);
            }


            try
            {
                string url = $"{Globals.BASE_URL}customers";


                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Accept = "application/json"; //"application/xml";
                request.Method = "POST";

                // serialize into JSON string
                var myContent = JsonSerializer.Serialize(customer);

                var data = Encoding.ASCII.GetBytes(myContent);

                request.ContentType = "application/json";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                response = (HttpWebResponse)request.GetResponse();

                ResponseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return RedirectToAction(nameof(Index));
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    response = (HttpWebResponse)ex.Response;
                    ResponseString = "Error: " + response.StatusCode.ToString();
                }
                else
                {
                    ResponseString = "Back-end System is not responding: " + ex.Status.ToString();
                }
            }
            return View(customer);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            Customer customer;

            string jsonString = string.Empty;
            string url = $"{Globals.BASE_URL}customers/{id}";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                jsonString = reader.ReadToEnd();
            }

            customer = JsonSerializer.Deserialize<Customer>(jsonString);
            return View(customer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}