namespace Assignment1.Utils;

using Assignment1.Models;

public class PatientUtils
{
    public static void CreatePatient(List<Patient> patients)
    {
        Console.Write("Enter patient name: ");
        string? name = Console.ReadLine();

        Console.Write("Enter address: ");
        string? address = Console.ReadLine();

        Console.Write("Enter birthdate (yyyy-mm-dd): ");
        DateTime birthdate;
        while (!DateTime.TryParse(Console.ReadLine(), out birthdate))
        {
            Console.Write("Invalid date or format. Enter again: ");
        }

        Console.Write("Enter race: ");
        string? race = Console.ReadLine();

        Console.Write("Enter gender: ");
        string? gender = Console.ReadLine();

        Console.Write("Enter patient's diagnoses, with commas if multiple: ");
        string? diagInput = Console.ReadLine();

        List<string> diagnoses =
            diagInput
                ?.Split(',')
                .Select(d => d.Trim())
                .Where(d => !string.IsNullOrEmpty(d))
                .ToList() ?? new List<string>();

        Console.Write("Enter prescriptions, with commas if multiple: ");
        string? prescriptionInput = Console.ReadLine();

        List<string>? prescriptions =
            prescriptionInput
                ?.Split(',')
                .Select(d => d.Trim())
                .Where(d => !string.IsNullOrEmpty(d))
                .ToList() ?? new List<string>();

        var patient = new Patient();

        patient.setName(name);
        patient.setAddress(address);
        patient.setBirthdate(birthdate);
        patient.setRace(race);
        patient.setGender(gender);
        patient.setDiagnoses(diagnoses);
        patient.setPrescriptions(prescriptions);

        patients.Add(patient);
        Console.WriteLine("Patient created successfully.");
    }
    public static void ListPatients(List<Patient> patients)
        {
            Console.WriteLine("\n Patients");
            foreach (var p in patients)
            {
                Console.WriteLine($"- {p.getName()} ({p.getBirthdate():yyyy-MM-dd})");
            }
        }
}

