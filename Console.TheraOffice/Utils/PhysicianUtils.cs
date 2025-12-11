namespace Assignment1.Utils;

using Assignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class PhysicianUtils
{
    public static void CreatePhysician(List<Physician> physicians)
    {
        Console.Write("Enter physician name: ");
        string? name = Console.ReadLine();

        Console.Write("Enter license number: ");
        string? licenseNumber = Console.ReadLine();

        Console.Write("Enter graduation date (yyyy-mm-dd): ");
        DateTime graduationDate;
        while (!DateTime.TryParse(Console.ReadLine(), out graduationDate))
        {
            Console.Write("Invalid date or format. Enter again: ");
        }

        Console.Write("Enter specializations, with commas if multiple: ");
        string? specInput = Console.ReadLine();

        List<string> specializations =
            specInput
                ?.Split(',')
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrEmpty(s))
                .ToList() ?? new List<string>();

        var physician = new Physician();
        if (name is not null)
        {
            physician.setName(name);
        }
        if (licenseNumber is not null)
        {
            physician.setLicenseNumber(licenseNumber);
        }
        physician.setGraduationDate(graduationDate);
        physician.setSpecializations(specializations);

        physicians.Add(physician);
        Console.WriteLine("Physician created successfully.");
    }

    public static void ListPhysicians(List<Physician> physicians)
    {
        Console.WriteLine("\nPhysicians:");
        foreach (var p in physicians)
        {
            Console.WriteLine($"- Dr. {p.getName()} ({string.Join(", ", p.getSpecializations() ?? new List<string>())})");
        }
    }
}
