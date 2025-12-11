namespace Assignment1.Models;

public class Patient
{
    private string? name;
    private string? address;
    private DateTime birthdate;
    private string? race;
    private string? gender;
    private List<string>? diagnoses;
    private List<string>? prescriptions;

    public string? getName()
    {
        return name;
    }

    public string? getAddress()
    {
        return address;
    }

    public DateTime getBirthdate()
    {
        return birthdate;
    }

    public string? getRace()
    {
        return race;
    }

    public string? getGender()
    {
        return gender;
    }

    public List<string>? getDiagnoses()
    {
        return diagnoses;
    }

    public List<string>? getPrescriptions()
    {
        return prescriptions;
    }

    public void setName(string? name)
    {
        this.name = name;
    }

    public void setAddress(string? address)
    {
        this.address = address;
    }

    public void setBirthdate(DateTime birthdate)
    {
        this.birthdate = birthdate;
    }

    public void setRace(string? race)
    {
        this.race = race;
    }

    public void setGender(string? gender)
    {
        this.gender = gender;
    }

    public void setDiagnoses(List<string>? diagnoses)
    {
        this.diagnoses = diagnoses;
    }

    public void setPrescriptions(List<string>? prescriptions)
    {
        this.prescriptions = prescriptions;
    }
}
