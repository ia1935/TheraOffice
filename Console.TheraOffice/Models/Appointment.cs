namespace Assignment1.Models;

public class Appointment
{
    private Patient patient;
    private Physician physician;

    private DateTime startTime;
    private TimeSpan duration;

    public Appointment(Patient patient, Physician physician, DateTime startTime, TimeSpan duration)
    {
        if (!isWithinBusinessHours(startTime, duration))
        {
            throw new Exception(
                "Appointment must be within business hours, from 8AM - 5PM Monday-Friday"
            );
        }

        this.patient = patient;
        this.physician = physician;
        this.startTime = startTime;
        this.duration = duration;
    }

    public Patient getPatient()
    {
        return patient;
    }

    public Physician GetPhysician()
    {
        return physician;
    }

    public DateTime getStartTime()
    {
        return startTime;
    }

    public TimeSpan getDuration()
    {
        return duration;
    }

    private bool isWithinBusinessHours(DateTime start, TimeSpan length)
    {
        if (start.DayOfWeek == DayOfWeek.Saturday || start.DayOfWeek == DayOfWeek.Sunday)
        {
            return false;
        }

        TimeSpan open = new TimeSpan(8, 0, 0);
        TimeSpan close = new TimeSpan(17, 0, 0);

        TimeSpan startOfDay = start.TimeOfDay;
        TimeSpan endOfDay = start.TimeOfDay + length;

        return startOfDay >= open && endOfDay <= close;
    }
}
