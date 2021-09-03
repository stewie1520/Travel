using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

using Travel.Application.Common.Interfaces;
using Travel.Application.TourLists.Queries.ExportTours;

namespace Travel.Shared.Files
{
    public class CsvFileBuilder : ICsvFileBuilder
    {
        public byte[] BuildTourPackagesFile(IEnumerable<TourPackageRecord> records)
        {
            using var memoryString = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryString))
            {
                using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
                csvWriter.WriteRecords(records);
            }

            return memoryString.ToArray();
        }
    }
}
