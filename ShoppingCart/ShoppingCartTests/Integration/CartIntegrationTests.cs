using IdentityModel.Client;
using IdentityServer4.Models;
using Newtonsoft.Json;
using ShoppingCartModels.Carts;
using ShoppingCartTests.Helpers;
using static IdentityModel.OidcConstants;

namespace ShoppingCartTests.Integration
{
    [TestClass]
    public class CartIntegrationTests
    {
        [TestMethod]
        public async Task GetCart()
        {
            var client = await AuthHelper.GetAuthedClient();

            // Send a request to our Protected API
            var response = await client.GetAsync("http://localhost:5267/ShoppingCart");
            if (!response.IsSuccessStatusCode)
            {
                Assert.Fail("Call to ShoppingCart failed!");
            }

            // All good! We have the data here
            var cart = JsonConvert.DeserializeObject<Cart>(await response.Content.ReadAsStringAsync());
            Assert.IsNotNull(cart);

        }
    }
}
