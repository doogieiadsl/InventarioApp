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
	public bool Eliminar(int id)
	{ 
		var producto = ObtenerPorId(id);
		if (producto == null) return false;

		return _productos.Remove(producto);
	}
	public int Cantidad => _productos.Count;
}
