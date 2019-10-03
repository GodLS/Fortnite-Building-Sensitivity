using System;
using System.Net;
using System.IO;

namespace Fortnite_Building_Sensitivity
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[>] Welcome to the Fortnite Build Sensitivity Generator");
            System.Threading.Thread.Sleep(50);
            Console.WriteLine("[>] Created by 60ms Sean");
            System.Threading.Thread.Sleep(50);
            Console.WriteLine("[>] @60msSean | TTV: SeanPlusGames\n");

            Console.WriteLine("[>] ---------------\n");
            Console.WriteLine("[>] Please type all key names you would like to use to enter BUILD sensitivity, \nseparated by commas. ex: LShift, XButton2, Q, V then press ENTER.");
            Console.WriteLine("[>] Refer to https://www.autohotkey.com/docs/KeyList.htm for proper key names");

            var buildkeys = Console.ReadLine();
            string[] buildkeyslist = buildkeys.Split(',');
            for (int i = 0; i < buildkeyslist.Length; i++)
                buildkeyslist[i] = buildkeyslist[i].Trim();

            Console.WriteLine("[>] Please type all key names you would like to use to enter WEAPON sensitivity, \nseparated by commas. ex: 1, 2, 3, 4 then press ENTER.");


            var weaponkeys = Console.ReadLine();
            string[] weaponkeyslist = weaponkeys.Split(',');
            for (int i = 0; i < weaponkeyslist.Length; i++)
                weaponkeyslist[i] = weaponkeyslist[i].Trim();

            Console.WriteLine("[>] Please type multiplier for build sensitivity. ex: 2.0");
            var buildmultiplier = Console.ReadLine();


            Console.WriteLine("[>] ---------------\n");

            Console.WriteLine("[>] Generating files in current directory..\n");

            string fileName = @"\FortniteBuildSens.ahk";

            WebClient client = new WebClient();
            string Script = client.DownloadString("https://hastebin.com/raw/capuhewago");
            System.IO.File.WriteAllText(Directory.GetCurrentDirectory() + @"\temp", Script);

            string Library = client.DownloadString("https://hastebin.com/raw/paxemaxaje");
            System.IO.File.WriteAllText(Directory.GetCurrentDirectory() + @"\MouseDelta.ahk", Library);

            string line = null;
            int line_number = 0;

            using (StreamReader reader = new StreamReader(Directory.GetCurrentDirectory() + @"\temp"))
            {
                using (StreamWriter writer = new StreamWriter(Directory.GetCurrentDirectory() + fileName))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        line_number++;

                        writer.WriteLine(line);

                        if (line_number == 9)
                        {
                            for (int i = 0; i < buildkeyslist.Length; i++)
                            {
                                writer.WriteLine("BuildKey" + i + " := \"~" + buildkeyslist[i] + "\"");
                            }
                        }

                        if (line_number == 13)
                        {
                            for (int i = 0; i < weaponkeyslist.Length; i++)
                            {
                                writer.WriteLine("WeaponKey" + i + " := \"~" + weaponkeyslist[i] + "\"");
                            }
                        }

                        if (line_number == 16)
                        {
                            writer.WriteLine("ScaleFactor := " + buildmultiplier + " ;The amount to multiply movement by when in Build Mode");

                        }

                        if (line_number == 31)
                        {
                            for (int i = 0; i < buildkeyslist.Length; i++)
                            {
                                writer.WriteLine("hotkey, % BuildKey" + i + ", BuildStart");
                            }
                        }

                        if (line_number == 36)
                        {
                            for (int i = 0; i < weaponkeyslist.Length; i++)
                            {
                                writer.WriteLine("hotkey, % WeaponKey" + i + ", BuildStop");
                            }
                        }
                    }

                }
            }

            if (File.Exists(Directory.GetCurrentDirectory() + @"\temp"))
                File.Delete(Directory.GetCurrentDirectory() + @"\temp");


            Console.WriteLine("[>] Complete. Run FortniteBuildSens.ahk as ADMIN when Fortnite is open. Have fun!");
            Console.Read();

        }
    }
}


