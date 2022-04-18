using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Dynamic;
using System.Text;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult NewUser()
        {
            return View();
        }

        [HttpPost]
      public async Task<IActionResult> NewUser(UserModel user)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7011/api/");
            var stringUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var result = client.PostAsync("User", stringUser);
            result.Wait();
            return RedirectToAction("Index");
        }

        public IActionResult UserQuery()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserQuery(int id)
        {
            List<InvoiceModel> iList = new List<InvoiceModel>();

            UserModel userModel = new UserModel();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:7011/api/");
                var responseTalk = client.GetAsync("User" + "/" + id);
                var responseTalkList = client.GetAsync("Invoice?id="+id);

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


                return View("Detay",myDynamicmodel);
            }
            else
            {
                return View();
            }

        }
        [HttpGet]
        public IActionResult Detay(dynamic myDynamicmodel)
        {
            if(myDynamicmodel.user!= null)
            {

                return View(myDynamicmodel);
            }
            return View();
        }

        public IActionResult InvoiceQuery()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InvoiceQuery(int id)
        {
            InvoiceModel invoiceModel = new InvoiceModel();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:7011/api/");
                var responseTalk = client.GetAsync("Invoice" + "/" + id);
                responseTalk.Wait();
                var result = responseTalk.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var invoiceGet = readTask.Result;
                    invoiceModel = JsonConvert.DeserializeObject<InvoiceModel>(invoiceGet);
                    
                }
            }
            if (invoiceModel != null)
            {
                return View("DetailsInvoice", invoiceModel);
            }
            else
            {
                return View();
            }

        }
        [Route("Payment/{id:int}")]


        public IActionResult Payment(int id)
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
            return View("Index");
        }
        [HttpGet]
        public IActionResult DetailsInvoice(InvoiceModel invoiceModel)
        {
            if (invoiceModel != null)
            {
                return View(invoiceModel);
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult CloseUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CloseUser(int id)
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
                List<InvoiceModel> dontPaymentList = new List<InvoiceModel>();

                dynamic myDynamicmodel = new System.Dynamic.ExpandoObject();
              

                foreach (var item in iList)
                {
                    if (item.Status==false)
                    {
                        dontPaymentList.Add(item);
                    }
                }

                myDynamicmodel.user = userModel;
                //myDynamicmodel.invoice = iList;
                myDynamicmodel.dontpayment = dontPaymentList;
 

                return View(myDynamicmodel);
            }
            else
            {
                return View();
            }


        }

        [Route("CloseUserActive/{id:int}")]
         public IActionResult ClosedUser(int id)
        {
            UserModel user = new UserModel();
            
           
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7011/api/");
            var responseTalk = client.GetAsync("User" + "/" + id);
            responseTalk.Wait();
 
            var SecondResult = responseTalk.Result;
             if (SecondResult.IsSuccessStatusCode)
            {
                var readTask = SecondResult.Content.ReadAsStringAsync();
                 readTask.Wait();
                 var userGet = readTask.Result;
                user = JsonConvert.DeserializeObject<UserModel>(userGet);

                user.Id = id;
                user.Status = false;
                user.Deposit = 0;

            }
            var stringUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var result = client.PutAsync("User", stringUser);
            result.Wait();
            return RedirectToAction("Index");
         }
    }
}