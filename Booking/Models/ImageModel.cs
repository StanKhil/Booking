using Booking.Data.Entities;
using Booking.Data;
using System.Windows.Forms;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Booking.Models
{
    public class ImageModel
    {
        DataContext context;
        public ItemImage? itemImage;
        public ImageModel(DataContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateImageAsync(Guid realtyId, string url)
        {
            try
            {
                if(context.ItemImages.Any(i => i.ItemId == realtyId && i.ImageUrl == url))
                {
                    MessageBox.Show("Image already exists.");
                    return true;
                }

                itemImage = new ItemImage()
                {
                    ItemId = realtyId,
                    ImageUrl = url,
                    Order = 0
                };
                context.ItemImages.Add(itemImage);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating image: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> CreateImages(Guid realtyId, List<string> urls)
        {
            try
            {
                foreach (var url in urls)
                {
                    itemImage = new ItemImage()
                    {
                        ItemId = realtyId,
                        ImageUrl = url,
                        Order = 0
                    };
                    context.ItemImages.Add(itemImage);
                }
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating images: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> LoadImageAsync(string slug, string localFilePath)
        {
            try
            {
                string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));

                string folderPath = Path.Combine(projectRoot, "Images", slug);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                localFilePath = localFilePath.Trim().Replace("\u202A", "").Replace("\u200E", "");

                if (!File.Exists(localFilePath))
                {
                    MessageBox.Show("Local image file not found.");
                    return false;
                }

                string fileName = Path.GetFileName(localFilePath);
                string destPath = Path.Combine(folderPath, fileName);

                File.Copy(localFilePath, destPath, overwrite: true);

                MessageBox.Show($"Image copied to: {destPath}");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error copying image: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteImagesByItemIdAsync(Guid itemId)
        {
            try
            {
                var images = await context.ItemImages.Where(i => i.ItemId == itemId).ToListAsync();
                if (images.Count == 0)
                {
                    MessageBox.Show("No images found for this item.");
                    return false;
                }

                context.ItemImages.RemoveRange(images);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting images: {ex.Message}");
                return false;
            }
        }

    }
}
