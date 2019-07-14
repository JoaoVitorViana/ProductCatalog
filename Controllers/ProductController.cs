using System;
using System.Collections.Generic;
using ProductCatalog.Repositories;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models;
using ProductCatalog.ViewModels;
using ProductCatalog.ViewModels.ProductViewModels;

namespace ProductCatalog.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _repository;

        public ProductController(ProductRepository repository)
        {
            _repository = repository;
        }

        [Route("v1/products")]
        [HttpGet]
        public IEnumerable<ListProductViewModel> Get()
        {
            return _repository.Get();
        }

        [Route("v1/products/{id}")]
        [HttpGet]
        public ListProductViewModel Get(int id)
        {
            return _repository.Get(id);
        }

        [Route("v1/products")]
        [HttpPost]
        public ResultViewModel Post([FromBody] EditorProductViewModel model)
        {
            try
            {
                model.Validate();
                if (model.Invalid)
                    return new ResultViewModel
                    {
                        Success = false,
                        Message = "Não foi possível alterar o produto",
                        Data = model.Notifications
                    };

                var product = new Product
                {
                    CreateDate = DateTime.Now,
                    Title = model.Title,
                    CategoryId = model.CategoryId,
                    Description = model.Description,
                    Image = model.Image,
                    LastUpdateDate = DateTime.Now,
                    Price = model.Price,
                    Quantity = model.Quantity
                };

                _repository.Save(product);

                return new ResultViewModel
                {
                    Success = true,
                    Message = "Produto cadastrado com sucesso!",
                    Data = product
                };
            }
            catch (Exception ex)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        [Route("v1/products")]
        [HttpPut]
        public ResultViewModel Put([FromBody] EditorProductViewModel model)
        {
            try
            {
                model.Validate();
                if (model.Invalid)
                    return new ResultViewModel
                    {
                        Success = false,
                        Message = "Não foi possível alterar o produto",
                        Data = model.Notifications
                    };

                var product = _repository.GetProduct(model.Id);
                product.Title = model.Title;
                product.CategoryId = model.CategoryId;
                product.Description = model.Description;
                product.Image = model.Image;
                product.LastUpdateDate = DateTime.Now;
                product.Price = model.Price;
                product.Quantity = model.Quantity;

                _repository.Update(product);

                return new ResultViewModel
                {
                    Success = true,
                    Message = "Produto alterado com sucesso!",
                    Data = product
                };
            }
            catch (Exception ex)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
}