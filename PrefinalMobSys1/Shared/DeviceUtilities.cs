using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Shared
{ 
    /// <summary>
    /// Shared Class to access Camera,Photo Picker and etc.
    /// </summary>
    public class DeviceUtilities
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="targetFilename"></param>
        /// <returns></returns>
        public static async Task<string> AddPhoto(string folderPath,string targetFilename ="")
        {
            string resp = "";
            var medStatus = await Permissions.RequestAsync<Permissions.Media>();
            //var micStatus = await Permissions.RequestAsync<Permissions.Microphone>();

            if (medStatus == PermissionStatus.Granted)
            {
                FileResult photo = await MediaPicker.Default.PickPhotoAsync();

                if (photo != null)
                {
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // save the file into local storage
                    //string localFilePath = Path.Combine(folderPath, photo.FileName);
                    //we cannot use the same file as spaces in file causes invalid urls in MAUI
                    string eventualFilename = string.IsNullOrWhiteSpace(targetFilename) ? DateTime.Now.Ticks + Path.GetExtension(photo.FileName) : targetFilename;
                    string localFilePath = Path.Combine(folderPath, eventualFilename);

                    using Stream sourceStream = await photo.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);

                    await sourceStream.CopyToAsync(localFileStream);

                    resp = localFilePath;
                }
            }

            return resp;
        }

        public static async Task<string> TakePhoto(string folderPath, string targetFilename = "")
        {
            string resp = "";
            if (MediaPicker.Default.IsCaptureSupported)
            {
                var camStatus = await Permissions.RequestAsync<Permissions.Camera>();
                var micStatus = await Permissions.RequestAsync<Permissions.Microphone>();

                if (camStatus == PermissionStatus.Granted && micStatus == PermissionStatus.Granted)
                {
                    FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                    if (photo != null)
                    {
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        // save the file into local storage
                        string eventualFilename = string.IsNullOrWhiteSpace(targetFilename) ? DateTime.Now.Ticks + Path.GetExtension(photo.FileName) : targetFilename;
                        string localFilePath = Path.Combine(folderPath, eventualFilename);
                        using Stream sourceStream = await photo.OpenReadAsync();
                        using FileStream localFileStream = File.OpenWrite(localFilePath);
                        await sourceStream.CopyToAsync(localFileStream); //finalize copy

                        resp = localFilePath;
                    }
                }
            }
            return resp;
        }
    }
}
