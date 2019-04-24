using Palestras.Tests.API.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Palestras.Tests.API.IntegrationTests
{
    public class PalestrantesControllerIntegrationTest
    {
        public PalestrantesControllerIntegrationTest()
        {
            Environment.CreateServer();
        }

        [Fact]
        public async Task PalestrasController_CreateNew_ReturnSuccess()
        {
            // Arrange
            var login = await UserUtils.LoginUser(Environment.Client);

            //Depoir de logar, chamar o teste de criação de Palestrante

        }
    }
}
