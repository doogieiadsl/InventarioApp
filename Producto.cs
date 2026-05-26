using System;

public class Producto
{
	private string _nombre = "";
	private decimal _precio;
	private int _cantidad;
	public int Id { get; set; }
	public string Nombre
	{
		get => _nombre;
		set
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new ArgumentException("Nombre no puede esatr vacio", nameof(Nombre));
			_nombre = value.Trim();
		}
	}
	public decimal Precio
	{
		get => _precio;
		set
		{
			if (value < 0)
				throw new ArgumentException("El precio no puede ser menor a 0", nameof(Precio));
			_precio = value;
		}
	}
	public int Cantidad
	{
		get => _cantidad;
		set
		{
			if (value < 0)
				throw new ArgumentException("Cantidad no puede ser menor a 0", nameof(Cantidad));
			_cantidad = value;
		}
	}
	public CategoriaProducto Categoria { get; set; }
	public EstadoProducto Estado { get; set; }
	public DateTime FechaRegistro { get; set; }
	public decimal ValorTotal => Precio * Cantidad;
}
