using ShoppingCartModels.Carts;
using ShoppingCartTests.Helpers;

namespace ShoppingCartTests.Unit
{
    [TestClass]
    public class CartUnitTests
    {
        [TestMethod]
        public void Cart_ShouldFailValidationWhenProductIdIsEmpty()
        {
            var model = new CartItem { ProductId = null, Quantity = 1 };
            var validationResult = ModelValidationHelper.ValidateModel(model);
            Assert.IsTrue(validationResult.Count > 0);
        }
        [TestMethod]
        public void Cart_ShouldFailValidationWhenQuantityIsNotInRange()
        {
            var model = new CartItem { ProductId = 1, Quantity = 0 };
            var validationResult = ModelValidationHelper.ValidateModel(model);
            Assert.IsTrue(validationResult.Count > 0);

            model = new CartItem { ProductId = 1, Quantity = 200 };
            validationResult = ModelValidationHelper.ValidateModel(model);
            Assert.IsTrue(validationResult.Count > 0);
        }
    }
}
