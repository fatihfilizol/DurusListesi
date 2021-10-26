using Business.Concrete;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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

            for (int i = 0; i < listIsEmri.Count; i++)
            {
                durus.Clear();
                for (int j = 0; j < listDurus.Count; j++)
                {
                    if (DateTime.Compare(listDurus[j].Baslangic, listIsEmri[i].Bitis) < 0 && DateTime.Compare(listIsEmri[i].Baslangic, listDurus[j].Baslangic) < 0)
                    {
                        if (listIsEmri[i].Baslangic < listDurus[j].Baslangic && listDurus[j].Bitis < listIsEmri[i].Bitis)
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

                        if (listIsEmri[i].Baslangic < listDurus[j].Baslangic && listIsEmri[i].Bitis < listDurus[j].Bitis)
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
                        if (listIsEmri[i].Baslangic < listDurus[j].Baslangic && listIsEmri[i].Bitis < listDurus[j].Bitis && durus.Contains(listDurus[j].DurusNedeni))
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
                    if (listDurus[j].Baslangic < listIsEmri[i].Baslangic && listIsEmri[i].Bitis < listDurus[j].Bitis)
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
            List<String> listColumns = new List<String>();
            listColumns.Add("İş Emri");
            for (int j = 0; j < listDurus.Count; j++)
            {
                for (int q = 0; q <= listColumns.Count; q++)
                {
                    if (listColumns.Contains(listDurus[j].DurusNedeni))
                    {

                    }
                    else
                    {
                        listColumns.Add(listDurus[j].DurusNedeni);

                    }
                }
            }

            var listMola = (from x in listSonuc
                            where x.DurusNedeni == listColumns[1]
                            select new
                            {
                                x.DurusSuresi,
                                x.DurusNedeni,
                                x.IsEmri

                            }).ToList();
            var listAriza = (from x in listSonuc
                             where x.DurusNedeni == listColumns[2]
                             select new
                             {
                                 x.DurusSuresi,
                                 x.DurusNedeni,
                                 x.IsEmri

                             }).ToList();
            var listSetup = (from x in listSonuc
                             where x.DurusNedeni == listColumns[3]
                             select new
                             {
                                 x.DurusSuresi,
                                 x.DurusNedeni,
                                 x.IsEmri

                             }).ToList();
            var listArge = (from x in listSonuc
                            where x.DurusNedeni == listColumns[4]
                            select new
                            {
                                x.DurusSuresi,
                                x.DurusNedeni,
                                x.IsEmri

                            }).ToList();
            var listEmir = (from x in listIsEmri
                            select new
                            {
                                x.IsEmriNumarasi

                            }).ToList();

            List<object> sonucListesi = new List<object>();
            sonucListesi.Add(listColumns);
            sonucListesi.Add(listIsEmri);
            sonucListesi.Add(listMola);
            sonucListesi.Add(listAriza);
            sonucListesi.Add(listSetup);
            sonucListesi.Add(listArge);
            Repeater1.DataSource = sonucListesi;
            Repeater1.DataBind();
        }
    }
}