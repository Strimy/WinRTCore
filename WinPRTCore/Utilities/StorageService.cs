using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.Storage;

namespace WinRTCore.Utilities
{
    public static class StorageService
    {
        #region [ Const ]
        private static readonly ApplicationDataContainer LocalSettings = ApplicationData.Current.LocalSettings;
        private const string SettingsContainerName = "LocalSettings";
        #endregion [ Const ]


        #region [ Methods ]
        /// <summary>
        /// Add new item in settings
        /// </summary>
        /// <param name="key">Item name</param>
        /// <param name="value">Item value</param>
        public static void CreateOrEditSetting(string key, object value)
        {
            ApplicationDataContainer container;

            if (LocalSettings.Containers.ContainsKey(SettingsContainerName))
                container = LocalSettings.Containers[SettingsContainerName];
            else
                container = LocalSettings.CreateContainer(SettingsContainerName, ApplicationDataCreateDisposition.Always);

            if (container.Values.ContainsKey(key))
                container.Values[key] = value;
            else
                container.Values.Add(key, value);
        }

        public static object ReadSetting(string key)
        {
            if (LocalSettings.Containers.ContainsKey(SettingsContainerName))
                if (LocalSettings.Containers[SettingsContainerName].Values.ContainsKey(key))
                    return LocalSettings.Containers[SettingsContainerName].Values[key];

            return null;
        }

        /// <summary>
        /// Open serialized object in IsolatedStorage
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public async static Task<T> LoadAsync<T>(string name) where T : class, new()
        {
            try
            {
                T loadedObject = null;

                var storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync(name);
                if (storageFile != null)
                {
                    using (var storageFileStream = await storageFile.OpenStreamForReadAsync())
                    {

                        var serializer = new DataContractJsonSerializer(typeof(T));
                        loadedObject = (T)serializer.ReadObject(storageFileStream);

                        if (loadedObject == null)
                            loadedObject = new T();
                    }
                }
                else
                    loadedObject = null;


                return loadedObject;
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            catch (Exception e)
            {
                String test = e.Message;
                return null;
            }
        }

        public async static Task<bool> IsFileExist(string name)
        {
            try
            {
                var storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync(name);
                if (storageFile != null)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Return all files in a specific folder
        /// </summary>
        /// <param name="folder">folder name</param>
        /// <returns>List of all files</returns>
        public async static Task<IReadOnlyList<StorageFile>> GetAllFiles(string folder)
        {
            try
            {
                var storageFolder = await ApplicationData.Current.LocalFolder.GetFolderAsync(folder);
                return await storageFolder.GetFilesAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Return the selected folder
        /// </summary>
        /// <param name="folderPath">Folder path</param>
        /// <returns>Selected folder</returns>
        public async static Task<StorageFolder> GetFolder(string folderPath)
        {
            try
            {
                return await ApplicationData.Current.LocalFolder.GetFolderAsync(folderPath);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Return all files in the root folder
        /// </summary>
        /// <returns>List of all files</returns>
        public async static Task<IReadOnlyList<StorageFile>> GetAllFiles()
        {
            try
            {
                return await ApplicationData.Current.LocalFolder.GetFilesAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Save serialized object in IsolatedStorage
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="objectToSave"></param>
        public async static Task SaveAsync<T>(string name, T objectToSave)
        {
            try
            {
                StorageFile storageFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);

                using (var storageFileStream = await storageFile.OpenStreamForWriteAsync())
                {
                    var serializer = new DataContractJsonSerializer(typeof (T));
                    serializer.WriteObject(storageFileStream, objectToSave);                    
                }
            }
            catch (Exception e)
            {
                String test = e.Message;
                //The file will be saved later
                //Sometimes there is an exception : "Operation not permitted on IsolatedStorageFileStream"
            }
        }

        /// <summary>
        /// Save bytes in IsolatedStorage
        /// </summary>
        /// <param name="name"></param>
        /// <param name="stream"></param>
        public async static Task SaveStreamAsync(string name, Stream stream)
        {
            try
            {
                stream.Position = 0;
                var storageFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);
                using (var storageFileStream = await storageFile.OpenStreamForWriteAsync())
                {
                    using (BinaryWriter writer = new BinaryWriter(storageFileStream))
                    {
                        stream.Position = 0;
                        using (BinaryReader reader = new BinaryReader(stream))
                        {
                            byte[] buffer = new byte[32];
                            int size;
                            while ((size = reader.Read(buffer, 0, buffer.Length)) > 0)
                                writer.Write(buffer, 0, size);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                String test = e.Message;
                //The file will be saved later
                //Sometimes there is an exception : "Operation not permitted on IsolatedStorageFileStream"
            }
        }

        /// <summary>
        /// Open bytes in IsolatedStorage
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static async Task<Stream> LoadStreamAsync(string name)
        {
            try
            {
                var storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync(name);
                if (storageFile != null)
                {
                    return await storageFile.OpenStreamForReadAsync();
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Delete the selected file
        /// </summary>
        /// <param name="path">Path of file to delete</param>
        /// <returns></returns>
        public static async Task DeleteFileAsync(string path)
        {
            try
            {
                if (!String.IsNullOrEmpty(path))
                {
                    StorageFile storageFile = null;
                    if (path.Contains(ApplicationData.Current.LocalFolder.Path))
                        storageFile = await StorageFile.GetFileFromPathAsync(path);
                    else
                        storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync(path);

                    if (storageFile != null)
                        await storageFile.DeleteAsync();
                }
            }
            catch (Exception e)
            {
                //the file is maybe openned, so it will be deleted later
                String test = e.Message;
            }
        }

        /// <summary>
        /// Delete the selected directory, and all directories and files that are inside
        /// </summary>
        /// <param name="pathDirectory">Path of directory to delete</param>
        public static async Task DeleteDirectoryAsync(string pathDirectory)
        {
            try
            {
                if (!String.IsNullOrEmpty(pathDirectory))
                {
                    StorageFolder storageFolder = null;
                    if (pathDirectory.Contains(ApplicationData.Current.LocalFolder.Path))
                        storageFolder = await StorageFolder.GetFolderFromPathAsync(pathDirectory);
                    else
                        storageFolder = await ApplicationData.Current.LocalFolder.GetFolderAsync(pathDirectory);

                    if (storageFolder != null)
                        await storageFolder.DeleteAsync();
                }
            }
            catch (Exception e)
            {
                //the directory is maybe openned, so it will be deleted later
                String test = e.Message;
            }
        }
        #endregion [ Methods ]
    }
}
