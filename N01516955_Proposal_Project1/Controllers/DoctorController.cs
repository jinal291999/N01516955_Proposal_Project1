using N01516955_Proposal_Project1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace N01516955_Proposal_Project1.Controllers
{
    public class DoctorController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static DoctorController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44306/api/");
        }
        

        // GET: Doctor
        [HttpGet]
        public ActionResult List()
        {
            //objective: communicate with our animal data api to retrieve a list of animals
            //curl https://localhost:44324/api/animaldata/listanimals


            string url = "DoctorData/ListDoctors";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<DoctorDto> Doctor = response.Content.ReadAsAsync<IEnumerable<DoctorDto>>().Result;
            //Debug.WriteLine("Number of animals received : ");
            //Debug.WriteLine(animals.Count());


            return View(Doctor);
        }

        // GET: Doctor/Details/5
        public ActionResult Details(int id)
        {


            string url = "Doctordata/findDoctor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DoctorDto Doctors = response.Content.ReadAsAsync<DoctorDto>().Result;

            return View(Doctors);
        }

        // GET: Doctor/Create
        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        // POST: Doctor/Create
        [HttpPost]
        public ActionResult Create(Doctor doctor)
        {
           
            string url = "Doctordata/addDoctor";


            string jsonpayload = jss.Serialize(doctor);

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

        // GET: Doctor/Edit/5
        public ActionResult Edit(int id)
        {
            DoctorViewModel doctorviewmodel = new DoctorViewModel();

            //the existing animal information
            string url = "Doctordata/findDoctor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DoctorDto SelectedDoctor = response.Content.ReadAsAsync<DoctorDto>().Result;
            doctorviewmodel.DoctorDto = SelectedDoctor;

            // all species to choose from when updating this animal
            //the existing animal information
           /* url = "speciesdata/listspecies/";
            response = client.GetAsync(url).Result;
            IEnumerable<DoctorDto> SpeciesOptions = response.Content.ReadAsAsync<IEnumerable<DoctorDto>>().Result;

            ViewModel.SpeciesOptions = SpeciesOptions;*/

            return View(doctorviewmodel);
        }
        // POST: Doctor/Edit/5
        [HttpPost]
        public ActionResult Update(int id, DoctorViewModel doctor)
        {
            try
            {
                Doctor doc1 = new Doctor();
                doc1.Id = doctor.DoctorDto.Id;
                doc1.Name = doctor.DoctorDto.Name;
                doc1.Description = doctor.DoctorDto.Description;
                doc1.Speciality = doctor.DoctorDto.Speciality;
                doc1.Experience = doctor.DoctorDto.Experience;

                string url = "animaldata/updateanimal/" + id;
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

        // GET: Doctor/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "Doctordata/findDoctor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DoctorDto selectedDoctor = response.Content.ReadAsAsync<DoctorDto>().Result;
            return View(selectedDoctor);
        }

        // POST: Doctor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "Doctordata/deleteDoctor/" + id;
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
