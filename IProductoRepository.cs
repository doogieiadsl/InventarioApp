using System;

public interface IProductoRepository
{
    // <summary>
    // Agregar un producto al repositorio
    // </summary>
    void Agregar(Producto producto);
    
    // <summary>
    // Obtiene un prodcuto por su Id
    // Returna Null si no lo encuentra
    // </summary>
    Producto? ObtenerPorId (int id);

    // <summary>
    // Obtiene todos los productos por Id
    // </summary>
    IEnumerable<Producto> ObtenerTodos();

    // <summary>
    // Actualiza un producto existente
    // </summary>
    bool Actualizar (Producto producto);

    // <summary>
    // Elimina un prodcuto por su Id
    // Returna Null si no lo encuentra
    // </summary>
    bool Eliminar(int id);

    // <summary>
    // Cantidad total de Preoductos en el repositorio
    // </summary>
    int Cantidad { get; }

}