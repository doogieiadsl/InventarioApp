// ============================================================
// SISTEMA DE INVENTARIO - Clase 1.1
// Estado: Mensaje de bienvenida
// ============================================================

using InventarioApp.src.Factories;

//var fileManager = new Filemanager();
//string contenido = ("Inventario Actualizado");
//fileManager.Escribir("inventario.txt", contenido);

//string leerContenido = fileManager.Leer("inventario.txt");
//Console.WriteLine(contenido); 


var servicio = new InventarioServices();
bool activo = true;

while (activo)
{
    MostrarMenu();
    string opcion = Console.ReadLine() ?? "";

    switch (opcion)
    {
        case "1":
            AgregarProducto();
            break;
        case "2":
            ListarProductos();
            break;
        case "3":
            BuscarPorId();
            break;
        case "4":
            EliminarProducto();
            break;
        case "5":
            BuscarPorCategoria();
            break;
        case "6":
            MostrarResumen();
           break;
        case "7":
            MostrarStockBajo();
            break;
        case "8":
            MostrarEstadisticas();
            break;
        case "9":
            ExportarCsv();
            break;
        case "10":
            activo = false;
            Console.WriteLine("\n !Hasta luego¡");
            break;
        default:
            Console.WriteLine("\n Opcion no valida.");
            break;
    }
    void MostrarMenu() 
    {
        Console.WriteLine("==========================================");
        Console.WriteLine("    SISTEMA DE GESTIÓN DE INVENTARIO      ");
        Console.WriteLine("==========================================");
        Console.WriteLine();
        Console.WriteLine("1. Agregar Producto");
        Console.WriteLine("2. Listar Producto");
        Console.WriteLine("3. Buscar por Id");
        Console.WriteLine("4. Eliminar Producto");
        Console.WriteLine("5. Buscar Por categoria");
        Console.WriteLine("6. Ver Resumen");
        Console.WriteLine("7. Ver Stock Bajo");
        Console.WriteLine("8. Ver Estadisticas");
        Console.WriteLine("9. Exportar CSV");
        Console.WriteLine("10. Salir");
        Console.WriteLine("\n  Selecciona una opcion");
    }

    void AgregarProducto()
    {
        Console.WriteLine("\n Introduce el nombre del Producto: ");
        string nombre = Console.ReadLine() ?? "";
        
        Console.WriteLine("\n Introduce el precio del Producto: ");
        decimal precio = decimal.Parse(Console.ReadLine() ?? "0");

        Console.WriteLine("\n Introduce la cantidad de Productos: ");
        int cantidad = int.Parse(Console.ReadLine() ?? "0");

        Console.WriteLine("\n Introduce la categoria del Producto: ");
        Console.WriteLine("\n Categorias: Electronica, Ropa, Alimentos, Hogar, Deportes, Libros, Muebles, Otros: ");
        string categoriaStr = Console.ReadLine() ?? "Otros";

        if (Enum.TryParse<CategoriaProducto>(categoriaStr, true, out var categoria))
        {
            servicio.AgregarProducto(nombre, precio, cantidad, categoria);
            Console.WriteLine(" \n Producto Agregado Exitosamente");
        }
        else
        {
            Console.WriteLine(" \n Categoria no valida");
        }
    }
    void ListarProductos()
    {
        var productos = servicio.ObtenerTodosLosProductos();
        if (productos == null)
        {
            Console.WriteLine(" \n No existen Productos");
            return;
        }
        Console.WriteLine(" \n === PRODCUTOS ===");
        foreach (var producto in productos) {
            Console.WriteLine($"ID: {producto.Id}, Nombre: {producto.Nombre}, Precio: {producto.Precio}, Cantidad: {producto.Cantidad}, Total: {producto.ValorTotal}, Categoria: {producto.Categoria }");
        }
    }
    void BuscarPorId()
    {
        Console.WriteLine("\nIngresa el Id del producto");
        int id = int.Parse(Console.ReadLine() ?? "0");

        var producto = servicio.ObtenerProdcutoPorId(id);
        if (producto != null)
        {
            Console.WriteLine("\n Producto Encontrado");
            Console.WriteLine($"ID: {producto.Id}");
            Console.WriteLine($"Nombre: {producto.Nombre}");
            Console.WriteLine($"Precio: {producto.Precio}");
            Console.WriteLine($"Cantidad: {producto.Cantidad}");
            Console.WriteLine($"Valor Total: {producto.ValorTotal}");
            Console.WriteLine($"Categoria: {producto.Categoria}");
        } 
        else {
        
            Console.WriteLine("\n Producto no localizado");
        }
    }
    void EliminarProducto()
    {
        Console.WriteLine("\nIngresa el Id del producto que deseas eliminar");
        int id = int.Parse(Console.ReadLine() ?? "0");

        var producto = servicio.ObtenerProdcutoPorId(id);
        if (producto != null)
        {
            servicio.EliminarProducto(id);
            Console.WriteLine("\n Prodcuto eliminado");
        }
        else
        {
            Console.WriteLine("\n Prodcuto no localizado");
        }

    }
    void BuscarPorCategoria()
    {
        Console.WriteLine("\n Introduce la categoria del Producto: ");
        Console.WriteLine("\n Categorias: Electronica, Ropa, Alimentos, Hogar, Deportes, Libros, Muebles, Otros: ");
        string categoriaStr = Console.ReadLine() ?? "Otros";

        if (Enum.TryParse<CategoriaProducto>(categoriaStr, true, out var categoria))
        {
            var productos = servicio.BuscarPorCategoria(categoria);
            if (!productos.Any())
            {
                Console.WriteLine("\n No hay Prodcutos en esta categoria");
                return;
            } 
            Console.WriteLine($" \n === PRODCUTOS EN ESTA CATEGORIA {categoria} ===");
            foreach (var producto in productos)
            {
                Console.WriteLine($"ID: {producto.Id}, Nombre: {producto.Nombre}, Precio: {producto.Precio}, Cantidad: {producto.Cantidad}, Total: {producto.ValorTotal}, Categoria: {producto.Categoria}");
            }
        }
        else
        {
            Console.WriteLine($"\n Categoria no valida");
        }
    }
    void MostrarResumen()
    {
        var resumen = servicio.GenerarResumen();
        Console.WriteLine($"\n{resumen}");
    }
    void MostrarStockBajo()
    {
        var reporte = servicio.GenerarReporteStockBajo();
        Console.WriteLine($"\n{reporte}");
    }
    void MostrarEstadisticas()
    {
        Console.WriteLine($" \n === ESTADISTICAS ===");
        Console.WriteLine($" \n Valor Totald el Inventario {servicio.ObtenerValorTotalInventario()}");
        Console.WriteLine($" \n Precio Promedio {servicio.ObtenerPrecioPromedio():F2}");

        var MasCaro = servicio.ObtenerProductoMasCaro();
        if (MasCaro != null)
        {
            Console.WriteLine($"Producto mas caro {MasCaro.Nombre}  , {MasCaro.Precio} ");
        }
    }
    void ExportarCsv()
    {
        string csv = servicio.ExportarCsv();
        Console.WriteLine($"\n{csv}");
    }
    
}

