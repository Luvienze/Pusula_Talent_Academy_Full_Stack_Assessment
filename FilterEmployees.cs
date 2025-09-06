using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PusulaTalentAcademySolutions
{
    public class FilterEmployees
    {
        /// <summary>
        /// Statik bir tuple üzerinde filtreleme ve hesaplama yapar.
        /// Tuple listesi null ise boş JSON dizisi döner.
        /// Tuple listesi dolu ise belirtilen kriterlere göre filtreleme ve hesaplamalar yapar.
        /// Sonuç JSON formatında döner.
        public static string FilterEmployee(IEnumerable<(string Name, int Age, string Department, decimal Salary,
            DateTime HireDate)> employees)
        {
            // Girdi boş veya null ise boş JSON dizisi döner
            if (employees == null)
            {
                return "[]";
            }
            else
            {
                // Belirtilen kriterlere göre filtreleme yap
                var filteredEmployees = employees.Where(e =>
                        (e.Age >= 25 && e.Age <= 40) &&
                        (e.Department == "IT" || e.Department == "Finance") &&
                        (e.Salary >= 5000 && e.Salary <= 9000) &&
                        e.HireDate.Year > 2017
                ).ToList();

                // Filtrelenen çalışanların isimlerini uzunluklarına göre azalan, isimlerine göre artan sırala
                var Names = filteredEmployees
                    .Select(e => e.Name)
                    .OrderByDescending(n => n.Length)
                    .ThenBy(n => n)
                    .ToList();

                // Toplam maaş, ortalama maaş, minimum maaş, maksimum maaş ve kişi sayısını hesapla
                decimal TotalSalary = filteredEmployees
                    .Count > 0 ? filteredEmployees.Sum(e => e.Salary) : 0;
                decimal AverageSalary = filteredEmployees
                    .Count > 0 ? TotalSalary / filteredEmployees.Count : 0;
                decimal MinSalary = filteredEmployees
                    .Count > 0 ? filteredEmployees.Min(e => e.Salary) : 0;
                decimal MaxSalary = filteredEmployees
                    .Count > 0 ? filteredEmployees.Max(e => e.Salary) : 0;
                int EmployeeCount = filteredEmployees.Count;

                // Sonucu JSON formatında döner
                var result = new
                {
                    Names = Names,
                    TotalSalary = TotalSalary,
                    AverageSalary = AverageSalary,
                    MinSalary = MinSalary,
                    MaxSalary = MaxSalary,
                    Count = EmployeeCount
                };
                return JsonSerializer.Serialize(result);
            }
        }
    }
}
