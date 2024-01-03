﻿using DataAccess.Dto;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dto.Request;
using FluentValidation;

namespace Business.Helpers
{
   public class DocValidationHelper
        {
            private readonly ErrorResponse _error;
            private readonly DtoWrapper _dto;
            private readonly IValidator<DocumentUploadDto> _DocUploadValidator;
            public DocValidationHelper(ErrorResponse error, DtoWrapper dto, IValidator<DocumentUploadDto> DocUploadValidator)
            {
                _error = error;
                _dto = dto;
            _DocUploadValidator= DocUploadValidator;
                

            }


            public async Task<ErrorResponse> ValidateImage(DocumentUploadDto _DocUpload)
            {
                ErrorResponse errorRes = null;

            var validationResult = await _DocUploadValidator.ValidateAsync(_DocUpload);
                errorRes = ReturnErrorRes(validationResult);

                return errorRes;


            }

            public ErrorResponse ReturnErrorRes(FluentValidation.Results.ValidationResult Res)

            {
                List<string> errors = new List<string>();
                foreach (var row in Res.Errors.ToArray())
                {
                    errors.Add(row.ErrorMessage.ToString());
                }
                _error.errorMessage = errors;
                return _error;
            }

        // (RESIZE an image in a byte[] variable.)  
        public byte[] ReduceImageSize(byte[] bytes, int size)
        {
            using var memoryStream = new MemoryStream(bytes);
            using var originalImage = new Bitmap(memoryStream);
            var resized = new Bitmap(size, size);
            using var graphics = Graphics.FromImage(resized);
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.DrawImage(originalImage, 0, 0, size, size);
            graphics.CompositingQuality = CompositingQuality.Default;
            using var stream = new MemoryStream();
            resized.Save(stream, ImageFormat.Jpeg);
            return stream.ToArray();
        }

        public byte[] IncreaseImageSize(byte[] bytes, int size)
        {
            using var memoryStream = new MemoryStream(bytes);
            using var originalImage = new Bitmap(memoryStream);
            var resized = new Bitmap(size, size);
            using var graphics = Graphics.FromImage(resized);
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.SmoothingMode = SmoothingMode.Default;
            graphics.InterpolationMode = InterpolationMode.Low;
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.DrawImage(originalImage, 0, 0, size, size);
            graphics.CompositingQuality = CompositingQuality.Default;
            using var stream = new MemoryStream();
            resized.Save(stream, ImageFormat.Jpeg);
            return stream.ToArray();

        }

    }
    }
