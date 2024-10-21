# Frontend CI build

If the frontend is javascript based (Angular, React, ...), the Frontend CI build is performed with the following steps:

![image.png](/.attachments/image-570655c4-47a3-4ad4-ac52-3dc765a65a86.png)

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
Download and install all npm dependencies and build the pipeline.
&nbsp;
:warning: <span style=color:black> **Note** </span>
<p>
<div style="background-color:orange;padding:20px;color:white">
This step can be expanded in the future to run a linter!  
This step can be expanded in the future to run unit tests!
This step can be expanded in the future to run a code coverage tool!    
</div>
</p>