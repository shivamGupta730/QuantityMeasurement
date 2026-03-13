# 📏 Quantity Measurement App 


## ✅ UC1 – Same Unit Equality (Length)
- Compare two lengths with same unit.
- Value-based equality using overridden `Equals()`.

## ✅ UC2 – Cross Unit Equality (Length)
- Compare different units (Feet ↔ Inches).
- Internally normalized to base unit (Feet).

## ✅ UC3 – Generic Length Class
- Single `Length` class.
- Introduced `LengthUnit` enum.
- Immutable design.

## ✅ UC4 – Extended Units (Length)
- Added: Feet, Inches, Yards, Centimeters.
- All units normalized to Feet internally.

## ✅ UC5 – Unit Conversion (Length)
- `Length.Convert(value, from, to)`
- Supports all unit combinations.
- Handles zero, negative, invalid values.

## ✅ UC6 – Addition (Implicit Target Unit, Length)
- `Add(Length other)`
- Result in first operand's unit.

## ✅ UC7 – Addition (Explicit Target Unit, Length)
- `Add(Length other, LengthUnit targetUnit)`

## ✅ UC8 – Refactoring (Standalone LengthUnit)
- **SRP**: Conversion logic in unit enums.
- Backward compatible.
- Scalable for other categories.

## ✅ UC9 – VolumeUnit Equality & Conversion
- Units: Litre (base), Millilitre, Gallon.
- Cross-unit equality (1L = 1000mL).
- `Quantity<VolumeUnit>.ConvertTo()`.

## ✅ UC10 – Volume Addition/Subtraction
- `Add`/`Subtract` (implicit/explicit target).
- E.g., 1 Gallon + 500 mL = ? Litre.

## ✅ UC11 – WeightUnit Full Operations
- Units: Kilogram (base), Gram.
- Equality, conversion, add, subtract.
- E.g., 1 Kg = 1000 g.

## ✅ UC12 – TemperatureUnit (Limited Arithmetic)
- Units: Celsius, Fahrenheit, Kelvin.
- Equality & conversion only (no add/sub due to non-ratio scale).
- E.g., 0°C = 32°F = 273.15K.

## ✅ UC13 – Generic Quantity<U>
- Type-safe for any `IUnit`-implementing enum.
- Immutable, validates inputs, epsilon equality.
- Base-unit normalization via dynamic dispatch.

## ✅ UC14 – Arithmetic Operation Validation
- `IUnit.supportsArithmeticOperation()` & `validateOperationSupport()`.
- Temperature throws on add/sub attempts.

## ✅ UC15 – Interactive Switch-Case Menu
- `ApplicationLayer/Menu/MainMenu`: Length/Volume/Weight/Temp submenus.
- User input for values/units/ops.
- BusinessLayer service demos.

---

## 🏗️ Architecture Overview
```
ApplicationLayer/
├── Program.cs → MainMenu
├── Menu/MainMenu.cs → Switch-case UI

BusinessLayer/
└── Services/QuantityMeasurementService.cs → Demos

ModelLayer/
├── Entity/Quantity<U>.cs → Core logic
├── Interface/IUnit.cs → Conversion contract
└── Enum/*.cs → LengthUnit, VolumeUnit, etc.
```
- **SRP**: Units handle own conversions.
- **Generic**: Reusable across categories.
- **Immutable**: Thread-safe.
- **Validated**: Null/infinite checks, op support.

## 🧪 Testing & Precision
- MSTest (legacy in QuantityMeasurementTests).
- Epsilon (1e-6) for floating-point.
- Round-trip conversions preserved.

