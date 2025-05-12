using Booking.Data.Entities;
using Booking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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

        public async Task<bool> CreateImage(Guid realtyId, string url)
        {
            try
            {
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
                string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", slug);
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

                MessageBox.Show($"Image copied to {destPath}");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error copying image: {ex.Message}");
                return false;
            }
        }
    }
}