//var productos = new List<Producto>
//{
//    ProductoFactory.Crear("Laptop", 1200.00m, 3, CategoriaProducto.Electronica),
//    ProductoFactory.Crear("Camisa", 45.00m, 15, CategoriaProducto.Ropa),
//    ProductoFactory.Crear("Arroz", 12.00m, 50, CategoriaProducto.Alimentos),
//    ProductoFactory.Crear("Lampara", 35.00m, 2, CategoriaProducto.Hogar),
//    ProductoFactory.Crear("Balon", 25.00m, 8, CategoriaProducto.Deportes),
//    ProductoFactory.Crear("Mesa", 150.00m, 4, CategoriaProducto.Muebles)
//};

//var generador = new GeneradorReportes(productos);

//Console.WriteLine(generador.GenerarResumen());
//Console.WriteLine();

//Console.WriteLine(generador.GenerarReporteStockBajo());
//Console.WriteLine();

//Console.WriteLine(generador.GenerarTopProductos());
//Console.WriteLine();

//Console.WriteLine(generador.ExportarCsv());
//Console.WriteLine();

//Console.WriteLine(generador.ExportarResumenJson());
//Console.WriteLine();
//var almacenamiento = new JsonInventarioStorage();
//var productos = new List<Producto>
//{
//    new Producto
//    {
//        Id = 1,
//        Nombre = "Laptop",
//        Precio = 999.99m,
//        Cantidad = 10,
//        Categoria = CategoriaProducto.Ropa,
//        Estado = EstadoProducto.Activo
//    },
//    new Producto
//    {
//        Id = 2,
//        Nombre = "Camiseta",
//        Precio = 19.99m,
//        Cantidad = 50,
//        Estado = EstadoProducto.Activo
//    },
//};
//string ruta = "inventario_test.json";
//almacenamiento.CrearBackup(ruta);
//almacenamiento.Guardar(productos, ruta);

//Console.WriteLine("Inventario guardado correctamente");
//var productosCargados = almacenamiento.Cargar(ruta);
//Console.WriteLine("Inventario cargado correctamente");

//foreach (var p in productosCargados)
//{
//    Console.WriteLine($"Id: {p.Id}, Nombre: {p.Nombre}, Precio: {p.Precio}, Cantidad: {p.Cantidad}, Categoria: {p.Categoria}, Estado: {p.Estado}" );
//}




