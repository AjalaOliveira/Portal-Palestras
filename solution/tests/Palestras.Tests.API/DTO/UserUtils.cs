using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Palestras.Infra.CrossCutting.Identity.Models.AccountViewModels;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Palestras.Tests.API.DTO
{
    public class UserUtils
    {
        public static async Task<UserJsonDTO.Data> LoginUser(HttpClient client)
        {
            var user = new LoginViewModel
            {
                Email = "ajala_oliveira@yahoo.com.br",
                Password = "@Ainter10"
            };

            var postContent = new StringContent(JsonConvert.SerializeObject(user),Encoding.UTF8,"application/json");
            var response = await client.PostAsync("api/v1/account", postContent);

            var postResult = await response.Content.ReadAsStringAsync();
            var userResult = JsonConvert.DeserializeObject<UserJsonDTO>(postResult);

            return userResult.data;
        }
    }
}
