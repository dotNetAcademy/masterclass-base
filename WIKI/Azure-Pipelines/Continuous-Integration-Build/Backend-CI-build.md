# Backend CI build

The backend CI build, builds the .NET backend code.

![image.png](/.attachments/image-dd065be8-7b64-435d-b738-f04fbab72c0b.png)

## Checkout needed code
The first steps is to checkout the code to build. Also some *.yml templates are checked out. Those templates concaint the code/steps to run in the pipeline.

## Install .NET tools
We need to install the correct .NET SDK to build the backend. We also need to install the latest version of NuGet and to restore all the needed NuGet packages.

💡<span style="color:black;padding:10px;">**Note** </span>
<p>
<div style=background-color:mediumseagreen;padding:20px;color:white>
The .NET SDK version to install is a defined as a variable in the azure-pipelines-ci.yml pipeline definition.
</div>
</p>

## Validate C# code style
We run the `dotnet format --verify-no-changes`. This will validate the coding style asd defined in the `.editorconfig` file.

💡<span style="color:black;padding:10px;">**Note** </span>
<p>
<div style=background-color:mediumseagreen;padding:20px;color:white>
The <strong>.editorconfig</strong> must be located in the root of the backend folder. This is the folder where the Visual Studio solution file (*.sln) is located.
</div>
</p>

## Determine a version number.
We use GitTools to 'calculate' a version number each time the CI pipeline in ran. The version will depend on the branch that is build.

💡<span style="color:black;padding:10px;">**Note** </span>
<p>
<div style=background-color:mediumseagreen;padding:20px;color:white>
For this step to work, is is necessary that a GitVersion.yml file is located in the root folder of the code repository.
</div>
</p>

## Build solution
The solution is build. Command line options are provided for the build task that include the version number as determined by GitTools in a previous step.

## Run unittests
All unit tests in the solution are executed. An option is configured to also capture the code coverage of the coded tested by the unit tests.

💡<span style="color:black;padding:10px;">**Note** </span>
<p>
<div style=background-color:mediumseagreen;padding:20px;color:white>
For this step to work, a <strong>.runsettings</strong> file is expected in the of the backend folder. This is the folder where the Visual Studio solution file (*.sln) is located.
</div>
</p>

## Quality gate
As a final step, the code coverage percentage is calculated. If it is below a certain threshold (target > 80%), the build is aborted.