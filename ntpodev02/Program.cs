using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ntpodev02
{
    public class Kisi
    {
        public string isim { get; set; }
        public string Soyisim { get; set; }
      
    }

    public class Film
    {
        public string isim { get; set; }
        public string Tur { get; set; }
        public int Yil { get; set; }

        public int günlükKira { get; set; }
    }

    public class KiralananFilm
    {
        public Kisi Kisi { get; set; }
        public Film Film { get; set; }
        public DateTime KiralamaTarihi { get; set; }
        public DateTime IadeTarihi { get; set; }

        public int ToplamKiralamaBedeli
        {
            get
            {
                int kiralananGunSayisi = (IadeTarihi - KiralamaTarihi).Days;
                return kiralananGunSayisi * Film.günlükKira;
            }
        }
    }
    public class filmDukkanı
    {
        public List<Kisi> Kisiler = new List<Kisi>();
        public List<Film> Filmler = new List<Film>();
        public List<KiralananFilm> KiralananFilmler = new List<KiralananFilm>();


        public void KisiEkle(Kisi kisi)
        {
            Kisiler.Add(kisi);
        }


        public void FilmEkle(Film film)
        {
            Filmler.Add(film);
        }


        public void FilmKirala(Kisi kisi, Film film, DateTime kiralamaTarihi, DateTime iadeTarihi)
        {
            KiralananFilm kiralananFilm = new KiralananFilm { Kisi = kisi, Film = film, KiralamaTarihi = kiralamaTarihi, IadeTarihi = iadeTarihi };


            KiralananFilmler.Add(kiralananFilm);
        }

        }


    internal class Program
    {
        static void Main(string[] args)
        {
            filmDukkanı filmdukkanı = new filmDukkanı();

            Kisi kisi1 = new Kisi { isim = "Ali", Soyisim = "Öztürk" };
            Kisi kisi2 = new Kisi { isim = "Ayşe", Soyisim = "Öztürk" };

            filmdukkanı.KisiEkle(kisi1);
            filmdukkanı.KisiEkle(kisi2);

            Film film1 = new Film { isim = "Inception", Tur = "Bilim Kurgu", Yil = 2010, günlükKira = 10 };
            Film film2 = new Film { isim = "The Matrix", Tur = "Bilim Kurgu", Yil = 1999, günlükKira = 8 };

            filmdukkanı.FilmEkle(film1);
            filmdukkanı.FilmEkle(film2);

            filmdukkanı.FilmKirala(kisi1, film1, DateTime.Now.AddDays(-7), DateTime.Now);
            filmdukkanı.FilmKirala(kisi2, film2, DateTime.Now.AddDays(-5), DateTime.Now);



            StreamWriter dosyayaYaz = new StreamWriter("C:\\Users\\Huaweı\\Desktop\\kişi film.txt");
            using (dosyayaYaz)
            {
                foreach (var kiralanan in filmdukkanı.KiralananFilmler)
                {
                    dosyayaYaz.WriteLine(kiralanan.Kisi.isim + " " + kiralanan.Kisi.Soyisim);
                    dosyayaYaz.WriteLine( "kiralanan filmin ismi :" +kiralanan.Film.isim);
                    dosyayaYaz.WriteLine("Toplam Kiralama Bedeli: "+kiralanan.ToplamKiralamaBedeli );
                    dosyayaYaz.WriteLine();
                }

            }
            Console.WriteLine("veriler dosyaya girildi");
            Console.ReadKey();

        }
    }
}
