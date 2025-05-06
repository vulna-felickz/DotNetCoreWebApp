using DotNetCoreWebApp.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IValidator<ErrorViewModel> _validator;
        private readonly string _aesKey;

        public HomeController(ILogger<HomeController> logger, IValidator<ErrorViewModel> validator)
        {
            _logger = logger;
            _validator = validator;
            _aesKey = "TXkgU3VwZXIgU2VjdXJlIEFFUyBLZXkK"; 
        }

        public IActionResult Index()
        {
            MD5.Create();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoWork(string input)
        {
            //Do an action in the context of the logged in user
            return new JsonResult(new { result = "Success" });
        }

        [HttpGet]
        public ActionResult LogItem(string input)
        {
            _logger.LogWarning(input + " log in requested.");
            
            //Do an action in the context of the logged in user
            return new JsonResult(new { result = "Success" });
        }

        public IActionResult ProcessRequest(HttpContext ctx)
        {
            string format = ctx.Request.Form["nameformat"];

            string Surname = "test", Forenames="test", FormattedName;
            // BAD: Uncontrolled format string.
            FormattedName = string.Format(format, Surname, Forenames);

            // Encrypt the Name            
            var encrytpedNameFormat = Encrypt(FormattedName, Convert.FromBase64String(_aesKey), Encoding.ASCII.GetBytes("IV-goes-here"));


            return View(new ErrorViewModel { RequestId = FormattedName + encrytpedNameFormat });
        }

        [HttpGet]
        public IActionResult GetRequest(string nameformat)
        {
            string format = nameformat;
            string Surname = "test", Forenames="test", FormattedName;
            // BAD: Uncontrolled format string.
            FormattedName = string.Format(format, Surname, Forenames);

            return View(new ErrorViewModel { RequestId = FormattedName });
        }


        [HttpPost]
        public IActionResult ProcessRequest2([FromForm]string nameformatFromForm)
        {
            string format = nameformatFromForm;
            string Surname = "test", Forenames="test", FormattedName;
            // BAD: Uncontrolled format string.
            FormattedName = string.Format(format, Surname, Forenames);

            return View(new ErrorViewModel { RequestId = FormattedName });
        }

        [HttpPost]
        public IActionResult ProcessRequestVM(ErrorViewModel model)
        {
            ValidationResult result = _validator.Validate(model);

            if (!result.IsValid)
            {
                // Copy the validation results into ModelState.
                // ASP.NET uses the ModelState collection to populate 
                // error messages in the View.
                result.AddToModelState(this.ModelState);

                // re-render the view when validation failed.
                return View("Create", model);
            }

            string format = model.RequestId;
            string Surname = "test", Forenames="test", FormattedName;
            // BAD: Uncontrolled format string.
            FormattedName = string.Format(format, Surname, Forenames);

            return View(new ErrorViewModel { RequestId = FormattedName });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        internal static byte[] Encrypt(string message, byte[] key, byte[] iv)
        {
            using var aesAlg = Aes.Create();
            aesAlg.Key = key;
            aesAlg.IV = iv;

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(message); // Write all data to the stream.
            }
            return ms.ToArray();
        }
    }
}
