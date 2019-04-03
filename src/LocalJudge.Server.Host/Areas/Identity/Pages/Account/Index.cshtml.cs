using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using LocalJudge.Server.Host.APIClients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LocalJudge.Server.Host.Areas.Identity.Pages.Account
{
    [Authorize]
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpClientFactory _clientFactory;

        // private readonly IEmailSender _emailSender;

        public IndexModel(IHttpClientFactory clientFactory,UserManager<User> userManager)
        {
            _userManager = userManager;
            _clientFactory = clientFactory;
        }

        public User CurrentUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                CurrentUser = await _userManager.GetUserAsync(User);
            }
            else
            {
                var httpclient = _clientFactory.CreateClient();
                var client = new UsersClient(httpclient);
                try
                {
                    CurrentUser = await client.GetAsync(id);
                }
                catch
                {
                    CurrentUser = null;
                }
            }
            
            if (CurrentUser == null)
            {
                return NotFound($"Unable to load user with ID '{id ?? _userManager.GetUserId(User)}'.");
            }

            return Page();
        }
    }
}
