using Assignment1.Models;
using Assignment1.Utils;

List<Patient> patients = new();
List<Physician> physicians = new();
AppointmentManager appointmentManager = new();

while (true)
{
    Console.WriteLine("\nMedical Charting System");
    Console.WriteLine("1. Create Patient");
    Console.WriteLine("2. List Patients");
    Console.WriteLine("3. Create Physician");
    Console.WriteLine("4. List Physicians");
    Console.WriteLine("5. Schedule Appointment");
    Console.WriteLine("6. List Appointments");
    Console.WriteLine("7. Exit");
    Console.Write("Select an option: ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            PatientUtils.CreatePatient(patients);
            break;
        case "2":
            PatientUtils.ListPatients(patients);
            break;
        case "3":
            PhysicianUtils.CreatePhysician(physicians);
            break;
        case "4":
            PhysicianUtils.ListPhysicians(physicians);
            break;
        case "5":
            AppointmentUtils.CreateAppointment(patients, physicians, appointmentManager);
            break;
        case "6":
            AppointmentUtils.ListAppointments(appointmentManager);
            break;
        case "7":
            return;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
}