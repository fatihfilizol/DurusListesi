using Business.Concrete;
using DataAccess.Concrete.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI
{
    public partial class Sonuc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IsEmriManager isEmriManager = new IsEmriManager(new InMemoryIsEmriDal());
            DurusManager durusManager = new DurusManager(new InMemoryDurusDal());
            SonucManager sonucManager = new SonucManager(new InMemorySonucDal());
            List<Sonuc> sonuc = new List<Sonuc>();
            var listIsEmri = isEmriManager.GetAll();
            var listDurus = durusManager.GetAll();
            var listSonuc = sonucManager.GetAll();
            List<String> durus = new List<String>();
            int k = 0;
            for (int i = 0; i < listIsEmri.Count; i++)
            {
                durus.Clear();
                for (int j = 0; j < listDurus.Count; j++)
                {

                    if (DateTime.Compare(listDurus[j].Baslangic, listIsEmri[i].Bitis) < 0 && DateTime.Compare(listIsEmri[i].Baslangic, listDurus[j].Baslangic) < 0)
                    {

                        TimeSpan durusDakika = listDurus[j].Baslangic.Subtract(listDurus[j].Bitis);
                        Decimal durusSuresi = Convert.ToDecimal(durusDakika.TotalMinutes) * -1;
                        sonuc.Add(new Sonuc { DurusNedeni = listDurus[j].DurusNedeni, IsEmri = listIsEmri[i].IsEmriNumarasi, DurusSuresi = durusSuresi });
                        if (listSonuc.Count == 0)
                        {
                            listSonuc.Add(new Sonuc() { DurusNedeni = listDurus[j].DurusNedeni, IsEmri = listIsEmri[i].IsEmriNumarasi, DurusSuresi = durusSuresi });
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
                                listSonuc.Add(new Sonuc() { DurusNedeni = listDurus[j].DurusNedeni, IsEmri = listIsEmri[i].IsEmriNumarasi, DurusSuresi = durusSuresi });
                                k++;
                            }
                        }
                        else
                        {
                            listSonuc.Add(new Sonuc() { DurusNedeni = listDurus[j].DurusNedeni, IsEmri = listIsEmri[i].IsEmriNumarasi, DurusSuresi = durusSuresi });
                            durus.Add(listDurus[j].DurusNedeni);
                            k++;
                        }
                    }
                }
            }
        }
    }
}