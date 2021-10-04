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
        private readonly ILogger<CreateModel> _logger;
        public CreateModel(ICosmosDbService cosmosDbService, ILogger<CreateModel> logger)
        {
            _cosmosDbService = cosmosDbService;
            _logger = logger;
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
                _logger.LogInformation($"Added Song: {Music.Artist} - {Music.Title} | Created: {Music.Created}");
                return RedirectToPage("/Index");
        }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception caught!");
                return RedirectToPage("/Index");
            }
}
    }
}
