using Booking.Data.Entities;
using Booking.Data;
using System.Windows.Forms;
using System.IO;
using Microsoft.EntityFrameworkCore;
using FontAwesome.Sharp;
using System.Windows;

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

        public async Task<string> CreateImageAsync(Guid realtyId, string url)
        {
            try
            {
                if(context.ItemImages.Any(i => i.ItemId == realtyId && i.ImageUrl == url))
                {
                    return "Image already exists.";
                }

                itemImage = new ItemImage()
                {
                    ItemId = realtyId,
                    ImageUrl = url,
                    Order = 0
                };
                context.ItemImages.Add(itemImage);
                await context.SaveChangesAsync();
                return "Created";
            }
            catch (Exception ex)
            {
                //CustomMessageBox.Show("System", $"Error creating image: {ex.Message}", MessageBoxButton.OK, IconChar.TriangleExclamation);
                return $"Error creating image: {ex.Message}";
            }
        }

        public async Task<string> CreateImages(Guid realtyId, List<string> urls)
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
                return "Created";
            }
            catch (Exception ex)
            {
                //CustomMessageBox.Show("System", $"Error creating images: {ex.Message}", MessageBoxButton.OK, IconChar.TriangleExclamation);
                return $"Error creating images: {ex.Message}";
            }
        }

        public async Task<string> LoadImageAsync(string slug, string localFilePath)
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
                    return "Local image file not found.";
                }

                string fileName = Path.GetFileName(localFilePath);
                string destPath = Path.Combine(folderPath, fileName);

                File.Copy(localFilePath, destPath, overwrite: true);
                return "Created";
            }
            catch (Exception ex)
            {
                return $"Error copying image: {ex.Message}";
            }
        }

        public async Task<string> DeleteImagesByItemIdAsync(Guid itemId)
        {
            try
            {
                var images = await context.ItemImages.Include(i => i.ImageUrl).Where(i => i.ItemId == itemId).ToListAsync();
                if (images.Count != 0)
                {
                    context.ItemImages.RemoveRange(images);
                    await context.SaveChangesAsync();
                }
                return "Deleted";
            }
            catch (Exception ex)
            {
                //CustomMessageBox.Show("System", "Error deleting images: " + ex.Message, MessageBoxButton.OK, IconChar.TriangleExclamation);
                return "Error deleting images: " + ex.Message;
            }
        }

    }
}
