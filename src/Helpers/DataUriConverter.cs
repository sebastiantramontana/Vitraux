using System.Net.Mime;
using DataUri = string;

namespace Vitraux.Helpers;

internal class DataUriConverter : IDataUriConverter
{
    public DataUri ToDataUri(MimeImage mime, IEnumerable<byte> data)
        => $"data:{MapMimeType(mime)};base64," + Convert.ToBase64String(data.ToArray());

    public DataUri ToDataUri(MimeImage mime, Stream data)
    {
        var bytes = new byte[data.Length];
        data.ReadAsync(bytes, 0, (int)data.Length);

        return ToDataUri(mime, bytes);
    }

    private static string MapMimeType(MimeImage mime)
        => mime switch
        {
            MimeImage.Gif => MediaTypeNames.Image.Gif,
            MimeImage.Jpeg => MediaTypeNames.Image.Jpeg,
            MimeImage.Png => MediaTypeNames.Image.Png,
            MimeImage.Svg => MediaTypeNames.Image.Svg,
            MimeImage.Apng => "image/apng",
            MimeImage.Avif => MediaTypeNames.Image.Avif,
            MimeImage.Webp => MediaTypeNames.Image.Webp,
            MimeImage.Bmp => MediaTypeNames.Image.Bmp,
            MimeImage.Tiff => MediaTypeNames.Image.Tiff,
            MimeImage.Heic => "image/heic",
            MimeImage.Heif => "image/heif",
            MimeImage.Djvu => "image/vnd.djvu",
            MimeImage.Invalid or _ => throw new InvalidOperationException($"Mime type {mime} is invalid"),
        };
}
