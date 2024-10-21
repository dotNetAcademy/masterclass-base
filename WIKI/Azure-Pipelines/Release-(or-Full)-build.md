# Release build

The `Release build` (sometimes also called the `Full build`) will build the entire solution to make it ready for deployment.

With this type of build we perform more or less the same steps as with a `Continuous Integration Build`. We want to make sure that the code can still be built after a merge. At the same time we capture the output of the build so that we can deploy it.

Pipeline definition
The pipeline is defined in the file 'azure-pipelines-build.yml' and must be present in the root folder of the solution.

Pipeline structure
The pipeline itself has the following structure:

Trigger:
Determines when the pipeline starts. Normal this pipeline will run as soon as code is pushed to the `master` or `main` branch. It is also triggered to run when a branch is created under `Release/` or ´releases/´.

💡<span style="color:black;padding:10px;">**Customization** </span>
<p>
<div style=background-color:mediumseagreen;padding:20px;color:white>
<strong>Never</strong> edit or modify this section in the *.yml file.
</div>
</p>

### Pool:
Determines the build agent used to execute the pipeline.  

💡<span style="color:black;padding:10px;">**Customization** </span>
<p>
<div style=background-color:mediumseagreen;padding:20px;color:white>
<strong>Never</strong> edit or modify this section in the *.yml file.<br/>
At the moment we use Hosted Agents and there is no need to change the VM image of those.
</div>
</p>

### Resources:
Retrieves code (*.yml templates) from another repo. This repo contains reusable templates (think methods).

💡<span style="color:black;padding:10px;">**Customization** </span>
<p>
<div style=background-color:mediumseagreen;padding:20px;color:white>
<strong>Never</strong> edit or modify this section in the *.yml file.<br/>
When this section is altered or removed the pipeline will no longer work!
</div>
</p>

### Variables:
This contains variables that can be used throughout the execution of the pipeline.  

💡<span style="color:black;padding:10px;">**Customization** </span>
<p>
<div style=background-color:mediumseagreen;padding:20px;color:white>
The intention is to adjust the variables depending on the project! For example, changing the project name or the version of .NET to use. Remove the variables from the frontend or backend if they are not needed.
</div>
</p>


### Stages:
Stages serve as logical boundaries within a pipeline. It gives us the opportunity to divide the work and possibly process parts of the build pipeline in parallel.

In most cases we will define stages per application part. For example, one to build the frontend and one to build the backend.

![image.png](/.attachments/image-75344006-e515-4dc4-bf53-46efa5fc06af.png)

💡<span style="color:black;padding:10px;">**Customization** </span>
<p>
<div style=background-color:mediumseagreen;padding:20px;color:white>
Normally no changes need to be made to this part of the pipeline as everything is controlled using the variables defined at the start of the pipeline.
</div>
</p>

