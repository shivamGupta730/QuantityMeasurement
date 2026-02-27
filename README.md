# 📏 Quantity Measurement App

The Quantity Measurement App is designed to handle measurement comparisons in a structured and object-oriented manner.  
It focuses on implementing value-based equality for different measurement units while maintaining clean architecture and proper unit testing practices.

---

## ✅ UC1 – Feet Measurement Equality

UC1 implements equality comparison for measurements expressed in feet.

- 👣 Two `Feet` objects are considered equal if their numerical values are the same.  
- 🧠 Object-oriented principles are followed by properly overriding the `Equals()` and `GetHashCode()` methods.  
- 🧪 The functionality is validated using MSTest unit tests to ensure correctness, null safety, and type safety.  

---

## ✅ UC2 – Feet and Inches Measurement Equality

UC2 extends the application by introducing equality comparison for measurements expressed in inches, along with feet.

- 📏 A separate `Inches` class is implemented similar to the `Feet` class to maintain clear separation of responsibilities.  
- 🔍 Both `Feet` and `Inches` objects are compared independently using value-based equality.  
- 🧠 Proper equality contract is maintained by overriding `Equals()` and `GetHashCode()` in both classes.  
- 🛡 The implementation ensures null safety, type checking, and same reference validation.  
- 🧪 Comprehensive MSTest unit tests are written to verify same value comparison, different value comparison, null comparison, and reference equality.  
## UC3 – Generic Quantity Implementation

UC3 refactors the application by replacing separate unit classes with a single generic Length class.

🔄 A unified Length class is introduced to handle multiple measurement units.

🏷 A LengthUnit enum is implemented to represent different measurement types.

📐 All values are internally converted to a common base unit (Inches) for comparison.

🧠 The design follows the DRY (Don't Repeat Yourself) principle by centralizing conversion logic.

🔍 Cross-unit comparison is enabled (e.g., 1 Foot equals 12 Inches).

🛡 Null safety, type validation, and reference checks are preserved.

🧪 All previous UC1 and UC2 test cases continue to pass, ensuring backward compatibility.

##  UC4 – Extended Unit Support (Yards and Centimeters)

UC4 extends the generic design introduced in UC3 by adding additional measurement units without changing the core comparison logic.

🏏 The Yards unit is added to the LengthUnit enum (1 Yard = 36 Inches).

📏 The Centimeters unit is added to the LengthUnit enum (1 cm = 0.393701 Inches).

📐 Conversion logic is extended to support these new units using the same base unit (Inches).

