using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace FilmsCatalog.ViewModels.Movie
{
    public class MovieViewModel
    {
        public Models.Movie Movie { get; set; }
        public IEnumerable<Models.Movie> Movies { get; set; }
        public PagingViewModel PagingViewModel { get; set; }
        private string FilesPath = "\\Uploads\\Movie\\Poster\\";

        public string SavePosterFile(IFormFile formFile, string webRoot)
        {
            if (formFile != null)
            {
                try
                {
                    string folder = webRoot + "/" + FilesPath;
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    var fileName = formFile.FileName;

                    FilesPath += fileName;
                    using var fileStream = new FileStream(folder + fileName, FileMode.Create);
                    formFile.CopyTo(fileStream);
                    return FilesPath;
                }
                catch (Exception e)
                {
                    return string.Empty;
                }
            }
            return string.Empty;
        }


        public string EditPosterFile(IFormFile formFile, string webRoot, string oldFilePath)
        {
            if (formFile == null || string.IsNullOrEmpty(webRoot) || string.IsNullOrEmpty(oldFilePath))
                return string.Empty;

            string newFilePath = SavePosterFile(formFile, webRoot);
            if (string.IsNullOrEmpty(newFilePath)) return string.Empty;

            if (File.Exists(webRoot + oldFilePath))
            {
                try
                {
                    File.Delete(webRoot + oldFilePath);
                } catch (Exception e)
                {
                    return string.Empty;
                }
            }
            return newFilePath;
        }
    }
}
