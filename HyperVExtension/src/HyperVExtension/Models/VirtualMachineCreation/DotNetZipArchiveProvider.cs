﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.IO.Compression;
using HyperVExtension.Extensions;
using Windows.Storage;

namespace HyperVExtension.Models.VirtualMachineCreation;

/// <summary>
/// Provides methods to extract a zip archive. This class uses the .NET <see cref="ZipArchive"/> class to extract the contents of the archive file.
/// <see cref="ZipArchive"/> is used by Hyper-V Manager's VM Quick Create feature. This gives us parity, but in the future it is expected that faster/more efficient
/// archive extraction libraries will be added.
/// </summary>
public sealed class DotNetZipArchiveProvider : IArchiveProvider
{
    // Same buffer size used by Hyper-V Manager's VM gallery feature.
    private readonly int _transferBufferSize = 4096;

    /// <inheritdoc cref="IArchiveProvider.ExtractArchiveAsync"/>
    public async Task ExtractArchiveAsync(IProgress<IOperationReport> progressProvider, StorageFile archivedFile, string destinationAbsoluteFilePath, CancellationToken cancellationToken)
    {
        using var zipArchive = ZipFile.OpenRead(archivedFile.Path);

        // Expect only one entry in the zip file, which would be the virtual disk.
        var zipArchiveEntry = zipArchive.Entries.First();
        var totalBytesToExtract = zipArchiveEntry.Length;
        using var outputFileStream = File.OpenWrite(destinationAbsoluteFilePath);
        using var zipArchiveEntryStream = zipArchiveEntry.Open();

        var fileExtractionProgress = new Progress<long>(bytesCopied =>
        {
            var percentage = (uint)(bytesCopied / (double)totalBytesToExtract * 100D);
            progressProvider.Report(new ArchiveExtractionOperation((ulong)bytesCopied, (ulong)totalBytesToExtract));
        });

        outputFileStream.SetLength(totalBytesToExtract);
        await zipArchiveEntryStream.CopyToAsync(outputFileStream, fileExtractionProgress, _transferBufferSize, cancellationToken);
        File.SetLastWriteTime(destinationAbsoluteFilePath, zipArchiveEntry.LastWriteTime.DateTime);
    }
}
