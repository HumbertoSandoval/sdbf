using Microsoft.EntityFrameworkCore.Internal;
using Practica01.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Practica01
{
    class Program
    {
        private static string Nombre;

        static void Main(string[] args)
        {
            string R;
            //directorios();
            // Archivos();
            // MetodoDeprueba();

            // var prueba = Clavedelocalizacion.Inicializar("1-1-0001-00-00-00-01");
            //Console.WriteLine(prueba.Correcta);
            /* using (var db = new SqliteContext())
             {
                 db.Database.EnsureCreated();
                 var prueba = Clavedelocalizacion.Inicializar("1-1-0001-00-00-00-01");
                 db.Add(prueba);
                 db.SaveChanges();
             }
              //Preguntar si quieres guardar en formato CSV O TAB*/
            //Preguntar si todos los valores le ponen comillas
            //pedir el nombre del archivo (extension por default,csv o txt)
            


            Console.WriteLine("Quiere que el Formato sea CSV O TAB ");
            Console.WriteLine("Presione 1 para CSV");
            Console.WriteLine("Presione 2 para TAB");
            R = Console.ReadLine();
            if (R == "1")
            {
                Console.WriteLine("Escogio la Opcion CSV");
                Console.WriteLine("¿Quieres que todos lo valores lleven comillas? ");
                Console.WriteLine("Presione 1 Si");
                Console.WriteLine("Presione 2 NO");
                R = Console.ReadLine();
                if (R=="1")
                {
                    Console.WriteLine("Escogio la Opcion de Comillas");
                    Console.WriteLine("¿Como que nombre quiere guardar el archivo?");
                    Nombre = Console.ReadLine();
                   
                    GuardarCSV();
                }
                else
                {
                    if (R=="2")
                    {
                        Console.WriteLine("Escogio la Opcion de No Comilllas");
                        Console.WriteLine("¿Como que nombre quiere guardar el archivo?");
                        Nombre = Console.ReadLine();

                        GuardarCSVSinComillas();

                    }
                    else
                    {
                        Console.WriteLine("Opcion Incorrecta Vuelva, a leer las intrucciones");
                    }
                }
                
                    
            }
            else
            {
                if (R == "2")
                {
                    Console.Write("Escogio la Opcion TAB");
                    Console.WriteLine("¿Quieres que todos lo valores lleven comillas? ");
                    Console.WriteLine("Presione 1 Si");
                    Console.WriteLine("Presione 2 NO");
                    R = Console.ReadLine();
                    if (R == "1")
                    {
                        Console.WriteLine("Escogio la Opcion de Comillas");
                        Console.WriteLine("¿Como que nombre quiere guardar el archivo?");
                        Nombre = Console.ReadLine();

                        GuardarTAB();
                    }
                    else
                    {
                        if (R=="2")
                        {
                            Console.WriteLine("Escogio la Opcion de No Comilllas");
                            Console.WriteLine("¿Como que nombre quiere guardar el archivo?");
                            Nombre = Console.ReadLine();

                           GuardarTABsincomillas ();
                        }
                        else
                        {
                            Console.WriteLine("Opcion Incorrecta Vuelva, a leer las intrucciones");
                        }
                    }

                }
                else
                {
                    Console.WriteLine("Opcion Incorrecta Vuelva, a leer las intrucciones");
                    Console.Read();
                    return;
                }
            }

            //GuardarCSV();
            Console.ReadLine();
            
            
        }
        private static void MetodoDeprueba()
        {
            string clave = "1-1-0001-00-00-00-01";
            var claveSeparada = clave.Split("-", StringSplitOptions.RemoveEmptyEntries);
            var nuevaClave = new Clavedelocalizacion();
            nuevaClave.Original = clave;
            nuevaClave.Subsistema = int.Parse(claveSeparada[0]);
            nuevaClave.Sector = int.Parse(claveSeparada[1]);
            nuevaClave.Manzana = int.Parse(claveSeparada[2]);
            nuevaClave.Lote = int.Parse(claveSeparada[3]);
            if (claveSeparada.Length == 8)
            {
                nuevaClave.Nivel1 = int.Parse(claveSeparada[4]);
                nuevaClave.Nivel2 = int.Parse(claveSeparada[5]);
                nuevaClave.Fraccion = int.Parse(claveSeparada[6]);
                nuevaClave.Toma = int.Parse(claveSeparada[7]);
            }
            else if (claveSeparada.Length == 7)
            {
                nuevaClave.Nivel1 = 0;
                nuevaClave.Nivel2 = int.Parse(claveSeparada[4]);
                nuevaClave.Fraccion = int.Parse(claveSeparada[5]);
                nuevaClave.Toma = int.Parse(claveSeparada[6]);

            }
            else
            {
                Console.WriteLine("loquesea");
            }
            Console.WriteLine($"Clave Original: {nuevaClave.Original}");
            Console.WriteLine($"Subsistema: {nuevaClave.Subsistema}");
            Console.WriteLine($"Sector: {nuevaClave.Sector}");
            Console.WriteLine($"Manzana: {nuevaClave.Manzana}");
            Console.WriteLine($"Lote: {nuevaClave.Lote}");
            Console.WriteLine($"Nivel: {nuevaClave.Nivel1}-{nuevaClave.Nivel2}");
            Console.WriteLine($"Fraccion: {nuevaClave.Fraccion}");
            Console.WriteLine($"Toma: {nuevaClave.Toma}");
            Console.WriteLine($"Clave Correcta:{ nuevaClave.Correcta}");

        }

        private static void Archivos()
        {
            string ruta = @"D:\Windows\Escritorio\Computacion IX\PrimerParcial\Datos";
            var archivos = Directory.GetFiles(ruta);
            var claves = new List<Clavedelocalizacion>();
            foreach (var a in archivos)
               
            {
                Console.WriteLine($"Archivo: {Path.GetFileName(a)} ");
                var contenido = File.ReadAllLines(a);
                var contador = 1;
                foreach (var linea in contenido)

                {
                    if (contador>1)
                    {
                        claves.Add(Clavedelocalizacion.Inicializar(linea.Replace("\"","")));
                    }
                    contador++;

                }
            }

            using (var db = new SqliteContext())
            {
                db.Database.EnsureCreated();
                db.AddRange(claves);
                db.SaveChanges();
                Console.WriteLine("Claves ");
            }
        }
        

        static void Directorios()
        {
            string rutaActual = Directory.GetCurrentDirectory();
            Console.WriteLine($"Directorio Actual:{ rutaActual}");

            string[] directorios = Directory.GetDirectories(rutaActual,"*",
            SearchOption.AllDirectories);
            foreach (var d in directorios)
            {
                Console.WriteLine($"Directorio: {d}");
            }


            Console.ReadLine();

   
        }
        private static void GuardarCSV()
        {
  
            //definie el buffer donde se guardan las lienas procesadas
            var buffer = new StringBuilder();

            var primeralinea= ObtenerPrimeraLinea();

            //agregar la primera linea al buffer
            buffer.AppendLine(primeralinea);


            //conectar a la db
            using (var db = new SqliteContext())
            {
                //obtener todos los registros de la tabla claves_de_localizacion
                var registro = db.Clavedelocalizacions.ToList();

                //obtener los nmbres de columna de tipo caracter
                var colsCadena = (from p in typeof(Clavedelocalizacion).GetProperties()
                                  where p.PropertyType == typeof(string)
                                  select p.Name).ToList();

                foreach (var r in registro)
                {
                    //procesar las columnas de cada registroo 
                    //agregar al bufer los registros procesados
                    var linea = new List<string>();
                    foreach (var p in r.GetType().GetProperties())
                    {
                        if (p.PropertyType == typeof(string))
                        {
                            linea.Add(ponerComillas(p.GetValue(r).ToString()));
                        }
                        else
                        {
                            linea.Add(p.GetValue(r).ToString());
                        }
                    }
                    buffer.AppendLine(string.Join(",", linea));
                }
                //Console.WriteLine(buffer);
                //Escribir Archivo CSV
                var ruta = @"C:\Users\INSPIRON 14-5448\Desktop\Primer Parcial\Datos\" + Nombre + ".csv";
                
                File.WriteAllText(ruta, buffer.ToString());
                Console.WriteLine("Archivo Escrito Correctamente");
            }
        }
        private static void GuardarCSVSinComillas()
        {

            //definie el buffer donde se guardan las lienas procesadas
            var buffer = new StringBuilder();

            var primeralinea = ObtenerPrimeraLinea();

            //agregar la primera linea al buffer
            buffer.AppendLine(primeralinea);


            //conectar a la db
            using (var db = new SqliteContext())
            {
                //obtener todos los registros de la tabla claves_de_localizacion
                var registro = db.Clavedelocalizacions.ToList();

                //obtener los nmbres de columna de tipo caracter
                var colsCadena = (from p in typeof(Clavedelocalizacion).GetProperties()
                                  where p.PropertyType == typeof(string)
                                  select p.Name).ToList();
                
                //Console.WriteLine(buffer);
                //Escribir Archivo CSV
                var ruta = @"C:\Users\INSPIRON 14-5448\Desktop\Primer Parcial\Datos\" + Nombre + ".csv";

                File.WriteAllText(ruta, buffer.ToString());
                Console.WriteLine("Archivo Escrito Correctamente");
            }
        }
        private static void GuardarTAB()
        {

            //definie el buffer donde se guardan las lienas procesadas
            var buffer = new StringBuilder();

            var primeralinea = ObtenerPrimeraLinea();

            //agregar la primera linea al buffer
            buffer.AppendLine(primeralinea);


            //conectar a la db
            using (var db = new SqliteContext())
            {
                //obtener todos los registros de la tabla claves_de_localizacion
                var registro = db.Clavedelocalizacions.ToList();

                //obtener los nmbres de columna de tipo caracter
                var colsCadena = (from p in typeof(Clavedelocalizacion).GetProperties()
                                  where p.PropertyType == typeof(string)
                                  select p.Name).ToList();

                foreach (var r in registro)
                {
                    //procesar las columnas de cada registroo 
                    //agregar al bufer los registros procesados
                    var linea = new List<string>();
                    foreach (var p in r.GetType().GetProperties())
                    {
                        if (p.PropertyType == typeof(string))
                        {
                            linea.Add(ponerComillas(p.GetValue(r).ToString()));
                        }
                        else
                        {
                            linea.Add(p.GetValue(r).ToString());
                        }
                    }
                    buffer.AppendLine(string.Join(",", linea));
                }
                //Console.WriteLine(buffer);
                //Escribir Archivo CSV
                var ruta = @"C:\Users\INSPIRON 14-5448\Desktop\Primer Parcial\Datos\" + Nombre + ".tab";

                File.WriteAllText(ruta, buffer.ToString());
                Console.WriteLine("Archivo Escrito Correctamente");
            }
        }
        private static void GuardarTABsincomillas()
        {

            //definie el buffer donde se guardan las lienas procesadas
            var buffer = new StringBuilder();

            var primeralinea = ObtenerPrimeraLinea();

            //agregar la primera linea al buffer
            buffer.AppendLine(primeralinea);


            //conectar a la db
            using (var db = new SqliteContext())
            {
                //obtener todos los registros de la tabla claves_de_localizacion
                var registro = db.Clavedelocalizacions.ToList();

                //obtener los nmbres de columna de tipo caracter
                var colsCadena = (from p in typeof(Clavedelocalizacion).GetProperties()
                                  where p.PropertyType == typeof(string)
                                  select p.Name).ToList();

               
                //Console.WriteLine(buffer);
                //Escribir Archivo CSV
                var ruta = @"C:\Users\INSPIRON 14-5448\Desktop\Primer Parcial\Datos\" + Nombre + ".tsv";

                File.WriteAllText(ruta, buffer.ToString());
                Console.WriteLine("Archivo Escrito Correctamente");
            }
        }
        private static  string ObtenerPrimeraLinea()
        {
            //obtener los nombres de las propiedades publicas de la clase a guardar en csv
            // var propiedades = typeof(Clavedelocalizacion).GetProperties().ToList();
            //var propiedades = (from p in typeof(Clavedelocalizacion).GetProperties() select p.Name).ToList();
            var propiedades = typeof(Clavedelocalizacion).GetProperties().Select(p => p.Name).ToList();
            var propsRevisadas = new List<string>();
            foreach (var p in propiedades)
            {

                //Console.Write(p + ",");
                //revisar si tienen espacios la propiedad}
                //"texto con espacio".indexOf(" ") ---5
                if (p.IndexOf(" ") >= 0)
                {
                    propsRevisadas.Add(ponerComillas(p));


                }
                else
                {
                    propsRevisadas.Add(p);
                }
            }
            //Console.WriteLine(string.Join(",", propiedades));
            // Console.WriteLine(propiedades.Join(","));
            return string.Join(",", propsRevisadas);
        }

        private static string ponerComillas(string p)
        {
            return "\"" + p + "\"";
        }
    }
}
