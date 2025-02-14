using BlazorApp1.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorApp1.Pages
{
    public partial class Personnage
    {
        [Inject]
        public HttpClient _Client { get; set; }
        private List<string> noms = new List<string> { "zorro", "titi" };
        private string rep { get; set; }
        protected override async Task OnInitializedAsync()
        {
            HttpClient client = _Client;
            rep = await client.GetStringAsync("https://localhost:7171/api/personnage");

           // List<PersonnageModel> liste = await _Client.GetFromJsonAsync<List<PersonnageModel>>("Personnage");

            


        }
    }
}
