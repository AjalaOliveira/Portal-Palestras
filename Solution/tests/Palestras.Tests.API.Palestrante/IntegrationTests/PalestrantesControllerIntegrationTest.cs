using Newtonsoft.Json;
using Palestras.Application.ViewModels;
using Palestras.Tests.API.Palestrante.DTO;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Palestras.Tests.API.Palestrante.IntegrationTests
{
    public class PalestrantesControllerIntegrationTest
    {
        public PalestrantesControllerIntegrationTest()
        {
            Environment.CreateServer();
        }

        [Fact]
        public async Task PalestrantesController_UpdatePalestrante()
        {
            // Arrange
            var login = await UserUtils.LoginUser(Environment.Client);

            var palestrante = new PalestranteViewModel
            {
                Id = Guid.Parse("6C032503-A520-4CB3-8A38-12AEF7293CA1"),
                Nome = "USADO PELO TESTE DE INTEGRAÇÃO EM: " + DateTime.Now + ".",
                MiniBio = "*** NÃO REMOVER ***",
                Url = "http://www.teste-integracao-api.com/"
            };

            var postContent = new StringContent(
                JsonConvert.SerializeObject(palestrante),
                Encoding.UTF8,
                "application/json");

            // Act
            var response = await Environment.Client.PutAsync("api/v1/palestrante-management", postContent);
            var userResult = JsonConvert.DeserializeObject<PalestranteJsonDTO>(await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode();
            var statusResult = userResult.success;
            Assert.True(statusResult);
        }
    }
}
