﻿using DataUri = string;

namespace Vitraux.Helpers;

public interface IDataUriConverter
{
    DataUri ToDataUri(MimeImage mime, IEnumerable<byte> data);
    DataUri ToDataUri(MimeImage mime, Stream data);
}
