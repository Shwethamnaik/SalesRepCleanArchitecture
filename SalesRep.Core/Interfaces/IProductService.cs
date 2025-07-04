﻿using SalesRep.Core.DTO;

namespace SalesRep.Core.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductListDto>> GetAllProductsAsync();
    }
}