//var repository = new InMemoryProductoRepositories();

//Producto laptop = ProductoFactory.Crear(nombre: "Laptop Dell XPS 13", precio: 1200, cantidad: 5, CategoriaProducto.Electronica);
//Producto mouse = ProductoFactory.Crear(nombre: "Mouse Logitech MX Master", precio: 99, cantidad: 20, CategoriaProducto.Electronica);
//Producto teclado = ProductoFactory.Crear(nombre: "Teclado Mecánico", precio: 150, cantidad: 3, CategoriaProducto.Electronica);
//Producto silla = ProductoFactory.Crear(nombre: "Silla Ergonómica Herman Miller", precio: 500, cantidad: 8, CategoriaProducto.Muebles);
//Producto escritorio = ProductoFactory.Crear(nombre: "Escritorio Stand-up", precio: 300, cantidad: 2, CategoriaProducto.Muebles);

//repository.Agregar(laptop);
//repository.Agregar(mouse);
//repository.Agregar(teclado);
//repository.Agregar(silla);
//repository.Agregar(escritorio);

//Console.WriteLine($"Productos agregados: {repository.Cantidad}");

//IEnumerable<Producto> electronicos = repository.BuscarPorCategoria(CategoriaProducto.Electronica);
//Console.WriteLine($"Productos electronicos: {electronicos.Count()}");

//foreach (Producto producto in electronicos)
//{
//    Console.WriteLine($" {producto.Nombre} : {producto.Precio:C2}");
//}

//IEnumerable<Producto> conMouse = repository.BuscarPorNombre("mouse");
//Console.WriteLine($"\nProductos con mouse: {conMouse.Count()}");

//foreach (Producto producto in conMouse)
//{
//    Console.WriteLine($" {producto.Nombre} : {producto.Precio:C2}");
//}

//IEnumerable<string> nombres = repository.ObtenerNombres();
//Console.WriteLine($"\nTodos los nombres de los productos: {string.Join(", ", nombres)}");

//bool hayStockBajo = repository.HayStockBajo();
//Console.WriteLine($"\nHay stock bajo: {hayStockBajo}");
//Console.WriteLine($"Versión: {version}");
//Console.WriteLine($"Plataforma: {Environment.OSVersion}");
//Console.WriteLine($".NET Version: {Environment.Version}");
//Console.WriteLine();
//Console.WriteLine("Estructura del proyecto");
//Console.WriteLine("     InventarioApp/");
//Console.WriteLine("  e,f,b,c,d,a       |-- Program.cs");
//Console.WriteLine("         |-- InventarioApp.csproj");
//Console.WriteLine("         |-- .gitignore");
//Console.WriteLine("         |-- README.d");
//Console.WriteLine("         |-- src/");
//Console.WriteLine("             |-- Models");
//Console.WriteLine("Configuracion .csproject");
//Console.WriteLine("Carpeta src/ creada");
//Console.WriteLine("Metadatos Configurados");
//Console.WriteLine();
//Console.WriteLine("Proximo Paso: Checkpoint"); */

////Declaracion de variables

//var assembly = Assembly.GetExecutingAssembly();
//var version = assembly.GetName().Version;
//int cantidadProductos = 0; 
//decimal valorTotalDelinventario = 0.00m;
//bool SistemaActivo = true;
//string nombreSistema = "Sistema de Gestion de Inventarios";
//bool ValidaPrecio = true;

////Procedimientos//
//void MostrarAyuda()
//{
//    Console.WriteLine("USO: InventarioApp [comando] [opciones]");
//    Console.WriteLine();
//    Console.WriteLine("COMANDOS:");
//    Console.WriteLine("  --help, -h      Muestra esta ayuda");
//    Console.WriteLine("  --version, -v   Muestra la version del programa");
//    Console.WriteLine();
//    Console.WriteLine("EJEMPLOS:");
//    Console.WriteLine(" dotnet run -- --help");
//    Console.WriteLine(" dotnet run -- --version");
//}
//void MostrarBanner()
//{
//    Console.WriteLine("╔══════════════════════════════════════╗");
//    Console.WriteLine("║   SISTEMA DE GESTIÓN DE INVENTARIO   ║");
//    Console.WriteLine("╚══════════════════════════════════════╝");
//    Console.WriteLine();
//    Console.WriteLine();
//}

//// Ejecucion del Programa

//MostrarBanner();
//Console.WriteLine("Estado del Sistema");
//Console.WriteLine($"Nombre: {nombreSistema}");
//Console.WriteLine($"Productos registrados: {cantidadProductos}");
//Console.WriteLine($"Valor total del Inventario: {valorTotalDelinventario:N2}");
//Console.WriteLine($"Sistema Activo: {SistemaActivo}");

