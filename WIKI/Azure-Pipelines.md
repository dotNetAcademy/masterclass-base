# Build pipelines @ dotNET lab.

At dotNET lab we use Azure DevOps pipelines to build our software.  
We use different pipelines for this:
- a `Continuous Integration Build` pipeline
- a `Release Build` pipeline (sometimes also called `Full Build`pipeline)
- a `Deploy` pipeline and finally
- a `Compliance Scan` pipeline.

Let's briefly discuss these pipelines to see what they do in our software development.

## The **Continuous Integration Build** pipeline.
Every time we push new code to a branch (except for the *master* or *main* branches) we build the whole solution.  
We do this in several steps to check whether:
- the code complies with our coding standard as defined in the .editorconfig file
- the code can be compiled at all (e.g. all code is checked in, and all dependencies are defined)
- we didn't introduce regressions. We check this by executing all the unit tests in the solution
- we have sufficiently tested the code, by checking whether we have sufficient code coverage. We need to have a minimum code coverage, we call this the **Quality Gate**. We strive to set the bar as high as possible (> 80% if possible)

If the solution consists of an `Angular` / `React` frontend and a .NET backend, we build our solution in two different steps. This is because a JavaScript frontend has a different build process than a .NET project.

## The **Release Build** pipeline.
After code is pushed to the `master` or `main` branch (using a Pull Request), the code is built again, this time to check whether any issues unexpectedly arise. Most steps of a 'Continuous Integration Build' are repeated. An extra is that after the build the 'output' is stored in the `artifacts` of the pipeline. We save the 'bin\release' assemblies to use them in the 'deployment' pipeline.

## The **Deploy** pipeline.
We will roll out the software to our Azure environment using a deployment pipeline. We use **I**nfrastructure **a**s **C**ode (IaC) to first set up the environment in Azure. This means that we define, in code, which Azure resources and/or services we need to run our software. We use [Bicep](https://learn.microsoft.com/en-us/azure/azure-resource-manager/bicep/) files for this.  
We then deploy the software using the saved artifacts from the previous step ('Release Build' pipeline).

## The **Compliance Scan** pipeline.
This pipeline starts a tool to see which dependencies our software has.  
For each NuGet package that is used in the solution, it will checked whether it has known vulnerabilities. If this is the case, an email will be sent automatically. Because vulnerabilities can be found in the already deployed software, this pipeline is ran every night.

Note: For now, this tool only works for .NET applications (it needs to be adapted to support JavaScript applications, such as `Angular` or `React`.