using System;
using ModelLayer;
using ModelLayer.Entity;


using BusinessLayer.Services;
using ApplicationLayer.Interface;




namespace ApplicationLayer
{
    public class MainMenu : IMainMenu
    {
        private readonly IQuantityMeasurementService _service;

        public MainMenu(IQuantityMeasurementService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public void ShowMainMenu()
        {
            bool continueProgram = true;


            while (continueProgram)
            {
                Console.WriteLine("===========================================");
                Console.WriteLine("       QUANTITY MEASUREMENT SYSTEM        ");
                Console.WriteLine("===========================================");
                Console.WriteLine("1. Length Measurement");
                Console.WriteLine("2. Volume Measurement");
                Console.WriteLine("3. Weight Measurement");
                Console.WriteLine("4. Temperature Measurement");
                Console.WriteLine("5. Exit");
                Console.WriteLine("===========================================");
                Console.Write("Select measurement type (1-5): ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        ShowLengthMenu();
                        break;
                    case "2":
                        ShowVolumeMenu();
                        break;
                    case "3":
                        ShowWeightMenu();
                        break;
                    case "4":
                        ShowTemperatureMenu();
                        break;
                    case "5":
                        continueProgram = false;
                        Console.WriteLine("Thank you for using Quantity Measurement System. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                if (continueProgram)
                {
                    Console.WriteLine();
                }
            }
        }

        private void ShowLengthMenu()
        {
            Console.WriteLine("\n===========================================");
            Console.WriteLine("         LENGTH MEASUREMENT MENU          ");
            Console.WriteLine("===========================================");
            Console.WriteLine("1. Convert Length");
            Console.WriteLine("2. Add Lengths");
            Console.WriteLine("3. Subtract Lengths");
            Console.WriteLine("4. Compare Equality");
            Console.WriteLine("5. Back to Main Menu");
            Console.WriteLine("===========================================");
            Console.Write("Select operation (1-5): ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    PerformLengthConversion();
                    break;
                case "2":
                    PerformLengthAddition();
                    break;
                case "3":
                    PerformLengthSubtraction();
                    break;
                case "4":
                    PerformLengthEquality();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        private void ShowVolumeMenu()
        {
            Console.WriteLine("\n===========================================");
            Console.WriteLine("         VOLUME MEASUREMENT MENU          ");
            Console.WriteLine("===========================================");
            Console.WriteLine("1. Convert Volume");
            Console.WriteLine("2. Add Volumes");
            Console.WriteLine("3. Subtract Volumes");
            Console.WriteLine("4. Compare Equality");
            Console.WriteLine("5. Back to Main Menu");
            Console.WriteLine("===========================================");
            Console.Write("Select operation (1-5): ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    PerformVolumeConversion();
                    break;
                case "2":
                    PerformVolumeAddition();
                    break;
                case "3":
                    PerformVolumeSubtraction();
                    break;
                case "4":
                    PerformVolumeEquality();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        private void ShowWeightMenu()
        {
            Console.WriteLine("\n===========================================");
            Console.WriteLine("         WEIGHT MEASUREMENT MENU          ");
            Console.WriteLine("===========================================");
            Console.WriteLine("1. Convert Weight");
            Console.WriteLine("2. Add Weights");
            Console.WriteLine("3. Subtract Weights");
            Console.WriteLine("4. Compare Equality");
            Console.WriteLine("5. Back to Main Menu");
            Console.WriteLine("===========================================");
            Console.Write("Select operation (1-5): ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    PerformWeightConversion();
                    break;
                case "2":
                    PerformWeightAddition();
                    break;
                case "3":
                    PerformWeightSubtraction();
                    break;
                case "4":
                    PerformWeightEquality();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        private void ShowTemperatureMenu()
        {
            Console.WriteLine("\n===========================================");
            Console.WriteLine("       TEMPERATURE MEASUREMENT MENU        ");
            Console.WriteLine("===========================================");
            Console.WriteLine("1. Convert Temperature");
            Console.WriteLine("2. Compare Equality");
            Console.WriteLine("3. Back to Main Menu");
            Console.WriteLine("===========================================");
            Console.Write("Select operation (1-3): ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    PerformTemperatureConversion();
                    break;
                case "2":
                    PerformTemperatureEquality();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        // ================= LENGTH OPERATIONS =================

        private void PerformLengthConversion()
        {
            Console.WriteLine("\n--- Length Conversion ---");
            Console.WriteLine("Available units: 1. Feet, 2. Inches, 3. Yards, 4. Centimeters");
            
            double value = GetDoubleInput("Enter value: ");
            int unitChoice1 = GetIntInput("Enter source unit (1-4): ");
            int unitChoice2 = GetIntInput("Enter target unit (1-4): ");

LengthUnit? sourceUnit = GetLengthUnit(unitChoice1) ?? LengthUnit.Feet;
            LengthUnit targetUnit = GetLengthUnit(unitChoice2);

            Quantity<LengthUnit> quantity = new Quantity<LengthUnit>(value, sourceUnit);
            Quantity<LengthUnit> converted = _service.ConvertLength(quantity, targetUnit);


            Console.WriteLine($"\nResult: {value} {sourceUnit} = {converted.Value} {targetUnit}");
        }

        private void PerformLengthAddition()
        {
            Console.WriteLine("\n--- Length Addition ---");
            Console.WriteLine("Available units: 1. Feet, 2. Inches, 3. Yards, 4. Centimeters");

            double value1 = GetDoubleInput("Enter first value: ");
            int unitChoice1 = GetIntInput("Enter first unit (1-4): ");
            double value2 = GetDoubleInput("Enter second value: ");
            int unitChoice2 = GetIntInput("Enter second unit (1-4): ");
            int targetChoice = GetIntInput("Enter target unit for result (1-4): ");

            LengthUnit unit1 = GetLengthUnit(unitChoice1);
            LengthUnit unit2 = GetLengthUnit(unitChoice2);
            LengthUnit targetUnit = GetLengthUnit(targetChoice);

            Quantity<LengthUnit> q1 = new Quantity<LengthUnit>(value1, unit1);
            Quantity<LengthUnit> q2 = new Quantity<LengthUnit>(value2, unit2);

            Quantity<LengthUnit> result = _service.AddLengths(q1, q2, targetUnit);


            Console.WriteLine($"\nResult: {value1} {unit1} + {value2} {unit2} = {result.Value} {targetUnit}");
        }

        private void PerformLengthSubtraction()
        {
            Console.WriteLine("\n--- Length Subtraction ---");
            Console.WriteLine("Available units: 1. Feet, 2. Inches, 3. Yards, 4. Centimeters");

            double value1 = GetDoubleInput("Enter first value: ");
            int unitChoice1 = GetIntInput("Enter first unit (1-4): ");
            double value2 = GetDoubleInput("Enter second value: ");
            int unitChoice2 = GetIntInput("Enter second unit (1-4): ");
            int targetChoice = GetIntInput("Enter target unit for result (1-4): ");

            LengthUnit unit1 = GetLengthUnit(unitChoice1);
            LengthUnit unit2 = GetLengthUnit(unitChoice2);
            LengthUnit targetUnit = GetLengthUnit(targetChoice);

            Quantity<LengthUnit> q1 = new Quantity<LengthUnit>(value1, unit1);
            Quantity<LengthUnit> q2 = new Quantity<LengthUnit>(value2, unit2);

            Quantity<LengthUnit> result = _service.SubtractLengths(q1, q2, targetUnit);


            Console.WriteLine($"\nResult: {value1} {unit1} - {value2} {unit2} = {result.Value} {targetUnit}");
        }

        private void PerformLengthEquality()
        {
            Console.WriteLine("\n--- Length Equality Comparison ---");
            Console.WriteLine("Available units: 1. Feet, 2. Inches, 3. Yards, 4. Centimeters");

            double value1 = GetDoubleInput("Enter first value: ");
            int unitChoice1 = GetIntInput("Enter first unit (1-4): ");
            double value2 = GetDoubleInput("Enter second value: ");
            int unitChoice2 = GetIntInput("Enter second unit (1-4): ");

            LengthUnit unit1 = GetLengthUnit(unitChoice1);
            LengthUnit unit2 = GetLengthUnit(unitChoice2);

            Quantity<LengthUnit> q1 = new Quantity<LengthUnit>(value1, unit1);
            Quantity<LengthUnit> q2 = new Quantity<LengthUnit>(value2, unit2);

            bool equal = q1.Equals(q2);

            Console.WriteLine($"\nResult: {value1} {unit1} == {value2} {unit2} : {equal}");
        }

        // ================= VOLUME OPERATIONS =================

        private void PerformVolumeConversion()
{
    Console.WriteLine("\n--- Volume Conversion ---");
    Console.WriteLine("Available units: 1. Litre, 2. Millilitre, 3. Gallon");

    double value = GetDoubleInput("Enter value: ");
    int unitChoice1 = GetIntInput("Enter source unit (1-3): ");
    int unitChoice2 = GetIntInput("Enter target unit (1-3): ");

    VolumeUnit sourceUnit = GetVolumeUnit(unitChoice1);
    VolumeUnit targetUnit = GetVolumeUnit(unitChoice2);

    Quantity<VolumeUnit> quantity = new Quantity<VolumeUnit>(value, sourceUnit);

    Quantity<VolumeUnit> converted = _service.ConvertVolume(quantity, targetUnit);

    Console.WriteLine($"\nResult: {value} {sourceUnit} = {converted.Value} {targetUnit}");
}

        private void PerformVolumeAddition()
{
    Console.WriteLine("\n--- Volume Addition ---");
    Console.WriteLine("Available units: 1. Litre, 2. Millilitre, 3. Gallon");

    double value1 = GetDoubleInput("Enter first value: ");
    int unitChoice1 = GetIntInput("Enter first unit (1-3): ");

    double value2 = GetDoubleInput("Enter second value: ");
    int unitChoice2 = GetIntInput("Enter second unit (1-3): ");

    int targetChoice = GetIntInput("Enter target unit (1-3): ");

    VolumeUnit unit1 = GetVolumeUnit(unitChoice1);
    VolumeUnit unit2 = GetVolumeUnit(unitChoice2);
    VolumeUnit targetUnit = GetVolumeUnit(targetChoice);

    Quantity<VolumeUnit> q1 = new Quantity<VolumeUnit>(value1, unit1);
    Quantity<VolumeUnit> q2 = new Quantity<VolumeUnit>(value2, unit2);

    Quantity<VolumeUnit> result = _service.AddVolumes(q1, q2, targetUnit);

    Console.WriteLine($"\nResult: {result.Value} {targetUnit}");
}

       private void PerformVolumeSubtraction()
{
    Console.WriteLine("\n--- Volume Subtraction ---");
    Console.WriteLine("Available units: 1. Litre, 2. Millilitre, 3. Gallon");

    double value1 = GetDoubleInput("Enter first value: ");
    int unitChoice1 = GetIntInput("Enter first unit (1-3): ");

    double value2 = GetDoubleInput("Enter second value: ");
    int unitChoice2 = GetIntInput("Enter second unit (1-3): ");

    int targetChoice = GetIntInput("Enter target unit (1-3): ");

    VolumeUnit unit1 = GetVolumeUnit(unitChoice1);
    VolumeUnit unit2 = GetVolumeUnit(unitChoice2);
    VolumeUnit targetUnit = GetVolumeUnit(targetChoice);

    Quantity<VolumeUnit> q1 = new Quantity<VolumeUnit>(value1, unit1);
    Quantity<VolumeUnit> q2 = new Quantity<VolumeUnit>(value2, unit2);

    Quantity<VolumeUnit> result = _service.SubtractVolumes(q1, q2, targetUnit);

    Console.WriteLine($"\nResult: {result.Value} {targetUnit}");
}

        private void PerformVolumeEquality()
        {
            Console.WriteLine("\n--- Volume Equality Comparison ---");
            Console.WriteLine("Available units: 1. Litre, 2. Millilitre, 3. Gallon");

            double value1 = GetDoubleInput("Enter first value: ");
            int unitChoice1 = GetIntInput("Enter first unit (1-3): ");
            double value2 = GetDoubleInput("Enter second value: ");
            int unitChoice2 = GetIntInput("Enter second unit (1-3): ");

            VolumeUnit unit1 = GetVolumeUnit(unitChoice1);
            VolumeUnit unit2 = GetVolumeUnit(unitChoice2);

            Quantity<VolumeUnit> q1 = new Quantity<VolumeUnit>(value1, unit1);
            Quantity<VolumeUnit> q2 = new Quantity<VolumeUnit>(value2, unit2);

            bool equal = q1.Equals(q2);

            Console.WriteLine($"\nResult: {value1} {unit1} == {value2} {unit2} : {equal}");
        }

        // ================= WEIGHT OPERATIONS =================

       private void PerformWeightConversion()
{
    Console.WriteLine("\n--- Weight Conversion ---");
     Console.WriteLine("Available units: 1. Kilogram, 2. Gram");

    double value = GetDoubleInput("Enter value: ");
    int unitChoice1 = GetIntInput("Enter source unit (1-2): ");
    int unitChoice2 = GetIntInput("Enter target unit (1-2): ");

    WeightUnit sourceUnit = GetWeightUnit(unitChoice1);
    WeightUnit targetUnit = GetWeightUnit(unitChoice2);

    Quantity<WeightUnit> quantity = new Quantity<WeightUnit>(value, sourceUnit);

    Quantity<WeightUnit> converted = _service.ConvertWeight(quantity, targetUnit);

    Console.WriteLine($"\nResult: {converted.Value} {targetUnit}");
}

        private void PerformWeightAddition()
        {
            Console.WriteLine("\n--- Weight Addition ---");
            Console.WriteLine("Available units: 1. Kilogram, 2. Gram");

            double value1 = GetDoubleInput("Enter first value: ");
            int unitChoice1 = GetIntInput("Enter first unit (1-2): ");
            double value2 = GetDoubleInput("Enter second value: ");
            int unitChoice2 = GetIntInput("Enter second unit (1-2): ");
            int targetChoice = GetIntInput("Enter target unit for result (1-2): ");

            WeightUnit unit1 = GetWeightUnit(unitChoice1);
            WeightUnit unit2 = GetWeightUnit(unitChoice2);
            WeightUnit targetUnit = GetWeightUnit(targetChoice);

            Quantity<WeightUnit> q1 = new Quantity<WeightUnit>(value1, unit1);
            Quantity<WeightUnit> q2 = new Quantity<WeightUnit>(value2, unit2);

            Quantity<WeightUnit> result = q1.Add(q2, targetUnit);

            Console.WriteLine($"\nResult: {value1} {unit1} + {value2} {unit2} = {result.Value} {targetUnit}");
        }

        private void PerformWeightSubtraction()
        {
            Console.WriteLine("\n--- Weight Subtraction ---");
            Console.WriteLine("Available units: 1. Kilogram, 2. Gram");

            double value1 = GetDoubleInput("Enter first value: ");
            int unitChoice1 = GetIntInput("Enter first unit (1-2): ");
            double value2 = GetDoubleInput("Enter second value: ");
            int unitChoice2 = GetIntInput("Enter second unit (1-2): ");
            int targetChoice = GetIntInput("Enter target unit for result (1-2): ");

            WeightUnit unit1 = GetWeightUnit(unitChoice1);
            WeightUnit unit2 = GetWeightUnit(unitChoice2);
            WeightUnit targetUnit = GetWeightUnit(targetChoice);

            Quantity<WeightUnit> q1 = new Quantity<WeightUnit>(value1, unit1);
            Quantity<WeightUnit> q2 = new Quantity<WeightUnit>(value2, unit2);

            Quantity<WeightUnit> result = q1.Subtract(q2, targetUnit);

            Console.WriteLine($"\nResult: {value1} {unit1} - {value2} {unit2} = {result.Value} {targetUnit}");
        }

        private void PerformWeightEquality()
        {
            Console.WriteLine("\n--- Weight Equality Comparison ---");
            Console.WriteLine("Available units: 1. Kilogram, 2. Gram");

            double value1 = GetDoubleInput("Enter first value: ");
            int unitChoice1 = GetIntInput("Enter first unit (1-2): ");
            double value2 = GetDoubleInput("Enter second value: ");
            int unitChoice2 = GetIntInput("Enter second unit (1-2): ");

            WeightUnit unit1 = GetWeightUnit(unitChoice1);
            WeightUnit unit2 = GetWeightUnit(unitChoice2);

            Quantity<WeightUnit> q1 = new Quantity<WeightUnit>(value1, unit1);
            Quantity<WeightUnit> q2 = new Quantity<WeightUnit>(value2, unit2);

            bool equal = q1.Equals(q2);

            Console.WriteLine($"\nResult: {value1} {unit1} == {value2} {unit2} : {equal}");
        }

        // ================= TEMPERATURE OPERATIONS =================

        private void PerformTemperatureConversion()
{
    Console.WriteLine("\n--- Temperature Conversion ---");
     Console.WriteLine("Available units: 1. Celsius, 2. Fahrenheit, 3. Kelvin");

    double value = GetDoubleInput("Enter value: ");
    int unitChoice1 = GetIntInput("Enter source unit (1-3): ");
    int unitChoice2 = GetIntInput("Enter target unit (1-3): ");

    TemperatureUnit sourceUnit = GetTemperatureUnit(unitChoice1);
    TemperatureUnit targetUnit = GetTemperatureUnit(unitChoice2);

    Quantity<TemperatureUnit> quantity = new Quantity<TemperatureUnit>(value, sourceUnit);

    Quantity<TemperatureUnit> converted = _service.ConvertTemperature(quantity, targetUnit);

    Console.WriteLine($"\nResult: {value} {sourceUnit} = {converted.Value} {targetUnit}");
}

        private void PerformTemperatureEquality()
        {
            Console.WriteLine("\n--- Temperature Equality Comparison ---");
            Console.WriteLine("Available units: 1. Celsius, 2. Fahrenheit, 3. Kelvin");

            double value1 = GetDoubleInput("Enter first value: ");
            int unitChoice1 = GetIntInput("Enter first unit (1-3): ");
            double value2 = GetDoubleInput("Enter second value: ");
            int unitChoice2 = GetIntInput("Enter second unit (1-3): ");

            TemperatureUnit unit1 = GetTemperatureUnit(unitChoice1);
            TemperatureUnit unit2 = GetTemperatureUnit(unitChoice2);

            Quantity<TemperatureUnit> q1 = new Quantity<TemperatureUnit>(value1, unit1);
            Quantity<TemperatureUnit> q2 = new Quantity<TemperatureUnit>(value2, unit2);

            bool equal = q1.Equals(q2);

            Console.WriteLine($"\nResult: {value1} {unit1} == {value2} {unit2} : {equal}");
        }

        // ================= HELPER METHODS =================

        private double GetDoubleInput(string prompt)
        {
            Console.Write(prompt);
            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out double value))
                {
                    return value;
                }
                Console.Write("Invalid input. Please enter a valid number: ");
            }
        }

        private int GetIntInput(string prompt)
        {
            Console.Write(prompt);
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    return value;
                }
                Console.Write("Invalid input. Please enter a valid integer: ");
            }
        }

private LengthUnit GetLengthUnit(int choice)
        {
            switch (choice)
            {
                case 1: return LengthUnit.Feet;
case 2: return LengthUnit.Inches;
case 3: return LengthUnit.Yards;
case 4: return LengthUnit.Centimeters;
                default: return LengthUnit.Feet;
            }
        }

        private VolumeUnit GetVolumeUnit(int choice)
        {
            switch (choice)
            {
                case 1: return VolumeUnit.Litre;
                case 2: return VolumeUnit.Millilitre;
                case 3: return VolumeUnit.Gallon;
                default: return VolumeUnit.Litre;
            }
        }

        private WeightUnit GetWeightUnit(int choice)
        {
            switch (choice)
            {
                case 1: return WeightUnit.Kilogram;
                case 2: return WeightUnit.Gram;
                default: return WeightUnit.Kilogram;
            }
        }

        private TemperatureUnit GetTemperatureUnit(int choice)
        {
            switch (choice)
            {
                case 1: return TemperatureUnit.CELSIUS;
                case 2: return TemperatureUnit.FAHRENHEIT;
                case 3: return TemperatureUnit.KELVIN;
                default: return TemperatureUnit.CELSIUS;
            }
        }
    }
}

