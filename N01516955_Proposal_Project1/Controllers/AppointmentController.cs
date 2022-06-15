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
    public class AppointmentController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static AppointmentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44306/api/");
        }
        // GET: Appointment
        [HttpGet]
        public ActionResult List()
        {
            //objective: communicate with our animal data api to retrieve a list of animals
            //curl https://localhost:44324/api/Appointmentdata/listappointments


            string url = "AppointmentData/ListAppointments";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<AppointmentDto> Appointments = response.Content.ReadAsAsync<IEnumerable<AppointmentDto>>().Result;
            //Debug.WriteLine("Number of animals received : ");
            //Debug.WriteLine(animals.Count());


            return View(Appointments);
        }

        // GET: Appointment/Details/5
        public ActionResult Details(int id)
        {
        

            string url = "appointmentdata/findappointment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            AppointmentDto Appointment = response.Content.ReadAsAsync<AppointmentDto>().Result;

            return View(Appointment);
        }

        // GET: Appointment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Appointment/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Appointment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Appointment/Edit/5
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

        // GET: Appointment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Appointment/Delete/5
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
