using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Models;
using Newtonsoft.Json;
using System.Text;

namespace WebAppMVC.Controllers
{
    public class EmployeeController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7207/api");
        HttpClient client = new HttpClient();

        public EmployeeController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }


        //READ
        public IActionResult Index(string nombre, string rfc, bool? estatus)
        {
            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();
            ServiceResponseList<IReadOnlyList<EmployeeViewModel>> employees2 = new ServiceResponseList<IReadOnlyList<EmployeeViewModel>>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/empleado?Nombre="+nombre+"&Rfc="+rfc+"&Estatus="+estatus).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                employees2 = JsonConvert.DeserializeObject<ServiceResponseList<IReadOnlyList<EmployeeViewModel>>>(data);
            }
            return View(employees2.Data);
        }

        
        public IActionResult Search( string nombre, string rfc,  bool estatus)
        {
            if( !string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(rfc) )
            {
                return RedirectToAction("Index", new
                {
                    nombre = nombre,
                    rfc = rfc,
                    estatus = estatus
                }
                );
            }
            return View();
        }



        //CREATE
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {
            string data = JsonConvert.SerializeObject(employeeViewModel);
            StringContent stringContent = new StringContent(data, Encoding.UTF8, "application/json" );

            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/empleado", stringContent ).Result;

            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }


            return View();
        }


        //EDIT
        public IActionResult Edit (int id)
        {
            ServiceResponse<EmployeeViewModelUpdate> employee = new ServiceResponse<EmployeeViewModelUpdate>();
            ServiceResponseList<IReadOnlyList<EmployeeViewModel>> employees2 = new ServiceResponseList<IReadOnlyList<EmployeeViewModel>>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/empleado/" + id).Result;


            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                employee = JsonConvert.DeserializeObject<ServiceResponse<EmployeeViewModelUpdate>>(data);
            }
            return View("Edit", employee.Data );

        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModelUpdate employeeViewModel)
        {
            string data = JsonConvert.SerializeObject(employeeViewModel);
            StringContent stringContent = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/empleado/" + employeeViewModel.Id, stringContent).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }


            return View("Edit", employeeViewModel);

        }




        //DELETE
        public ActionResult DeleteView(int id)
        {
            ServiceResponse<EmployeeViewModel> employee = new ServiceResponse<EmployeeViewModel>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/empleado/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                employee = JsonConvert.DeserializeObject<ServiceResponse<EmployeeViewModel>>(data);
                return View("Delete", employee.Data );
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            ServiceResponse<EmployeeViewModel> result = new ServiceResponse<EmployeeViewModel>();
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/empleado/" + id).Result;


            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<ServiceResponse<EmployeeViewModel>>(data);
                return RedirectToAction("Index");
            }
            return View("Delete");
        }





    }
}
