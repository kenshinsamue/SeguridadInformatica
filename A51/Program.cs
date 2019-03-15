using System;
using System.Collections;

namespace A51
{
    class Program
    {

/******************** LFSR *********************************/
        public class LFSR{              // este objeto se encarga de representar los LFSR que usa el A5/1
            BitArray lfsr;              // El BitArray en que se van haciendo las operaciones de xor, push, etc
            int[] polinomios;           // array de ints, que representan al polinomio de cada LFSR ( basicamente las posiciones dentro del Array)
            int entrada_;               // int que representa la posicion que vamos a vigilar para ver si vamos a tener en cuenta para realizar la operacion xor
            public LFSR(int a){         // constructor que se le indica su size
                lfsr= new BitArray(a);  // lo creamos, e inicializamos toda la estructura segun su size
                definir();              

            }

            public void definir(){

                int sz=lfsr.Length;                     // obtenemos su size
                if(sz==19){             // en  caso de que sea el LFSR con 19 bits
                   
                    polinomios= new int [] {18,17,16,13};       // representa el polinomio del lfsr
                    entrada_=8;                                 // indica el indice de la posicion del array de bits que determina la entrada
                }
                if(sz==22){                // en caso de que sea el LFSR con 22 bits
                    
                    polinomios= new int [] {21,20};
                    entrada_=10;
                }
                if(sz==23){                 // en caso de que sea el LFSR con 23 bits
                    
                    polinomios= new int [] {22,21,20,7};
                    entrada_=10;
                }
            }  

            public bool get_bit_entrada(){          // retrornamos el valor del bit en las posiciones {9,10,10}, segun el LFSR

                return lfsr[entrada_];
            }

            public bool get_valor(){                // metodo que retorna el valor resultante de la operacion del polinomio

                bool valor= false;
                valor=lfsr[polinomios[0]];
                for(int i =1; i< polinomios.Length;i++){

                    valor=make_xor(valor,lfsr[polinomios[i]]);

                }

                return valor;
            }

            public bool push_valor(bool v){         // obtenemos un bool, que representa a un bit
                BitArray aux=lfsr;                  // creamos un Bitarray que sera igual al lfsr actual
                lfsr = new BitArray (aux.Length);
                lfsr[0]=v;                          // pondremos en la primera posicion del lfsr el bit nuevo
                for(int i = 0; i<aux.Length-1;i++){ // vamos a insertar el resto de valores de aux, hasta que se llegue al ultimo valor

                    lfsr[i+1]=aux[i];


                }
                return aux[aux.Length-1];           // retornamos el ultimo valor( el sobrante) 
            }
            public void set_bits(BitArray a){           // obtenemos un bitarray y lo metemos dentro de lfsr
                lfsr=a;

            }
            
            public static bool make_xor(bool a, bool b){        // basicamente hace una xor, con bools
                if(a==b){
                    return false;

                }
                else{
                    return true;
                }

            }
            
               
        };
/******************** /LFSR *********************************/

/********************  A5 *********************************/
        public class A5{    
            BitArray S;                             // Bitarray que contiene la cadena cifrante 
            BitArray mensaje;                       // Bitarray que contiene el mensaje
            BitArray cifrado;                       // Bitarray que contendra el mensaje cifrado
            LFSR[] mi_lfsr;                         // creamos un array de LFSR's 
            BitArray semilla;                       // Bitarray con la semilla



            public A5(BitArray sem,BitArray mens){          // constructor
               
                mensaje=mens;                               // guardamos el mensaje
                S = new BitArray (mensaje.Length);          // definimos el vector cifrante , segun la longitud del mensaje
                cifrado = new BitArray(mensaje.Length);     // lo mismo hacemos con el cifrado



               // S = new BitArray ();
                mi_lfsr= new LFSR [3];                      // convertimos el array de LFSR de 3, segun A5/1
                mi_lfsr[0]= new LFSR (19);                  // creamos cada LFSR segun el size que le corresponde
                mi_lfsr[1]= new LFSR (22);
                mi_lfsr[2]= new LFSR (23);

                semilla = sem;                              // guardamos la semilla                  
                
                set_lfsr();                                 // llamamos a llenar los LFSR con la semilla dada                  
            }

