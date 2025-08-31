﻿using App.Domain.Entities.Common;

namespace App.Domain.Entities;
public class Category : BaseEntity<long>, IAuditEntity
{
    public string Name { get; set; }
    public List<Product>? Products { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
}

