using N01516955_Proposal_Project1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace N01516955_Proposal_Project1.Controllers
{
    public class TreatmentController : Controller
    {
        // GET: Treatment
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static TreatmentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44306/api/");
        }

        // GET: Treatment/Details/5
        public ActionResult List()
        {
            //objective: communicate with our animal data api to retrieve a list of animals
            //curl https://localhost:44324/api/Treatmentdata/listTreatment


            string url = "TreatmentData/ListTreatment";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<TreatmentDto> Appointment = response.Content.ReadAsAsync<IEnumerable<TreatmentDto>>().Result;
            //Debug.WriteLine("Number of animals received : ");
            //Debug.WriteLine(animals.Count());


            return View(Appointment);
        }
        public ActionResult Details(int id)
        {


            string url = "Treatmentdata/findTreatment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            TreatmentDto Treatment = response.Content.ReadAsAsync<TreatmentDto>().Result;

            return View(Treatment);
        }
        // GET: Treatment/Create
        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        // POST: Treatment/Create
        [HttpPost]
        public ActionResult Create(Treatment treatment)
        {

            string url = "TreatmentData/addTreatment";


            string jsonpayload = jss.Serialize(treatment);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }


        }
        // GET: Treatment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Treatment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Treatment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Treatment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
