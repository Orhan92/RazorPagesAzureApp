using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmosWebApp.Models;
using Microsoft.Azure.Cosmos;

namespace CosmosWebApp.Services
{

    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(Song song)
        {
            await this._container.CreateItemAsync<Song>(song, new PartitionKey(song.Id));
        }
        public async Task<IEnumerable<Song>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Song>(new QueryDefinition(queryString));
            List<Song> results = new List<Song>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        //public async Task DeleteItemAsync(string id)
        //{
        //    await this._container.DeleteItemAsync<Song>(id, new PartitionKey(id));
        //}

        //public async Task<Song> GetItemAsync(string id)
        //{
        //    try
        //    {
        //        ItemResponse<Song> response = await this._container.ReadItemAsync<Item>(id, new PartitionKey(id));
        //        return response.Resource;
        //    }
        //    catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        //    {
        //        return null;
        //    }

        //}


        //public async Task UpdateItemAsync(string id, Song song)
        //{
        //    await this._container.UpsertItemAsync<Song>(song, new PartitionKey(id));
        //}
    }
}
