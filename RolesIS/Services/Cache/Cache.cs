﻿using RolesIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RolesIS.Services.Cache
{
    public static class Cache //: ICache
    {
        private static ApplicationDbContext _dbContext;

        public static void Initialize()
        {
            _dbContext = new ApplicationDbContext();

            UpdateUsers(); 
            UpdateProductos(); 
            UpdateCompras();
        }

        #region Models
        private static List<ApplicationUser> Users { get; set; }
        private static List<Compra> Compras { get; set; }
        private static List<Producto> Productos { get; set; }
        #endregion

        #region Methods
        public static ApplicationUser GetUser(Func<ApplicationUser, bool> predicate)
        {
            return Users.FirstOrDefault(predicate);
        }

        public static void AddOrRemoveUser(bool add, ApplicationUser user)
        {
            if (add)
                _dbContext.Users.Add(user);
            else
                _dbContext.Users.Remove(user);

            _dbContext.SaveChanges();

            UpdateUsers();
        }

        private static void UpdateUsers()
        {
            Users = _dbContext.Users.ToList();
        }

        public static Compra GetCompra(Func<Compra, bool> predicate)
        {
            return Compras.FirstOrDefault(predicate);
        }

        public static void AddOrRemoveCompra(bool add, Compra compra)
        {
            if (add)
                _dbContext.Compras.Add(compra);
            else
                _dbContext.Compras.Remove(compra);

            _dbContext.SaveChanges();

            UpdateCompras();
        }

        private static void UpdateCompras()
        {
            Compras = _dbContext.Compras.ToList();
        }

        public static Producto GetProducto(Func<Producto, bool> predicate)
        {
            return Productos.FirstOrDefault(predicate);
        }

        public static void AddOrRemoveProducto(bool add, Producto producto)
        {
            if (add)
                _dbContext.Productoes.Add(producto);
            else
                _dbContext.Productoes.Remove(producto);

            _dbContext.SaveChanges();

            UpdateProductos();
        }

        public static void UpdateProductos()
        {
            Productos = _dbContext.Productoes.ToList();
        }
        #endregion
    }
}