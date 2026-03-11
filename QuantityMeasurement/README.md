# 📏 Quantity Measurement App (UC1–UC8)

A clean, scalable, immutable value-object implementation for length comparison, conversion, and arithmetic operations in C# using MSTest.

---

## ✅ UC1 – Same Unit Equality
- Compare two lengths with same unit.
- Value-based equality using overridden `Equals()`.

## ✅ UC2 – Cross Unit Equality
- Compare different units (Feet ↔ Inches).
- Internally normalized to base unit (Feet).

## ✅ UC3 – Generic Length Class
- Single `Length` class.
- Introduced `LengthUnit` enum.
- Immutable design.

## ✅ UC4 – Extended Units
- Added: Feet, Inches, Yards, Centimeters.
- All units normalized to Feet internally.

## ✅ UC5 – Unit Conversion
- `Length.Convert(value, from, to)`
- Supports all unit combinations.
- Handles zero, negative, invalid values.
- Round-trip conversion supported.

## ✅ UC6 – Addition (Implicit Target Unit)
- `Add(Length other)`
- Result returned in first operand’s unit.
- Maintains immutability and commutativity.

## ✅ UC7 – Addition (Explicit Target Unit)
- `Add(Length other, LengthUnit targetUnit)`
- Caller controls result unit.
- Supports all cross-unit combinations.

## ✅ UC8 – Refactoring (Standalone LengthUnit)

### 🔹 Architectural Improvement
- Extracted `LengthUnit` as standalone enum.
- Moved conversion responsibility to unit itself.
- Removed conversion logic from `Length`.

### 🔹 Benefits
- Follows **Single Responsibility Principle**
- Eliminates circular dependency risk
- Improves cohesion & scalability
- Backward compatible with UC1–UC7
- Ready for future categories (WeightUnit, VolumeUnit)

---

## 🧪 Testing
- Full MSTest coverage
- Equality tests
- Conversion tests
- Addition tests (implicit & explicit target unit)
- Precision handled using epsilon comparison

---

