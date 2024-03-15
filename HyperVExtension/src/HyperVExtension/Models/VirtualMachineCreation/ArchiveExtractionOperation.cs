﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace HyperVExtension.Models.VirtualMachineCreation;

/// <summary>
/// Represents an operation to extract an archive file.
/// </summary>
public sealed class ArchiveExtractionOperation : IOperationReport
{
    public ReportKind ReportKind => ReportKind.ArchiveExtraction;

    public string LocalizationKey => "ExtractingFile";

    public ulong BytesReceived { get; private set; }

    public ulong TotalBytesToReceive { get; private set; }

    public ArchiveExtractionOperation(ulong bytesReceived, ulong totalBytesToReceive)
    {
        BytesReceived = bytesReceived;
        TotalBytesToReceive = totalBytesToReceive;
    }
}
