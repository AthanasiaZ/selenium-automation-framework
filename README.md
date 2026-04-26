# Selenium Automation Framework (C# | NUnit | Page Object Model)

## Overview

This project is a UI automation framework built using:
- C#
- Selenium WebDriver
- NUnit

It implements a structured approach to automated testing using the Page Object Model (POM) and covers functional, visual, and performance-related scenarios based on the SauceDemo application.

---

## Architecture

The project is organized into the following layers:

- **Core**
  - WebDriverBase (common utilities, waits, helpers)
  - DriverFactory (browser initialization)
  - TestConfig (configuration via appsettings)

- **PageObjects**
  - Login
  - Inventory
  - Cart
  - Checkout

- **TestCases**
  - UC01_Login
  - UC02_Inventory
  - UC03_Cart
  - UC04_Checkout

---

## Key Features

- Page Object Model (POM)
- Centralized locator handling
- Explicit wait strategy
- Scroll handling with optional container targeting
- Reusable helper methods (e.g. element containment checks)
- Separation between test logic and UI interaction logic

---

## Test Coverage

### UC01 – Login

| ID | Description |
|----|------------|
| TC01_01 | Valid login |
| TC01_02 | Invalid login |
| TC01_03 | Locked out user validation |
| TC01_06 | Logout |
| TC01_07 | Reset app state |
| TC01_08 | About link navigation |
| TC01_09 | Login duration comparison (standard vs performance_glitch_user) |

---

### UC02 – Inventory

| ID | Description |
|----|------------|
| TC02_05 | Add and remove single product |
| TC02_06 | Add multiple products and remove one |
| TC02_07 | Problem user – product image mismatch |
| TC02_08 | Problem user – remove from cart behavior |
| TC02_09 | Visual user – product image consistency |
| TC02_10 | Visual user – layout alignment (button vs container) |

---

### UC03 – Cart

| ID | Description |
|----|------------|
| TC03_01 | Cart displays selected products |
| TC03_02 | Remove product from cart |
| TC03_03 | Cart state after updates |

---

### UC04 – Checkout

| ID | Description |
|----|------------|
| TC04_01 | Mandatory fields validation |
| TC04_02 | Successful checkout flow |
| TC04_03 | Checkout totals validation |

---

## Verification Scenarios

Certain user types in SauceDemo expose specific behaviors that are used as verification scenarios.

These behaviors are intentionally leveraged to reverse engineer potential defects and validate how the system responds to inconsistent data, rendering issues, or performance degradation.

The expected outcomes in these cases reflect known deviations in behavior, allowing the tests to confirm that such issues can be detected and reproduced.

---

### Problem User

The `problem_user` is used to verify data consistency between UI elements.

Checks include:
- Product name and image matching
- Consistency of displayed product data

These types of checks are applicable in cases where UI data may be incorrectly mapped or cached.

---

### Visual User

The `visual_user` is used to verify layout and rendering behavior.

Checks include:
- Element alignment within containers
- Button positioning relative to product cards
- Detection of overflow or misalignment

These validations are implemented using DOM geometry (element position and size), without relying on screenshots.

---

### Performance Glitch User

The `performance_glitch_user` is used to verify differences in response time.

Checks include:
- Login duration compared to standard user

The comparison is relative (glitch vs standard) instead of using fixed thresholds.

---

## Visual Validation Approach

Layout validation is based on element positioning:

- Comparing element boundaries
- Verifying containment within parent containers

This allows detection of layout issues such as:
- overflow
- misalignment
- incorrect positioning

---

## Configuration

Configuration is defined in `appsettings.json`.

---

## How to Run

### Prerequisites

- .NET SDK (6.0 or later)
- Google Chrome

---

### 1. Clone the repository

### 2. Restore dependencies

### 3. Run tests

```bash
git clone https://github.com/AthanasiaZ/selenium-automation-framework.git
cd selenium-automation-framework

dotnet restore
dotnet test

