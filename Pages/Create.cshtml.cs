using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmosWebApp;
using CosmosWebApp.Models;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CosmosWebApp.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ICosmosDbService _cosmosDbService;
        private readonly ILogger _logger;
        public CreateModel(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [BindProperty]
        public Song Music { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                Music.Id = Guid.NewGuid().ToString();
                Music.Created = DateTime.Now;
                await _cosmosDbService.AddItemAsync(Music);
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                return RedirectToPage("/Index");
            }

        }
    }
}
