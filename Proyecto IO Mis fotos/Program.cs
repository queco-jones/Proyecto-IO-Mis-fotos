using System;
using System.IO; // "Quiero leer y escribir ficheros"
using System.Diagnostics; //Quiero utilizar el "Process.Start"
using System.Drawing; //Libreria para poder dibujar
using System.Drawing.Imaging; 
using System.Windows.Forms;

namespace MisFotos
{
    class Program
    {
     
    //Paso 1:

    class Foto
    {
        public string Fichero;
        public string Fecha;
        public string Album;
        public int Likes;
    }
    // Lista de fotos implementada con un vector de como máximo 100 fotos

    class ListaFotos
    {
        //Capacidad máxima de la lista
        public const int MAX_FOTOS = 100;
        //Vector que almacena fotografias:
        public Foto[] Fotos = new Foto[MAX_FOTOS];
        //Numero de fotografias almacenadas:
        public int NumFotos = 0;
    }

  //Paso 2:Funciones

  //Función 1:

  static void EscribeFoto(Foto foto)
  {
   Console.WriteLine(foto.Fichero);
   Console.WriteLine(foto.Fecha);
   Console.WriteLine(foto.Album);
   Console.WriteLine(foto.Likes);
  }

  //Función 2: 
  static int PonFoto(ListaFotos lista,Foto foto)
  {
      if (lista.NumFotos == ListaFotos.MAX_FOTOS)
          return -1;

  lista.Fotos[lista.NumFotos]= foto;
  lista.NumFotos++;

  return 0;

  }

  //Función 3:
  static void EscribeLista (ListaFotos lista)
  {
      int i = 0;
      while (i < lista.NumFotos)
      {
          EscribeFoto(lista.Fotos[i]);
          i++;

      }

  } 
  //Función 4:Creamos una función que recibe una lista original con fotografias y el nombre del album que queremos buscar. 

  static ListaFotos DameFotosAlbum(ListaFotos lista, string album)
  {
            //Queremos una lista de todas las fotos de la lista que pertenecen al mismo album que nos piden. 
      ListaFotos lista2 = new ListaFotos();
       int i = 0;

       while (i < lista.NumFotos)
        {
         if (lista.Fotos[i].Album == album)
         {  
                    PonFoto(lista2, lista.Fotos[i]);
         }
                i++;

        }

            if (lista2.NumFotos == 0)
            return null;

            else
                return lista2;
  }

        //Paso 3:
        //Creo la función:

        static int CargaLista(ListaFotos lista, string nombreFichero)
        {
            //Comprobamos si existe el archivo para devolver el primer caso: 
            if (!File.Exists(nombreFichero))
                return -1;
            //Leemos archivo con "StreamReader" de nombre lector

            StreamReader Lector = new StreamReader(nombreFichero);
            //Leemos linea antes del while
            String Linea = Lector.ReadLine();

            while (Linea != null)
            {
                //Convertimos los datos de la linea en un vector de datos con el split para luego saber cuantos datos hay:
                string[] Datos = Linea.Split();

                //Comprobamos si, como dice el enunciado, el formato de 4 atributos es correcto o no:
                if (Datos.Length != 4)
                {
                    Lector.Close(); //cerramos lector antes del return 
                    return -2;
                }
                //Convertimos el dato Likes en Entero
                int likes = Convert.ToInt32(Datos[3]);
                //Creamos variable foto e Intentamos añadir la foto y si devuelve -1 la lista esta llena y no caben todas las fotos en la lista, asi que da -3:
                Foto foto = new Foto();
                foto.Fichero = Datos[0];
                foto.Fecha = Datos[1];
                foto.Album = Datos[2];
                foto.Likes = likes;
                if (PonFoto(lista, foto) == -1)
                    {
                        Lector.Close(); //Cerramos lector antes del return
                        return -3;
                    }
                Linea = Lector.ReadLine();
            }
            //Hay que cerrar el lector:
            Lector.Close();
            return 0;
        }
        //Paso 4:Dentro del Main del programa principal:

        //Paso 5:Escribir funciones:



        //Función 1 Paso 5:
        static Foto DameFavorita(ListaFotos lista)
        {
           if(lista.NumFotos==0)
            {
                return null;
            }
            Foto FotoConMasLikes = lista.Fotos[0];

            int i = 1;

            while(i<lista.NumFotos)
            {
                if (lista.Fotos[i].Likes > FotoConMasLikes.Likes)
                {
                    FotoConMasLikes = lista.Fotos[i];

                }
                i++;

            }
            return FotoConMasLikes;
        }

