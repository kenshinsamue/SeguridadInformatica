using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    public class vernam
    {

        string s, k, enc, desc;
        BitArray b1 = new BitArray(8);
        BitArray clave = new BitArray(0);

        BitArray result = new BitArray(8);
        public vernam(string a, BitArray b)
        {
            s = a;
            clave = b;

        }

        public void encrypt()
        {
            int j = 0;
            int[] valor;
            for (int i = 0; i < s.Length; i++)
            {
                valor = new int[1] { Convert.ToInt32(s[i]) };       // creamos un array que contiene el valor en entero del caracter
                b1 = new BitArray(valor);                           // convertimos ese valor en una cadena de bits


                for (int h = 0; h < 8; h++)
                {                       // bucle para la operacion xor
                                        // b1  clave  |  xor
                    if (b1[h] == true && clave[j] == true)          // 1     1        0
                        result[h] = false;

                    if (b1[h] == true && clave[j] == false)         // 1     0        1
                        result[h] = true;

                    if (b1[h] == false && clave[j] == true)         // 0     1        1 

                        result[h] = true;
                    if (b1[h] == false && clave[j] == false)        // 0     0        0
                        result[h] = false;

                    j++;
                    if (j >= clave.Length)
                    {
                        j = 0;
                    }
                }

                result.CopyTo(valor, 0);                    // convertimos la cadena de bits a un numero 
                enc = enc + (char)valor[0];                 // traducimos el numero a su caracter equivalente y se inserta en la cadena enc
            }
        }

        public void print_encr()
        {

            Console.WriteLine(enc);

        }

        public void decrypt()
        {


            int j = 0;
            int[] valor;
            for (int i = 0; i < enc.Length; i++)
            {
                valor = new int[1] { Convert.ToInt32(enc[i]) };       // creamos un array que contiene el valor en entero del caracter
                b1 = new BitArray(valor);                           // convertimos ese valor en una cadena de bits


                for (int h = 0; h < 8; h++)
                {                       // bucle para la operacion xor
                                        // b1  clave  |  xor
                    if (b1[h] == true && clave[j] == true)          // 1     1        0
                        result[h] = false;

                    if (b1[h] == true && clave[j] == false)         // 1     0        1
                        result[h] = true;

                    if (b1[h] == false && clave[j] == true)         // 0     1        1 

                        result[h] = true;
                    if (b1[h] == false && clave[j] == false)        // 0     0        0
                        result[h] = false;

                    j++;
                    if (j >= clave.Length)
                    {
                        j = 0;
                    }
                }

                result.CopyTo(valor, 0);                    // convertimos la cadena de bits a un numero 
                desc = desc + (char)valor[0];                 // traducimos el numero a su caracter equivalente y se inserta en la cadena enc
            }


        }


        public void print_desc()
        {

            Console.WriteLine(desc);

        }


    }

    class Program
    {

        static BitArray Convert_my_string(string cadena, BitArray b)
        {

            b = new BitArray(cadena.Length);

            for (int i = 0; i < cadena.Length; i++)
            {
                if (cadena[i] == '1')
                {

                    b[i] = true;
                }
                else
                {

                    b[i] = false;
                }

            }

            return b;
        }
        static void Main(string[] args)
        {
            string s, k;
            Console.WriteLine("Introduzca el mensaje en texto plano: ");
            s = Console.ReadLine();

            Console.WriteLine("Introduzca la llave para encriptar: ");
            k = Console.ReadLine();

            BitArray b = new BitArray(0);
            b = Convert_my_string(k, b);


            vernam v = new vernam(s, b);
            v.encrypt();
            Console.WriteLine("La cadena codificada es: ");
            v.print_encr();

            v.decrypt();
            Console.WriteLine("La cadena decodificada es : ");
            v.print_desc();

            k = Console.ReadLine();


        }
    }
}
