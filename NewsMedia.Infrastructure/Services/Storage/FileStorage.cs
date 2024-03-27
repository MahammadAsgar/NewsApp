using Microsoft.AspNetCore.Http;
using NewsMedia.Application.Repositories.Abstractions;
using NewsMedia.Application.UnitOfWorks;
using NewsMedia.Domain.Models.Entities;
using NewsMedia.Infrastructure.DTOS.Entities.Article.Post;
using System.Text;

namespace NewsMedia.Infrastructure.Services.Storage
{
    public class FileStorage : IFilelStorage
    {
        readonly IUnitofWork _unitofWork;
        readonly IGenericRepository<ArticleTitleFile> _titleFile;
        readonly IGenericRepository<ArticleContentFile> _contentFile;
        public FileStorage(IUnitofWork unitofWork, IGenericRepository<ArticleTitleFile> titleFile, IGenericRepository<ArticleContentFile> contentFile)
        {
            _titleFile = titleFile;
            _contentFile = contentFile;
            _unitofWork = unitofWork;
        }
        public async Task UploadAsync(AddArticleDto addArticleDto, Article article)
        {
            string upload = "wwwroot\\images\\";
            StringBuilder fileName = new StringBuilder();
            StringBuilder path = new StringBuilder();
            if (!Directory.Exists(upload))
            {
                Directory.CreateDirectory(upload);
            }
            var rand = new Random();
            var files = new List<ArticleContentFile>();

            //await UploadFile(upload, addArticleDto.ArticleTitleFile, fileName, path, rand);
            //files.Add(new ArticleTitleFile
            //{
            //    Article = article,
            //    FileName = fileName.ToString(),
            //    Path = path.ToString()
            //});
            //fileName.Clear();
            //path.Clear();

            if (addArticleDto.ArticleContentFiles is not null && addArticleDto.ArticleContentFiles.Count > 0)
            {
                foreach (IFormFile file in addArticleDto.ArticleContentFiles)
                {
                    await UploadFile(upload, file, fileName, path, rand);
                    files.Add(new ArticleContentFile
                    {
                        Article = article,
                        FileName = fileName.ToString(),
                        Path = path.ToString()
                    });
                    fileName.Clear();
                    path.Clear();
                }
            }

            await _contentFile.AddRangeAsync(files);
            await _unitofWork.CommitAsync();
        }

        public async Task<ArticleTitleFile> UploadArticleTitleFile(AddArticleDto addArticleDto)
        {
            string upload = "wwwroot\\images\\";
            StringBuilder fileName = new StringBuilder();
            StringBuilder path = new StringBuilder();
            if (!Directory.Exists(upload))
            {
                Directory.CreateDirectory(upload);
            }
            var rand = new Random();

            await UploadFile(upload, addArticleDto.ArticleTitleFile, fileName, path, rand);
            var file = new ArticleTitleFile
            {
                FileName = fileName.ToString(),
                Path = path.ToString()
            };
            await _titleFile.AddAsync(file);
            await _unitofWork.CommitAsync();
            return file;
        }
        private async Task UploadFile(string upload, IFormFile file, StringBuilder fileName, StringBuilder path, Random rand)
        {
            fileName.Append(DateTime.Now.ToString("yyyy-MM-dd") + "_" + rand.Next(10000, 99999).ToString() + "." + file.ContentType.Remove(0, 6).ToString());
            path.Append(Path.Combine(upload, $"{fileName}"));
            using FileStream fileStream = new FileStream(path.ToString(), FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            await fileStream.DisposeAsync();
        }
    }
}

