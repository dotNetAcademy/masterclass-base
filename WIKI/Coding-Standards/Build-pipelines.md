# Build pipelines
## General
A build pipeline is a sequence of steps that automate the process of building, testing, and deploying your software solution. A build pipeline can help you achieve the following benefits:

- It can improve the quality and reliability of your software by ensuring that it passes all the required tests and checks before it is released to the end-users.
- It can increase the speed and efficiency of your software development by reducing the manual tasks and errors that can slow down or interrupt the workflow.
- It can enhance the collaboration and communication among the developers and other stakeholders by providing a clear and consistent view of the software status and progress.
- It can facilitate the continuous delivery and integration of your software by enabling you to deploy your changes frequently and seamlessly to the target environment.

<p>&nbsp;</p>

:hand: <span style="color:black"> **Do create a Continuous Integration Build pipeline** </span>
<div style="background-color:steelblue;padding:20px;color:white">
Why? The master branch should always be in a state where it can be deployed to production. That is why we do everything possible to prevent a polluted master branch. One tool in our toolbelt is to set up a CI build for this.

The CI pipeline must build your frontend and backend applications, validate the coding style and run all the unit tests.
</div>

<p>&nbsp;</p>

``` yml
#######################################################################################################################
#
# This pipeline defines the build steps for a CI (Continuous Integration) pipeline.
#
# This pipeline will validate that the solution can be build and that all the unit tests passes.
# It is ran when:
#   - code is pushed to a branch other than master
#   - a pull request is created
#                                                                                                            DotnetLab
#######################################################################################################################

trigger:
  batch: yes                  # Whether to batch changes per branch. (false,n,no,off,on,true,y,yes)
  branches:                   # Branch names to include or exclude for triggering a run.
    include:                  # List of items to include.
    - '*'                     # => All branches
    exclude:                  # List of items to exclude.
    - master                  # => except the master branch

pool:
  vmImage: 'windows-latest'

variables:
  # The name of the application.
  applicationName: '<NAME-OF-APPLICATION>'

  # The name of the solution to build.
  solutionName: '<NAME-OF-SOLUTIONFILE>.sln'

  # The name of the folder containing the solution.
  # Leave empty if the solution is in the root of the repository.
  solutionFolder: ''

  # Specifies the version of the .NET (Core) SDK or runtime to install.
  sdkVersion: '7.0.x'

  # Specifies the build version for the assemblies.
  version: '1.0.0.$(Build.BuildId)'

stages:
- stage: Build
  displayName: Build solution

  jobs:
  - job: Build
    displayName: Build solution

    steps:
    - template: templates/dotnet_install_sdk_task.yml
      parameters:
        sdkVersion: $(sdkVersion)

    - template: templates/dotnet_install_nuget_task.yml
      parameters:
        solutionName: $(solutionName)
        solutionFolder: $(solutionFolder)

    - template: templates/dotnet_format_task.yml
      parameters:
        solutionName: $(solutionName)
        solutionFolder: $(solutionFolder)

    - template: templates/dotnet_build_solution_task.yml
      parameters:
        solutionName: $(solutionName)
        solutionFolder: $(solutionFolder)
        version: $(version)

    - template: templates/dotnet_run_solution_unit_tests_task.yml
      parameters:
        solutionName: $(solutionName)
        solutionFolder: $(solutionFolder)
```

<p>&nbsp;</p>

:hand: <span style="color:black"> **Do create a Release Build pipeline** </span>
<div style="background-color:steelblue;padding:20px;color:white">
Why? The release pipeline builds the application into a ready-to-deploy state.
The Release Build should perform all the same steps as the CI build and is must create a Software Bill of Materials (SBOM of short).
</div>

<p>&nbsp;</p>

:hand: <span style="color:black"> **Do create a Deployment pipeline when the application must be deployed to Azure** </span>
<div style="background-color:steelblue;padding:20px;color:white">
Why? A deployment pipeline ensures that we always deploy our application to Azure in a consistent manner.

It also has the additional advantage that anyone (such as an intern) can deploy the application without having extensive Azure knowledge.
</div>

<p>&nbsp;</p>

:hand: <span style="color:black"> **Do create pipeline templates for reusable functionality** </span>
<div style="background-color:steelblue;padding:20px;color:white">
Why? Just as you do in C# code, when you use the same functionality over and over again (like installing the .NET SDK, or build the solution), you create a reusable piece. In Azure DevOps this is called a template.
</div>
