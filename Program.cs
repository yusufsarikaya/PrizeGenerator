using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace PrizeGenerator
{
    class MainClass
    {
        public static int BarcodeNumbers;
        public static int BarcodeSubListCount;
        public static int AtaPrize;
        public static int USBPrize;
        public static int PWBPrize;
        public static int AtaPrize2;
        public static int USBPrize2;
        public static int PWBPrize2;

        public static Dictionary<int, string> BarcodePrizeDict = new Dictionary<int, string>();

        public static void Main(string[] args)
        {
            Console.WriteLine("Lütfen Barkod Sayısını Giriniz...");
            BarcodeNumbers = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Aşağıda girilecek Hediyeler ilk kaç Barkoda Verilecek Giriniz...");
            BarcodeSubListCount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Atatürk Hologram Hediye Adedini Giriniz...");
            AtaPrize = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("USB Bellek Hediye Adedini Giriniz...");
            USBPrize = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("PowerBank Hediye Adedini Giriniz...");
            PWBPrize = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Aşağıda girilecek Hediyeler Geriye kalan Barkodlara dağıtılacak...\n" +
                              "Atatürk Hologram Hediye Adedini Giriniz...");
            AtaPrize2 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("USB Bellek Hediye Adedini Giriniz...");
            USBPrize2 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("PowerBank Hediye Adedini Giriniz...");
            PWBPrize2 = Convert.ToInt32(Console.ReadLine());

            //Create Barcode List to select Random and Delete it
            var BarcodeList = Enumerable.Range(1, BarcodeNumbers).ToList();
            var BarcodeFirstSubList = BarcodeList.Take(BarcodeSubListCount).ToList();
            var BarcodeRemainingSubList = BarcodeList.Skip(BarcodeSubListCount).Take(BarcodeList.Count).ToList();

            //Create Pair the key and Value in the Dictionary
            for (int i = 1; i <= BarcodeNumbers; i++)
            {
                BarcodePrizeDict.Add(i, "NONE");
            }

            //Select Random Barcode from List and Update the Dictionary Value based on Key(Barcode)
            Random rnd = new Random();
            ChooseWinnerForList(BarcodeFirstSubList, AtaPrize, USBPrize, PWBPrize, rnd);
            ChooseWinnerForList(BarcodeRemainingSubList, AtaPrize2, USBPrize2, PWBPrize2, rnd);

            //Write the Result to a json
            string jsonStr = JsonConvert.SerializeObject(BarcodePrizeDict, Formatting.Indented);
            //write string to file
            var pathOfFile = @"/Users/yusufsarikaya/Documents/repos/PrizeGenerator/PrizeGenerator/WinnerList.json";
            File.WriteAllText(pathOfFile, jsonStr);
            Console.WriteLine("Hediye listesi Hazırlanmıştır.");

        }

        public static void ChooseWinnerForList(List<int> BarcodeList, int AtaPrizeC, int USBPrizeC, int PWBPrizeC, Random rnd)
        {
            //todo: Linq olarak düzeltilebilir.
            //var AtaPrizeIndexs = BarcodeList.OrderBy(x => rnd.Next()).Take(AtaPrize);
            //BarcodeList.RemoveAll(t => AtaPrizeIndexs.Contains(BarcodeList.IndexOf(t)));
            int PrizeIndex;
            for (int i = 1; i <= AtaPrizeC; i++)
            {
                PrizeIndex = BarcodeList.OrderBy(x => rnd.Next()).ToList()[0];
                BarcodePrizeDict[PrizeIndex] = "Atatürk Hologram";
                BarcodeList.RemoveAt(BarcodeList.IndexOf(PrizeIndex));
            }
            for (int i = 1; i <= USBPrizeC; i++)
            {
                PrizeIndex = BarcodeList.OrderBy(x => rnd.Next()).ToList()[0];
                BarcodePrizeDict[PrizeIndex] = "USB Bellek";
                BarcodeList.RemoveAt(BarcodeList.IndexOf(PrizeIndex));
            }
            for (int i = 1; i <= PWBPrizeC; i++)
            {
                PrizeIndex = BarcodeList.OrderBy(x => rnd.Next()).ToList()[0];
                BarcodePrizeDict[PrizeIndex] = "PowerBank";
                BarcodeList.RemoveAt(BarcodeList.IndexOf(PrizeIndex));
            }
        }
    }
}
