using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vigenere
{
    class Program
    {

         public class Vingenere{
 
            string mensaje, clave,cifrado,descifrado;
            public  Vingenere(string a,string b) {

                mensaje = a;
                clave = b;

            }
            public void show_me_da_cyf() {

           
                Console.WriteLine("El mensaje cifrado es : "+cifrado);
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
            }
            public void cyf() {

                int a = 0;
                int b = 0;
                int result = 0;
                int index = 0;
                for(int i = 0; i < mensaje.Length; i++)
                {
                    a = (int)mensaje[i];
                    b = (int)clave[index];

                    Console.Write(mensaje[i] + ":" + a + ", ");
                    Console.Write(clave[index] + ":" + b+" -> ");

                    a -= 65;
                    b -= 65;
                    
                    result = (a + b) % 25;
                    result += 65;
                    
                    Console.WriteLine((char)result + ":"+result);
                    cifrado = cifrado + (char)result;


                    index++;
                    if(index>= clave.Length)
                    {
                        index = 0;
                    }

                }

            }

            public void show_init_decript() {

                Console.WriteLine("Descifrado:");
                Console.WriteLine("mensaje cifrado: " + cifrado);
                Console.WriteLine("");
                Console.WriteLine("clave: "+clave);
            }

            public int get_div() {
                int result = 0;

                return result;
            }

            public void descyf() {

                int a = 0;
                int b = 0;
                int result = 0;
                int index = 0;
                for(int i=0; i < cifrado.Length; i++)
                {
                    a = (int)cifrado[i];
                    b = (int)clave[index];

                    a -= 65;
                    b -= 65;

                    result = a - b;
                    if (result < 0)
                    {
                        result = 90 + result;

                    }
                    else {

                        result += 65;
                    }
                    descifrado += (char)result;
                    Console.WriteLine(result);
                    index++;
                    if (index >= clave.Length)
                    {
                        index = 0;
                    }
                }
                
                Console.WriteLine(descifrado);
            }
        }

        static void Main(string[] args)
        {


            string mensaje,clave;


            Console.WriteLine("introduzca el mensaje a encriptar: ");           // pedimos el mensaje a encriptar y lo guardamos
            mensaje=Console.ReadLine();
            mensaje=mensaje.ToUpper();
            while (mensaje.Contains(" "))
            {
                mensaje = mensaje.Replace(" ", "");
            }

            Console.WriteLine("Introduzca la clave con la que encriptar: ");        // pedimos la clave con la que encriptar y la guardamos
            clave = Console.ReadLine();
            clave=clave.ToUpper();
            Vingenere cifrar = new Vingenere(mensaje, clave);                           // creamos el objeto que nos permite cifrar y descifrar
            cifrar.cyf();
            cifrar.show_me_da_cyf();


            cifrar.show_init_decript();
            cifrar.descyf();
            Console.ReadLine();
            

        }
    }
}
