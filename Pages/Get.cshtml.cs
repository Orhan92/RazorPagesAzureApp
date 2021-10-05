using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmosWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace CosmosWebApp.Pages
{
    public class GetModel : PageModel
    {
        private readonly ICosmosDbService _cosmosDbService;
        private readonly ILogger<GetModel> _logger;
        public GetModel(ICosmosDbService cosmosDbService, ILogger<GetModel> logger)
        {
            _logger = logger;
            _cosmosDbService = cosmosDbService;
        }

        [BindProperty]
        public List<Song> Song { get; set; } = new List<Song>();

        public async Task<IActionResult> OnGet()
        {
            try
            {
                var result = await _cosmosDbService.GetItemsAsync("SELECT c.id, c.artist, c.title, c.timeCreated FROM c ORDER BY c.artist");

                foreach (var x in result)
                {
                    Song.Add(x);
                }
                _logger.LogInformation("SHOWING ALL SONGS IN ARCHIVE");
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception executed!");
                return RedirectToPage("/Index");
            }
        }
    }
}

