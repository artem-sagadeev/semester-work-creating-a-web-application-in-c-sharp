modelBuilder
    .Entity<Employee>()
    .ToTable("Employees", b => b.IsTemporal());