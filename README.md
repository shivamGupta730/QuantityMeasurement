# 📏 Quantity Measurement App

A clean, immutable, value-object based implementation for length comparison, conversion, and arithmetic operations using C# and MSTest.

---

## ✅ UC1 – Same Unit Equality
- Compares two lengths of the same unit.
- Value-based equality using overridden `Equals()`.

## ✅ UC2 – Cross Unit Equality
- Supports comparison across units.
- Internally normalizes to base unit (Feet).

## ✅ UC3 – Generic Length
- Single `Length` class with `LengthUnit` enum.
- Extensible and immutable design.

## ✅ UC4 – Extended Units
- Added: Feet, Inches, Yards, Centimeters.
- All units normalized to Feet internally.

## ✅ UC5 – Unit Conversion
- `Length.Convert(value, from, to)`
- Supports all unit combinations.
- Handles zero, negative, and invalid values.

## ✅ UC6 – Addition (Implicit Unit)
- `Add(Length other)`
- Returns result in first operand’s unit.
- Preserves immutability and commutativity.

## ✅ UC7 – Addition (Explicit Target Unit)
- `Add(Length other, LengthUnit targetUnit)`
- Result returned in specified unit.
- Caller-controlled output unit.
- Maintains precision and validation.

---

## 🧪 Testing
- Full MSTest coverage
- Equality, conversion, addition
- Edge cases & precision handling

