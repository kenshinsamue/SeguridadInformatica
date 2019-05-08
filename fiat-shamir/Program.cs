using System;

namespace fiat_shamir
{
    class Program
    {
       

        public class fiat{  
            int P,Q,S,I,N,V;

            int[] E,X,Y,A;
            

            public fiat(int a, int b, int d){
                P=a;
                Q=b;
                I=d;
                E=new int[I];
                X=new int[I];
                Y=new int[I];
                A=new int[I];
            } 

            public int[] resize (int[] a,int b){

                int sizu=a.Length;
                int[]  aux = new int[sizu+1];
                
                for(int i=0;i<sizu;i++)
                    aux[i]=a[i];

                aux[aux.Length-1]=b;
                return aux;
            }
            public int[] get_divisores(int a){

                int[] div= new int [] {}; 

                for(int i=1;i<=a;i++){
                    if(a%i==0)
                        div=resize(div,i);

                }
                return div;
            }

            public bool is_prime_both(int a, int b){

                int[] div1=get_divisores(a);
                int[] div2=get_divisores(b);

                for(int i=0;i<div1.Length;i++){
                    for(int j=0;j<div2.Length;j++){

                        if(div1[i]==div2[j] && div1[i]!=1)
                            return false;
                    }
                }
                return true;
            }

            public void create(){

                int pos=0;
                N=P*Q;
                Console.WriteLine("Inicializacion: P*Q="+N);

                do{
                    
                    Console.WriteLine("Introduzca un valor S tal que :  0<S<N");
                    S=Convert.ToInt32(Console.ReadLine());

                }while(is_prime_both(S,N)==false);
                
                Console.Clear();
                

                V=(Convert.ToInt32(Math.Pow(S,2)))%N;
                Console.WriteLine("\nIdentificacion Publica de A: V="+V+"\n");
                

                do{
                    do{
                        Console.WriteLine("Introduzca el error "+pos+" :");
                        E[pos]=Convert.ToInt32(Console.ReadLine());

                    }while(E[pos]<0 && E[pos]>1);
                    
                    Console.WriteLine("\nIntroduzca el numero secreto "+pos+", tal que 0<X<N: ");
                    X[pos]=Convert.ToInt32(Console.ReadLine());
                    
                    A[pos]=Convert.ToInt32(Math.Pow(X[pos],2))%N;
                    Console.WriteLine("\ntestigo A envia a B: "+A[pos]+"\n");
                    

                    pos++;
                }while(pos<I);

                Console.Clear();
            }

            public void execute_alg(){

                Console.WriteLine("N:"+N);
                Console.WriteLine("S:"+S);
                Console.WriteLine("V:"+V);
                Console.WriteLine("\n------------------------------------------\n");
                int pos=0;
                double resultado=0;
                Int64 y2=0;
                do{
                    Console.WriteLine("Iteracion "+(pos+1)+" :");

                    if(E[pos]==0){
                        Y[pos]=X[pos]%N;
                        Console.WriteLine("X: "+X[pos]);
                        Console.WriteLine("E: "+E[pos]);
                        Console.WriteLine("Y = X mod(N)");
                        Console.WriteLine("Y:"+Y[pos]+" = "+X[pos]+" mod("+N+")");     // mostramos Y

                        y2=Convert.ToInt32(Math.Pow(Y[pos],2))%N;

                        resultado=(A[pos])%N;
                        Console.WriteLine("A: "+A[pos]);
                        Console.WriteLine("Resultado: (Y^2)mod(N): "+y2);
                        Console.WriteLine("             A mod (N): "+resultado);
                    }
                       
                    if(E[pos]==1){
                        Y[pos]=(X[pos]*S)%N;
                        Console.WriteLine("X: "+X[pos]);
                        Console.WriteLine("E: "+E[pos]);
                        Console.WriteLine("Y = X*S mod(N)");
                        Console.WriteLine("Y:"+Y[pos]+" = "+X[pos]+" * "+S+" mod("+N+")");     // mostramos Y
       
                        y2=Convert.ToInt64(Math.Pow(Y[pos],2))%N;   // obtenemos Y^2

                        resultado=(Convert.ToDouble(A[pos])*Convert.ToDouble(V))%N;       // obtenemos (A*V)%N
                       Console.WriteLine("A: "+A[pos]);
                        Console.WriteLine("Resultado: (Y^2)mod(N): "+y2);
                        Console.WriteLine("           (A*V)mod(N): "+resultado);
                    }
                    Console.WriteLine();
                    pos++;
                }while(pos<I);
            }
        }


          static bool is_prime(int a){
            int divisor=1,divisores=0;

            do{
                if(a%divisor==0){
                    divisores++;
                }
                divisor++;

            }while(divisor<=a);
            if(divisores==2)
                return true;
            else
                return false;
        }

        
        static void Main(string[] args)
        {
            int p=0,q=0,i=0;


            do{

                Console.WriteLine("Introduzca el valor p: ");
                p=Convert.ToInt32(Console.ReadLine());


            }while(!is_prime(p));


            do{


                Console.WriteLine("Introduzca el valor q: ");
                q=Convert.ToInt32(Console.ReadLine());
        


            }while(!is_prime(q));

            Console.WriteLine("Cuantas iteraciones se van a realizar?:  ");
            i=Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            fiat hola = new fiat(p,q,i);
            hola.create();
            hola.execute_alg();
        }
    }
}
