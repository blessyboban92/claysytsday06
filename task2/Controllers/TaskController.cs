using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task2.DAL;
using task2.Models;

namespace task2.Controllers
{
    public class TaskController : Controller
    {
        // GET: Task
        entrydal edal = new entrydal();

        public ActionResult Index()
        {
            var entry_details = edal.GetAllDetails();
            if(entry_details.Count == 0)
            {
                TempData["InfoMessage"] = "Currenty clients are not available";
                return RedirectToAction("Index");
            }
            return View(entry_details);

        }

        // GET: Task/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var details = edal.GetDetailsById(id).FirstOrDefault();
                if (details == null)
                {
                    TempData["InfoMessage"] = "Currenty clients are not available with id" +id.ToString();
                    return RedirectToAction("Index");
                }
                return View(details);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return View();
                
            }
        }

        // GET: Task/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Task/Create
        [HttpPost]
        public ActionResult Create(Task task)
        {
            bool IsInserted = false;
            try
            {

                if (ModelState.IsValid)
                {
                    IsInserted = edal.InsertDetails(task);
                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Your Personal Details are  Saved Successfully....!";

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to save the details....!";

                    }
                    
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "e.Message";
                return View();
            }
        } 

        // GET: Task/Edit/5
        public ActionResult Edit(int id)
            
        {
            var details = edal.GetDetailsById(id).FirstOrDefault(); ;
            if(details == null)
            {
                TempData["InfoMessage"] = "This person  is not available with id " + id.ToString();
                return RedirectToAction("Index");
            }
            return View(details);
        }

        // POST: Task/Edit/5
        [HttpPost,ActionName("Edit")]
        public ActionResult UpdateDetails(Task task)
        {
            try
            {

                if (ModelState.IsValid)
                {
                   bool  IsUpdated = edal.UpdateDetails(task);
                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Details Updated Successfully....!";

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to update the details....!";

                    }

                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "e.Message";
                return View();
            }
        }

        // GET: Task/Delete/5
       
        public ActionResult Delete(int id)
        {
            try
            {
                var details = edal.GetDetailsById(id).FirstOrDefault(); ;
                if (details == null)
                {
                    TempData["InfoMessage"] = "This person  is not available with id " + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(details);

            }
            catch(Exception e)
            {
                TempData["ErrorMessage"] = "e.Message";
                return View();
            }
           
        }

        // POST: Task/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                string result = edal.DeleteDetails(id);
                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["Errormessage"] = result;
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {

                TempData["ErrorMessage"] = "e.Message";
                return View();
            }
        }
    }
}
