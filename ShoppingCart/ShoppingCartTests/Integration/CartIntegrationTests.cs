using IdentityModel.Client;
using IdentityServer4.Models;
using Newtonsoft.Json;
using ShoppingCartModels.Carts;
using ShoppingCartTests.Helpers;
using System.Text;
using System;
using static IdentityModel.OidcConstants;
using ShoppingCartModels.Products;
using ShoppingCartApi.Controllers.Models;
using System.Net.Http;

namespace ShoppingCartTests.Integration
{
    [TestClass]
    public class CartIntegrationTests
    {
        [TestMethod]
        public async Task GetCart()
        {
            var client = await AuthHelper.GetAuthedClient();

            var response = await client.GetAsync("http://localhost:5267/ShoppingCart");
            if (!response.IsSuccessStatusCode)
            {
                Assert.Fail("Call to ShoppingCart failed!");
            }

            var cart = JsonConvert.DeserializeObject<Cart>(await response.Content.ReadAsStringAsync());
            Assert.IsNotNull(cart);

        }

        [TestMethod]
        public async Task AddCartItem()
        {
            var client = await AuthHelper.GetAuthedClient();

            var addItem = new CartAddItem{ ProductId = 1 };
            var json = JsonConvert.SerializeObject(addItem);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "http://localhost:5267/ShoppingCart/AddItem";

            var response = await client.PostAsync(url, data);
            if (!response.IsSuccessStatusCode)
            {
                Assert.Fail("Call to ShoppingCart failed!");
            }

            var cartItem = JsonConvert.DeserializeObject<CartItem>(await response.Content.ReadAsStringAsync());
            Assert.IsNotNull(cartItem);

        }

        [TestMethod]
        public async Task UpdateCartItem()
        {
            var client = await AuthHelper.GetAuthedClient();

            var updateItem = new CartUpdateItem { ProductId = 1, Quantity = 2 };
            var json = JsonConvert.SerializeObject(updateItem);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "http://localhost:5267/ShoppingCart/UpdateItem";

            var response = await client.PutAsync(url, data);
            if (!response.IsSuccessStatusCode)
            {
                Assert.Fail("Call to ShoppingCart failed!");
            }
        }

        [TestMethod]
        public async Task RemoveCartItem()
        {
            var client = await AuthHelper.GetAuthedClient();

            var request = new HttpRequestMessage(HttpMethod.Delete, "http://localhost:5267/ShoppingCart/RemoveItem?productId=1");
            var response = client.Send(request);
            if (!response.IsSuccessStatusCode)
            {
                Assert.Fail("Call to ShoppingCart failed!");
            }
        }
    }
}
