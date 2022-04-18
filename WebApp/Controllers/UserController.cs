using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Invoice()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Invoice(int id)
        {
            List<InvoiceModel> iList = new List<InvoiceModel>();

            UserModel userModel = new UserModel();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:7011/api/");
                var responseTalk = client.GetAsync("User" + "/" + id);
                var responseTalkList = client.GetAsync("Invoice?id=" + id);

                responseTalk.Wait();
                responseTalkList.Wait();

                var result = responseTalk.Result;
                var resultList = responseTalkList.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    var readTaskList = resultList.Content.ReadAsStringAsync();
                    readTask.Wait();
                    readTaskList.Wait();
                    var userGet = readTask.Result;
                    var invoiceList = readTaskList.Result;
                    userModel = JsonConvert.DeserializeObject<UserModel>(userGet);
                    iList = JsonConvert.DeserializeObject<List<InvoiceModel>>(invoiceList);


                }
            }
            if (userModel != null)
            {
                dynamic myDynamicmodel = new System.Dynamic.ExpandoObject();
                myDynamicmodel.user = userModel;
                myDynamicmodel.invoice = iList;


                return View(myDynamicmodel);
            }
            else
            {
                return View();
            }

        }
        [Route("UserPayment/{id:int}")]
        public IActionResult UserPayment(int id)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:7011/api/");
                var responseTalk = client.GetAsync("Invoice/Update/" + id);
                responseTalk.Wait();
                var result = responseTalk.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                }
            }
            return View("Invoice");
        }
    }
}
