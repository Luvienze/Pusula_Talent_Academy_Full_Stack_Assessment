using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Text.Json;
using System.Xml;

namespace PusulaTalentAcademySolutions
{
    public class FilterPeopleFromXml
    {
        /// <summary>
        /// XML formatındaki çalışan verilerini filtreler ve JSON formatında döner.
        /// Girdi boş veya null ise boş JSON dizisi döner.
        /// Girdi dolu ise önce xml formatı parse edilir, ardından belirtilen kriterlere göre filtreleme yapılır.
        /// Filtreleme sonrası isimler alfabetik olarak sıralanır ve toplam maaş, ortalama maaş, maksimum maaş ve kişi sayısı hesaplanır.
        /// Sonuç JSON formatında döner.
        public static string FilterPeopleFromXML(string xmlData)
        {
            // Girdi boş veya null ise boş JSON dizisi döner
            if (string.IsNullOrWhiteSpace(xmlData))
            {
                return "[]";
            }
            else
            {
                // XML verisini parse et ve belirtilen kriterlere göre filtreleme yap
                XDocument xDocument = XDocument.Parse(xmlData);
                var filteredPeople = xDocument.Descendants("Person")
                    .Select(p => new
                    {
                        Name = (String)p.Element("Name"),
                        Age = (int?)p.Element("Age") ?? 0,
                        Department = (String)p.Element("Department"),
                        Salary = (decimal?)p.Element("Salary") ?? 0,
                        HireDate = (DateTime?)p.Element("HireDate") ?? DateTime.MinValue
                    })
                    .Where(p =>
                    p.Age > 30 &&
                    p.Department == "IT" &&
                    p.Salary > 5000 &&
                    p.HireDate.Year < 2019
                    ).ToList();

                // Filtrelenen kişilerin isimlerini alfabetik olarak sırala
                var names = filteredPeople
                    .Select(p => p.Name)
                    .OrderBy(n => n).ToList();

                // Toplam maaş, ortalama maaş, maksimum maaş ve kişi sayısını hesapla
                decimal TotalSalary = filteredPeople
                    .Sum(p => p.Salary);

                decimal averageSalary = filteredPeople.
                    Count > 0 ? TotalSalary / filteredPeople.Count : 0;

                decimal maxSalary = filteredPeople.
                    Count > 0 ? filteredPeople.Max(p => p.Salary) : 0;

                int count = filteredPeople.Count;

                // Sonucu JSON formatında döner
                var result = new
                {
                    Names = names,
                    TotalSalary = TotalSalary,
                    AverageSalary = averageSalary,
                    MaxSalary = maxSalary,
                    Count = count
                };
                return JsonSerializer.Serialize(result);
            }
        }
    }
}
