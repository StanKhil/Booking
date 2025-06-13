using Booking.Data;
using Booking.Data.Entities;
using Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Models
{
    [TestClass]
    public sealed class ImageModelTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            DataContext context = new();
            ImageModel model = new(context);
            Assert.IsNotNull(model, "The constructor must take non-null DataContext instance");
        }
        [TestMethod]
        public void ImageCreationTest()
        {
            DataContext context = new();
            ImageModel model = new(context);

            // A single image
            var creationResultSingle = model?.CreateImageAsync(new Guid(), "url");
            Assert.IsNotInstanceOfType<ItemImage>(creationResultSingle?.Result);
            Assert.IsInstanceOfType<Task<string>>(creationResultSingle);

            // Multiple images
            var creationResultMultiple = model?.CreateImages(new Guid(), [ "url1", "url2", "url3" ]);
            Assert.IsNotInstanceOfType<ItemImage>(creationResultMultiple?.Result);
            Assert.IsInstanceOfType<string>(creationResultMultiple?.Result);
        }
        [TestMethod]
        public void ImageLoadingTest()
        {
            DataContext context = new();
            ImageModel model = new(context);

            var result = model?.LoadImageAsync("slug", "path");
            Assert.IsNotInstanceOfType<ItemImage>(result?.Result);
            Assert.IsInstanceOfType<Task<string>>(result);
        }
    }
}
