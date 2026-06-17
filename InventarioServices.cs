using InventarioApp.src.Factories;
using System;

public class InventarioServices
{
    private readonly InMemoryProductoRepositories _repository;
    private readonly JsonInventarioStorage _storage;
    private readonly string _rutaInventario;

    public InventarioServices(string rutaInventario = "inventario.json")
    {
        _repository = new InMemoryProductoRepositories();
        _storage = new JsonInventarioStorage();
        _rutaInventario = rutaInventario;

        CargarInventario();
    }
    public void CargarInventario()
    {
        if (File.Exists(_rutaInventario))
        {
            var productos = _storage.Cargar(_rutaInventario);
            foreach (var producto in productos)
            {
                _repository.Agregar(producto);
            }
        }
    }
    public void AgregarProducto(string nombre, decimal precio, int cantidad, CategoriaProducto categoria)
    {
        var producto = ProductoFactory.Crear(nombre, precio, cantidad, categoria);
        _repository.Agregar(producto);
        Persistir();
    }
    public IEnumerable<Producto> ObtenerTodosLosProductos()
    {
        return _repository.ObtenerTodos();
    }
    public Producto? ObtenerProdcutoPorId(int id)
    {
        return _repository.ObtenerPorId(id);
    }
    public void ActualizarProducto(int id, string nombre, decimal precio, int cantidad, CategoriaProducto categoria)
    {
        var producto = _repository.ObtenerPorId(id);
        if (producto != null)
        {
            producto.Nombre = nombre;
            producto.Precio = precio;
            producto.Cantidad = cantidad;
            producto.Categoria = categoria;
            _repository.Actualizar(producto);
            Persistir();
        }
    }
    public void EliminarProducto(int id)
    {
        _repository.Eliminar(id);
        Persistir();
    }
    public IEnumerable<Producto> BuscarPorCategoria(CategoriaProducto categoria)
    {
        return _repository.BuscarPorCategoria(categoria);
    }
    public IEnumerable<Producto> BuscarPorNombre(string nombre)
    {
        return _repository.BuscarPorNombre(nombre);
    }
    public IEnumerable<Producto> ObtenerProductosBajoStock(int minimo = 5)
    {
        return _repository.ObtenerStockBajo(minimo);
    }
    public decimal ObtenerValorTotalInventario()
    {
        return _repository.ObtenerValorTotalInventario();
    }
    public decimal ObtenerPrecioPromedio()
    {
        return _repository.ObtenerPrecioPromedio();
    }
    public Producto? ObtenerProductoMasCaro()
    {
        return _repository.ObtenerProductoMasCaro();
    }

    // Metodos Reportes
    public string GenerarResumen()
    {
        var productos = _repository.ObtenerTodos();
        var generador = new GeneradorReportes(productos);
        return generador.GenerarResumen();
    }
    public string GenerarReporteStockBajo(int minimo = 5)
    {
        var productos = _repository.ObtenerTodos();
        var generador = new GeneradorReportes(productos);
        return generador.GenerarReporteStockBajo();
    }
    public string GenerarTopProductos()
    {
        var productos = _repository.ObtenerTodos();
        var generador = new GeneradorReportes(productos);
        return generador.GenerarTopProductos();
    }
    public string ExportarCsv()
    {
        var productos = _repository.ObtenerTodos();
        var generador = new GeneradorReportes(productos);
        return generador.ExportarCsv(); 
    }

    private void Persistir()
    {
        _storage.CrearBackup(_rutaInventario);
        var productos = _repository.ObtenerTodos().ToList();
        _storage.Guardar(productos, _rutaInventario);
    }
}	

