using Azure.Storage.Blobs;
using Classifieds.Core;
using Classifieds.Core.Models.Domains;
using Classifieds.Core.Services;
using Classifieds.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Classifieds.Persistence.Services
{
    public class ClassifiedService : IClassifiedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        private const int MaxImageSizeToCloudUpload = 256 * 1024;
        private const int MaxImageHeightToCloudUpload = 600;

        public ClassifiedService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _config = configuration;
        }

        public Classified GetClassified(int id)
        {
            return _unitOfWork.ClassifiedRepository.GetClassified(id);
        }

        public IEnumerable<Classified> GetClassifieds(FilterClassifieds classifiedParams)
        {
            var classifieds = _unitOfWork.ClassifiedRepository.GetClassifieds(classifiedParams);
            return classifieds;
        }
        
        public IEnumerable<Classified> SortClassifieds(string sortByRule, IEnumerable<Classified> classifiedsToSort)
        {
            switch (sortByRule)
            {
                case "NameAsc":
                    return classifiedsToSort.OrderBy(x => x.Title);
                case "NameDesc":
                    return classifiedsToSort.OrderByDescending(x => x.Title);
                case "PriceAsc":
                    return classifiedsToSort.OrderBy(x => x.Price);
                case "PriceDesc":
                    return classifiedsToSort.OrderByDescending(x => x.Price);
                default: 
                    return classifiedsToSort;
            }
        }        

        public void Add(Classified classified)
        {
            classified.Active = true;
            _unitOfWork.ClassifiedRepository.Add(classified);
            _unitOfWork.Complete();
        }

        public void Update(CreateClassifiedViewModel viewModel)
        {
            _unitOfWork.ClassifiedRepository.Update(viewModel);
            _unitOfWork.Complete();
        }

        public void Activate(int id, string userId)
        {
            _unitOfWork.ClassifiedRepository.Activate(id, userId);
            _unitOfWork.Complete();
        }

        public void Deactivate(int id, string userId)
        {
            _unitOfWork.ClassifiedRepository.Deactivate(id,userId);
            _unitOfWork.Complete();
        }

        public void Delete(int id, string userId)
        {
            _unitOfWork.ClassifiedRepository.Delete(id, userId);
            _unitOfWork.Complete();
        }

        public void AttachImagesToClassified(CreateClassifiedViewModel vm)
        {            
            if (vm.ThumbnailChanged)
            {
                var thumbnailParams = new ProductImageParams { Classified = vm.Classified, File = vm.File, UserId = vm.Classified.UserId, Thumbnail = true };
                AttachImage(thumbnailParams);
            }

            if (vm.ImagesChanged && vm.Files != null)
            {
                var productImageParams = new ProductImageParams { Classified = vm.Classified, UserId = vm.Classified.UserId };                

                foreach (var productImage in vm.Files)
                {
                    productImageParams.File = productImage;
                    AttachImage(productImageParams);
                }
            }            
        }

        private void AttachImage(ProductImageParams productImageParams)
        {
            var imageUrl = uploadImage(productImageParams.File);            
            ProductImage productImage = new ProductImage { ImageUrl= imageUrl, UserId = productImageParams.UserId, Thumbnail = productImageParams.Thumbnail };
            productImageParams.Classified.ProductImages.Add(productImage);
        }

        public void convertPrice(Classified classified, string formattedPrice)
        {
            decimal.TryParse(formattedPrice, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price);
            classified.Price = price;
        }

        private string GenerateFileName(string fileName)
        {
            try
            {
                string strFileName = string.Empty;
                string[] strName = fileName.Split('.');
                strFileName = "classifieds" + "/"
                   + DateTime.Now.ToUniversalTime().ToString("yyyyMMdd\\THHmmssfff") + "." +
                   strName[strName.Length - 1];
                return strFileName;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string uploadImage(IFormFile file)
        {
            try
            {
                string filename = GenerateFileName(file.FileName);
                string fileUrl = "";
                string connectionString = _config.GetConnectionString("BlobConnection");
                BlobContainerClient container = new BlobContainerClient(connectionString, "images");
                BlobClient blob = container.GetBlobClient(filename);
                using (Stream stream = file.OpenReadStream())
                {
                    using (Stream imageResized = ResizeImage(stream, MaxImageHeightToCloudUpload, MaxImageSizeToCloudUpload))
                    {
                        blob.Upload(imageResized);
                        fileUrl = blob.Uri.AbsoluteUri;
                    }
                }
                return fileUrl;
            }
            catch (Exception ex)
            {
                return ex.StackTrace;
            }
        }

        private Stream ResizeImage(Stream stream, int maxHeight, int maxSize)
        {            
            try
            {                
                using (var image = Image.Load(stream))
                {
                    if (image.Height > maxHeight || stream.Length > maxSize)
                    {                        
                        var options = new ResizeOptions
                        {
                            Size = new Size(maxHeight, maxHeight),
                            Mode = ResizeMode.Max,
                        };
                        image.Mutate(x => x.Resize(options));                        
                        var outputStream = new MemoryStream();
                        image.Save(outputStream, new JpegEncoder());
                        outputStream.Seek(0, SeekOrigin.Begin);
                        return outputStream;
                    }
                }
                stream.Seek(0, SeekOrigin.Begin);
                return stream;
            }
            catch (Exception)
            {                
                return null;
            }
        }
    }
}
