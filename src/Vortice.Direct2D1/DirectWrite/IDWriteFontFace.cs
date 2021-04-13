﻿// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.DirectWrite
{
    public partial class IDWriteFontFace
    {
        public int FilesCount
        {
            get
            {
                int numberOfFiles = 0;
                GetFiles(ref numberOfFiles, null);
                return numberOfFiles;
            }
        }

        public IDWriteFontFile[] GetFiles()
        {
            int numberOfFiles = FilesCount;
            IDWriteFontFile[] files = new IDWriteFontFile[numberOfFiles];
            GetFiles(ref numberOfFiles, files).CheckError();
            return files;
        }

        public Result GetFiles(IDWriteFontFile[] files)
        {
            int numberOfFiles = files.Length;
            return GetFiles(ref numberOfFiles, files);
        }

        public GlyphMetrics[] GetDesignGlyphMetrics(ushort[] glyphIndices, bool isSideways)
        {
            var glyphMetrics = new GlyphMetrics[glyphIndices.Length];
            GetDesignGlyphMetrics(glyphIndices, glyphIndices.Length, glyphMetrics, isSideways);
            return glyphMetrics;
        }

        public Result GetDesignGlyphMetrics(ushort[] glyphIndices, GlyphMetrics[] glyphMetrics, bool isSideways)
        {
            return GetDesignGlyphMetrics(glyphIndices, glyphIndices.Length, glyphMetrics, isSideways);
        }

        public unsafe bool TryGetFontTable(int openTypeTableTag, out Span<byte> tableData, out IntPtr tableContext)
        {
            void* tableDataPtr = null;
            Result result = TryGetFontTable(openTypeTableTag, &tableDataPtr, out int tableDataSize, out tableContext, out RawBool exists);

            if (result.Failure)
            {
                tableData = default;
                return false;
            }

            tableData = new Span<byte>(tableDataPtr, tableDataSize);
            return exists;
        }
    }
}