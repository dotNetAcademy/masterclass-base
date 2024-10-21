# Frontend Full build
If the frontend is javascript based (Angular, React, ...), the Frontend full build is performed with the following steps:

![image.png](/.attachments/image-e2578f1a-4344-4b66-b62d-a97d279cba0d.png)

## Checkout needed code
The first steps is to checkout the code to build. Also some *.yml templates are checked out. Those templates concaint the code/steps to run in the pipeline.

## Determine a version number.
We use GitTools to 'calculate' a version number each time the CI pipeline in ran. The version will depend on the branch that is build.

💡<span style="color:black;padding:10px;">**Note** </span>
<p>
<div style=background-color:mediumseagreen;padding:20px;color:white>
For this step to work, is is necessary that a GitVersion.yml file is located in the root folder of the code repository.
</div>
</p>

## Install Node.js
Download and install a version of Node.js needed to build the project.

💡<span style="color:black;padding:10px;">**Note** </span>
<p>
<div style=background-color:mediumseagreen;padding:20px;color:white>
The version to install is a defined as a variable in the azure-pipelines-ci.yml pipeline definition.
</div>
</p>

## Npm install and build
Download and install all npm dependencies and build the code.

## Copy files and publish artifacts
This step copies the 'compiled' code and stores it in the output of the pipeline (the artifacts).

When the pipeline executes successfully, artifacts are attached to it. (see next schreenshot)

![image.png](/.attachments/image-2f99f0e3-fb8c-4187-a208-e9d4fc830ee0.png)

When you click on the link it will show you the stored artifacts, sa shown here.

![image.png](/.attachments/image-7011b8b0-9d30-4c8b-a18e-b5c4f49332ae.png)