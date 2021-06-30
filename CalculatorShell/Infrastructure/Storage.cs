using CalculatorShell.Base;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text.Json;

namespace CalculatorShell.Infrastructure
{
    internal sealed class Storage : IStorage
    {
        private FileStream? _realFile;
        private ZipArchive? _archive;

        public Storage(string fileSystemFile)
        {
            _realFile = File.Open(fileSystemFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            _archive = new ZipArchive(_realFile, ZipArchiveMode.Update);
        }

        public T? Deserialize<T>(string name) where T : class, new()
        {
            var entry = _archive?.GetEntry(name);
            if (entry == null)
            {
                return default;
            }
            using (var entryStream = entry.Open())
            {
                byte[] data = new byte[entry.Length];
                entryStream.Read(data, 0, data.Length);
                return JsonSerializer.Deserialize<T>(data);
            }
        }

        public void CreateOrUpdateEntry<T>(string name, T entryData) where T : class, new()
        {
            ZipArchiveEntry? entry = (_archive?.GetEntry(name)) ?? (_archive?.CreateEntry(name, CompressionLevel.Optimal));
            if (entry != null)
            {
                var data = JsonSerializer.SerializeToUtf8Bytes(entryData);
                using (var entryStream = entry.Open())
                {
                    entryStream.Write(data, 0, data.Length);
                }
            }
        }

        public void Delete(string name)
        {
            var entry = _archive?.GetEntry(name);
            entry?.Delete();
        }

        public void Dispose()
        {
            if (_archive != null)
            {
                _archive.Dispose();
                _archive = null;
            }
            if (_realFile != null)
            {
                _realFile.Dispose();
                _realFile = null;
            }
        }

        public bool Exists(string name)
        {
            var entry = _archive?.GetEntry(name);
            return entry != null;
        }

        public IEnumerator<IStorageEntry> GetEnumerator()
        {
            if (_archive?.Entries == null)
            {
                yield break;
            }
            foreach (var entry in _archive.Entries)
            {
                yield return new StorageEntry
                {
                    LastWrite = entry.LastWriteTime,
                    Name = entry.FullName
                };
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
