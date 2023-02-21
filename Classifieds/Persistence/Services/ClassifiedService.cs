using Classifieds.Core;
using Classifieds.Core.Models.Domains;
using Classifieds.Core.Services;
using Classifieds.Core.ViewModels;
using Classifieds.Persistence.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
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
        public IEnumerable<Classified> GetClassifieds(string title = null, int categoryId = 0)
        {
            return _unitOfWork.ClassifiedRepository.GetClassifieds(title, categoryId);
        }

        public void Add(Classified classified)
        {
            _unitOfWork.ClassifiedRepository.Add(classified);
            _unitOfWork.Complete();
        }

        public async Task AttachPhotosAsync(CreateClassifiedViewModel vm)
        {
            var thumbnailParams = new ProductImageParams { Classified = vm.Classified, File = vm.File, UserId = vm.Classified.UserId, Thumbnail = true };
            var productImageParams = new ProductImageParams { Classified = vm.Classified, UserId = vm.Classified.UserId };

            await AttachImage(thumbnailParams);
            if (vm.Files == null)
                return;

            foreach (var productImage in vm.Files)
            {
                productImageParams.File = productImage;
                await AttachImage(productImageParams);
            }
        }
        public void convertPrice(Classified classified, string formattedPrice)
        {
            decimal.TryParse(formattedPrice, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price);
            classified.Price = price;
        }

        private async Task AttachImage(ProductImageParams productImageParams)
        {
            var productImage = new ProductImage { Image = await productImageParams.File.GetBytesAsync(), UserId = productImageParams.UserId, Thumbnail = productImageParams.Thumbnail };
            productImageParams.Classified.ProductImages.Add(productImage);
        }
    }
}
