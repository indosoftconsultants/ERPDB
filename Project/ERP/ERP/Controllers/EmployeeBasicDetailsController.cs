﻿using AspNetCoreHero.ToastNotification.Abstractions;
using ERP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Controllers
{
    public class EmployeeBasicDetailsController : Controller
    {
        private readonly ILogger<EmployeeBasicDetailsController> _logger;
        private readonly INotyfService _notyf;

        public EmployeeBasicDetailsController(ILogger<EmployeeBasicDetailsController> logger, INotyfService notyf)
        {
            _logger = logger;
            _notyf = notyf;
        }

        // GET: EmployeeBasicDetailsController
        public ActionResult Index()
        {

            return View();
        }
      
        // GET: EmployeeBasicDetailsController/Details/5
        public ActionResult Details(int id)
        {            
            return View();
        }

        // GET: EmployeeBasicDetailsController/Create
        public ActionResult Create()
        {
            ViewBag.List_GetNamePrefix = DatatableToClass.CommonMethod.ConvertToList<NamePrefixModel>(Utility.GetData("List_GetNamePrefix"));
            ViewBag.List_GetMaritialStatus = DatatableToClass.CommonMethod.ConvertToList<MaritialStatusModel>(Utility.GetData("List_GetMaritialStatus"));
            ViewBag.List_GetGender = DatatableToClass.CommonMethod.ConvertToList<GenderModel>(Utility.GetData("List_GetGender"));
            ViewBag.List_GetEducation = DatatableToClass.CommonMethod.ConvertToList<EducationModel>(Utility.GetData("List_GetEducation"));
            ViewBag.CountryList = DatatableToClass.CommonMethod.ConvertToList<StateModel>(Utility.GetData("List_GetCountry"));
            ViewBag.StateList = DatatableToClass.CommonMethod.ConvertToList<StateModel>(Utility.GetData("List_GetState"));
            ViewBag.CityList = DatatableToClass.CommonMethod.ConvertToList<CityModel>(Utility.GetData("List_GetCity"));
            ViewBag.DepartmentList = DatatableToClass.CommonMethod.ConvertToList<DepartmentMasterModel>(Utility.GetData("List_GetDepartment"));
            ViewBag.DesignationList = DatatableToClass.CommonMethod.ConvertToList<DesignationMasterModel>(Utility.GetData("List_GetDesignation"));
            ViewBag.List_GetShiftSchedule = DatatableToClass.CommonMethod.ConvertToList<EmployeeOtherDetailsModel>(Utility.GetData("List_GetShiftSchedule"));

            EmployeeBasicDetailsModel obj = new EmployeeBasicDetailsModel();
            //obj.OtherDeails_EmployeeId = Convert.ToInt32(TempData["LastId"]);
            return View();
        }

        // POST: EmployeeBasicDetailsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        #region
        //public   ActionResult Create(EmployeeBasicDetailsModel emp)
        //{
        //    try
        //    {
        //        using (var client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri("http://192.168.10.34:81/api/EmployeeBasicDetails");

        //            //HTTP POST
        //            var postTask = client.PostAsJsonAsync<EmployeeBasicDetailsModel>(client.BaseAddress, emp);
        //            postTask.Wait();

        //            var result = postTask.Result;
        //            if (result.IsSuccessStatusCode)                  
        //            {
        //                _notyf.Success("Successfully Added");
        //                return RedirectToAction("create");
        //            }
        //            else
        //            {
        //                _notyf.Custom("Unable To Save", 5, "#FA5F55", "fa fa-exclamation-circle");
        //            }
        //        }

        //        ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
        //        return RedirectToAction("create");
        //        //return View("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        return View();
        //    }
        //}

        #endregion
        public async Task<ActionResult> Create(EmployeeBasicDetailsModel emp)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://192.168.10.34:81/api/EmployeeBasicDetails");
                    var stringContent = new StringContent(JsonConvert.SerializeObject(emp), Encoding.UTF8, "application/json");
                    HttpResponseMessage postTask = await client.PostAsync(Convert.ToString(client.BaseAddress), stringContent);
                    var result = postTask.Content;
                    var jsonString = postTask.Content.ReadAsStringAsync().Result;
                    int LastId = int.Parse(jsonString);
                    TempData["LastId"] = LastId;
                    if (LastId > 0)
                    {
                        _notyf.Success("Successfully Added" );
                        //return null;
                        return RedirectToAction("create");
                    }
                    else
                    {
                        _notyf.Custom("Unable To Save", 5, "#FA5F55", "fa fa-exclamation-circle");
                    }
                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return RedirectToAction("create");
            }
            catch (Exception ex)
            {               
                return RedirectToAction("create");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertOtherDetails(EmployeeBasicDetailsModel collection)
        {
            try
            {
                
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://192.168.10.34:81/api/EmployeeOtherDetails");


                    var postTask = client.PostAsJsonAsync<EmployeeBasicDetailsModel>(client.BaseAddress, collection);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        _notyf.Custom("Successfully Added", 5, "Green", "fa fa-check");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        _notyf.Custom("Unable To Save", 5, "#FA5F55", "fa fa-pencil");
                    }
                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertBankDetails(EmployeeBasicDetailsModel collection)
        {
            try
            {
                _notyf.Success("Bank Details Saved ");
                return RedirectToAction("Create", "EmployeeBasicDetails");
            }
            catch
            {
                return RedirectToAction("Create", "EmployeeBasicDetails");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertEducationDetails(EmployeeBasicDetailsModel collection)
        {
            try
            {
                _notyf.Success("Bank Details Saved ");
                return RedirectToAction("Create", "EmployeeBasicDetails");
            }
            catch
            {
                return RedirectToAction("Create", "EmployeeBasicDetails");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertExperienceDetails(EmployeeBasicDetailsModel collection)
        {
            try
            {
                _notyf.Success("Bank Details Saved ");
                return RedirectToAction("Create", "EmployeeBasicDetails");
            }
            catch
            {
                return RedirectToAction("Create", "EmployeeBasicDetails");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertSalaryDetails(EmployeeBasicDetailsModel collection)
        {
            try
            {
                _notyf.Success("Bank Details Saved ");
                return RedirectToAction("Create", "EmployeeBasicDetails");
            }
            catch
            {
                return RedirectToAction("Create", "EmployeeBasicDetails");
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeeBasicDetailsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeBasicDetailsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeBasicDetailsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
