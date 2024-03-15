﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace HyperVExtension.Models.VMGalleryJsonToClasses;

/// <summary>
/// Represents the 'symbol' json object of an image in the VM Gallery. See Gallery Json "https://go.microsoft.com/fwlink/?linkid=851584"
/// </summary>
public sealed class VMGallerySymbol : VMGalleryItemWithHashBase
{
    public string Base64Image { get; set; } = string.Empty;
}
