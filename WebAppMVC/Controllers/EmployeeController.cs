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
        public IActionResult Index()
        {
            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();
            ServiceResponseList<IReadOnlyList<EmployeeViewModel>> employees2 = new ServiceResponseList<IReadOnlyList<EmployeeViewModel>>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/empleado").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                //employees = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(data);
                employees2 = JsonConvert.DeserializeObject<ServiceResponseList<IReadOnlyList<EmployeeViewModel>>>(data);
            }
            return View(employees2.Data);
        }

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





        public IActionResult Delete(int id)
        {
            ServiceResponse<EmployeeViewModel> result = new ServiceResponse<EmployeeViewModel>();
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/empleado/" + id).Result;


            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<ServiceResponse<EmployeeViewModel>>(data);
            }
            return RedirectToAction("Index");

        }





    }
}
