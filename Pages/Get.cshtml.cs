using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmosWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents.Linq;
using RestSharp;

namespace CosmosWebApp.Pages
{
    public class GetModel : PageModel
    {
        private readonly ICosmosDbService _cosmosDbService;
        public GetModel(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [BindProperty]
        public List<Song> Song { get; set; } = new List<Song>();

        public async Task<IActionResult> OnGet()
        {

            var result = await _cosmosDbService.GetItemsAsync("SELECT c.id, c.artist, c.title, c.timeCreated FROM c ORDER BY c.artist");

            foreach (var x in result)
            {
                Song.Add(x);
            }

            return Page();
        }
    }
}

