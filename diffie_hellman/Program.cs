﻿using System;

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
            
            int alpha,p,xA,xB,yA,yB,kA,kB;
            exponente mi_exp;
           public  diffie(int Alpha, int P,int Xa,int Xb){
                alpha=Alpha;
                p=P;
                xA=Xa;
                xB=Xb;

                mi_exp= new exponente(alpha,xA,p);
                yA=mi_exp.get_x();
                Console.WriteLine("Ya = "+yA);

                mi_exp=new exponente(alpha,xB,p);
                yB=mi_exp.get_x();
                Console.WriteLine("Yb = "+yB);

                mi_exp=new exponente(yB,xA,p);
                kA=mi_exp.get_x();
                Console.WriteLine("Ka = "+kA);

                mi_exp =new exponente(yA,xB,p);
                kB=mi_exp.get_x();
                Console.WriteLine("Kb = "+kB);

            }

        }

        static void Main(string[] args)
        {
            int p=0,a=0,Xa=0,Xb=0;

            Console.WriteLine("Introduzca un numero primo: ");
            p= Convert.ToInt32( Console.ReadLine());
        
            do{
            Console.WriteLine("introduzca le valor alpha :");
            a=Convert.ToInt32(Console.ReadLine());
            }while(a>=p || a<=0);

            Console.WriteLine("introduzca el valor Xa: ");
            Xa=Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("introduzca el valor Xb: ");
            Xb=Convert.ToInt32(Console.ReadLine());         
        
            Console.Clear();

            Console.WriteLine("Xa = "+Xa);
            
            Console.WriteLine("Xb = "+Xb);
            diffie hellman = new diffie(a,p,Xa,Xb);

        
        }
    }
}
