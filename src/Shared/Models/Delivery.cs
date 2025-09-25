using Shared.Abstracts;
using Shared.Validators.Models;

namespace Shared.Models;

public class Delivery : Entity
{
    public string Name { get; private set; }
    public string Cnpj { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string CnhNumber { get; private set; }
    public string CnhType { get; private set; }
    public string? CnhImage { get; private set; }

    public Delivery(string name, string cnpj, DateTime birthDate, string cnhNumber, string cnhType, string? cnhImage)
    {
        Name = name;
        Cnpj = cnpj;
        BirthDate = birthDate;
        CnhNumber = cnhNumber;
        CnhType = cnhType;
        CnhImage = cnhImage;

        Validate(this, new DeliveryValidator());
    }

    public static Delivery New(string name, string cnpj, DateTime birthDate, string cnhNumber, string cnhType, string? cnhImage)
        => new(name, cnpj, birthDate, cnhNumber, cnhType, cnhImage);

    public void SetCnhImage(string image) => CnhImage = image;
}