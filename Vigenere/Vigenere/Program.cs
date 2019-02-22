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

           
                Console.WriteLine(cifrado);
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
                    Console.WriteLine(mensaje[i] + ":" + a);
                    Console.WriteLine(clave[index] + ":" + b);
                    result = (a + b)%25+65;
                    Console.WriteLine((char)result + ":"+result+", resultado divicion:"+);
                    cifrado = cifrado + (char)result;


                    index++;
                    if(index>= clave.Length)
                    {
                        index = 0;
                    }

                }

            }

            public int get_div() {



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




                }


            }
        }

        static void Main(string[] args)
        {


            string mensaje,clave;


            Console.WriteLine("introduzca el mensaje a encriptar: ");           // pedimos el mensaje a encriptar y lo guardamos
            mensaje=Console.ReadLine();
            mensaje=mensaje.ToUpper();
            Console.WriteLine("Introduzca la clave con la que encriptar: ");        // pedimos la clave con la que encriptar y la guardamos
            clave = Console.ReadLine();
            clave=clave.ToUpper();
            Vingenere cifrar = new Vingenere(mensaje, clave);                           // creamos el objeto que nos permite cifrar y descifrar
            cifrar.cyf();
            cifrar.show_me_da_cyf();
            Console.ReadLine();    


        }
    }
}
