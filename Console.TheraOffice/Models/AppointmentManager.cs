namespace Assignment1.Models;

public class AppointmentManager
{
    private List<Appointment> appointments = new();

    public void ScheduleAppointment(
        Patient patient,
        Physician physician,
        DateTime startTime,
        TimeSpan duration
    )
    {
        foreach (var appt in appointments)
        {
            if (appt.GetPhysician() == physician)
            {
                var apptStart = appt.getStartTime();
                var apptEnd = apptStart + appt.getDuration();
                var newEnd = startTime + duration;

                if (startTime < apptEnd && newEnd > apptStart)
                {
                    throw new Exception("Physician is already booked in that time.");
                }
            }
        }
        appointments.Add(new Appointment(patient, physician, startTime, duration));
    }

    public List<Appointment> getAppointments()
    {
        return appointments;
    }
}
