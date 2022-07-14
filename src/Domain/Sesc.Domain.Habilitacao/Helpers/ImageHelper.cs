using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Sesc.Domain.Habilitacao.Helpers
{
    public class ImageHelper
    {
        public static MemoryStream Resize(IFormFile image, int size)
        {
            const long quality = 50L;

            using (var imageResized = new Bitmap(Image.FromStream(image.OpenReadStream(), true, true)))
            {
                int width, height;
                if (imageResized.Width > imageResized.Height)
                {
                    width = size;
                    height = Convert.ToInt32(imageResized.Height * size / (double)imageResized.Width);
                }
                else
                {
                    width = Convert.ToInt32(imageResized.Width * size / (double)imageResized.Height);
                    height = size;
                }

                var resized_Bitmap = new Bitmap(width, height);

                using (var graphics = Graphics.FromImage(resized_Bitmap))
                {

                    graphics.CompositingQuality = CompositingQuality.HighSpeed;

                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    graphics.CompositingMode = CompositingMode.SourceCopy;

                    graphics.DrawImage(imageResized, 0, 0, width, height);

                    var qualityParamId = Encoder.Quality;

                    var encoderParameters = new EncoderParameters(1);

                    encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);

                    var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

                    var stream = new MemoryStream();
                    resized_Bitmap.Save(stream, codec, encoderParameters);

                    return stream;
                }
            }
        }
    }
}
