using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioApp.src.Factories
{
    public static class ProductoFactory
    {
        private static int _nextId = 1;

        public static Producto Crear(string nombre,decimal precio,int cantidad,CategoriaProducto categoria = CategoriaProducto.Otros)
        {
            // Realizamos los guard Clauses - Validar Temprano
            // Validamos nombre
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentNullException("Nombre es requerido", nameof(nombre));
            // Validamos Precio
            if (precio < 0) throw new ArgumentOutOfRangeException("Precio no puede ser negativo", nameof(precio));
            // Validamos Cantidad
            if (cantidad < 0) throw new ArgumentOutOfRangeException("Cantida no puede ser menor a cero", nameof(cantidad));

            return new Producto //usamos el return regresando un nuevo obejeto ya con valor asigando a susu atributos
            {
                Id = _nextId++,
                Nombre = nombre,
                Precio = precio,
                Cantidad = cantidad,
                Categoria = categoria,
                FechaRegistro = DateTime.Now
            };
        }
        public static Producto CrearConStock (string nombre, decimal precio, int cantidad)
        {
            // Validamos Cantidad
            if (cantidad <= 0) throw new ArgumentOutOfRangeException("Cantida no puede ser menor a cero", nameof(cantidad));

            return (Crear(nombre, precio, cantidad));
        }
    }
}
