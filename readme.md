# 💰 Loan Amortization Calculator

A high-precision, client-side **FinTech application** built with **.NET 8 Blazor WebAssembly**. 
This project demonstrates Clean Architecture principles, financial mathematics precision, and performant UI rendering for complex data sets.

## 🚀 Tech Stack & Libraries

* **Framework:** .NET 8 Blazor WebAssembly (Standalone / Client-side only)
* **UI Library:** [MudBlazor](https://mudblazor.com/) (Material Design components)
* **Charting:** [Blazor-ApexCharts](https://github.com/apexcharts/Blazor-ApexCharts) (Interactive SVG visualizations)
* **Testing:** xUnit + FluentAssertions
* **Reporting:** Native Browser Print Engine (CSS optimized for minimal bundle size)

## 🏗 Architecture

The solution follows a **Simplified Clean Architecture** approach suitable for SPA (Single Page Applications):

### 1. **Core (Class Library)**
* **Zero Dependencies:** Contains only pure C# logic and domain models.
* **Domain Driven:** Uses `record` types for immutable data structures (`LoanInput`, `AmortizationRow`).
* **Precision:** All financial calculations use `decimal` type to avoid floating-point errors common with `double`.
* **Interfaces:** Defines `ILoanCalculator` contract for loose coupling.

### 2. **Client (Blazor WASM)**
* **State Management:** Uses a scoped `LoanStateService` to bridge the Input Form (Sidebar) and Results View (Main Content).
* **MVVM Pattern:** Uses `LoanInputViewModel` for form validation before mapping to Domain models.
* **Components:** Split into "Smart Containers" (Pages) and "Dumb Components" (Charts, Tables).
* **Virtualization:** Utilizing `MudDataGrid` virtualization to handle 360+ rows of amortization data without UI lag.

## ✨ Key Features

* **Real-time Calculation:** Instantly calculates monthly payments, total interest, and payoff dates.
* **Interactive Visualizations:** Stacked Area Charts showing the shift from Interest-heavy to Principal-heavy payments over time.
* **Amortization Schedule:** Detailed monthly breakdown of payments.
* **Print-Ready Reports:** Optimized CSS `@media print` logic allows users to save professional PDF reports using the native browser printer, keeping the application bundle size small (avoiding heavy server-side PDF libraries like SkiaSharp).

## 🛠️ How to Run

1.  **Prerequisites:**
    * .NET 8.0 SDK or later.

2.  **Clone and Run:**
    ```bash
    git clone [repository-url]
    cd LoanAmortizationCalculator
    dotnet run --project LoanAmortizationCalculator.Client
    ```

3.  **Open in Browser:**
    Navigate to `http://localhost:5000` (or the port indicated in the terminal).

## 🧪 Testing

Business logic is covered by unit tests to ensure financial accuracy.

```bash
dotnet test LoanAmortizationCalculator.Tests