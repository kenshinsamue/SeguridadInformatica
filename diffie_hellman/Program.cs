using System;

namespace gg
{
    class Program
    {

        class exponente{

            int x,y,b,p;
            public exponente(int Y,int B,int P){
                x=1;
                y=Y;
                b=B;
                p=P;
                recursivo();
                
            }
            public int get_x(){

                return x;
            }
             void recursivo(){
                
                if(b!=0){

                    if(b%2==0){
                        y= (Convert.ToInt32(Math.Pow(y,2)))%p;
                        b/=2;
                        recursivo();
                    }
                    if(b%2==1){
                        x=(x*y)%p;
                        b=b-1;
                        recursivo();

                    }
                }
            }
        }

        class diffie{
            
            int alpha,p;
            int[] X,Y,K,K2;
            exponente mi_exp;

            public diffie(int Alpha, int P,int usuarios){

                alpha=Alpha;
                p=P;
                X= new int [usuarios];
                Y= new int [usuarios];
                K= new int [usuarios];
                K2 = new int [usuarios];

                for(int i=0;i<usuarios;i++){
                    Console.WriteLine("Introduzca el valor del usuario: "+i);
                    X[i]=Convert.ToInt32(Console.ReadLine());
                }

                Console.Clear();
                Console.WriteLine("Primo: "+p);
                Console.WriteLine("Alpha: "+alpha);
                Console.WriteLine("\n-----------------------------------------");
                for(int i=0;i<usuarios;i++){
                    Console.WriteLine("X"+i+" = "+X[i]);
                }

                Console.WriteLine("-----------------------------------------");
                for(int i=0;i<usuarios;i++){

                    mi_exp = new exponente(alpha,X[i],p);
                    Y[i]=mi_exp.get_x();
                    Console.WriteLine("Y"+i+" = "+Y[i]);
                }
                Console.WriteLine("-----------------------------------------");
                for(int i=0;i<usuarios;i++){

                    
                    mi_exp = new exponente(Y[((usuarios+i)+1)%usuarios],X[i],p);
                    K[i]=mi_exp.get_x();
                    mi_exp = new exponente(Y[((usuarios+i)-1)%usuarios],X[i],p);
                    K2[i]=mi_exp.get_x();
                    Console.WriteLine("K"+i+" = "+K[i]+" | K2"+i+" = "+K2[i]);
                }
                
            }
           public  diffie(int Alpha, int P,int Xa,int Xb){
                alpha=Alpha;
                p=P;
                X=new int [2];
                Y=new int [2];
                K=new int [2];
                X[0]=Xa;
                X[1]=Xb;
                

                mi_exp= new exponente(alpha,X[0],p);
                Y[0]=mi_exp.get_x();
                Console.WriteLine("Ya = "+Y[0]);

                
                mi_exp= new exponente(alpha,X[1],p);
                Y[1]=mi_exp.get_x();
                Console.WriteLine("Yb = "+Y[1]);
                
                mi_exp=new exponente(Y[1],X[0],p);
                K[0]=mi_exp.get_x();
                Console.WriteLine("Ka = "+K[0]);
                
                mi_exp = new exponente(Y[0],X[1],p);
                K[1]=mi_exp.get_x();
                Console.WriteLine("Kb = "+K[1]);
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
            int p=0,a=0,Xa=0,Xb=0;
            int opt=0,n_user=0;
            do{
                Console.WriteLine("Introduzca un numero primo: ");
                p= Convert.ToInt32( Console.ReadLine());
            }while(is_prime(p)==false);
           
            do{
                Console.WriteLine("introduzca le valor alpha :");
                a=Convert.ToInt32(Console.ReadLine());
            }while(a>=p || a<=0);

            do{
                Console.WriteLine("Introduzca una opcion: \n1.Dos usuarios\n2.Varios usuarios");    
                opt=Convert.ToInt32(Console.ReadLine());
            }while(opt!=1 && opt !=2 );
           
            if(opt==1){
                Console.WriteLine("introduzca el valor Xa: ");
                Xa=Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("introduzca el valor Xb: ");
                Xb=Convert.ToInt32(Console.ReadLine());         
            
                Console.Clear();

                Console.WriteLine("Xa = "+Xa);
                
                Console.WriteLine("Xb = "+Xb);
                diffie hellman = new diffie(a,p,Xa,Xb);
            }
            if(opt==2){
                do{
                    Console.WriteLine("Cuantos usuarios va a introducir(numero par): ");
                    n_user=Convert.ToInt32(Console.ReadLine());
                    
                }while( n_user<=0 );
                
                diffie hellman= new diffie(a,p,n_user);

            }

        
        }
    }
}
