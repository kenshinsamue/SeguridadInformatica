using System;
using System.Collections;

namespace E0
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
                if(sz==25){             
                    polinomios= new int [] {24,19,11,7};
                         
                    entrada_=11;                                 // indica el indice de la posicion del array de bits que determina la entrada
                }
                if(sz==31){                
                    polinomios=new int []{30,23,25,11};
                 
                    entrada_=14;
                }
                if(sz==33){                 
                    
                    polinomios= new int [] {32,27,23,3};
                    entrada_=15;
                }
                if(sz==39){

                    polinomios = new int [] {38,35,27,3};
                    entrada_=18;



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
            public  bool get_last(){

                return lfsr[(lfsr.Length)-1];
            }
            
               
        };
/******************** /LFSR *********************************/

/******************* E0 ************************************/
        public class E0{
        BitArray message;
        BitArray seed;
        BitArray S;
        LFSR[] lfsr;
        public E0(BitArray msg,BitArray sd){
            message=msg;
            seed=sd;
            S=new BitArray (msg.Length);
            lfsr = new LFSR[4];
            lfsr[0]=new LFSR (25);
            lfsr[1]=new LFSR (31);
            lfsr[2]=new LFSR (33);
            lfsr[3]=new LFSR (39);
            set_lfsr();
        }

        public void set_lfsr(){ 
                BitArray aux = new BitArray (25);               // creamos un bitarray auxiliar

                for(int i=0;i<25;i++){                          // bucle para el primer LFSR

                    aux[i]=seed[i];                       // obtenemos los primeros 19 bits y los metemos dentro de aux
                    
                }
                
                    lfsr[0].set_bits(aux);               // trasformamos el LFSR en aux
                    aux = new BitArray(31);                 // redeclaramos aux, para que se pueda usar con el LFSR de 22 bits

                for(int i=25; i<56;i++){                    // bucle para obtener los siguientes 22 bits de la semilla
                    
                    aux[i-25]=seed[i];
                    
                }
                
                    lfsr[1].set_bits(aux);               // definimos el LFSR con aux, y redeclaramos aux
                    aux= new BitArray(33);

                for(int i=56;i<89;i++){                     // .....
                    aux[i-56]=seed[i];
                    
                }
                

                lfsr[2].set_bits(aux);                   //.....
                aux=new BitArray(39);

                for(int i=89;i<127;i++){                     // .....
                    aux[i-89]=seed[i];
                    
                }
                
                lfsr[3].set_bits(aux);                   //.....
                aux= new BitArray(0);
            }

            public int contad(bool a, bool b, bool c, bool d){
                int cont=0;
                if(a==true)
                    cont++;
                if(b==true)
                    cont++;
                if(c==true)
                    cont++;
                if(d==true)
                    cont++;

                return cont;
            }

            public void crear_cadena_cifrante(){        
            
                bool result_a=false,result_b=false,result_c=false,result_d=false;      // bits resultantes de operaciones
                bool[] inic=new bool [2]{true,false};                                   // bits de inicio de R1
                int index_S=0,contador=0,r1,resultado=0;              //indice de la cadena cifrante,resultado de suma(decimal) de LFSR,r1 en decimal,resultado de la suma contador+r1                                     
                int[] sum= new int [1];
                bool end;
                BitArray suma = new BitArray(2,false);
                BitArray R1 = new BitArray(inic);
                BitArray R2 = new BitArray(2);
                BitArray T2 = new BitArray(2);
                BitArray T1 ;                                   

                do{                                                 
                                // R1 inicializado a '01'                                     
                    
                                // obtenemos los bits de los LFSR
                        result_a=lfsr[0].get_valor();        
                        result_b=lfsr[1].get_valor();        
                        result_c=lfsr[2].get_valor();
                        result_d=lfsr[3].get_valor();

                        result_a=lfsr[0].push_valor(result_a);   
                        result_b=lfsr[1].push_valor(result_b);
                        result_c=lfsr[2].push_valor(result_c);
                        result_d=lfsr[3].push_valor(result_d);
                        
                        Console.WriteLine("Bit numero"+(index_S+1));
                        Console.WriteLine("R1:");
                        show_chipher(R1);
                        end=LFSR.make_xor(result_a,result_b);
                        end=LFSR.make_xor(end,result_c);
                        end=LFSR.make_xor(end,result_d);
                        end=LFSR.make_xor(end,R1[1]);
                        
                        
                        //Console.WriteLine(result_a);
                        //Console.WriteLine(result_b);
                        //Console.WriteLine(result_c);
                        //Console.WriteLine(result_d);
                        //Console.WriteLine(R1[1]);
                                // hacemos la suma decimal de los bits
                        contador=contad(result_a,result_b,result_c,result_d);
                  
                                // obtenemos el valor decimal de R1 e inicializamos T1
                        T1=R1;
                        Console.WriteLine("T1:");
                        show_chipher(T1);
                        r1=get_int(R1);
                        S[index_S]=end;
                                // le damos la vuela a R1 y lo guardamos en R2
                        R2=swap_bit(R1);
                        Console.WriteLine("R2:");
                        show_chipher(R2);
                                // inicializamos T en base del valor de R2
                        T2[0]=R2[1];
                        T2[1]=LFSR.make_xor(R2[0],R2[1]);
                        Console.WriteLine("T2:");
                        show_chipher(T2);
                                // inicializado R1 y R2, hacemos la suma con R1 y la  divicion
                        resultado=contador+r1;
                        Console.WriteLine("Suma 1:"+contador);
                        Console.WriteLine("Suma 2: "+resultado);
                        resultado/=2;

                                
                                // convertimos el resultado en bits
                        
                        suma= int_to_bool(resultado);
                        
                        Console.WriteLine("Div:");
                        show_chipher(suma);


                        suma=sumita_no_mas(suma,T2);
                        Console.WriteLine("Suma T2: ");
                        show_chipher(suma);


                        suma=sumita_no_mas(suma,T1);
                        Console.WriteLine("Suma T1: ");
                        show_chipher(suma);
                        R1=swap_bit(suma);



                       Console.WriteLine(""); 
                       Console.WriteLine("");
                    index_S++;
                }while(index_S<S.Length);

            }
            static public BitArray int_to_bool(int a){
                
                string binario="";
                int valor=a;
                
                do{

                    if(valor%2==1)
                        binario="1"+binario;
                    else{
                        binario+="0"+binario;
                    }
                    valor/=2;
                }while(valor>1);
                
                binario=valor+binario;
                BitArray aux= new BitArray(binario.Length);
                aux=convert_to_bool(aux,binario);
                return aux;

            }
            static public BitArray sumita_no_mas(BitArray A, BitArray B){
                BitArray aux= new BitArray(A.Length);
                
                for(int i=0; i<A.Length;i++){

                        aux[i]=LFSR.make_xor(A[i],B[i]);
                }

                return aux;
            }
            static public BitArray swap_bit(BitArray A){

            BitArray aux = new BitArray(A.Length);

            for(int i=0;i<A.Length;i++){

                aux[i]=A[(A.Length-1)-i];

            }

            return aux;

        }
        public BitArray get_S(){
            return S;
        }

        };
