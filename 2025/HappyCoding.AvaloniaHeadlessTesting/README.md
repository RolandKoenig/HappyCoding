# Testing with Avalonia.Headless
 - A sample for testing with Avalonia.Headless
 - Tests are written using xUnit
 - Custom control locators help to find and interact with controls in the logical tree
   - see HappyCoding.AvaloniaHeadless.Tests.Util

## Things to consider
 - Using custom control locators to find ui elements an interact with them

## Recommendation
 - Test your UI automatically
   - Not every detail, but basic tests should be there
 - Try not to depend on technical things like names within tests - interact with the application like the user does