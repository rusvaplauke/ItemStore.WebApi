﻿namespace ItemStore.WebApi.Models.DTOs.ShopDtos;

public class PutShopDto
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public string Address { get; set; } = "";
}
