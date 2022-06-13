using System;

namespace SortPuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            string eldeki = "----";

            //verilerin saklandığı array
            string[,] sortPuzz = new string[,]
            {
                {
                    "kahv",
                    "kahv",
                    "kahv",
                    "pemb"
                },
                {
                    "sari",
                    "yesi",
                    "morr",
                    "turu"
                },
                {
                    "sari",
                    "grey",
                    "morr",
                    "yesi"
                },
                {
                    "grey",
                    "grey",
                    "pemb",
                    "pemb"
                },
                {
                    "sari",
                    "pemb",
                    "turu",
                    "yesi"
                },
                {
                    "sari",
                    "grey",
                    "morr",
                    "kahv"
                },
                {
                    "turu",
                    "yesi",
                    "turu",
                    "morr"
                },
                {
                    "----",
                    "----",
                    "----",
                    "----"
                },
                {
                    "----",
                    "----",
                    "----",
                    "----"
                },
            };


            //tüp sayısı
            int tupsayisi = sortPuzz.GetLength(0) - 1;
            int cikmazSayaci = 0;
            int geriAdimSayaci = 1;
            //adımların listessini saklayan array
            int adimSayisi = 0;
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
                Console.ReadLine();
            }
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

                cikti();
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
            void bitisKontrol()
            {
                string kntrl = "----";
                int doluSayac = 0;
                for (int i = 0; i <= tupsayisi; i++)
                {
                    kntrl = sortPuzz[i, 3];

                    if (sortPuzz[i, 0] == kntrl && sortPuzz[i, 1] == kntrl && sortPuzz[i, 2] == kntrl && sortPuzz[i, 3] == kntrl && kntrl != "----")
                    {
                        doluSayac++;
                    }
                }

                if (doluSayac == tupsayisi - 1)
                {
                    Console.WriteLine("başardık");
                    Environment.Exit(0);
                }
            }

            //dökme işlemi gerçekleşir
            void tupDok(int i, int j, int k, int l)
            {
                Console.WriteLine((i + 1) + " to " + (k + 1));//ekrana atılması gereken adımları yazar

                adimListesi[adimSayisi] = (i + 1) + " to " + (k + 1);//atılan adımların listesini tutar
                adimSayisi++;
                Console.WriteLine();

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
                cikti();
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
                                    tupDok(i, j, k, l);
                                    sondurum();
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
                bitisKontrol();
            }
            goto basa;
        }
    }
}
