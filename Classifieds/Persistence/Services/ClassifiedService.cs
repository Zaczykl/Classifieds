using Classifieds.Core;
using Classifieds.Core.Models.Domains;
using Classifieds.Core.Services;
using Classifieds.Core.ViewModels;
using Classifieds.Persistence.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Classifieds.Persistence.Services
{
    public class ClassifiedService : IClassifiedService
    {
        IUnitOfWork _unitOfWork;

        public ClassifiedService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Classified GetClassified(int id)
        {

            return _unitOfWork.ClassifiedRepository.GetClassified(id);
        }

        public IEnumerable<Classified> GetClassifieds(FilterClassifieds classifiedParams)
        {
            var classifieds = _unitOfWork.ClassifiedRepository.GetClassifieds(classifiedParams);
            GetThumbnailsUrls(classifieds);
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

        public async Task AttachPhotosToClassifiedAsync(CreateClassifiedViewModel vm)
        {
            
            if (vm.ThumbnailChanged)
            {
                var thumbnailParams = new ProductImageParams { Classified = vm.Classified, File = vm.File, UserId = vm.Classified.UserId, Thumbnail = true };
                await AttachImageToClassified(thumbnailParams);
            }

            if (vm.ImagesChanged)
            {
                var productImageParams = new ProductImageParams { Classified = vm.Classified, UserId = vm.Classified.UserId };

                if (vm.Files == null)
                    return;

                foreach (var productImage in vm.Files)
                {
                    productImageParams.File = productImage;
                    await AttachImageToClassified(productImageParams);
                }
            }            
        }

        public void convertPrice(Classified classified, string formattedPrice)
        {
            decimal.TryParse(formattedPrice, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price);
            classified.Price = price;
        }

        private async Task AttachImageToClassified(ProductImageParams productImageParams)
        {
            var productImage = new ProductImage { Image = await productImageParams.File.GetBytesAsync(), UserId = productImageParams.UserId, Thumbnail = productImageParams.Thumbnail };
            productImageParams.Classified.ProductImages.Add(productImage);
        }

        public void GetThumbnailsUrls(IEnumerable<Classified> classifieds)
        {
            foreach (var classified in classifieds)
            {
                var thumbnail = classified.ProductImages.Single(x => x.Thumbnail);
                GetImageUrl(thumbnail);
            }
        }

        public void GetImagesUrls(Classified classified)
        {
            foreach (var item in classified.ProductImages)
            {
                GetImageUrl(item);
            }
        }

        public string GetImageUrl(ProductImage image)
        {
            var imageBase64 = Convert.ToBase64String(image.Image);
            image.ImageUrl = $"data:image/jpeg;base64,{imageBase64}";
            return image.ImageUrl;
        }

        public string GetThumbnailUrl(Classified classified)
        {
            if (classified.ProductImages.Count == 0)
                return string.Empty;

            var image= classified.ProductImages.Single(x => x.Thumbnail);            
            return GetImageUrl(image);
        }
    }
}