/******************* /E0 ************************************/

        static public int get_int(BitArray a){
            int value=0;

            for( int i=0;i<a.Length;i++){

                if(a[i])
                    value+=Convert.ToInt32(Math.Pow(2,(a.Length-1)-i));

            }

            return value;
        }
        static public BitArray convert_to_bool(BitArray a, string b){      // este metodo se encarga de convertir una cadena de string binaria
                                                                                    // a un BitArray(bool)
            
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

        static public BitArray swap_bit(BitArray A){

            BitArray aux = new BitArray(A.Length);

            for(int i=0;i<A.Length;i++){

                aux[i]=A[(A.Length-1)-i];

            }

            return aux;

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
            Console.WriteLine("");
        }
        static void Main(string[] args)
        {
            bool pass=false;
            string mensaje;
            string semilla;
            do{
                Console.WriteLine("Introduzca la semilla: ");
                semilla=Console.ReadLine();
                pass=check_binary(semilla);
            }while(pass==false);

            pass=false;

            do{
                Console.WriteLine("Introduzca el mensaje: ");
                mensaje=Console.ReadLine();
                pass=check_binary(mensaje);
            }while(pass==false);

            semilla ="01101010101010101010111110100110101010101010100101011111010111100010101010101010010001111101011010100101010100010101010101011010";
                



            
            
            BitArray seed = new BitArray(semilla.Length);
            BitArray message = new BitArray(mensaje.Length);
            
            

            seed=convert_to_bool(seed,semilla);
            message= convert_to_bool(message,mensaje);

            E0 algoritmo = new E0(message,seed);
            algoritmo.crear_cadena_cifrante();
            BitArray S = algoritmo.get_S();
            Console.WriteLine("La cadena cifrante es : ");
            show_chipher(S);


        }
    }
}
