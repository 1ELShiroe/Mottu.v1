using Shared.Abstracts;
using Shared.Validators.Models;

namespace Shared.Models;

public class Motorcycle : Entity
{
    public int Year { get; private set; }
    public string Model { get; private set; }
    public string Plate { get; private set; }

    public Motorcycle(int year, string model, string plate)
    {
        Year = year;
        Model = model;
        Plate = plate;

        Validate(this, new MotorcycleValidator());
    }

    public static Motorcycle New(int year, string model, string plate)
        => new(year, model, plate);

    public void SetPlate(string plate) => Plate = plate;
}