            public void set_lfsr(){ 
                BitArray aux = new BitArray (19);               // creamos un bitarray auxiliar

                for(int i=0;i<19;i++){                          // bucle para el primer LFSR

                    aux[18-i]=semilla[i];                       // obtenemos los primeros 19 bits y los metemos dentro de aux

                }

                    mi_lfsr[0].set_bits(aux);               // trasformamos el LFSR en aux
                    aux = new BitArray(22);                 // redeclaramos aux, para que se pueda usar con el LFSR de 22 bits


                for(int i=19; i<41;i++){                    // bucle para obtener los siguientes 22 bits de la semilla
                    
                    aux[21-(i-19)]=semilla[i];
                }


                    mi_lfsr[1].set_bits(aux);               // definimos el LFSR con aux, y redeclaramos aux
                    aux= new BitArray(23);


                for(int i=41;i<64;i++){                     // .....

                    aux[22-(i-41)]=semilla[i];

                }
                mi_lfsr[2].set_bits(aux);                   //.....
            }

            public void crear_cadena_cifrante(){        // realizamos todas las operaciones xor y push'es
                bool a=false,b=false,c=false;      // variables donde guardaremos el valor del bit de entrada de cada vector        
                bool result_a=false,result_b=false,result_c=false;      // variables donde guardamos las operaciones xor
                bool valor=false;                                   // valor que pondremos dentro de S
                int index_S=0;                                      // indice de S
                // poner condicion al bucle
                do{                                                 // bucle

                    a=mi_lfsr[0].get_bit_entrada();          // obtenemos el valor de los bits que definen la entrada{8,10,10}
                    b=mi_lfsr[1].get_bit_entrada();
                    c=mi_lfsr[2].get_bit_entrada();

                    
                    if(a==b && b==c){                       // en caso de que todos coincidadn
                    
                        result_a=mi_lfsr[0].get_valor();        // obtenemos el valor de la operacion xor segun el polinomio
                        result_b=mi_lfsr[1].get_valor();        // de cada LFSR
                        result_c=mi_lfsr[2].get_valor();
                        
                        result_a=mi_lfsr[0].push_valor(result_a);   // el valor resultante lo pusheamos y obtendremos el bit sobrante
                        result_b=mi_lfsr[1].push_valor(result_b);
                        result_c=mi_lfsr[2].push_valor(result_c);

                        valor=LFSR.make_xor(result_a,result_b);     // con el bit sobrante hacemos 2 xor, 
                        valor=LFSR.make_xor(valor,result_c);
                       
                        
                        S[index_S]=valor;               // con el resultado, lo metemos dentro del vector cifrante

                    }
                    // basicamente repetir lo anterior pero con 2 coincidencias en vez de 3
                    else{
                        if(a==b){
                            result_a=mi_lfsr[0].get_valor();
                            result_b=mi_lfsr[1].get_valor();

                            result_a=mi_lfsr[0].push_valor(result_a);
                            result_b=mi_lfsr[1].push_valor(result_b);

                            valor=LFSR.make_xor(result_a,result_b);
                            S[index_S]=valor;
                            
                        }
                        if(a==c){

                            result_a=mi_lfsr[0].get_valor();
                            result_c=mi_lfsr[2].get_valor();

                            result_a=mi_lfsr[0].push_valor(valor);
                            result_c=mi_lfsr[2].push_valor(valor);

                            valor = LFSR.make_xor(result_a,result_c);

                            S[index_S]=valor;
                            

                        }
                        if(b==c){
                            result_c=mi_lfsr[2].get_valor();
                            result_b=mi_lfsr[1].get_valor();

                            result_b=mi_lfsr[1].push_valor(valor);
                            result_c=mi_lfsr[2].push_valor(valor);

                            valor=LFSR.make_xor(result_a,result_b);
                        
                            S[index_S]=valor;
                            
                        }   

                    }
                    index_S++;
                }while(index_S<S.Length);



            // una vez acabado de crear la cadena cifrante, mostramos la misma en bits
            Console.WriteLine("La cadena cifrante generada es : ");
               for(int i=0;i<S.Length;i++){
                    if(S[i]==true)
                        Console.Write(" 1");
                    else{
                        Console.Write(" 0");
                    }
               }

            }



