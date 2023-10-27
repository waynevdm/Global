using ShoppingCartApi.Controllers.Models;
using ShoppingCartTests.Helpers;

namespace ShoppingCartTests.Unit
{
    [TestClass]
    public class CartUnitTests
    {
        [TestMethod]
        public void Cart_ShouldFailValidationWhenQuantityIsNotInRange()
        {
            var model = new CartUpdateItem { ProductId = 1, Quantity = 0 };
            var validationResult = ModelValidationHelper.ValidateModel(model);
            Assert.IsTrue(validationResult.Count > 0);

            model = new CartUpdateItem { ProductId = 1, Quantity = 200 };
            validationResult = ModelValidationHelper.ValidateModel(model);
            Assert.IsTrue(validationResult.Count > 0);
        }
    }
}
