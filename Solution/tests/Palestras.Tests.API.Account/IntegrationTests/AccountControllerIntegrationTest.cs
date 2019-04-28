using Newtonsoft.Json;
using Palestras.Infra.CrossCutting.Identity.Models.AccountViewModels;
using Palestras.Tests.API.Account.DTO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Palestras.Tests.API.Account.IntegrationTests
{
    public class AccountControllerIntegrationTest
    {
        public AccountControllerIntegrationTest()
        {
            Environment.CreateServer();
        }

        [Fact]
        public async Task AccountController_LoginUser_ReturnSuccess()
        {
            // Arrange
            var user = new LoginViewModel
            {
                Email = "ajala_oliveira@yahoo.com.br",
                Password = "@Ainter10"
            };

            var postContent = new StringContent(
                JsonConvert.SerializeObject(user), 
                Encoding.UTF8, 
                "application/json");

            // Act
            var response = await Environment.Client.PostAsync("api/v1/account", postContent);
            var userResult = JsonConvert.DeserializeObject<UserJsonDTO>(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode();
            var statusResult = userResult.success;
            Assert.True(statusResult);
        }

    }
}
