# Avalonia UI Test example

This is an example of a custom UI test framework for Avalonia, based on Android Espresso.

The UI tests here run on the main app code, but are only included in the app if a certain preprocessor directive is specified during compilation for Release configuration. Running on Debug configuration also allows the UI tests to be run.

To allow UI tests in a Release build, pass the `UITestsEnabled` parameter like below:

```pwsh
dotnet run --configuration Release -p:UITestsEnabled=true
```

Check the .csproj file to understand how the `UITestsEnabled` compilation flag removes the UI tests code and enables the `UI_TESTS_ENABLED` compiler directive.