        //Función 2 Paso 5: 

        static int HazLike(ListaFotos lista, string ficheroImagen)
        {
            int i = 0;
            while(i<lista.NumFotos)
            {
                if (lista.Fotos[i].Fichero==ficheroImagen)
                {
                    lista.Fotos[i].Likes++;
                    return 0;
                }
                i++;

            }
            return -1;

        }

        //Paso 6: 
    //Función que me dan:
    
        static int MuestraImagen(ListaFotos lista, string ficheroImagen)
        {
            int i = 0; 

            while(i<lista.NumFotos) //recorremos "lista.NumFotos" para encontrar la imagen que nos han pasado
            {
                if (lista.Fotos[i].Fichero == ficheroImagen) //Comprobamos si el fichero en la opsición i es igual al que nos han pasado. 
                {
                                                           //Enseñamos la fotografia por Pantalla y devolvemos 0 para acabar la función. 
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = Path.GetFullPath(ficheroImagen),
                        UseShellExecute = true
                    });
                    Console.ReadKey();
                    return 0;
                }
                i++;
            }
            return -1; //Si ha llegado al final es porque no ha enocntrado el fichero en la lista




        }

        //Paso 7:

        //Funcion 1:

        static int EliminaFoto(ListaFotos lista,string ficheroImagen)
        {
            bool encontrado = false;
            int valordei = 0;
            int i = 0; 
            while (i < lista.NumFotos && !encontrado) //Recorremos el vector de Fotos hasta encontrar el fichero que nos envian
            {
                if (lista.Fotos[i].Fichero == ficheroImagen) //si en la posición i de la lista de Fotos (Apartado de ficheros) es igual al fichero imagen que se ha enviado guardamos la posición i y terminamos la busqueda
                {
                    valordei = i; //guardamos posición
                    encontrado = true; //Terminamos búsqueda
                }
                i++; 
            }


            if(encontrado) //En el caso de que encontremos la foto en la lista
            {
            int j = valordei;
                while (j < lista.NumFotos - 1)
                {
                    lista.Fotos[j] = lista.Fotos[j + 1]; //Desplazamos todas las fotos a la izquierda a partir de la fotografia que me envían para sustituirla y que desaparezca
                    j++;
                }
                lista.NumFotos--; //Disminuimos numero de fotografias que consideramos almacenadas en el.
                return 0; //Se ha hecho la operación así que devolvemos 0
            }

            return -1; //Si no encontramos la foto
        
        }

        //Función 2:
        static void SalvarLista (ListaFotos lista, string fichero)
        {
            StreamWriter escritor = new StreamWriter(fichero);  //Utilizamos un escritor de archivos de texto que escribe lo que hay en el fichero

            int i = 0; 
            while(i<lista.NumFotos)
            {
                escritor.WriteLine(lista.Fotos[i].Fichero +" "+ lista.Fotos[i].Fecha +" "+ lista.Fotos[i].Album + " "+ lista.Fotos[i].Likes); //Con escritor.WriteLine escribimos todos los datos de la foto en un fichero
                
                i++;
            }

            escritor.Close(); //Cerramos el escritor de archivos

        }
        //Paso 8: 

        //Función 1: Funcion que convierte la imagen.
        static int FotoBlancoNegro(ListaFotos lista, string ficheroImagen)
        {
            int i = 0; 
            // Buscamos la foto en nuestra lista
            while (i < lista.NumFotos && lista.Fotos[i].Fichero != ficheroImagen)
            {
                i++;
            }

            if (i == lista.NumFotos)
                return -1;

            // Cargamos la imagen como matriz de píxeles
            Color[,] pixels = CargaImagen(ficheroImagen);

            int filas = pixels.GetLength(0);
            int columnas = pixels.GetLength(1);

            int fila = 0;

            while (fila < filas)
            {
                int columna = 0;

                while (columna < columnas)
                {
                    Color pixel = pixels[fila, columna];

                    // Calculamos el gris haciendo la media de los tres colores
                    int gris = (pixel.R + pixel.G + pixel.B) / 3;

                    pixels[fila, columna] = Color.FromArgb(gris, gris, gris);

                    columna++;
                }

                fila++;
            }

            // Creamos el nombre del nuevo fichero
            string nuevoFichero = "BN_" + Path.GetFileNameWithoutExtension(ficheroImagen) + ".jpg";

            // Guardamos la imagen transformada
            GuardarImagen(pixels, nuevoFichero);

            // Creamos una nueva foto y la añadimos a la lista
            Foto nuevaFoto = new Foto();
            nuevaFoto.Fichero = nuevoFichero;
            nuevaFoto.Fecha = lista.Fotos[i].Fecha;
            nuevaFoto.Album = lista.Fotos[i].Album;
            nuevaFoto.Likes = lista.Fotos[i].Likes;

            if (PonFoto(lista, nuevaFoto) == -1)
                return -2;

            return 0;   
        }

        //Función 2:Función para trabajar con las imagenes 

        static Color[,] CargaImagen(string nombreFichero)
        {
            Bitmap imagen = new Bitmap(nombreFichero);
            Color[,] pixels = new Color[imagen.Height, imagen.Width];

            for (int fila = 0; fila < imagen.Height; fila++)
            {
                for (int columna = 0; columna < imagen.Width; columna++)
                {
                    pixels[fila, columna] = imagen.GetPixel(columna, fila);
                }
            }

            imagen.Dispose();
            return pixels;
        }

        //Función 3: Mostrar la Imagen: 
        static void MuestraImagenPixels(Color[,] pixels)
        {
            int filas = pixels.GetLength(0);
            int columnas = pixels.GetLength(1);

            Bitmap imagen = new Bitmap(columnas, filas);

            for (int fila = 0; fila < filas; fila++)
            {
                for (int columna = 0; columna < columnas; columna++)
                {
                    imagen.SetPixel(columna, fila, pixels[fila, columna]);
                }
            }

            Form ventana = new Form();
            ventana.Text = "Imagen";
            ventana.Width = 800;
            ventana.Height = 600;

            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Image = imagen;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            ventana.Controls.Add(pictureBox);
            Application.Run(ventana);
            imagen.Dispose();
        }

        //Función 4: Guardar la Imagen: 

        static void GuardarImagen(Color[,] pixels, string nombreFichero)
        {
            int filas = pixels.GetLength(0);
            int columnas = pixels.GetLength(1);

            Bitmap imagen = new Bitmap(columnas, filas);

            for (int fila = 0; fila < filas; fila++)
            {
                for (int columna = 0; columna < columnas; columna++)
                {
                    imagen.SetPixel(columna, fila, pixels[fila, columna]);
                }
            }

            imagen.Save(nombreFichero, ImageFormat.Jpeg);
            imagen.Dispose();
        }


        //Programa principal
        static void Main(string[] args)
        {
            //Creamos la lista vacía de fotos que se usará en todo el programa:
            ListaFotos lista = new ListaFotos();
            //Inicializamos la opción en -1 para forzar a que el bucle while arranque:
            string opcion = "";
            //Bucle para que el menú se repita constantemente hasta que el usuario pulse 0
            while (opcion != "0")
            {
                //1.Mostraremos las opciones del menú por pantalla
                Console.WriteLine("Menú");
                Console.WriteLine("0.Salir");
                Console.WriteLine("1.Cargar desde fichero");
                Console.WriteLine("2.Ver todas las fotos");
                Console.WriteLine("3.Ver fotos de un album");
                Console.WriteLine("4.Añadir una foto");
                Console.WriteLine("5.Mostrar en consola los datos de la foto favorita ");
                Console.WriteLine("6.Sumar 1 al número de likes de la foto identificada con el nombre del fichero que contiene su imagen, que introduce el usuario por teclado ");
                Console.WriteLine("7.Mostrar imagen del fichero correspondiente ");
                Console.WriteLine("8.Eliminar la foto identificada con el nombre del fichero ");
                Console.WriteLine("9.Salvar la lista de Fotos en el Fichero ");
                Console.WriteLine("10- Convertir una foto a blanco y negro");
                Console.Write("Elige una opción  ");
                opcion = Console.ReadLine();

                if (opcion == "0")
                {
                    Console.WriteLine("Saliendo");
                }
                else if (opcion == "1")
                {
                    Console.WriteLine("Nombre del fichero: ");
                    string fichero = Console.ReadLine();
                    int res = CargaLista(lista, fichero);
                    if (res == 0)
                        Console.WriteLine("Cargado correctamente.");
                    else if (res == -1)
                        Console.WriteLine("Error:Fichero no encontrado.");
                    else if (res == -2)
                        Console.WriteLine("Error:Formato incorrecto.");
                    else if (res == -3)
                        Console.WriteLine("Error:lista llena.");
                }
                else if (opcion == "2")
                {
                    EscribeLista(lista);
                }
                else if (opcion == "3")
                {
                    Console.Write("Álbum a buscar:");
                    string album = Console.ReadLine();
                    ListaFotos sublista = DameFotosAlbum(lista, album);

                    if (sublista == null)
                        Console.WriteLine("No hay fotos de ese album");
                    else EscribeLista(sublista);
                }
                else if(opcion== "4")
                {
                    Console.Write("Nombre: ");
                    string n = Console.ReadLine();
                    Console.Write("Fecha: ");
                    string f = Console.ReadLine();
                    Console.Write("Album: ");
                    string a = Console.ReadLine();
                    Console.Write("Likes: ");
                    int l = Convert.ToInt32(Console.ReadLine());
                    Foto nueva = new Foto();
                    nueva.Fichero = n;
                    nueva.Fecha = f;
                    nueva.Album = a;
                    nueva.Likes = l;
                    if (PonFoto(lista, nueva) == 0)
                        Console.WriteLine("Añadida con éxito. ");
                    else Console.WriteLine("Error:La lista esta llena. ");

                }
                else if (opcion == "5") //Mostrar fotografia favorita
                {
                    Foto favorita = DameFavorita(lista);
                    if(favorita== null)
                    {
                        Console.WriteLine("No hay fotografias en la lista. ");
                    }

                    else
                    {
                        Console.WriteLine("La fotografia con más likes es: ");
                        EscribeFoto(favorita);

                    }

                }

                else if(opcion == "6") //Dar un like
                {
                    Console.WriteLine("Introduce el nombre del fichero: ");
                    string ficheroImagen = Console.ReadLine();

                    int resultado = HazLike(lista, ficheroImagen); //Se le pasa a la función Haz Like para que aumente una vez el numero de Likes

                    if (resultado == 0)
                    {
                        Console.WriteLine("Like añadido correctamente. ");
                    }
                    else
                    {
                        Console.WriteLine("No se ha encontrado esa fotografia ");
                    }


                }
                else if(opcion == "7") //Mostrar imagen del fichero correspodiente 
                {
                    Console.WriteLine("Introduce el nombre del fichero: "); //Nos pasan el nombre del fichero 
                    string ficheroImagen = Console.ReadLine(); //asignamos el nombre del fichero al parametro ficheroImagen de tipo string

                    int resultado = MuestraImagen(lista, ficheroImagen); //Llamaos a la función para que nos de un entero como resultado
                    if(resultado==0)
                    {
                        Console.WriteLine("Imagen mostrada correctamente");
                    }
                    else if (resultado==-1)
                    {
                        Console.WriteLine("El fichero no existe en la lista ");
                    }
                }
                else if(opcion == "8") //Eliminar foto con el nombre del fichero que contiene su imagen
                {
                    Console.WriteLine("Introduce el nombre del fichero: ");
                    string ficheroImagen = Console.ReadLine(); //Asignamos de nuevo el nombre del fichero al parametro ficheroImagen de tipo string
                    int resultado = EliminaFoto(lista, ficheroImagen);
                    if (resultado == 0)
                        Console.WriteLine("Operación realizada con éxito. ");
                    else if (resultado == -1)
                        Console.WriteLine("Foto no encontrada en la lista. ");

                }
                else if(opcion == "9")
                {
                    Console.WriteLine("Introduce el nombre del fichero. ");
                    string fichero = Console.ReadLine();   //Asignamos de nuevo el nombre del fichero al parametro ficheroImagen de tipo string
                    SalvarLista(lista, fichero);
                    Console.WriteLine("Lista guardada correctamente. ");
                }
                
                else if (opcion == "10")
                {
                    Console.WriteLine("Introduce el nombre del fichero de la foto:");
                    string ficheroImagen = Console.ReadLine();

                    int resultado = FotoBlancoNegro(lista, ficheroImagen);

                    if (resultado == 0)
                        Console.WriteLine("Foto convertida a blanco y negro correctamente.");
                    else if (resultado == -1)
                        Console.WriteLine("No se ha encontrado la foto.");
                    else if (resultado == -2)
                        Console.WriteLine("La lista está llena.");
                }

                else
                {
                    Console.WriteLine("Opcion no válida. ");
                }

             }

        }
    }
        


}  //Barra del nameSpace 



