using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ProjektProjektProjekt;
using ProjektProjektProjekt.Database;
using ProjektProjektProjekt.Database.Tabulky;
using ProjektProjektProjekt.Database.Tabulky.DTO;

namespace StartProject
{
    class Program
    {
        static void Main(string[] args)
        {

            

            // 1.Klub //

            var k = new Klub
            {
                klub_nazev = "Ostrava",
                sponzoring = 0,
                smazano = false
            };

            var k2 = new Klub
            {
                klub_nazev = "Sparta",
                sponzoring = 50000,
                smazano = false
            };

            //int c1 = KlubTable.Select();
            Console.WriteLine("Funkce 1.1. Vlozeni klubu");
            int id = KlubTable.Insert(k);
            int id2 = KlubTable.Insert(k2);
            Console.WriteLine("Vlozene 2x id: " + id + " " + id2);
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////
            var u = new Uzi
            {
                username = "admin",
                password = "admin",
                is_admin = true,
                is_manager = false,
                klub_id = 1
            };

            var u2 = new Uzi
            {
                username = "manager",
                password = "manager",
                is_admin = false,
                is_manager = true,
                klub_id = 1
            };

            var u3 = new Uzi
            {
                username = "uzivatel",
                password = "uzivatel",
                is_admin = false,
                is_manager = false,
                klub_id = 1
            };

            int idu = UziTable.Insert(u);
            int idu2 = UziTable.Insert(u2);
            int idu3 = UziTable.Insert(u3);
            Console.WriteLine("Vlozeni uzivatelu");
            Console.WriteLine("Vlozene 2x id: " + idu + " " + idu2 + " " + idu3);
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////
            
            Console.WriteLine("Funkce 1.2. Aktualizace klubu");
            Console.WriteLine("Pred Updatem: " + KlubTable.Select(id).klub_id + " " + KlubTable.Select(id).klub_nazev + " " + KlubTable.Select(id).sponzoring);
            var kk = KlubTable.Select(id);
            kk.klub_nazev = "Vitkovice";
            kk.sponzoring = 500000;
            KlubTable.Update(kk);
            Console.WriteLine("Po Updatu: " + KlubTable.Select(id).klub_id + " " + KlubTable.Select(id).klub_nazev + " " + KlubTable.Select(id).sponzoring);
            
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////
            
            Console.WriteLine("Funkce 1.4. Zruseni klubu");
            Console.WriteLine("Pred Updatem: " + KlubTable.Select(id).klub_id + " " + KlubTable.Select(id).smazano);
            KlubTable.Smazat(kk);
            Console.WriteLine("Po Updatu: " + KlubTable.Select(id).klub_id + " " + KlubTable.Select(id).smazano);
           
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////
            


            Console.WriteLine("Funkce 1.5. Pripsani financnich prostredku probehne az po 7.1. Vlozeni sponzora");
            
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////

            // 2.Tym //

           
           

           

            var t = new Tym()
            {
                tym_nazev = "Praha A - Tym",
                klub_id = KlubTable.Select(id).klub_id,
                smazano = false
            };

            var t2 = new Tym()
            {
                tym_nazev = "Vitkovice A - Tym",
                klub_id = KlubTable.Select(id2).klub_id,
                smazano = false
            };

            Console.WriteLine("Funkce 2.1. Vlozeni tymu");
            int idt = TymTable.Insert(t);
            int idt2 = TymTable.Insert(t2);
            Console.WriteLine("Vlozene id: " + idt + " " + idt2);
           
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////
            
            Console.WriteLine(TymTable.Select(idt).tym_id);
            Console.WriteLine(" " + idt);
            Console.WriteLine("Funkce 2.2. aktualizace tymu");
            Console.WriteLine("Pred Updatem: " + TymTable.Select(idt).tym_id + " " + TymTable.Select(idt).tym_nazev);
            var tt = TymTable.Select(idt);
            tt.tym_nazev = "Sparta A - Tym";
            TymTable.Update(tt);
            Console.WriteLine("Po Updatu: " + TymTable.Select(idt).tym_id + " " + TymTable.Select(idt).tym_nazev);
            
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////

            Console.WriteLine("Funkce 2.3. Zruseni tymu");
            Console.WriteLine("Pred Updatem: " + TymTable.Select(idt).tym_id + " " + TymTable.Select(idt).smazano);
            TymTable.Smazat(tt);
            Console.WriteLine("Po Updatu: " + TymTable.Select(idt).tym_id + " " + TymTable.Select(idt).smazano);
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////
            
            Console.WriteLine("Funkce 2.5. Koupe tymu proběhne po funkci 1.5. Pripsani financnich prostredku");
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////

            // 3.Hráči //
            string jmeno = "Pavel Maximus";
            var h = new Hrac()
            {
                hrac_jmeno = jmeno,
                datum_narozeni = new DateTime(1998, 1, 1),
                cena = 10000,
                vyska = 180,
                vaha = 80,
                zahyb = "leva",
                post = "utocnik",
                tym_id = idt2,
                smazano = false
            };

            string jmeno2 = "Bejkar Maximus";
            var h2 = new Hrac()
            {
                hrac_jmeno = jmeno2,
                datum_narozeni = new DateTime(1998, 1, 1),
                cena = 15000,
                vyska = 180,
                vaha = 80,
                zahyb = "leva",
                post = "utocnik",
                tym_id = idt2,
                smazano = false
            };

            string jmeno3 = "Trojkar Maximus";
            var h3 = new Hrac()
            {
                hrac_jmeno = jmeno3,
                datum_narozeni = new DateTime(2002, 1, 1),
                cena = 20000,
                vyska = 180,
                vaha = 80,
                zahyb = "leva",
                post = "utocnik",
                tym_id = idt2,
                smazano = false
            };
            string jmeno4 = "Ctyrkar Maximus";
            var h4 = new Hrac()
            {
                hrac_jmeno = jmeno4,
                datum_narozeni = new DateTime(2006, 1, 1),
                cena = 20000,
                vyska = 180,
                vaha = 80,
                zahyb = "leva",
                post = "utocnik",
                tym_id = idt,
                smazano = false
            };
            Console.WriteLine("Funkce 3.1. Vlozeni hrace");
            int idh = HracTable.Insert(h);
            int idh2 = HracTable.Insert(h2);
            int idh3 = HracTable.Insert(h3);
            int idh4 = HracTable.Insert(h4);
            Console.WriteLine("Vlozene 4 id: " + idh + " " + idh2 + " " + idh3 + " " + idh4);
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////

            Console.WriteLine("Funkce 3.3. Seznam hracu tymu");
            Console.WriteLine("Pocet hracu daneho tymu: " + HracTable.SelectTym(2).Count);
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////
            var hh = HracTable.Select(idh);
            Console.WriteLine("Funkce 3.4. Vymazani hrace");
            Console.WriteLine("Pred Updatem: " + HracTable.Select(idh).hrac_id + " " + HracTable.Select(idh).smazano);
            HracTable.Smazat(hh);
            Console.WriteLine("Po Updatu: " + HracTable.Select(idh).hrac_id + " " + HracTable.Select(idh).smazano);
            
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////

            Console.WriteLine("Funkce 3.5. Koupe hrace proběhne po funkci 1.5. Pripsani financnich prostredku");
            
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////

            Console.WriteLine("Funkce 3.6. Vymena Hracu funkce probehne po funkci 1.5. Pripsani financnich prostredku");
            
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////

            Console.WriteLine("Funkce 3.7. Oceneni nejlepsich mladiku v lize --- provede se az po 4.1. Pridani statistiky");
            Console.WriteLine("Pred Updatem hrac mladsi dvaceti let: id, cena: " + HracTable.Select(idh4).hrac_id + " " + HracTable.Select(idh4).cena);
            HracTable.Ocenit();
            Console.WriteLine("Po Updatem hrac mladsi dvaceti let: id, cena: " + HracTable.Select(idh4).hrac_id + " " + HracTable.Select(idh4).cena);

            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////
            
            Console.WriteLine(HracTable.Select(idh).hrac_jmeno);
            Console.WriteLine("Funkce 3.8. Vyhledat hrace");
            Console.WriteLine("Pocet hracu se zadanym jmenem: " + HracTable.SelectJmeno(jmeno).Count);
            
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////

            Console.WriteLine("Funkce 3.9. Zobrazeni podrobnosti o hraci Pouzita az po funkci 4.2 Aktualitace statistiky");
            
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////

            //4. Statistika//

            var s = new Statistika()
            {
                body = 0,
                goly = 0,
                asistence = 0,
                zapasy = 0,
                hrac_id = idh,
                smazano = false
            };

            var s2 = new Statistika()
            {
                body = 0,
                goly = 0,
                asistence = 0,
                zapasy = 0,
                hrac_id = idh2,
                smazano = false
            };

            var s3 = new Statistika()
            {
                body = 0,
                goly = 0,
                asistence = 0,
                zapasy = 0,
                hrac_id = idh3,
                smazano = false
            };

            var s4 = new Statistika()
            {
                body = 0,
                goly = 0,
                asistence = 0,
                zapasy = 0,
                hrac_id = idh4,
                smazano = false
            };
            Console.WriteLine("Funkce 4.1. Vlozeni Statistiky");
            int ids = StatistikaTable.Insert(s);
            int ids2 = StatistikaTable.Insert(s2);

            int ids3 = StatistikaTable.Insert(s3);
            int ids4 = StatistikaTable.Insert(s4);
            Console.WriteLine("Vlozene id: " + ids);

            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////

            Console.WriteLine("Funkce 3.7. Oceneni nejlepsich mladiku v lize");
            Console.WriteLine("Pred Updatem hrac mladsi dvaceti let: id, cena: " + HracTable.Select(idh4).hrac_id + " " + HracTable.Select(idh4).cena);
            HracTable.Ocenit();
            Console.WriteLine("Po Updatem hrac mladsi dvaceti let: id, cena: " + HracTable.Select(idh4).hrac_id + " " + HracTable.Select(idh4).cena);

            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////

            Console.WriteLine("Funkce 4.2. Aktualizace Statistiky");
            
            Console.WriteLine("Pred Updatem: " + StatistikaTable.Select(ids).stat_id + " " + StatistikaTable.Select(ids).body + " " + StatistikaTable.Select(ids).goly + " " + StatistikaTable.Select(ids).asistence + " " + StatistikaTable.Select(ids).zapasy);
           
            var ss = StatistikaTable.Select(ids);
            ss.body = 10;
            ss.goly = 5;
            ss.asistence = 5;
            ss.zapasy = 8;

            StatistikaTable.Update(ss);
            Console.WriteLine("Po Updatu: " + StatistikaTable.Select(ids).stat_id + " " + StatistikaTable.Select(ids).body + " " + StatistikaTable.Select(ids).goly + " " + StatistikaTable.Select(ids).asistence + " " + StatistikaTable.Select(ids).zapasy);
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////
            ///
            Console.WriteLine("Funkce 3.9. Zobrazeni podrobnosti o hraci");
            Console.WriteLine(HracTable.Select(idh).hrac_id + " " + HracTable.Select(idh).hrac_jmeno + " " + HracTable.Select(idh).datum_narozeni + " " + StatistikaTable.Select(ids).body + " " + StatistikaTable.Select(ids).goly + " " + StatistikaTable.Select(ids).asistence);
           
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////
            Console.WriteLine("Funkce 4.4. Vymazani Statistiky");
            Console.WriteLine("Pred Updatem: " + StatistikaTable.Select(ids).stat_id + " " + StatistikaTable.Select(ids).smazano);
            StatistikaTable.Smazat(ss);
            Console.WriteLine("Po Updatu: " + StatistikaTable.Select(ids).stat_id + " " + StatistikaTable.Select(ids).smazano);

            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////

            //5. Zebricek//

            
            
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////

            Console.WriteLine("Funkce 5.1. Vytvoreni zebricku");
            var z = new Zebricek()
            {
                zapasy = 0,
                body = 0,
                skore = 0,
                tym_id = idt,
                smazano = false
            };

            var z2 = new Zebricek()
            {
                zapasy = 0,
                body = 0,
                skore = 0,
                tym_id = idt2,
                smazano = false
            };

            int idz = ZebricekTable.Insert(z);
            int idz2 = ZebricekTable.Insert(z2);
            Console.WriteLine("Vlozene id: " + idz);
            Console.WriteLine("Vlozene id: " + idz2);

            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////

            Console.WriteLine(idz);
            Console.WriteLine("Pred Updatem: " + ZebricekTable.Select(idz).zebricek_id + " " + ZebricekTable.Select(idz).zapasy + " " + ZebricekTable.Select(idz).body + " " + ZebricekTable.Select(idz).skore);

            Console.WriteLine("Funkce 5.2. Aktualizace zebricku");
            var zebb = ZebricekTable.Select(idz);
            zebb.zapasy = 10;
            zebb.body = 20;
            zebb.skore = 5;

            ZebricekTable.Update(zebb);
            Console.WriteLine("Po Updatu: " + ZebricekTable.Select(idz).zebricek_id + " " + ZebricekTable.Select(idz).zapasy + " " + ZebricekTable.Select(idz).body + " " + ZebricekTable.Select(idz).skore);
            
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////

            Console.WriteLine("Funkce 5.3. Zobrazeni zebricku");
            Console.WriteLine(ZebricekTable.Select(idz).zebricek_id + " " + ZebricekTable.Select(idz).zapasy + " " + ZebricekTable.Select(idz).body + " " + ZebricekTable.Select(idz).skore);

            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////

            Console.WriteLine("Funkce 5.4. Vymazani zebricku");
            Console.WriteLine("Pred Updatem: " + ZebricekTable.Select(idz).zebricek_id + " " + ZebricekTable.Select(idz).smazano);
            ZebricekTable.Smazat(zebb);
            Console.WriteLine("Po Updatu: " + ZebricekTable.Select(idz).zebricek_id + " " + ZebricekTable.Select(idz).smazano);

            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////

            Console.WriteLine("Funkce 5.5. Pripsani bodu po zapase");
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////

            //6. Zapasy//

            Console.WriteLine("Funkce 6.1. Vlozeni zapasu");
            var za = new Zapas()
            {
                goly_tym1 = 4,
                goly_tym2 = 3,
                datum = new DateTime(2020, 1, 1),
                vyherce_id = 1,
                smazano = false
            };

            int zaz = ZapasTable.Insert(za);
            
            
            //napojení zápasu a týmů hrající daný zápas podle m:n relation

            var tz = new Tymy_Zapasy()
            {
                tym_id = idt,
                zapas_id = zaz
            };
            Tymy_ZapasyTable.Insert(tz);

            var tz2 = new Tymy_Zapasy()
            {
                tym_id = idt2,
                zapas_id = zaz
            };
            Tymy_ZapasyTable.Insert(tz2);

            Console.WriteLine("Vlozene id: " + zaz);
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////
            
            Console.WriteLine("Funkce 6.3. Seznam zapasu v intervalu");
            var zapasyUdaje2 = Tymy_ZapasyTable.SelectInterval(new DateTime(2019, 1, 1), new DateTime(2021, 1, 1));
            foreach (var zapasUdaj in zapasyUdaje2)
            {
                Console.WriteLine($"{zapasUdaj.Tym1.tym_nazev} {zapasUdaj.GolyTym1}:{zapasUdaj.GolyTym2} {zapasUdaj.Tym2.tym_nazev}");
            }
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////

            Console.WriteLine("Funkce 6.4. Seznam zapasu podle nazvu");
            var zapasyUdaje = Tymy_ZapasyTable.SelectNazev("Vitkovice A - Tym");
            
            foreach (var zapasUdaj in zapasyUdaje)
            {
                Console.WriteLine($"{zapasUdaj.Tym1.tym_nazev} {zapasUdaj.GolyTym1}:{zapasUdaj.GolyTym2} {zapasUdaj.Tym2.tym_nazev}");
            }
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////
            
            var zaza = ZapasTable.Select(zaz);
            Console.WriteLine("Funkce 6.5. Vymazani zapasu");
            Console.WriteLine("Pred: " + ZapasTable.Select(zaz).zapas_id + " " + ZapasTable.Select(zaz).smazano);
            ZapasTable.Smazat(zaza);
            Console.WriteLine("Po: " + ZapasTable.Select(zaz).zapas_id + " " + ZapasTable.Select(zaz).smazano);
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////

            //7. Sponzor//

            Console.WriteLine("Funkce 7.1. Vlozeni Sponzora");
            var p = new Sponzor()
            {
                sponzor_nazev = "Vitkovice Steel",
                castka = 100000,
                klub_id = KlubTable.Select(id).klub_id,
                smazano = false
            };
           
            int idp = SponzorTable.Insert(p);
            Console.WriteLine("Vlozene id: " + idp);

            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////
            ///
            Console.WriteLine("Funkce 1.5. Pripsani financnich prostredku");
            Console.WriteLine("Pred Updatem: " + KlubTable.Select(id).klub_id + " " + KlubTable.Select(id).sponzoring);
            Console.WriteLine(KlubTable.Sponzoring(id));
            Console.WriteLine("Po Updatu: " + KlubTable.Select(id).klub_id + " " + KlubTable.Select(id).sponzoring);

            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////
            ///
            Console.WriteLine("Funkce 3.5. Koupe hrace proběhne");
            Console.WriteLine("Pred Updatem kupujici klub id, sponzoring: " + KlubTable.Select(id).klub_id + " " + KlubTable.Select(id).sponzoring);
            Console.WriteLine("Pred Updatem prodavajici klub id, sponzoring: " + KlubTable.Select(id2).klub_id + " " + KlubTable.Select(id2).sponzoring);
            Console.WriteLine("Pred Updatem Hrac hrac_id, tym_id: " + HracTable.Select(idh2).hrac_id + " " + HracTable.Select(idh2).tym_id);

            HracTable.Koupit(idh2, idt, id);

            Console.WriteLine("Po Updatu kupujici klub id, sponzoring: " + KlubTable.Select(id).klub_id + " " + KlubTable.Select(id).sponzoring);
            Console.WriteLine("Po Updatu prodavajici klub id, sponzoring: " + KlubTable.Select(id2).klub_id + " " + KlubTable.Select(id2).sponzoring);
            Console.WriteLine("Po Updatu Hrac hrac_id, tym_id: " + HracTable.Select(idh2).hrac_id + " " + HracTable.Select(idh2).tym_id);
            
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////
            ///
            Console.WriteLine("Funkce 3.6. Vymena Hracu");
            Console.WriteLine("Pred hrac_id, tym_id " + HracTable.Select(idh3).hrac_id + " " + HracTable.Select(idh3).tym_id);
            Console.WriteLine("Pred hrac_id, tym_id " + HracTable.Select(idh4).hrac_id + " " + HracTable.Select(idh4).tym_id);
            HracTable.Vymenit(idh3, idh4);
            Console.WriteLine("Po hrac_id, tym_id " + HracTable.Select(idh3).hrac_id + " " + HracTable.Select(idh3).tym_id);
            Console.WriteLine("Po hrac_id, tym_id " + HracTable.Select(idh4).hrac_id + " " + HracTable.Select(idh4).tym_id);

            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////

            Console.WriteLine("Funkce 2.5. Koupe tymu");
            Console.WriteLine("Pred Updatem kupujici klub id, sponzoring: " + KlubTable.Select(id).klub_id + " " + KlubTable.Select(id).sponzoring);
            Console.WriteLine("Pred Updatem prodavajici klub id, sponzoring: " + KlubTable.Select(id2).klub_id + " " + KlubTable.Select(id2).sponzoring);
            Console.WriteLine("Pred Updatem Tym tym_id, klub_id: " + TymTable.Select(idt2).tym_id + " " + TymTable.Select(idt2).klub_id);

            TymTable.Koupit(idt2, id);

            Console.WriteLine("Po Updatu kupujici klub id, sponzoring: " + KlubTable.Select(id).klub_id + " " + KlubTable.Select(id).sponzoring);
            Console.WriteLine("Po Updatu prodavajici klub id, sponzoring: " + KlubTable.Select(id2).klub_id + " " + KlubTable.Select(id2).sponzoring);
            Console.WriteLine("Po Updatu Tym tym_id, klub_id: " + TymTable.Select(idt2).tym_id + " " + TymTable.Select(idt2).klub_id);
           
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////
            ///
            
            ///
            Console.WriteLine("Funkce 7.3. Seznam Sponzoru");
            Console.WriteLine("Celkem sponzoru: " + SponzorTable.SelectAll().Count);
            
            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////
           
            var pp = SponzorTable.Select(idp);
            Console.WriteLine("Funkce 7.4. Zruseni Sponzora");
            Console.WriteLine("Pred Updatem: " + SponzorTable.Select(idp).sponzor_id + " " + SponzorTable.Select(idp).smazano);
            SponzorTable.Smazat(pp);
            Console.WriteLine("Po Updatu: " + SponzorTable.Select(idp).sponzor_id + " " + SponzorTable.Select(idp).smazano);

            //////////////////////////////////////////////////////
            Console.WriteLine(" ");
            //////////////////////////////////////////////////////
            ///
            

        }
    }
}
