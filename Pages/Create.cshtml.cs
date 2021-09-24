using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmosWebApp;
using CosmosWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CosmosWebApp.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ICosmosDbService _cosmosDbService;
        public CreateModel(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [BindProperty]
        public Song Music { get; set; }
        public async Task <IActionResult> OnPostAsync()
        {
            if (Music.Artist != null  || Music.Title != null)
            {
                Music.Id = Guid.NewGuid().ToString();
                Music.Created = DateTime.Now;
                await _cosmosDbService.AddItemAsync(Music);           
            }
                return RedirectToPage("/Index");         
        }
    }
}
