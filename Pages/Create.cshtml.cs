using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmosWebApp;
using CosmosWebApp.Models;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CosmosWebApp.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ICosmosDbService _cosmosDbService;
        private TelemetryClient _aiClient;
        public CreateModel(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }
        public CreateModel(TelemetryClient aiClient)
        {
            _aiClient = aiClient;
        }

        [BindProperty]
        public Song Music { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            Music.Id = Guid.NewGuid().ToString();
            Music.Created = DateTime.Now;
            await _cosmosDbService.AddItemAsync(Music);
            _aiClient.TrackEvent("AddedSong");
            return RedirectToPage("/Index");

        }
    }
}
