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
                        if (listIsEmri[i].Baslangic < listDurus[j].Baslangic && listIsEmri[i].Bitis < listDurus[j].Bitis&&durus.Contains(listDurus[j].DurusNedeni))
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
                                sonucManager.Add(listDurus[j].DurusNedeni, listIsEmri[i + 2].IsEmriNumarasi, durusSuresiArtan-Convert.ToDecimal(480));
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

            




            //Sütunları listeden çekip yeni listeye atma
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
            listColumns.Add("Toplam");
            dataGridView2.ColumnCount = listColumns.Count;


            //Sütun Ekleme
            for (int i = 0; i < listColumns.Count; i++)
            {
                dataGridView2.Columns[i].Name = listColumns[i];
            }

            //Verileri kategorize ederek eşleştirme
            List<int> listMola = new List<int>() {0,0,0,0,0,0,0,0};
            List<int> listAriza = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 };
            List<int> listArge = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 };
            List<int> listSetup = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 };
            List<String> listMolaKontrol = new List<string>();
            List<String> listArizaKontrol = new List<string>();
            List<String> listArgeKontrol = new List<string>();
            List<String> listSetupKontrol = new List<string>();

            string atama;
            char atamaindex;
            int sira = 0;
            for (int j = 0; j < listIsEmri.Count; j++)
            {
                for (int i = 0; i < listSonuc.Count; i++)
                {
                    
                    if (listSonuc[i].DurusNedeni == listColumns[1])
                    {
                        if (!listMolaKontrol.Contains(listSonuc[i].IsEmri.ToString()))
                        {
                            atama = listSonuc[i].IsEmri.ToString();
                            atamaindex = atama[atama.Length - 1];
                            sira = Convert.ToInt32(atamaindex.ToString());
                            listMola.Insert(sira - 1, Convert.ToInt32(listSonuc[i].DurusSuresi));
                            listMolaKontrol.Add(listSonuc[i].IsEmri.ToString());

                        }
                    }
                    if (listSonuc[i].DurusNedeni == listColumns[2])
                    {
                        if (!listArizaKontrol.Contains(listSonuc[i].IsEmri.ToString()))
                        {
                            atama = listSonuc[i].IsEmri.ToString();
                            atamaindex = atama[atama.Length - 1];
                            sira = Convert.ToInt32(atamaindex.ToString());
                            listAriza.Insert(sira-1, Convert.ToInt32(listSonuc[i].DurusSuresi));
                            listArizaKontrol.Add(listSonuc[i].IsEmri.ToString());
                        }
                    }
                    if (listSonuc[i].DurusNedeni == listColumns[3])
                    {
                        if (!listSetupKontrol.Contains(listSonuc[i].IsEmri.ToString()))
                        {
                            atama = listSonuc[i].IsEmri.ToString();
                            atamaindex = atama[atama.Length - 1];
                            sira = Convert.ToInt32(atamaindex.ToString());
                            listSetup.Insert(sira-1, Convert.ToInt32(listSonuc[i].DurusSuresi));
                            listSetupKontrol.Add(listSonuc[i].IsEmri.ToString());
                        }
                    }
                    if (listSonuc[i].DurusNedeni == listColumns[4])
                    {
                        if (!listArgeKontrol.Contains(listSonuc[i].IsEmri.ToString()))
                        {
                            atama = listSonuc[i].IsEmri.ToString();
                            atamaindex = atama[atama.Length - 1];
                            sira = Convert.ToInt32(atamaindex.ToString());
                            listArge.Insert(sira-1, Convert.ToInt32(listSonuc[i].DurusSuresi));
                            listArgeKontrol.Add(listSonuc[i].IsEmri.ToString());
                        }
                    }
                }
                       
            }


            //Satır Ekleme
            int toplamMola = 0;
            int toplamAriza = 0;
            int toplamSetup = 0;
            int toplamArge = 0;
            int toplam = 0;
            int toplamToplam = 0;
            for (int i = 0; i < listIsEmri.Count; i++)
            {
                toplamMola += listMola[i];
                toplamAriza += listAriza[i];
                toplamSetup += listSetup[i];
                toplamArge += listArge[i];
                toplam = (listMola[i] + listAriza[i] + listSetup[i] + listArge[i]);
                toplamToplam += toplam;

                dataGridView2.Rows.Add(listIsEmri[i].IsEmriNumarasi.ToString()
                    , listMola[i].ToString()
                          , listAriza[i].ToString()
                          , listSetup[i].ToString()
                          , listArge[i].ToString()
                          , toplam.ToString()

                          );  
            }
            dataGridView2.Rows.Add("Toplam",toplamMola,toplamAriza,toplamSetup,toplamArge,toplamToplam);
            //dataGridView2.DataSource = listSonuc;
            
        }
    }
}
