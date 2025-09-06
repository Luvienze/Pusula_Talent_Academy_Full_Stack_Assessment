using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PusulaTalentAcademySolutions
{
    public class MaxIncreasingSubArrayAsJson
    {
        /// <summary>
        /// Ardışık artan en uzun alt diziyi JSON formatında döner.
        /// Girdi boş veya null ise boş JSON dizisi döner.
        /// Girdi dolu ise ardışık artans en uzun alt diziyi JSON formatında döner.
        public static string MaxIncreasingSubarrayAsJson(List<int> numbers)
        {
            if (numbers == null || numbers.Count == 0)
            {
                return "[]";
            }
            else
            {
                // En uzun ardışık artan alt diziyi ve geçici alt diziyi tutacak listeler
                List<int> maxIncreasingNumbers = new List<int>();
                List<int> currentSubarray = new List<int>();

                // En uzun alt dizinin toplamını ve geçici alt dizinin toplamını tutacak değişkenler
                int sumOfMaxIncreasingNumbers = 0;
                int sumOfCurrentSubarray = 0;

                // İlk elemanı currentSubarray'e ekle ve toplamı başlat
                currentSubarray.Add(numbers[0]);
                sumOfCurrentSubarray = numbers[0];

                // Dizi elemanlarını dolaşarak ardışık artan alt dizileri bulma
                for (int i = 0; i < numbers.Count -1; i++) {

                    // Ardışık artan ise currentSubarray'e ekle
                    if (numbers[i] == numbers[i-1] + 1)
                    {
                        currentSubarray.Add(numbers[i]);
                        sumOfCurrentSubarray += numbers[i];
                    }
                    else
                    {
                        // Ardışık artan değil ise dolaşılan listenin toplamını en uzun listenin toplamı ile karşılaştır ve gerekirse güncelle
                        if (sumOfCurrentSubarray > sumOfMaxIncreasingNumbers)
                        {
                           maxIncreasingNumbers = new List<int>(currentSubarray);
                           sumOfMaxIncreasingNumbers = sumOfCurrentSubarray;
                        }

                        // currentSubarray'i temizle ve yeni alt diziye başla
                        currentSubarray.Clear();
                        currentSubarray.Add(numbers[i]);
                        sumOfCurrentSubarray = numbers[i];
                    }
                }

                // Döngü sonrasında kalan currentSubarray'i kontrol et
                if (sumOfCurrentSubarray > sumOfMaxIncreasingNumbers)
                {
                    // Son alt diziyi en uzun liste olarak güncelle
                    maxIncreasingNumbers = new List<int>(currentSubarray);
                    sumOfMaxIncreasingNumbers = sumOfCurrentSubarray;
                }

                // Sonucu JSON formatında döndür
                return System.Text.Json.JsonSerializer.Serialize(maxIncreasingNumbers);
            }
        }
    }
}
