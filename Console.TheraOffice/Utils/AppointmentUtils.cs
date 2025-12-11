namespace Assignment1.Utils;

using Assignment1.Models;
using System;
using System.Collections.Generic;

public class AppointmentUtils
{
    public static void CreateAppointment(List<Patient> patients, List<Physician> physicians, AppointmentManager appointmentManager)
    {
        if (patients.Count == 0)
        {
            Console.WriteLine("No patients available. Please create a patient first.");
            return;
        }

        if (physicians.Count == 0)
        {
            Console.WriteLine("No physicians available. Please create a physician first.");
            return;
        }

        Console.WriteLine("Select a patient:");
        for (int i = 0; i < patients.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {patients[i].getName()}");
        }
        Console.Write("Enter patient number: ");
        int patientIndex;
        while (!int.TryParse(Console.ReadLine(), out patientIndex) || patientIndex < 1 || patientIndex > patients.Count)
        {
            Console.Write("Invalid input. Enter patient number: ");
        }
        Patient selectedPatient = patients[patientIndex - 1];

        Console.WriteLine("\nSelect a physician:");
        for (int i = 0; i < physicians.Count; i++)
        {
            Console.WriteLine($"{i + 1}. Dr. {physicians[i].getName()}");
        }
        Console.Write("Enter physician number: ");
        int physicianIndex;
        while (!int.TryParse(Console.ReadLine(), out physicianIndex) || physicianIndex < 1 || physicianIndex > physicians.Count)
        {
            Console.Write("Invalid input. Enter physician number: ");
        }
        Physician selectedPhysician = physicians[physicianIndex - 1];

        Console.Write("Enter appointment start time (yyyy-mm-dd HH:mm): ");
        DateTime startTime;
        while (!DateTime.TryParse(Console.ReadLine(), out startTime))
        {
            Console.Write("Invalid date/time format. Enter again: ");
        }

        Console.Write("Enter appointment duration in minutes: ");
        int durationMinutes;
        while (!int.TryParse(Console.ReadLine(), out durationMinutes) || durationMinutes <= 0)
        {
            Console.Write("Invalid duration. Enter again: ");
        }
        TimeSpan duration = TimeSpan.FromMinutes(durationMinutes);

        try
        {
            appointmentManager.ScheduleAppointment(selectedPatient, selectedPhysician, startTime, duration);
            Console.WriteLine("Appointment scheduled successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error scheduling appointment: {ex.Message}");
        }
    }

    public static void ListAppointments(AppointmentManager appointmentManager)
    {
        Console.WriteLine("\nAppointments:");
        var appointments = appointmentManager.getAppointments();
        if (appointments.Count == 0)
        {
            Console.WriteLine("No appointments scheduled.");
            return;
        }

        foreach (var appt in appointments)
        {
            Console.WriteLine($"- Patient: {appt.getPatient().getName()}, Physician: Dr. {appt.GetPhysician().getName()}, Time: {appt.getStartTime()} for {appt.getDuration().TotalMinutes} minutes.");
        }
    }
}
