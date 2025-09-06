using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PusulaTalentAcademySolutions
{
    public class LongestVowelSubsequenceAsJson
    {
        /// <summary>
        /// Bir kelime listesindeki her kelime için en uzun ardışık ünlü harf dizisini ve uzunluğunu JSON formatında döner.
        /// Liste boş veya null ise boş JSON dizisi döner.
        /// Liste dolu ise her kelime için en uzun ardışık ünlü harf dizisini ve uzunluğunu JSON formatında döner.
        public static string LongestVowelSubsequenceAsJSON(List<string> words)
        {
            // Girdi boş veya null ise boş JSON dizisi döner
            if (words == null || words.Count == 0)
            {
                return "[]";
            }
            else 
            {
                // Ünlü harfleri içeren bir HashSet oluştur
                HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };
                var results = new List<object>();

                // Her kelime için en uzun ardışık ünlü harf dizisini ve uzunluğunu bul
                foreach (string word in words)
                {
                    // Geçici ve en uzun ünlü harf dizilerini tutacak değişkenler
                    string currentSeq = "";
                    string longestSeq = "";

                    // Kelimenin her karakterini dolaş
                    foreach (char c in word)
                    {
                        // Eğer karakter ünlü harf ise currentSeq'e ekle ve en uzun diziyi güncelle
                        if (vowels.Contains(c))
                        {
                            currentSeq += c;
                            if (currentSeq.Length > longestSeq.Length)
                            {
                                longestSeq = currentSeq;
                            }
                        }
                        // Eğer karakter ünlü harf değil ise currentSeq'i sıfırla
                        else
                        {
                            currentSeq = "";
                        }
                    }

                    // Sonucu listeye ekle
                    results.Add(new
                    {
                        word = word,
                        sequence = longestSeq,
                        length = longestSeq.Length
                    });
                }

                // Sonucu JSON formatında döner
                return JsonSerializer.Serialize(results);
            }
        }

    }
}