////EmpiezaNullable

//Console.WriteLine("Menu de aplicacion inventario");
//Console.WriteLine("Opcion 1 : listar");
//Console.WriteLine("Opcion 2 : agregar");
//Console.WriteLine("Opcion 3 : buscar");
//Console.WriteLine("Opcion 4 : salir");
//Console.WriteLine("Que desea hacer");
//Console.WriteLine();

//while (SistemaActivo)
//{
//    Console.WriteLine("Inventario");
//    Console.WriteLine("Elige una opcion");
//    string? Entrada = Console.ReadLine();

//    //Empieza switch
//    string comando = Entrada?.Trim().ToLower() ?? "salir";
//    switch (comando)
//    {
//        case "listar":
//            listar();
//            break;
//        case "agregar":
//            agregar();
//            break;
//        case "buscar":
//            break;
//        case "salir":
//            salir();
//            break;
//        default:
//            break;
//    }
//}
//void listar()
//{
//    Console.WriteLine($"Cantidad de productos del imventario: {cantidadProductos}");
//    Console.WriteLine($"Valor total del Inventario: {valorTotalDelinventario:N2}");
//}
//void agregar()
//{
//    Console.Write("Ingresa el Numero de productos: ");
//    string? Numproducto = Console.ReadLine();

//    if (string.IsNullOrWhiteSpace(Numproducto) || Numproducto.ToLower() == "salir")
//    {
//        Console.WriteLine("Caracter invalido");
//    }
//    else
//    {
//        if (int.TryParse(Numproducto, out int cantidad))
//        {
//            cantidadProductos = cantidad;
//            Console.Write($"{cantidadProductos} Producto(s) Agregado(s)"); //Ahora forzamos a ingresar el precio
//            while (ValidaPrecio)
//            {
//                Console.Write("Ingresa el Precio del  producto: ");
//                string? PrecioProducto = Console.ReadLine();

//                if (string.IsNullOrWhiteSpace(PrecioProducto) || PrecioProducto.ToLower() == "salir")
//                {
//                    Console.WriteLine("Precio invalido");
//                }
//                else
//                {
//                    if (decimal.TryParse(PrecioProducto, out decimal precio))
//                    {
//                        valorTotalDelinventario = cantidadProductos * precio;
//                        Console.WriteLine($"El Precio(s)  {PrecioProducto:N2} se Agrego a los {cantidadProductos} productos "); //Precio Valido
//                        Console.WriteLine($"El valor total del inventario es   {valorTotalDelinventario:N2}");
//                        ValidaPrecio = false;
//                    }
//                }
//            }
//        }
//    }

//}
//void salir()
//{
//    Console.WriteLine("Saliendo del Inventario, Hasta luego");
//    SistemaActivo = false;
//}
//////Captura de Cantidad
////Console.Write("Ingrese una cantidad: ");
////string? entradaCantidad = Console.ReadLine();
////if (int.TryParse(entradaCantidad, out int cantidad))
////{
////    Console.WriteLine($"Cantidad Valida: {cantidad}");
////    cantidadProductos = cantidad;
////}
////else
////{
////    Console.WriteLine("Error, debes ingresar un entero");
////}
//////Captura de Precio
////Console.Write("Ingrese un precio: ");
////string? entradaPrecio = Console.ReadLine();
////if (decimal.TryParse(entradaPrecio, out decimal precio))
////{
////    Console.WriteLine($"Precio Validado: {precio:N2}");
////    valorTotalDelinventario = cantidadProductos * precio;
////    Console.WriteLine($"El Valor Total del Inventario es : {valorTotalDelinventario}");
////}
////else
////{
////    Console.WriteLine("Error, debes ingresar un decimal");
////}


//// Modo interactivo si no se tienen argumentos

//Console.Write("Ingrese un comando o ingrese salir para terminar: ");
//string? entrada = Console.ReadLine();

//if (string.IsNullOrWhiteSpace(entrada) || entrada.ToLower() == "salir")
//{
//    Console.WriteLine("Saliendo del programa...");
//    Environment.Exit(0);
//}
//if (args.Length > 0)
//{
//    switch (args[0].ToLower())
//    {
//        case "--help":
//            MostrarAyuda();
//            Environment.Exit(0);
//            break;

//        case "--version":
//            Console.WriteLine($"InventariosApp v[{version}]");
//            Environment.Exit(0);
//            break;

//        default:
//            Console.WriteLine($"Error: comando desconociso: [{args[0]}]");
//            Console.WriteLine("Use --help para ver los comando disponibles");
//            Environment.Exit(1);
//            break;  
//    }
//}



