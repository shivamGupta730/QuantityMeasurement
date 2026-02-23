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