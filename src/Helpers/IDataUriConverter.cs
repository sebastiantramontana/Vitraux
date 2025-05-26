using DataUri = string;

namespace Vitraux.Helpers;

public interface IDataUriConverter
{
    DataUri ToDataUri(MimeImage mime, IEnumerable<byte> data);
    Task<DataUri> ToDataUri(MimeImage mime, Stream data);
}
