using System;
using System.IO;
using CKAN;

namespace PartManagerPlugin
{
    public static class Cache
    {

        public static void RemovePartFromCache(string part)
        {
            var partManagerPath = Path.Combine(Main.Instance.CurrentInstance.CkanDir(), "PartManager");
            if (!Directory.Exists(partManagerPath))
            {
                Directory.CreateDirectory(partManagerPath);
            }

            var cachePath = Path.Combine(partManagerPath, "cache");
            if (!Directory.Exists(cachePath))
            {
                Directory.CreateDirectory(cachePath);
            }

            var fullPath = Path.Combine(cachePath, part);
            File.Delete(fullPath);
        }

        public static void MovePartToCache(string part)
        {
            var partManagerPath = Path.Combine(Main.Instance.CurrentInstance.CkanDir(), "PartManager");
            if (!Directory.Exists(partManagerPath))
            {
                Directory.CreateDirectory(partManagerPath);
            }

            var cachePath = Path.Combine(partManagerPath, "cache");
            if (!Directory.Exists(cachePath))
            {
                Directory.CreateDirectory(cachePath);
            }

            var fullPath = Path.Combine(Main.Instance.CurrentInstance.GameDir(), part);
            var targetPath = Path.Combine(cachePath, part);

            try
            {
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetPath));
            }
            catch (Exception) { }

            File.Move(fullPath, targetPath);
        }

        public static void MovePartFromCache(string part)
        {
            var partManagerPath = Path.Combine(Main.Instance.CurrentInstance.CkanDir(), "PartManager");
            if (!Directory.Exists(partManagerPath))
            {
                Directory.CreateDirectory(partManagerPath);
            }

            var cachePath = Path.Combine(partManagerPath, "cache");
            if (!Directory.Exists(cachePath))
            {
                Directory.CreateDirectory(cachePath);
            }

            var fullPath = Path.Combine(cachePath, part);
            var targetPath = Path.Combine(Main.Instance.CurrentInstance.GameDir(), part);

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(targetPath));
            }
            catch (Exception) { }

            File.Move(fullPath, targetPath);
        }

    }
}
