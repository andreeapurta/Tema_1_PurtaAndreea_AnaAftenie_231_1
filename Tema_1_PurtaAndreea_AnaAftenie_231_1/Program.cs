using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Tema_1_PurtaAndreea_AnaAftenie_231_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string sirInitial = "";
            string simbolStart = "";
            string tipDerivare;
            var terminale = new List<string>();
            var neterminale = new List<string>();
            var perechi = new List<(string, string)>();
          
            using (StreamReader fisier = new StreamReader(@"C:\Users\Andreea Purta\source\repos\Tema_1_PurtaAndreea_AnaAftenie_231_1\Tema_1_PurtaAndreea_AnaAftenie_231_1\FileIn.txt"))
            {
                while (fisier.Peek() > -1)
                {
                    simbolStart += fisier.ReadLine();
                    string auxTerm = fisier.ReadLine();
                    foreach (var i in auxTerm.Split(","))
                    {
                        terminale.Add(i);
                    }
                    string aux = fisier.ReadLine();
                    foreach(var i in aux.Split(","))
                    {
                        neterminale.Add(i);
                    }
                    sirInitial += fisier.ReadLine();
                }
            }
            
            Console.Write("Simbol start");
            Console.WriteLine(simbolStart);
            Console.Write("Semne terminale");
            terminale.ForEach(Console.WriteLine);
            Console.Write("Semne neterminale");
            neterminale.ForEach(Console.WriteLine);
            Console.Write("Reguli de derivare");
            Console.WriteLine(sirInitial);
            Console.WriteLine("Alege Tipul de derivare: 1 stanga sau 2 dreapta");
            tipDerivare = Console.ReadLine();
            
            string[] sirImpartit = sirInitial.Split(",");

            foreach (var entities in sirImpartit)
            {
                ImpartireSubSiruri(entities);
            }

        //generare random
            for(int i=0;i<45;i++)
            {
                Console.WriteLine(GenerareTransofrmari());
            }
            GenerareTransofrmari();
            void ImpartireSubSiruri(string subSir)
            {
                string[] SubSirImpartitStangaDreapta = subSir.Split("-");
                string[] derivare = SubSirImpartitStangaDreapta[1].Split("|");               
                foreach (var value in derivare)
                {
                    perechi.Add((SubSirImpartitStangaDreapta[0], value)); //E si E+t dupa E si T toate comb de perechi
                }               
            }

            string GenerareTransofrmari()
            {
                string rezultat = simbolStart;
                if(tipDerivare=="1")
                {
                    for(int i=0;i<rezultat.Length;i++)
                    {
                        //ia perechia e si e+t
                        //compara cu stringul de start
                        if(!terminale.Contains(rezultat[i].ToString())) //parcurci sirul de la 0 si te uiti daca primul termen, e simbol termina??!
                        { //nu e terminal modifica!!!
                            var reguliDerivare = new List<string>();
                            foreach(var x in perechi)                             //ia toate reg de derivare pentru litera gasita
                            {
                                if (x.Item1.Equals(rezultat[i].ToString())) //item 1 simbol dupa care cauti item 2 e regula de derivare 
                                {
                                    reguliDerivare.Add(x.Item2);
                                }
                            }
                            Random derivareRnd = new Random();
                            int index = derivareRnd.Next()%reguliDerivare.Count; //ca sa imi ia random doar din lungimea reg de derivare
                            rezultat = rezultat.Replace(rezultat[i].ToString(), reguliDerivare[index].ToString()); //inlocuieste cu regula de derivare random
                            i = - 1 ; //pentru ca sa nu pierd cea mai din stanga litera ca dupa ce face ++ sa treaca la 0 
                            Thread.Sleep(1);
                        }
                    }               
                }
                else
                {
                    for (int i = rezultat.Length-1; i >= 0; i--)
                    {
                        //ia perechia e si e+t
                        //compara cu stringul de start
                        if (!terminale.Contains(rezultat[i].ToString()))
                        {
                            //ia toate reg de derivare pentru litera gasita
                            var reguliDerivare = new List<string>();
                            foreach (var x in perechi)
                            {
                                if (x.Item1.Equals(rezultat[i].ToString()))
                                {
                                    reguliDerivare.Add(x.Item2);
                                }
                            }
                            Random derivareRnd = new Random();
                            int index = derivareRnd.Next() % reguliDerivare.Count; //ca sa imi ia random doar din lungimea reg de derivare
                            rezultat = rezultat.Replace(rezultat[i].ToString(), reguliDerivare[index].ToString());
                            i=rezultat.Length; //pentru ca sa nu pierd cea mai din stanga litera
                            Thread.Sleep(1); //ca sa nu bage acelasi sir de 100 ori //ca randomul sa ia alt numar
                        }
                    }
                }
                return rezultat;
            }
            Console.ReadKey();
        }
    }
}
