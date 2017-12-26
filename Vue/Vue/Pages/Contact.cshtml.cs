using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Vue.Pages
{
    public class ContactModel : PageModel
    {
        public string Message { get; set; }
        public string Subject { get; set; }
        public string Sender { get; set; }
       
    }
}
