﻿// Copyright (c) Microsoft Corporation and Contributors
// Licensed under the MIT license.

using System;
using System.Threading.Tasks;
using DevHome.SetupFlow.Models;

namespace DevHome.SetupFlow.Services.WinGet;

public interface IWinGetProtocolParser
{
    /// <summary>
    /// Get the package id and catalog from a package uri
    /// </summary>
    /// <param name="packageUri">Input package uri</param>
    /// <returns>Package id and catalog, or null if the URI protocol is inaccurate</returns>
    public Task<WinGetProtocolParserResult> ParsePackageUriAsync(Uri packageUri);

    /// <summary>
    /// Create a package uri from a package id and catalog
    /// </summary>
    /// <param name="packageId">Package id</param>
    /// <param name="catalog">Catalog</param>
    /// <returns>Package uri</returns>
    public Uri CreatePackageUri(string packageId, WinGetCatalog catalog);

    /// <summary>
    /// Create a winget catalog package uri from a package id
    /// </summary>
    /// <param name="packageId">Package id</param>
    /// <returns>Package uri</returns>
    public Uri CreateWinGetCatalogPackageUri(string packageId);

    /// <summary>
    /// Create a Microsoft store catalog package uri from a package id
    /// </summary>
    /// <param name="packageId">Package id</param>
    /// <returns>Package uri</returns>
    public Uri CreateMsStoreCatalogPackageUri(string packageId);

    /// <summary>
    /// Create a custom catalog package uri from a package id and catalog name
    /// </summary>
    /// <param name="packageId">Package id</param>
    /// <param name="catalogName">Catalog name</param>
    /// <returns>Package uri</returns>
    public Uri CreateCustomCatalogPackageUri(string packageId, string catalogName);
}