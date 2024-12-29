# OldPhonePadApp

## Overview

OldPhonePadApp is a C# application that simulates the behavior of an old phone keypad, decoding user inputs into text messages. It incorporates modern coding practices, robust logging, and comprehensive unit tests to ensure maintainability and scalability.

The application was designed to handle inputs that include:
- Multiple consecutive button presses to select characters.
- Backspaces (`*`) to remove previously typed characters.
- Spaces (` `) for delays between button presses.
- A send key (`#`) to finalize input and produce the decoded output.

### Key Features:
- Accurate decoding of inputs based on old phone keypad behavior.
- Handles edge cases like consecutive backspaces and incomplete inputs.
- Includes structured logging and follows best practices for dependency injection.
- Comprehensive unit tests covering valid and invalid scenarios.
- GitHub Actions CI/CD pipeline integration for automated testing and deployment.
- Code coverage reporting.
- Automatically generated documentation.

## Demo

![image](https://github.com/user-attachments/assets/49796686-a8a1-4ae6-adba-4c60776fb9b3)


Documentation: https://devshridhar.github.io/OldPhonePadApp/index.html

EXE File: https://github.com/devshridhar/OldPhonePadApp/releases/download/main/OldPhonePadApp.exe

### Online Documentation
The generated documentation for the project can be accessed [here](https://devshridhar.github.io/OldPhonePadApp/index.html).

---

## Requirements

- .NET SDK 9.0 or higher
- Doxygen for documentation generation (optional but recommended)
- A compatible IDE (e.g., Visual Studio, JetBrains Rider, or Visual Studio Code)

---

## Installation

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/devshridhar/OldPhonePadApp.git
   cd OldPhonePadApp
   ```

2. **Install .NET**:
   Ensure you have .NET SDK 9.0 or later installed. You can download it from the [official .NET site](https://dotnet.microsoft.com/download).

3. **Install Doxygen** (for documentation):
   - **Linux**:
     ```bash
     sudo apt-get install doxygen
     ```
   - **Mac**:
     ```bash
     brew install doxygen
     ```
   - **Windows**:
     Download and install from the [Doxygen official site](http://www.doxygen.nl/download.html).

---

## Running the Application

### 1. Run the Application
```bash
cd src/OldPhonePadApp
dotnet run
```
By default, the program processes a predefined input. Modify the `Main` method in `Program.cs` to provide custom inputs.

### 2. Run Unit Tests
```bash
cd tests/OldPhonePadApp.Tests
dotnet test
```

### 3. Format the Code
To ensure the code adheres to styling guidelines:
```bash
dotnet format
```

### 4. Generate Documentation
Generate a detailed HTML-based documentation using Doxygen:
```bash
cd src/OldPhonePadApp
doxygen Doxyfile
```
Open `docs/html/index.html` in a browser to view the documentation or access it online [here](https://devshridhar.github.io/OldPhonePadApp/index.html).

---

## GitHub Actions CI/CD Integration

The project uses GitHub Actions to automate the CI/CD pipeline. The `ci.yml` workflow file is located in `.github/workflows/ci.yml`.

### Workflow Features:
1. **Automated Testing**:
   - Runs tests on every push or pull request to the `main` branch.
2. **Code Coverage**:
   - Generates a coverage report using `dotnet test` with `XPlat Code Coverage`.
   - Uploads the coverage report as an artifact.
3. **Code Formatting**:
   - Verifies code formatting using `dotnet format`.
4. **Documentation Generation**:
   - Builds the documentation using Doxygen and deploys it to GitHub Pages.
5. **Release Management**:
   - Creates GitHub Releases for versioned tags.
   - The release page contains a downloadable Windows executable file that runs as a console application.

### Running the Workflow Locally
If needed, you can replicate the CI steps locally:
```bash
# Restore dependencies
dotnet restore

# Build the application
dotnet build

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"

# Format code
dotnet format
```

---

## Packages Used

### Core Application
- **Microsoft.Extensions.Logging**:
   - Provides structured and configurable logging.
   - Enables seamless logging in both development and production environments.

### Unit Testing
- **xUnit**:
   - A modern and widely-used testing framework.
   - Handles both simple and complex test cases effectively.

### Additional Tools
- **StyleCop.Analyzers**:
   - Ensures adherence to coding standards and best practices.
- **Microsoft.NET.Test.Sdk**:
   - Integrates xUnit tests with the .NET ecosystem.
- **Doxygen**:
   - Generates detailed and interactive documentation for the codebase.

---

## Highlights

### Best Practices Followed
1. **Dependency Injection**:
   - Logger injection ensures modularity and testability.
   - Code is decoupled and easy to extend.

2. **Robust Error Handling**:
   - Input validations prevent undefined behaviors.
   - Comprehensive handling of edge cases.

3. **Unit Testing**:
   - Covers various scenarios including:
      - Valid and invalid inputs.
      - Multiple backspaces.
      - Complex sequences like `222 2 22#`.
   - Includes logging for test clarity.

4. **Code Formatting**:
   - Follows consistent and maintainable coding standards.
   - StyleCop ensures adherence to best practices.

5. **Code Coverage**:
   - Integrated with GitHub Actions.
   - Generates detailed HTML reports.

6. **Documentation**:
   - Uses Doxygen to generate detailed documentation.

7. **Windows Executable**:
   - Publishes a self-contained Windows executable file.

---

## Sample Usage

1. **Input**: `4433555 555666#`
   **Output**: `HELLO`

2. **Input**: `222 2 22#`
   **Output**: `CAB`

3. **Input**: `8 88777444666*664#`
   **Output**: `TURING`

4. **Input**: `4433*#`
   **Output**: `H`

5. **Input**: `4***#`
   **Output**: (empty string)

---

## Contribution

We welcome contributions! If you'd like to improve the application or add new features, feel free to fork the repository and create a pull request.

---

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.

---

