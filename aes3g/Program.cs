using System;
using System.Collections;

namespace aes3g
{
    class Program
    {

        public class multiplicacion{
            public BitArray A9= new BitArray(8);
            public BitArray[] tabla=new BitArray[8];
            int [] indice;
            public BitArray resultado= new BitArray(8);
            BitArray A,B;

            public multiplicacion(BitArray a, BitArray b){
                A=a;
                B=b;
                int cont=0;
                for(int i=0;i<8;i++){
                    if(B[i]==true)
                        cont++;
                }
                
                indice= new int[cont];
                cont--;
          
                for(int i=0;i<8;i++){
                    if(B[i]==true){
                        
                        indice[cont]=7-i;
                        cont--;
                    }
                }

                for(int i=0;i<8;i++){
                    tabla[i]= new BitArray(8);
                }

                tabla[0]=A;
                
            }
            public void make_lista (){
               
                for(int i=1;i<8;i++){
                    if(tabla[i-1][0]==false)
                        tabla[i]=desplazar(tabla[i-1]);
                    if(tabla[i-1][0]==true){
                        tabla[i]=desplazar(tabla[i-1]);
                        tabla[i]=sumita_no_mas(tabla[i],A9);
                        
                    }
                    
                }
            }

             public BitArray desplazar(BitArray boii){

                BitArray aux= new BitArray(8);
                aux[7]=false;
                for(int i=7;i>0;i--){
                    aux[(i-1)]=boii[i];
                }
                return aux;
            }
            

            public static bool make_xor(bool a, bool b){        // basicamente hace una xor, con bools
                if(a==b){
                    return false;

                }
                else{
                    return true;
                }

            }
            static public BitArray sumita_no_mas(BitArray A, BitArray B){
                BitArray aux= new BitArray(A.Length);
                
                for(int i=0; i<A.Length;i++){

                        aux[i]=make_xor(A[i],B[i]);
                }

                return aux;
            }

           
            public  BitArray make_multiple(){
                BitArray result=tabla[indice[0]];
                show_bool(result);
                for(int i=1;i<indice.Length;i++){
                    show_bool(tabla[indice[i]]);
                    result=sumita_no_mas(result,tabla[indice[i]]);

                }
                Console.WriteLine("----------------------------");
                show_bool(result);
                return result;
                
            }


        }

        public class snow:multiplicacion{
            bool[] binarios=new bool [8]{true,false,true,false,true,false,false,true};
            BitArray a9;
            public snow(BitArray A, BitArray B):base(A,B){

                a9= new BitArray(binarios);
                A9=a9;
                make_lista();
                make_multiple();

            }

            
        }

        public class aes:multiplicacion{

            bool[] binarios=new bool [8]{false,false,false,true,true,false,true,true};
            BitArray a9;
            public aes(BitArray A, BitArray B):base(A,B){

                a9= new BitArray(binarios);

                A9=a9;
                make_lista();
                make_multiple();

            }
        }


        static bool operando (string A){

            switch(A){
                case "0":return true;
                case "1": return true;
                case "2": return true;
                case "3": return true;
                case "4": return true;
                case "5": return true;
                case "6": return true;
                case "7": return true;
                case "8": return true;
                case "9": return true;
                case "A": return true;
                case "B": return true;
                case "C": return true;
                case "D": return true;
                case "E": return true;
                case "F": return true;
                default:return false;
            }
        }
        static bool check_byte(string B){

            if(B.Length!=2)
                return false;

            else{
                bool respuesta =true;

                respuesta=(operando(B[0].ToString()) && operando(B[1].ToString()));
               
                return respuesta;
            }   
        }
        static bool[] get_val(string a){
            bool[] valor=new bool [4]{false,false,false,false};

            switch(a){

                case "0": valor= new bool[4]{false,false,false,false};break;
                case "1": valor= new bool[4]{false,false,false,true};break;
                case "2": valor= new bool[4]{false,false,true,false};break;
                case "3": valor= new bool[4]{false,false,true,true};break;
                case "4": valor= new bool[4]{false,true,false,false};break;
                case "5": valor= new bool[4]{false,true,false,true};break;
                case "6": valor= new bool[4]{false,true,true,false};break;
                case "7": valor= new bool[4]{false,true,true,true};break;
                case "8": valor= new bool[4]{true,false,false,false};break;
                case "9": valor= new bool[4]{true,false,false,true};break;
                case "A": valor= new bool[4]{true,false,true,false};break;
                case "B": valor= new bool[4]{true,false,true,true};break;
                case "C": valor= new bool[4]{true,true,false,false};break;
                case "D": valor= new bool[4]{true,true,false,true};break;
                case "E": valor= new bool[4]{true,true,true,false};break;
                case "F": valor= new bool[4]{true,true,true,true};break;


            }
            return valor;
        }
        public static BitArray hex_to_bool(string a){
            BitArray aux= new BitArray(8);
            bool[] gg=get_val(a[0].ToString());
            for(int i=0;i<4;i++){

                aux[i]=gg[i];
            }
            gg=get_val(a[1].ToString());

            for(int i=4;i<8;i++){

                aux[i]=gg[i-4];
            }

            return aux;
        }
         static public void show_bool(BitArray a){        // nos deja mostrar el bit array pero en formato binario

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
            
            string byte1,byte2;
            do{

                Console.WriteLine("Introduzca el primer byte: ");
                byte1=Console.ReadLine();
                

            }while(check_byte(byte1)==false);
            

            do{
                Console.WriteLine("Introduzca el segundo byte: ");
                byte2=Console.ReadLine();

            }while(check_byte(byte2)==false);
            

            BitArray A= hex_to_bool(byte1);
            BitArray B= hex_to_bool(byte2);

            Console.WriteLine("Snow 3G:");
            show_bool(A);
            show_bool(B);
            Console.WriteLine("----------------------------");
            
            snow algoritmo= new snow(A,B);
            Console.WriteLine("\n");

            Console.WriteLine("AES");
            show_bool(A);
            show_bool(B);
            Console.WriteLine("----------------------------");
            aes Algoritmo= new aes(A,B);
        }
    }
}
