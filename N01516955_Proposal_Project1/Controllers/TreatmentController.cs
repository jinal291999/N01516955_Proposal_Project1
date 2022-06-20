using N01516955_Proposal_Project1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static N01516955_Proposal_Project1.Models.TreatmentDto;

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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Treatment/Create
        [HttpPost]
        public ActionResult Create(Treatment treatment)
        {

            string url = "TreatmentData/addtreatment";


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
            TreatmentViewModel treatmentviewmodel = new TreatmentViewModel();

            //the existing Doctor information
            string url = "Doctordata/findDoctor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            TreatmentDto SelectedDoctor = response.Content.ReadAsAsync<TreatmentDto>().Result;
            treatmentviewmodel.TreatmentDto = SelectedDoctor;

            return View(treatmentviewmodel);
        }

        // POST: Treatment/Edit/5
        [HttpPost]

        public ActionResult Update(int id, TreatmentViewModel treatment)
        {
            try
            {
                Treatment doc1 = new Treatment();
                doc1.Id = treatment.TreatmentDto.Id;
                doc1.Name = treatment.TreatmentDto.Name;
                doc1.Duration = treatment.TreatmentDto.Duration;
                doc1.Cost = treatment.TreatmentDto.Cost;
               

                string url = "TreatmemntData/updateTreatment/" + id;
                string jsonpayload = jss.Serialize(doc1);
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
            catch
            {
                return View();
            }
        }

        // GET: Treatment/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "Treatmentdata/findTreatment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            TreatmentDto selectedTreatment = response.Content.ReadAsAsync<TreatmentDto>().Result;
            return View(selectedTreatment);
        }

        // POST: Doctor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "Treatmentdata/deleteTreatment/" + id;
            HttpContent content = new StringContent("");
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
    }
}
