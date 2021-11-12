using Business.Concrete;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Dynamic;
namespace UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            IsEmriManager isEmriManager = new IsEmriManager(new InMemoryIsEmriDal());
            DurusManager durusManager = new DurusManager(new InMemoryDurusDal());
            SonucManager sonucManager = new SonucManager(new InMemorySonucDal());

            List<String> durus = new List<String>();
            List<Sonuc> sonuc = new List<Sonuc>();

            var listIsEmri = isEmriManager.GetAll();
            var listDurus = durusManager.GetAll();
            var listSonuc = sonucManager.GetAll();

            int k = 0;

            for (int i = 0; i < listIsEmri.Count; i++)//İş Emirlerini Gezmek İçin
            {
                durus.Clear();
                for (int j = 0; j < listDurus.Count; j++)//Duruşları gezmek için
                {
                    if (DateTime.Compare(listDurus[j].Baslangic, listIsEmri[i].Bitis) < 0 && DateTime.Compare(listIsEmri[i].Baslangic, listDurus[j].Baslangic) < 0)//Duruş Aralığının, Gelen İş Emrinin İçerisinde Olup Olmadığını Sorguluyor
                    {
                        if (listIsEmri[i].Baslangic < listDurus[j].Baslangic && listDurus[j].Bitis < listIsEmri[i].Bitis)//Duruş Süresi tek İş Emri aralığındaysa
                        {
                            TimeSpan durusDakika = listDurus[j].Baslangic.Subtract(listDurus[j].Bitis);
                            Decimal durusSuresi = Convert.ToDecimal(durusDakika.TotalMinutes) * -1;
                            sonuc.Add(new Sonuc { DurusNedeni = listDurus[j].DurusNedeni, IsEmri = listIsEmri[i].IsEmriNumarasi, DurusSuresi = durusSuresi });
                            if (listSonuc.Count == 0)
                            {
                                sonucManager.Add(listDurus[j].DurusNedeni, listIsEmri[i].IsEmriNumarasi, durusSuresi);
                                durus.Add(listDurus[j].DurusNedeni);
                                k++;
                            }
                            else if (durus.Contains(listDurus[j].DurusNedeni))
                            {
                                if (listIsEmri[i].IsEmriNumarasi == listSonuc[k - 1].IsEmri)
                                {
                                    sonucManager.Update(sonuc[sonuc.Count - 1]);
                                }
                                else
                                {
                                    sonucManager.Add(listDurus[j].DurusNedeni, listIsEmri[i].IsEmriNumarasi, durusSuresi);
                                    k++;
                                }
                            }
                            else
                            {
                                sonucManager.Add(listDurus[j].DurusNedeni, listIsEmri[i].IsEmriNumarasi, durusSuresi);
                                durus.Add(listDurus[j].DurusNedeni);
                                k++;
                            }
                        }

                        if (listIsEmri[i].Baslangic < listDurus[j].Baslangic && listIsEmri[i].Bitis < listDurus[j].Bitis)//İş Emri süresi bitiyor fakat Duruş devam ediyorsa
                        {
                            TimeSpan durusDakikaAsan = listDurus[j].Baslangic.Subtract(listIsEmri[i].Bitis);
                            Decimal durusSuresiAsan = Convert.ToDecimal(durusDakikaAsan.TotalMinutes) * -1;

                            sonuc.Add(new Sonuc { DurusNedeni = listDurus[j].DurusNedeni, IsEmri = listIsEmri[i].IsEmriNumarasi, DurusSuresi = durusSuresiAsan });

                            if (durus.Contains(listDurus[j].DurusNedeni))
                            {
                                if (listIsEmri[i].IsEmriNumarasi == listSonuc[k - 1].IsEmri)
                                {
                                    sonucManager.Update(sonuc[sonuc.Count - 1]);
                                }
                                else
                                {
                                    sonucManager.Add(listDurus[j].DurusNedeni, listIsEmri[i].IsEmriNumarasi, durusSuresiAsan);
                                    k++;
                                }
                            }
                            else
                            {
                                sonucManager.Add(listDurus[j].DurusNedeni, listIsEmri[i].IsEmriNumarasi, durusSuresiAsan);
                                durus.Add(listDurus[j].DurusNedeni);
                                k++;
                            }
                        }
                        if (listIsEmri[i].Baslangic < listDurus[j].Baslangic && listIsEmri[i].Bitis < listDurus[j].Bitis && durus.Contains(listDurus[j].DurusNedeni))//Duruş bir İş Emri Süresini aşıyorsa
                        {
                            TimeSpan durusDakikaArtan = listIsEmri[i].Bitis.Subtract(listDurus[j].Bitis);
                            Decimal durusSuresiArtan = Convert.ToDecimal(durusDakikaArtan.TotalMinutes) * -1;
                            if (Convert.ToInt32(durusSuresiArtan) < 480)
                            {
                                sonucManager.Add(listDurus[j].DurusNedeni, listIsEmri[i + 1].IsEmriNumarasi, durusSuresiArtan);
                                sonuc.Add(new Sonuc { DurusNedeni = listDurus[j].DurusNedeni, IsEmri = listIsEmri[i + 1].IsEmriNumarasi, DurusSuresi = durusSuresiArtan });
                                k++;
                            }
                            else
                            {
                                sonucManager.Add(listDurus[j].DurusNedeni, listIsEmri[i + 2].IsEmriNumarasi, durusSuresiArtan - Convert.ToDecimal(480));
                                sonuc.Add(new Sonuc { DurusNedeni = listDurus[j].DurusNedeni, IsEmri = listIsEmri[i + 1].IsEmriNumarasi, DurusSuresi = durusSuresiArtan });
                                k++;
                            }
                        }
                    }
                    if (listDurus[j].Baslangic < listIsEmri[i].Baslangic && listIsEmri[i].Bitis < listDurus[j].Bitis)//Duruş gelen İş Emrinden önce başlamış ve gelen İş Emri içerisinde de devam ediyorsa gelen iş emri içerisindeki süreyi hesaplamak için
                    {
                        TimeSpan durusDakikaKalan = listIsEmri[i].Baslangic.Subtract(listIsEmri[i].Bitis);
                        Decimal durusSuresiKalan = Convert.ToDecimal(durusDakikaKalan.TotalMinutes) * -1;
                        sonuc.Add(new Sonuc { DurusNedeni = listDurus[j].DurusNedeni, IsEmri = listIsEmri[i].IsEmriNumarasi, DurusSuresi = durusSuresiKalan });
                        if (durus.Contains(listDurus[j].DurusNedeni))
                        {
                            if (listIsEmri[i].IsEmriNumarasi == listSonuc[k - 1].IsEmri)
                            {
                                sonucManager.Update(sonuc[sonuc.Count - 1]);
                            }
                            else
                            {
                                sonucManager.Add(listDurus[j].DurusNedeni, listIsEmri[i].IsEmriNumarasi, durusSuresiKalan);
                                k++;
                            }
                        }
                        else
                        {
                            sonucManager.Add(listDurus[j].DurusNedeni, listIsEmri[i].IsEmriNumarasi, durusSuresiKalan);
                            durus.Add(listDurus[j].DurusNedeni);
                            k++;
                        }
                    }


                }
            }


            var list = new List<object>();

            var listScreen = listSonuc.GroupBy(l => l.IsEmri).OrderBy(o => o.Key).ToList();//Duruşları İş Emirlerine göre gruplar
            var bosIsEmirleri = listIsEmri.GroupBy(l => l.IsEmriNumarasi).OrderBy(o => o.Key).Select(i => i.Key);//Zaman aralığında duruş olmayan iş emirlerini çeker
            var bosIsEmriEkleme = bosIsEmirleri.Where(c => listScreen.Select(l => l.Key).Contains(c) == false).Select(ekelenecek => new Sonuc
            {
                DurusNedeni = null,
                DurusSuresi = 0,
                IsEmri = ekelenecek
            }).ToList();//Zaman aralığında Duruş olmayan İş Emirlerini null şekilde oluşturur
            listSonuc.AddRange(bosIsEmriEkleme);//Zaman aralığında Duruş olmayan İş Emirlerini null şekilde Sonuc listesine ekler
            listScreen = listSonuc.GroupBy(l => l.IsEmri).OrderBy(o => o.Key).ToList();//Sonuç Listesinin son halini İş Emirlerine gçre gruplayarak Ekran Listesine atar

            DataTable dt = new DataTable();//Burdan Sonra Ekran Listesinde gelen verileri tablo haline getirip datagrdiview'e datasource ediyor
            List<KeyValuePair<string, decimal>> toplamList = new List<KeyValuePair<string, decimal>>();

            foreach (var column in listScreen)
            {
                if (!dt.Columns.Contains("İş Emri"))
                    dt.Columns.Add("İş Emri");

                DataRow dr = dt.NewRow();
                dr["İş Emri"] = column.Key;
                decimal toplam = 0;
                foreach (var row in column)
                {
                    if (row.DurusNedeni != null && !dt.Columns.Contains(row.DurusNedeni))
                        dt.Columns.Add(row.DurusNedeni);
                    if (row.DurusNedeni != null)
                    {
                        dr[row.DurusNedeni] = row.DurusSuresi;
                        toplamList.Add(new KeyValuePair<string, decimal>(row.DurusNedeni, row.DurusSuresi));
                    }
                    toplam += row.DurusSuresi;

                }
                if (!dt.Columns.Contains("Toplam"))
                {
                    dt.Columns.Add("Toplam");
                }

                dr["Toplam"] = toplam;
                dt.Columns["Toplam"].SetOrdinal(dt.Columns.Count - 1);
                toplamList.Add(new KeyValuePair<string, decimal>("Toplam", toplam));
                dt.Rows.Add(dr);
            }
            DataRow drTop = dt.NewRow();
            drTop["İş Emri"] = "Toplam";
            var listToplam = toplamList.GroupBy(tl => tl.Key);
            foreach (var item in listToplam)
            {
                drTop[item.Key] = item.Select(ab => ab.Value).Sum();
            }
            dt.Rows.Add(drTop);





            dataGridView2.DataSource = dt;

        }
    }
}
