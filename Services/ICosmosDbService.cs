using CosmosWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosWebApp
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<Song>> GetItemsAsync(string query);
        Task AddItemAsync(Song song);
        //Task<Song> GetItemAsync(string id);
        //Task UpdateItemAsync(string id, Song song);
        //Task DeleteItemAsync(string id);
    }
}
