# 📏 Quantity Measurement App

The Quantity Measurement App is designed to handle measurement comparisons in a structured and object-oriented manner.  
It focuses on implementing value-based equality for different measurement units while maintaining clean architecture and proper unit testing practices.

---

## ✅ UC1 – Feet Measurement Equality

UC1 implements equality comparison for measurements expressed in feet.

- 👣 Two `Feet` objects are considered equal if their numerical values are the same.  
- 🧠 Object-oriented principles are followed by properly overriding the `Equals()` and `GetHashCode()` methods.  
- 🧪 The functionality is validated using MSTest unit tests to ensure correctness, null safety, and type safety.