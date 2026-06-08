using System;

public class InMemoryProductoRepositories : IProductoRepository
{
	private readonly List<Producto> _productos = new();
	private int _proximoId = 1;

	//Comenzamos la implementacion de la interfaz "IProductoRepository" con cada uno de sus metodos
	//1.- Agregar
	public void Agregar (Producto producto)
	{
		producto.Id = _proximoId++;
		_productos.Add(producto);
	}
	//2.- Obtener por Id
	public Producto? ObtenerPorId(int id)
	{
        return _productos.FirstOrDefault(p => p.Id == id);
	}
	//3.- Obtener Todos
	public IEnumerable<Producto> ObtenerTodos()
	{
		return _productos.AsReadOnly();
	}
	//4.- Actualizar
	public bool Actualizar(Producto producto)
	{
		Producto? existente  = ObtenerPorId(producto.Id);
		if (existente == null) return false;

		existente.Nombre = producto.Nombre;
		existente.Precio = producto.Precio;
		existente.Cantidad = producto.Cantidad;
		existente.Categoria = producto.Categoria;
		existente.Estado = producto.Estado;
		return true;
	}
	//5. Eliminar
	public bool Eliminar(int id)
	{ 
		var producto = ObtenerPorId(id);
		if (producto == null) return false;

		return _productos.Remove(producto);
	}
	//6. Cantidad
	public int Cantidad => _productos.Count;

	//======== Busquedas con Where LINQ ==============
	public IEnumerable<Producto> BuscarPorCategoria (CategoriaProducto categoria)
	{
		return _productos.Where(p => p.Categoria == categoria);
	}
	public IEnumerable<Producto> BuscarPorNombre(string nombre)
	{
        return _productos.Where(p => p.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase));
    }
	public IEnumerable<Producto> BuscarPorRangoPrecio (decimal PrecioMinmo, decimal PrecioMaximo)
	{
		return _productos.Where(p => p.Precio >= PrecioMinmo && p.Precio <= PrecioMaximo);
	}
    //======== Busquedas con Select & Any ==============
	public IEnumerable<String> ObtenerNombres()
	{
		return _productos.Select(p => p.Nombre);
	}
	public bool HayStockBajo()
	{
		return _productos.Any(p => p.Cantidad < 5);
	}
	// ============= Metodos de Ordenacion ========
	public IEnumerable<Producto> ObtenerOrdenadosPorPrecio()
	{
		return _productos.OrderBy(p => p.Precio);
	}
	public IEnumerable<Producto> ObtenerTopPorPrecio(int cantidad)
	{
		return _productos.OrderByDescending(p => p.Precio).Take(cantidad);
	}
	// =========== GroupBy y conversion a Dictionary ======
	public IEnumerable<IGrouping<CategoriaProducto, Producto>> AgruparPorCategoria()
	{
		return _productos.GroupBy(p => p.Categoria);
	}
	public Dictionary<CategoriaProducto, int> ContarPorCategoria()
	{
		return _productos //Genera List<Producto>
			.GroupBy(p => p.Categoria) // Agrupa por Categoria
			.ToDictionary(g => g.Key, g => g.Count()); //Realiza el conteo
	}
	// ====== Agregaciones Sum, Average y Maxby =========
	public decimal ObtenerValorTotalInventario()
	{
		return _productos.Sum(p => p.ValorTotal);
	}
	public decimal ObtenerPrecioPromedio()
	{
		if (_productos.Count == 0) return 0;
		return _productos.Average(p => p.Precio);
	}
	public Producto? ObtenerProductoMasCaro()
	{
		return _productos.MaxBy(p => p.Precio);
	}
	public Dictionary<CategoriaProducto, decimal> ObtenerValoroPorCategoria()
	{
		return _productos //Listar Productos
			.GroupBy(p => p.Categoria) //Agrupamos por categoria
			.ToDictionary(g => g.Key, g => g.Sum(x => x.ValorTotal)); //Despues de Agrupar x categoria sumamos cada categoria  (Valor x categoria)
	}
	public IEnumerable<Producto> ObtenerStockBajo (int umbral = 5)
	{
		return _productos.Where (p => p.Cantidad <  umbral);
	}

}
