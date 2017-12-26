using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Vue.Pages;

namespace Vue.Controllers
{
    public class FeedbackController : Controller
    {

        public class ContactController : Controller
        {

            readonly IOptions<EmailProperties> _EmailProperties;

            public ContactController(IOptions<EmailProperties> EmailProperties)
            {
                _EmailProperties = EmailProperties;
            }

            [HttpGet]
            public IActionResult Index()
            {
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> Send(ContactModel contactModel)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        Feedback EmailService = new Feedback();
                        await EmailService.SendEmailAsync(
                            contactModel.Sender,
                            contactModel.Subject,
                            contactModel.Message,
                            _EmailProperties.Value.ContactEmail,
                            _EmailProperties.Value.ContactPassword,
                            int.Parse(_EmailProperties.Value.SmtpPort),
                            _EmailProperties.Value.SmtpServer);

                        TempData["result"] = "Message sent.";
                        return RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        TempData["result"] = @"Error: " + e.Message;
                    }
                }
                return RedirectToAction("Index");
            }

        }

    }
}