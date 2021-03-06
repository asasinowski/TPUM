﻿using Data.Model;

namespace Data
{
    public class DataFactory : IDataFiller
    {
        public DataContext Fill()
        {
            DataContext data = new DataContext();

            data.customers.Add(new Customer("Arthur", "Avalon Hill", "arthur@imaking.uk", "(360) 6104514"));
            data.customers.Add(new Customer("Agravain", "57536 Heffernan Parkway", "rfiddiman1@census.gov", "(601) 2223017"));
            data.customers.Add(new Customer("Angwish", "53115 Katie Place", "sgrewar2@slashdot.org", "(171) 3708095"));
            data.customers.Add(new Customer("Aries", "8684 Park Meadow Lane", "kdottridge3@dyndns.org", "(357) 6525989"));
            data.customers.Add(new Customer("Baudwin", "98 Pond Parkway", "jfreezor4@usda.gov", "(672) 2237532"));
            data.customers.Add(new Customer("Bedivere", "6 Fremont Center", "vgaye5@ning.com", "(706) 6567075"));
            data.customers.Add(new Customer("Belieus", "993 Westend Drive", "dheymann6@amazon.co.jp", "(657) 6660260"));
            data.customers.Add(new Customer("Bors de Granis", "11 Buena Vista Point", "cboucher7@netlog.com", "(463) 1729881"));
            data.customers.Add(new Customer("Cynric", "6503 Ludington Point", "eaasaf8@soup.io", "(577) 8232569"));
            data.customers.Add(new Customer("Colgrevance", "1 Dunning Drive", "tkirman9@businessweek.com", "(173) 6527093"));
            data.customers.Add(new Customer("Dagonet", "89 Di Loreto Street", "hgivesa@cbc.ca", "(411) 7848478"));
            data.customers.Add(new Customer("Dinadan", "0113 Vermont Junction", "mbillinb@zimbio.com", "(391) 3903169"));
            data.customers.Add(new Customer("Hektor de Maris", "1554 Steensland Circle", "lfrickerc@altervista.org", "(996) 2873128"));
            data.customers.Add(new Customer("Frigues", "05630 Sundown Parkway", "aschirakd@eepurl.com", "(795) 4323037"));
            data.customers.Add(new Customer("Gaheris", "337 Lakewood Pass", "golyffe@dmoz.org", "(696) 4448452"));
            data.customers.Add(new Customer("Galahad", "82 Rowland Circle", "lnicholasf@arstechnica.com", "(924) 3023758"));
            data.customers.Add(new Customer("Gareth", "9 Golf View Pass", "kmacilurickg@mashable.com", "(181) 2565447"));
            data.customers.Add(new Customer("Gawain", "5772 Sunfield Road", "lmapplesh@domainmarket.com", "(128) 3525812"));
            data.customers.Add(new Customer("Girflet", "2 West Road", "lelsleyi@ca.gov", "(989) 4416543"));
            data.customers.Add(new Customer("Kay", "6610 Burning Wood Plaza", "ealltimesj@dell.com", "(228) 4491670"));
            data.customers.Add(new Customer("Lamorak de Galles", "1175 Fordem Way", "wlittleyk@mashable.com", "(206) 4740017"));
            data.customers.Add(new Customer("Lancelot du Lac", "37 Kennedy Point", "jminshalll@hubpages.com", "(537) 4978032"));
            data.customers.Add(new Customer("Lavaine", "81 Swallow Center", "nlucem@dropbox.com", "(904) 9289994"));
            data.customers.Add(new Customer("King Leodegrance", "900 American Ash Point", "akamalln@imageshack.us", "(243) 1689106"));
            data.customers.Add(new Customer("Lionel", "860 Killdeer Avenue", "myvenso@spotify.com", "(786) 3180512"));
            data.customers.Add(new Customer("Lucan", "718 Upham Road", "bgrinnovp@scribd.com", "(686) 7528580"));
            data.customers.Add(new Customer("Malagant", "0384 Muir Place", "lmorefieldq@sbwire.com", "(927) 2454561"));
            data.customers.Add(new Customer("Morhołt", "8 Quincy Point", "gkirlinr@yellowpages.com", "(892) 1116647"));
            data.customers.Add(new Customer("Mordred", "Camlann Place", "mordred@killer.uk", "(726) 6714464"));
            data.customers.Add(new Customer("Palomides Saracen", "0 Brickson Park Drive", "lcritchert@biglobe.ne.jp", "(951) 3362668"));
            data.customers.Add(new Customer("Pelleas", "873 Boyd Way", "dmathewsonu@icq.com", "(892) 8421344"));
            data.customers.Add(new Customer("King Pellinore", "7805 Holy Cross Park", "mhallutv@drupal.org", "(618) 8113363"));
            data.customers.Add(new Customer("Parsifal", "07 Westridge Lane", "ghurtw@java.com", "(731) 9656057"));
            data.customers.Add(new Customer("Sagramore", "834 Bunting Lane", "fcromackx@cnn.com", "(934) 5360212"));
            data.customers.Add(new Customer("Safir", "17 Waywood Park", "dbuckley@163.com", "(515) 2110508"));
            data.customers.Add(new Customer("Segwarides", "4 Logan Lane", "mmillz@deliciousdays.com", "(719) 3190499"));
            data.customers.Add(new Customer("Tor", "50 Gale Lane", "abartoshevich10@amazon.com", "(806) 3938484"));
            data.customers.Add(new Customer("Tristan", "2837 Kedzie Plaza", "lbogies11@ucsd.edu", "(354) 9374105"));
            data.customers.Add(new Customer("Nimue", "Lake Avenue", "nimue@caliburn.uk", "(151) 9016881"));
            data.customers.Add(new Customer("Ginewra", "Camelot Palace", "hlunk13@cloudflare.com", "(151) 8150600"));
            data.customers.Add(new Customer("Elaine ", "Corbenic Street ", "lbiddiss14@va.gov", "(973) 3643235"));
            data.customers.Add(new Customer("Morgana Le Fay", "Wavery Place ", "dvarney15@mit.edu", "(925) 8527446"));
            data.customers.Add(new Customer("Merlin ", "Neverland", "ryea16@fema.gov", "(926) 3852562"));
            data.customers.Add(new Customer("Uther Pendragon", "Camelot King Chamber", "yoldey17@flavors.me", "(719) 5899787"));

            data.employees.Add(new Employee("Boruta"));
            data.employees.Add(new Employee("Swaróg"));
            data.employees.Add(new Employee("Radogost"));
            data.employees.Add(new Employee("Jaga"));
            data.employees.Add(new Employee("Leszek"));

            data.pizzas.Add(new Pizza("Krowi placek", 28.23f, "sos pomidorowy, ser, oregano", "/pizzaPhotos/1.png"));
            data.pizzas.Add(new Pizza("Gruba świnka", 21.00f, "gyros wieprzowy, kabanos , podwójny ser, cebula czerwona, pieczarki", "/pizzaPhotos/2.png"));
            data.pizzas.Add(new Pizza("Tłusta krówka", 18.22f, "sos boloński (sos pomidorowy + mięso mielone), podwójny ser, cebula biała, czosnek, ogórek konserwowy", "/pizzaPhotos/3.png"));
            data.pizzas.Add(new Pizza("Egzotyczny prosiak", 26.18f, "szynka, ananas, curry", "/pizzaPhotos/5.png"));
            data.pizzas.Add(new Pizza("Płacze osioł", 27.87f, "salami, cebula biała", "/pizzaPhotos/6.png"));
            data.pizzas.Add(new Pizza("Wiejskie klimaty", 19.55f, "bekon, cebula biała", "/pizzaPhotos/5.png"));
            data.pizzas.Add(new Pizza("Osioł na Hawajach", 26.21f, "salami, ananas, cebula biała", "/pizzaPhotos/7.png"));
            data.pizzas.Add(new Pizza("Świnka tańczy i śpiewa", 31.55f, "boczek, pieczarki, cebula biała", "/pizzaPhotos/8.png"));
            data.pizzas.Add(new Pizza("Czym chata bogata", 29.30f, "szynka, kiełbasa, cebula biała, papryka świeża, kabanos", "/pizzaPhotos/9.png"));
            data.pizzas.Add(new Pizza("Pipidówka", 26.86f, "pieczarki, kabanos, groszek, ogórek kiszony", "/pizzaPhotos/10.png"));
            data.pizzas.Add(new Pizza("Kurczę pieczone", 24.21f, "gyros drobiowy, ogórek kiszony cebula biała", "/pizzaPhotos/11.png"));
            data.pizzas.Add(new Pizza("Dzika świnia", 31.74f, "boczek, gyros wieprzowy, ogórek konserwowy, cebula biała", "/pizzaPhotos/12.png"));
            data.pizzas.Add(new Pizza("Piecze tu i ówdzie", 20.33f, "salami, jalapeno (sos sambal)", "/pizzaPhotos/13.png"));
            data.pizzas.Add(new Pizza("Muchomoty", 23.36f, "ser, szynka, pieczarki", "/pizzaPhotos/14.png"));
            data.pizzas.Add(new Pizza("Pieją kury pieją", 26.97f, "gyros drobiowy, brokuły, ser typu greckiego", "/pizzaPhotos/17.png"));

            return data;
        }
    }
}
