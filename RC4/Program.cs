using System;
using System.Text;

namespace Seguridad_informatica
{
    class Program{


        public class rc4{

            int[] S=new int [256];
            int[] K=new int [256];      // llave realizada de forma pseudoaleatoria

            string mensaje,clave,cifrado,descifrado;
            public rc4(string a, string b){
                 mensaje=a;clave=b;             // guardamos tanto la clave como el mensaje
                 for(int i=0;i<256;i++){        // guardamos el valor hex dentro del array S (de 0 a 255)
                     S[i]=i;
                 }
            }


            public void mostrar(){

                Console.WriteLine(cifrado);

            }
            public int find_val(int val){           // recibimos un valor a buscar dentro del array
                int pos=0;
                for(int i=0;i<S.Length;i++){        // cuando encontremos el valor dentro del array, guardamos su posicion
                    if( S[i]== val)
                        pos=i;
                }
                return pos;                         // retornamos la posicion
            }

            public void ksa(){

                int val1=0,val2=0;                  // inicializamos variables para guardar los valores de la clave y del array
                int i=0,j=0;                        // inicializamos una variable para indicarnos la posicion del array
                int total=0;                        // inicializamos una variable para guardar el valor total
                
                
                for(i=0;i<256;i++){

                    val1=(int)clave[i%clave.Length]; // guardamos el valor de la clave en i
                    val2=S[i];                       // guardamos el hexadecimal dentro de S en la posicion i
                    total=(val1+val2+j)%256;         // Sumamos todos y corregimos el rango, nos dara una posicion
                    j=find_val(total);               // llamamos a buscar la posicion del valor y la guardamos en j

                    S[i]=total;                      // intercambiamos los valores dentro del array en i y en j 
                    S[j]=val2;

                }
            }
            public void show_K(){
                Console.WriteLine("La secuencia cifrante es : ");
                for(int i=0; i< K.Length;i++){

                    Console.Write(" " +K[i]+" ");

                }
                Console.WriteLine("");
            }


            public void spritz(){

                int i=0;
                int k=0;
                int j=0;
                int w =1;                                                   // numero impar
                int aux=0;
                int plen= mensaje.Length;
                K = new int [plen];
                K[0]=0;
                int index=0;
                while(plen>0){
                    i=(i+w)%256;
                    j= (k + S[(j+S[i])%256])%256;
                    k=(i+k+S[j])%256;
                    aux=S[i];
                    S[i]=S[j];
                    S[j]=aux;
                    if(index>0){
                         K[index]=S[(j + S[(i + S[(index-1+ k)%256])%256])%256];    //  K = 2465246
                    }
                    else{
                        K[index]=S[(j + S[(i + S[(K[0]+ k)%256])%256])%256];
                    }


                    //Z=S[(j + S[(i + S[(Z+ k)%256])%256])%256];
                    // return Z 
                    index++;
                    plen--;
                }
            }



            public void prga(){
                
                int i=0,j=0;                            // inicializamos una variable para indicarnos la posicion del array        
                int index=0;
                int plen=mensaje.Length;                // obtenemos la longitud del mensaje, y la guardamos en plen
                K=new int [plen]; 
                int aux=0;
                while(plen>0){                          // mientras que plen sea mayor que 0

                    i=(i+1)%256;
                    j=(j+S[i])%256;
                    aux=S[i];
                    S[i]=S[j];
                    S[j]=aux; 
                    
                    int test = (S[i]+S[j])%256;
                    
                    K[index]=S[test];
                    plen--;
                    index++;  

                }

            }
            public void encript(){
                
                ksa();                              // llamamos al ksa para desordenar el vector de hex segun la clave
                prga();
                Console.WriteLine("PRGA: ");
                show_K();
                spritz();
                Console.WriteLine("SPRITZ: ");
                
                show_K();
                byte[] cs = new byte[256];

                int val1 =0;
                
                for(int a=0; a<mensaje.Length;a++){

                    val1=(int)mensaje[a];
                    
                    cifrado=cifrado+(char)(val1^K[a%256]);

                }
            }

            public void decrypt(){

                int val1=0;

                 for(int a=0; a<cifrado.Length;a++){

                    val1=(int)cifrado[a];
                    
                    descifrado=descifrado+(char)(val1^K[a%256]);

                }


                Console.WriteLine("Descifrado:" +descifrado);

            }



        };
        static void Main(string[] args)
        {
            string mensaje, clave;          // declaramos los strings donde se guardaran mensaje y clave

            Console.WriteLine("Introduzca el mensaje a encriptar: ");
            mensaje = Console.ReadLine();
            
            Console.WriteLine("Introduzca la clave: ");
            clave = Console.ReadLine();

            rc4 cripto= new rc4(mensaje,clave);     // inicializamos el objeto pasandole la clave y el mensaje
            cripto.encript();                       // llamamos a encriptar

            Console.WriteLine("El mensaje cifrado es : ");
            cripto.mostrar();
            cripto.decrypt();
        }
    }
}
