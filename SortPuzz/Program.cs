using System;
using System.IO;

namespace SortPuzz
{
    class Program
    {

        static void Main(string[] args)
        {

            string[,] sortPuzz;
            int tupSayisi;

           

            string islem;
        OkuIslem:
            Console.Write("Renkleri klavyeden giriniz veya dosyadan seçiniz : Dosya/D - Klavye/K --> D - K = ");
            islem = Console.ReadLine();


            if (islem == "K")
            {
                Console.WriteLine("Tüp sayısı gir :");//Tüp sayısı belirleme

                tupSayisi = int.Parse(Console.ReadLine());
                sortPuzz = new string[tupSayisi, 4];

                for (int i = 0; i < tupSayisi; i++)
                {
                    Console.WriteLine("{0}. tüpteki renkler ", i + 1);//Renkleri klavyeden girme işlemi
                    int k = 0;

                    for (int j = 0; j < 4; j++)
                    {
                        Console.Write("{0}. tüpteki {1}. rengini gir : ", i + 1, k + 1);
                        sortPuzz[i, j] = Console.ReadLine();
                        k++;
                    }
                }
            }
            else if (islem == "D")
            {
                String textFile = File.ReadAllText(@"D:/Arcelik/C#/SortPuzz/renkKlasor/tubelist8.txt");//Dosyadan renkleri çekme

                //Dosyadaki tüp sayısını bulma
                int tupSayisiDosya = 0;

                foreach (var row in textFile.Split('\n'))
                {
                    tupSayisiDosya++;
                }

                sortPuzz = new string[tupSayisiDosya, 4];
                Console.WriteLine("Dosyadaki Tüp Sayisi : " + tupSayisiDosya);

                int i = 0, j = 0;
                //Dosyadan renkleri getirme ve sortPuzz çok boyutlu diziye kaydetme
                foreach (var row in textFile.Split('\n'))
                {

                    j = 0;
                    foreach (var col in row.Trim().Split(' '))
                    {

                        sortPuzz[i, j] = Convert.ToString(col.Trim());
                        j++;
                    }
                    i++;
                }

            }
            else
            {
                Console.WriteLine("Yanlış tuşlama yaptınız. Yeniden Deneyin.");
                goto OkuIslem;
            }
            Console.Clear();
            string eldeki = "----";

            //tüp sayısı
            int tupsayisi = sortPuzz.GetLength(0) - 1;
            int cikmazSayaci = 0;
            int geriAdimSayaci = 1;

            int hareketSayisi = 1;

            //adımların listessini saklayan array
            int adimSayisi = 0;

            string[] hareketDetay = new string[100];
            
            string[] adimListesi = new string[100];

            //gelinen son durumdaki hareketlerin listesini string olarak ilk 2 harfini saklayan array
            int sonDurumStringSayisi = 1;
            string[,] sonDurumString = new string[100, sortPuzz.GetLength(0)];

            //gelinen noktanın durum listesinndeki stringleri çevirmek için kullanılan array
            string[,] sonDurumHareketListesi = new string[sortPuzz.GetLength(0), 4];

            //kullanıcıya ekranda anlık çıktıyı gösterir
            void cikti()
            {
                for (int j = 3; j >= 0; j--)
                {
                    for (int i = 0; i <= tupsayisi; i++)
                    {
                        Console.Write(sortPuzz[i, j] + " ");

                    }
                    Console.WriteLine();
                }
                //sondurum();
                Console.WriteLine("");

            }
            Console.WriteLine("");
            cikti();

            // son durum hareket listesindeki renklerin ilk iki karakterini string tipinde saklıyoruz
            void sondurum()
            {
                for (int i = 0; i <= tupsayisi; i++)
                {
                    for (int j = 0; j <= 3; j++)
                    {
                        sonDurumString[sonDurumStringSayisi - 1, i] = sonDurumString[sonDurumStringSayisi - 1, i] + sortPuzz[i, j].Substring(0, 2);
                    }
                }
                sonDurumStringSayisi++;

            }

            //son durum hareket listesini geri alıyoruz
            void geriAdim(int adim)
            {
                int sayac = 0;
                for (int j = 0; j <= tupsayisi; j++)
                {
                    for (int k = 0; k <= 3; k++)
                    {
                        for (int i = 0; i <= tupsayisi; i++)
                        {
                            for (int s = 0; s <= 3; s++)
                            {
                                if (sayac <= 8 && sonDurumString[adim, j].Substring(sayac, 2) == sortPuzz[i, s].Substring(0, 2))
                                {
                                    sonDurumHareketListesi[j, k] = sortPuzz[i, s];

                                }
                            }
                        }
                        sayac = sayac + 2;
                    }
                    sayac = 0;
                }

                for (int j = 3; j >= 0; j--)
                {
                    for (int i = 0; i <= tupsayisi; i++)
                    {
                        sortPuzz[i, j] = sonDurumHareketListesi[i, j];
                    }
                    Console.WriteLine();
                }

                //cikti();
                Console.ReadLine();


            }

            //eldeki verinin taşınmaya uygunluğunu kontrol eder uygun ise true döner
            bool eldekiUygun = false;
            void eldekiKontrol(int i, int j)
            {
                if (j == 3 && sortPuzz[i, j] != "----" || (j < 3 && sortPuzz[i, j] != "----" && sortPuzz[i, j + 1] == "----"))
                {
                    eldekiUygun = true;
                }
                else
                {
                    eldekiUygun = false;
                }
            }

            //tüplerin taşınmaya uygunluğunu kontrol eder uygun ise true döner
            bool tupUygun = false;
            void tupKontrol(int i, int j, int k, int l)
            {
                if ((sortPuzz[k, l] == "----" && i != k && eldeki != "----")//baktığın yer boş değil VE aynı konum değil VE eldeki değişkeni boş değil ise VE
                &&
                (
                (l == 0 && sortPuzz[k, 1] == "----" && sortPuzz[k, 2] == "----" && sortPuzz[k, 3] == "----") //baktığın tupun en altındayken VE tüpün tümü boş ise
                ||
                (l != 0 && sortPuzz[k, l - 1] == eldeki)//tüpün en altında değilken VE baktığın yerin bir altı eldeki ile aynı ise
                )
                )
                {
                    if ((j < 3 && sortPuzz[i, 0] == eldeki && sortPuzz[i, 1] == eldeki && sortPuzz[i, 2] == eldeki))
                    {
                        tupUygun = false;
                    }
                    else
                    {
                        tupUygun = true;
                    }
                }
                else
                {
                    tupUygun = false;
                }

                tupDoluKontrol(i);
            }

            //tüplerin doluluğunu kontrol eder uygun ise true döner
            bool tupDolu = false;
            void tupDoluKontrol(int i)
            {
                if (sortPuzz[i, 0] == eldeki && sortPuzz[i, 1] == eldeki && sortPuzz[i, 2] == eldeki && sortPuzz[i, 3] == eldeki)
                {
                    tupDolu = true;
                }
                else
                {
                    tupDolu = false;
                }
            }

            //tüp sayısının iki eksiği kadar aynı renkte tüp olunca oyunu bitir
            void bitisKontrol(int hareketSayisi)
            {
                string kntrl = "----";
                int doluSayac = 0;
                for (int x = 0; x <= tupsayisi; x++)
                {
                    kntrl = sortPuzz[x, 3];

                    if (sortPuzz[x, 0] == kntrl && sortPuzz[x, 1] == kntrl && sortPuzz[x, 2] == kntrl && sortPuzz[x, 3] == kntrl && kntrl != "----")
                    {
                        doluSayac++;
                    }
                }

                if (doluSayac == tupsayisi - 1)
                {
                    for(int y = 1; y <= hareketSayisi; y++)
                    {
                        if (hareketDetay[y] != null)
                        {
                            Console.WriteLine(hareketDetay[y].ToString());
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                        
                    }
                   
                  
                    
                }
            }

            //dökme işlemi gerçekleşir
            void tupDok(int i, int j, int k, int l)
            {
  
                hareketSayisi++;

                adimListesi[adimSayisi] = (i + 1) + " to " + (k + 1);//atılan adımların listesini tutar
                adimSayisi++;
                

                if (l < 2 && j > 1 && sortPuzz[i, j - 1] == eldeki && sortPuzz[i, j - 2] == eldeki)//3lü taşı
                {
                    sortPuzz[k, l] = eldeki;
                    sortPuzz[k, l + 1] = eldeki;
                    sortPuzz[k, l + 2] = eldeki;
                    eldeki = "----";
                    sortPuzz[i, j] = "----";
                    sortPuzz[i, j - 1] = "----";
                    sortPuzz[i, j - 2] = "----";
                }
                else if (l < 3 && j > 0 && sortPuzz[i, j - 1] == eldeki)//2li taşı
                {
                    sortPuzz[k, l] = eldeki;
                    sortPuzz[k, l + 1] = eldeki;
                    eldeki = "----";
                    sortPuzz[i, j] = "----";
                    sortPuzz[i, j - 1] = "----";
                }
                else//teki taşı
                {
                    sortPuzz[k, l] = eldeki;
                    eldeki = "----";
                    sortPuzz[i, j] = "----";
                }
                cikmazSayaci = 0;
                //cikti();
            }


        basa:
            for (int i = 0; i <= tupsayisi; i++)//sutunları gez soldan sağa
            {
                for (int j = 3; j >= 0; j--)//satırlaeı gez soldan sağa
                {
                    eldekiKontrol(i, j);
                    if (eldekiUygun == true)//içinde veri var ise veya (en üst hariç) bir üstü boş ise ve kendisi boş değil ise)
                    {
                        eldeki = sortPuzz[i, j];//eldeki değişkenine veriyi at

                        for (int l = 3; l >= 0; l--)//satırlaeı gez soldan sağa
                        {
                            for (int k = 0; k <= tupsayisi; k++)//sutunları gez soldan sağa
                            {
                                tupKontrol(i, j, k, l);
                                if (tupUygun == true && tupDolu == false)
                                {
                                    //birim sıvı sayısı bulma
                                    int birimSiviSayisi = 0;
                                    if (l < 2 && j > 1 && sortPuzz[i, j - 1] == eldeki && sortPuzz[i, j - 2] == eldeki)//3lü taşı
                                    {
                                        birimSiviSayisi = birimSiviSayisi + 3;
                                    }
                                    else if (l < 3 && j > 0 && sortPuzz[i, j - 1] == eldeki)//2li taşı
                                    {
                                        birimSiviSayisi = birimSiviSayisi + 2;
                                    }
                                    else
                                    {
                                        birimSiviSayisi++;
                                    }
                                    //***********************

                                    Console.WriteLine((i + 1) + "->" + (k + 1) + " " + eldeki + " TOPLAM " + birimSiviSayisi + " adet");
                                    Console.WriteLine("Hareket sayısı :" + hareketSayisi);
                                    hareketDetay[hareketSayisi]= hareketSayisi + ". hareket " + (i + 1) + ". tüpten -> " + (k + 1) + ". nolu tüpe "+eldeki+" renginden "+birimSiviSayisi+" birim sıvı.";//ekrana atılması gereken adımları yazar
                                    tupDok(i, j, k, l);
                                    sondurum();
                                    cikti();
                                    bitisKontrol(hareketSayisi);
                                    cikmazSayaci = 0;
                                }
                                else
                                {
                                    cikmazSayaci++;
                                    if (cikmazSayaci > 300)
                                    {
                                        geriAdimSayaci++;
                                        cikmazSayaci = 0;
                                        Console.WriteLine(adimSayisi);
                                        geriAdim(adimSayisi - geriAdimSayaci);
                                        goto basa;
                                    }
                                }
                            }
                        }
                    }
                }
                
            }
            goto basa;




            Console.ReadKey();

            
        }
    }
}
