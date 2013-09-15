using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLogin
{
    public class Options
    {
        public bool SetRealm = true,
                    SetResolution = true,
                    SetAccountName = true,
                    SetAccountList = true,
                    SetGraphicsQuality = true,
                    SetLastCharacterIndex = true;

        Account account;

        public void SetAccount(Account account)
        {
            this.account = account;
        }

        public string[] CompiledList()
        {
            List<string> list = new List<string>();

            list.Add("SET readTOS \"1\"");
            list.Add("SET readEULA \"1\"");

            if (SetRealm && account.SetRealm)
            {
                list.Add("SET realmName \"" + account.Realm + "\"");
            }

            if (SetResolution && account.Windowed)
            {
                list.Add("SET gxWindow \"1\"");
                list.Add("SET gxMaximize \"0\"");
                list.Add("SET gxResolution \"" + account.Resolution.Replace(" ", string.Empty) + "\"");
            }
            else
            {
                list.Add("SET gxWindow \"0\"");
            }

            if (SetAccountName)
            {
                list.Add("SET accountName \"" + account.Email + "\"");
            }

            if (SetAccountList && account.Multiple)
            {
                string accountList = "";
                int i = 0;
                foreach (string s in account.AccountNames)
                {
                    if (i == account.SelectedAccount)
                    {
                        accountList += "!" + s.Replace(" ", string.Empty) + "|";
                    }
                    else
                    {
                        accountList += s.Replace(" ", string.Empty) + "|";
                    }
                    i++;
                }
                list.Add("SET accountList \"" + accountList + "\"");
            }

            if (SetGraphicsQuality && account.LowDetail)
            {
                list.Add("SET hwDetect \"0\"");
                list.Add("SET graphicsQuality \"0\"");
                list.Add("SET gxApi \"D3D9\"");
            }
            else
            {
                list.Add("SET hwDetect \"1\"");
            }

            if (SetLastCharacterIndex && account.SetCharacter)
            {
                list.Add("SET lastCharacterIndex \"" + account.CharacterSlot + "\"");
            }

            return list.ToArray();
        }

        public static List<string> GetRealms()
        {
            List<string> Realms = new List<string>();
            Realms.Add("Aegwynn");
            Realms.Add("Aerie Peak");
            Realms.Add("Agamaggan");
            Realms.Add("Aggra (Português)");
            Realms.Add("Aggramar");
            Realms.Add("Ahn'Qiraj");
            Realms.Add("Akama");
            Realms.Add("Al'Akir");
            Realms.Add("Alexstrasza");
            Realms.Add("Alleria");
            Realms.Add("Alonsus");
            Realms.Add("Altar of Storms");
            Realms.Add("Alterac Mountains");
            Realms.Add("Aman'Thul");
            Realms.Add("Ambossar");
            Realms.Add("Anachronos");
            Realms.Add("Andorhal");
            Realms.Add("Anetheron");
            Realms.Add("Antonidas");
            Realms.Add("Anub'arak");
            Realms.Add("Anvilmar");
            Realms.Add("Arak-arahm");
            Realms.Add("Arathi");
            Realms.Add("Arathor");
            Realms.Add("Archimonde");
            Realms.Add("Area 52");
            Realms.Add("Argent Dawn");
            Realms.Add("Arthas");
            Realms.Add("Arygos");
            Realms.Add("Ashenvale");
            Realms.Add("Aszune");
            Realms.Add("Auchindoun");
            Realms.Add("Azgalor");
            Realms.Add("Azjol-Nerub");
            Realms.Add("Azralon");
            Realms.Add("Azshara");
            Realms.Add("Azuregos");
            Realms.Add("Azuremyst");
            Realms.Add("Baelgun");
            Realms.Add("Balnazzar");
            Realms.Add("Barthilas");
            Realms.Add("Black Dragonflight");
            Realms.Add("Blackhand");
            Realms.Add("Blackmoore");
            Realms.Add("Blackrock");
            Realms.Add("Blackscar");
            Realms.Add("Blackwater Raiders");
            Realms.Add("Blackwing Lair");
            Realms.Add("Bladefist");
            Realms.Add("Blade's Edge");
            Realms.Add("Bleeding Hollow");
            Realms.Add("Blood Furnace");
            Realms.Add("Bloodfeather");
            Realms.Add("Bloodhoof");
            Realms.Add("Bloodscalp");
            Realms.Add("Blutkessel");
            Realms.Add("Bonechewer");
            Realms.Add("Booty Bay");
            Realms.Add("Borean Tundra");
            Realms.Add("Boulderfist");
            Realms.Add("Bronze Dragonflight");
            Realms.Add("Bronzebeard");
            Realms.Add("Burning Blade");
            Realms.Add("Burning Legion");
            Realms.Add("Burning Steppes");
            Realms.Add("Caelestrasz");
            Realms.Add("Cairne");
            Realms.Add("Cenarion Circle");
            Realms.Add("Cenarius");
            Realms.Add("Chamber of Aspects");
            Realms.Add("Chants éternels");
            Realms.Add("Cho'gall");
            Realms.Add("Chromaggus");
            Realms.Add("Coilfang");
            Realms.Add("Colinas Pardas");
            Realms.Add("Confrérie du Thorium");
            Realms.Add("Conseil des Ombres");
            Realms.Add("Crushridge");
            Realms.Add("C'Thun");
            Realms.Add("Culte de la Rive noire");
            Realms.Add("Daggerspine");
            Realms.Add("Dalaran");
            Realms.Add("Dalvengyr");
            Realms.Add("Dark Iron");
            Realms.Add("Darkmoon Faire");
            Realms.Add("Darksorrow");
            Realms.Add("Darkspear");
            Realms.Add("Darrowmere");
            Realms.Add("Das Konsortium");
            Realms.Add("Das Syndikat");
            Realms.Add("Dath'Remar");
            Realms.Add("Dawnbringer");
            Realms.Add("Deathguard");
            Realms.Add("Deathweaver");
            Realms.Add("Deathwing");
            Realms.Add("Deepholm");
            Realms.Add("Defias Brotherhood");
            Realms.Add("Demon Soul");
            Realms.Add("Dentarg");
            Realms.Add("Der abyssische Rat");
            Realms.Add("Der Mithrilorden");
            Realms.Add("Der Rat von Dalaran");
            Realms.Add("Destromath");
            Realms.Add("Dethecus");
            Realms.Add("Detheroc");
            Realms.Add("Die Aldor");
            Realms.Add("Die Arguswacht");
            Realms.Add("Die ewige Wacht");
            Realms.Add("Die Nachtwache");
            Realms.Add("Die Silberne Hand");
            Realms.Add("Die Todeskrallen");
            Realms.Add("Doomhammer");
            Realms.Add("Draenor");
            Realms.Add("Dragonblight");
            Realms.Add("Dragonmaw");
            Realms.Add("Draka");
            Realms.Add("Drakkari");
            Realms.Add("Drak'Tharon");
            Realms.Add("Drak'thul");
            Realms.Add("Dreadmaul");
            Realms.Add("Drek'Thar");
            Realms.Add("Drenden");
            Realms.Add("Dun Modr");
            Realms.Add("Dun Morogh");
            Realms.Add("Dunemaul");
            Realms.Add("Durotan");
            Realms.Add("Duskwood");
            Realms.Add("Earthen Ring");
            Realms.Add("Echo Isles");
            Realms.Add("Echsenkessel");
            Realms.Add("Eitrigg");
            Realms.Add("Eldre'Thalas");
            Realms.Add("Elune");
            Realms.Add("Emerald Dream");
            Realms.Add("Emeriss");
            Realms.Add("Eonar");
            Realms.Add("Eredar");
            Realms.Add("Eversong");
            Realms.Add("Executus");
            Realms.Add("Exodar");
            Realms.Add("Farstriders");
            Realms.Add("Feathermoon");
            Realms.Add("Fenris");
            Realms.Add("Festung der Stürme");
            Realms.Add("Firetree");
            Realms.Add("Fizzcrank");
            Realms.Add("Fordragon");
            Realms.Add("Forscherliga");
            Realms.Add("Frostmane");
            Realms.Add("Frostmourne");
            Realms.Add("Frostwhisper");
            Realms.Add("Frostwolf");
            Realms.Add("Galakrond");
            Realms.Add("Gallywix");
            Realms.Add("Garithos");
            Realms.Add("Garona");
            Realms.Add("Garrosh");
            Realms.Add("Genjuros");
            Realms.Add("Ghostlands");
            Realms.Add("Gilneas");
            Realms.Add("Gnomeregan");
            Realms.Add("Goldrinn");
            Realms.Add("Gordunni");
            Realms.Add("Gorefiend");
            Realms.Add("Gorgonnash");
            Realms.Add("Greymane");
            Realms.Add("Grim Batol");
            Realms.Add("Grizzly Hills");
            Realms.Add("Grom");
            Realms.Add("Gul'dan");
            Realms.Add("Gundrak");
            Realms.Add("Gurubashi");
            Realms.Add("Hakkar");
            Realms.Add("Haomarush");
            Realms.Add("Hellfire");
            Realms.Add("Hellscream");
            Realms.Add("Howling Fjord");
            Realms.Add("Hydraxis");
            Realms.Add("Hyjal");
            Realms.Add("Icecrown");
            Realms.Add("Illidan");
            Realms.Add("Jaedenar");
            Realms.Add("Jubei'Thos");
            Realms.Add("Kael'thas");
            Realms.Add("Kalecgos");
            Realms.Add("Karazhan");
            Realms.Add("Kargath");
            Realms.Add("Kazzak");
            Realms.Add("Kel'Thuzad");
            Realms.Add("Khadgar");
            Realms.Add("Khaz Modan");
            Realms.Add("Khaz'goroth");
            Realms.Add("Kil'jaeden");
            Realms.Add("Kilrogg");
            Realms.Add("Kirin Tor");
            Realms.Add("Kor'gall");
            Realms.Add("Korgath");
            Realms.Add("Korialstrasz");
            Realms.Add("Krag'jin");
            Realms.Add("Krasus");
            Realms.Add("Kul Tiras");
            Realms.Add("Kult der Verdammten");
            Realms.Add("La Croisade écarlate");
            Realms.Add("Laughing Skull");
            Realms.Add("Les Clairvoyants");
            Realms.Add("Les Sentinelles");
            Realms.Add("Lethon");
            Realms.Add("Lich King");
            Realms.Add("Lightbringer");
            Realms.Add("Lightninghoof");
            Realms.Add("Lightning's Blade");
            Realms.Add("Llane");
            Realms.Add("Lordaeron");
            Realms.Add("Los Errantes");
            Realms.Add("Lothar");
            Realms.Add("Madmortem");
            Realms.Add("Madoran");
            Realms.Add("Maelstrom");
            Realms.Add("Magtheridon");
            Realms.Add("Maiev");
            Realms.Add("Malfurion");
            Realms.Add("Mal'Ganis");
            Realms.Add("Malorne");
            Realms.Add("Malygos");
            Realms.Add("Mannoroth");
            Realms.Add("Marécage de Zangar");
            Realms.Add("Mazrigos");
            Realms.Add("Medivh");
            Realms.Add("Minahonda");
            Realms.Add("Misha");
            Realms.Add("Mok'Nathal");
            Realms.Add("Moon Guard");
            Realms.Add("Moonglade");
            Realms.Add("Moonrunner");
            Realms.Add("Mug'thol");
            Realms.Add("Muradin");
            Realms.Add("Nagrand");
            Realms.Add("Nathrezim");
            Realms.Add("Naxxramas");
            Realms.Add("Nazgrel");
            Realms.Add("Nazjatar");
            Realms.Add("Nefarian");
            Realms.Add("Nemesis");
            Realms.Add("Neptulon");
            Realms.Add("Nera'thor");
            Realms.Add("Ner'zhul");
            Realms.Add("Nesingwary");
            Realms.Add("Nethersturm");
            Realms.Add("Nordrassil");
            Realms.Add("Norgannon");
            Realms.Add("Nozdormu");
            Realms.Add("Onyxia");
            Realms.Add("Outland");
            Realms.Add("Perenolde");
            Realms.Add("Pozzo dell'Eternità");
            Realms.Add("Proudmoore");
            Realms.Add("Quel'dorei");
            Realms.Add("Quel'Thalas");
            Realms.Add("Ragnaros");
            Realms.Add("Rajaxx");
            Realms.Add("Rashgarroth");
            Realms.Add("Ravencrest");
            Realms.Add("Ravenholdt");
            Realms.Add("Razuvious");
            Realms.Add("Rexxar");
            Realms.Add("Rivendare");
            Realms.Add("Runetotem");
            Realms.Add("Sanguino");
            Realms.Add("Sargeras");
            Realms.Add("Saurfang");
            Realms.Add("Scarlet Crusade");
            Realms.Add("Scarshield Legion");
            Realms.Add("Scilla");
            Realms.Add("Sen'jin");
            Realms.Add("Sentinels");
            Realms.Add("Shadow Council");
            Realms.Add("Shadowmoon");
            Realms.Add("Shadowsong");
            Realms.Add("Shandris");
            Realms.Add("Shattered Halls");
            Realms.Add("Shattered Hand");
            Realms.Add("Shattrath");
            Realms.Add("Shen'dralar");
            Realms.Add("Shu'halo");
            Realms.Add("Silver Hand");
            Realms.Add("Silvermoon");
            Realms.Add("Sinstralis");
            Realms.Add("Sisters of Elune");
            Realms.Add("Skullcrusher");
            Realms.Add("Skywall");
            Realms.Add("Smolderthorn");
            Realms.Add("Soulflayer");
            Realms.Add("Spinebreaker");
            Realms.Add("Spirestone");
            Realms.Add("Sporeggar");
            Realms.Add("Staghelm");
            Realms.Add("Steamwheedle Cartel");
            Realms.Add("Stonemaul");
            Realms.Add("Stormrage");
            Realms.Add("Stormreaver");
            Realms.Add("Stormscale");
            Realms.Add("Sunstrider");
            Realms.Add("Suramar");
            Realms.Add("Sylvanas");
            Realms.Add("Taerar");
            Realms.Add("Talnivarr");
            Realms.Add("Tanaris");
            Realms.Add("Tarren Mill");
            Realms.Add("Teldrassil");
            Realms.Add("Temple noir");
            Realms.Add("Terenas");
            Realms.Add("Terokkar");
            Realms.Add("Terrordar");
            Realms.Add("Thaurissan");
            Realms.Add("The Forgotten Coast");
            Realms.Add("The Maelstrom");
            Realms.Add("The Scryers");
            Realms.Add("The Sha'tar");
            Realms.Add("The Underbog");
            Realms.Add("The Venture Co");
            Realms.Add("Theradras");
            Realms.Add("Thermaplugg");
            Realms.Add("Thorium Brotherhood");
            Realms.Add("Thrall");
            Realms.Add("Throk'Feroth");
            Realms.Add("Thunderhorn");
            Realms.Add("Thunderlord");
            Realms.Add("Tichondrius");
            Realms.Add("Tirion");
            Realms.Add("Todeswache");
            Realms.Add("Tol Barad");
            Realms.Add("Tortheldrin");
            Realms.Add("Trollbane");
            Realms.Add("Turalyon");
            Realms.Add("Twilight's Hammer");
            Realms.Add("Twisting Nether");
            Realms.Add("Tyrande");
            Realms.Add("Uldaman");
            Realms.Add("Ulduar");
            Realms.Add("Uldum");
            Realms.Add("Undermine");
            Realms.Add("Un'Goro");
            Realms.Add("Ursin");
            Realms.Add("Uther");
            Realms.Add("Varimathras");
            Realms.Add("Vashj");
            Realms.Add("Vek'lor");
            Realms.Add("Vek'nilash");
            Realms.Add("Velen");
            Realms.Add("Vol'jin");
            Realms.Add("Warsong");
            Realms.Add("Whisperwind");
            Realms.Add("Wildhammer");
            Realms.Add("Windrunner");
            Realms.Add("Winterhoof");
            Realms.Add("Wrathbringer");
            Realms.Add("Wyrmrest Accord");
            Realms.Add("Xavius");
            Realms.Add("Ysera");
            Realms.Add("Ysondre");
            Realms.Add("Zangarmarsh");
            Realms.Add("Zenedar");
            Realms.Add("Zirkel des Cenarius");
            Realms.Add("Zul'jin");
            Realms.Add("Zuluhed");

            return Realms;
        }
    }
}
