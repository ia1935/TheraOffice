namespace Assignment1.Models;

public class Physician
{
    private string? name;
    private string? licenseNumber;
    private DateTime? graduationDate;
    private List<string>? specializations;

    public string? getName()
    {
        return name;
    }

    public string? getLicenseNumber()
    {
        return licenseNumber;
    }

    public DateTime? getGraduationDate()
    {
        return graduationDate;
    }

    public List<string>? getSpecializations()
    {
        return specializations;
    }

    public void setName(string name)
    {
        this.name = name;
    }

    public void setLicenseNumber(string licenseNumber)
    {
        this.licenseNumber = licenseNumber;
    }

    public void setGraduationDate(DateTime graduationDate)
    {
        this.graduationDate = graduationDate;
    }

    public void setSpecializations(List<string> specializations)
    {
        this.specializations = specializations;
    }
}