            public BitArray get_cript(){        // retorna la cadena cifrada

                return cifrado;
            }
            
            
            public void cifrar(){               // hace operacion xor con el vector cifrante y el mensaje
                
                for(int i=0;i<S.Length;i++){
                    
                    cifrado[i]=LFSR.make_xor(S[i],mensaje[i]);
                }
            }
        };



/******************** /A5 *********************************/
        static public BitArray convert_to_bool(BitArray a, string b, int max){      // este metodo se encarga de convertir una cadena de string binaria
                                                                                    // a un BitArray(bool)
            a= new BitArray (max);
            for(int i=0;i<a.Length;i++){
                if(b[i]=='1'){
                    a[i]=true;
                }
                if(b[i]=='0'){
                    a[i]=false;
                }

            }

            return a;
        }

        static public bool check_binary(string a){          // metodo que se encarga de decirnos si toda la cadena es binaria, o
                                                            // hay algun inconveniente
            bool result =true;

                for(int i=0;i<a.Length;i++){

                    if(a[i]!='1' && a[i]!='0'){
                        result=false;
                        break;
                    }


                }

            return result;
        }

        static public void show_chipher(BitArray a){        // nos deja mostrar el bit array pero en formato binario

            for(int i=0;i<a.Length;i++){

                if(a[i]==true)
                    Console.Write(" 1");
                else{
                    Console.Write(" 0");
                }                   
            }

        }
        static void Main(string[] args)
        {
            bool bien=false;                // variable para comprobar que la cadena es binaria
            // semilla enuciado : 1001000100011010001010110011110001001101010111100110111100001111
            string mensaje;
            string semilla="1001000100011010001010110011110001001101010111100110111100001111";      // semilla del enunciado
            Console.WriteLine("Se ha establecido la siguiente semilla para crear la secuencia cifrante: "+semilla);     // mostramos la semilla

            BitArray sem_bit= new BitArray (1);                 // creamos un array de bits que representan a la semilla
            sem_bit=convert_to_bool(sem_bit,semilla,64);        // dentro de el guardamos la cadena semilla convertida a Bitarray


            do{                                                      // pedimos el mensaje, y nos aseguramos de que es binario           

                Console.WriteLine("Introduzca el mensaje a cifrar(binario): ");
                mensaje=Console.ReadLine();
                bien=check_binary(mensaje);

            }while(bien==false);

            BitArray message= new BitArray(1);                              // creamos una variable BitArray donde se guardara el mensaje
            message=convert_to_bool(message,mensaje,mensaje.Length);        // lo convertimos en bitarray y lo guardamos

            A5 algoritmo = new A5 (sem_bit,message);                        // creamos el objeto A5 que se encargara de crear todo
            
            Console.WriteLine("La cadena a cifrar es: ");                   // mostramos el mensaje a cifrar
            show_chipher(message);
            Console.WriteLine("");

            algoritmo.crear_cadena_cifrante();                              // llamamos a crear la cadena cifrante en base a la semilla
            algoritmo.cifrar();                                             // una vez creada la cadena cifrante ciframos el mensaje 1
            Console.WriteLine("");          

            BitArray cifrado = algoritmo.get_cript();                       // obtenemos el mensaje cifrado y lo mostramos
            show_chipher(cifrado);

        }
    }
}
