# Continuous Integration Build

We have already seen that the **Continuous Integration Build** is used to check whether the project can be built before the code is merged into the *master* branch.

## Pipeline definition
The pipeline is defined in the file **'azure-pipelines-ci.yml'** and must be present in the **root** folder of the solution.

## Pipeline structure
The pipeline itself has the following structure:

### Trigger: 
Determines when the pipeline starts. Normal this pipeline will run as soon as code is pushed to a branch.

:hand: <span style="color:black"> **Note**</span>
<p>
<div style="background-color:steelblue;padding:20px;color:white">
Some branches are exclude from starting the <strong>Continuous Integration Build</strong>.
They are the <i>master</i> branch and/or the <i>main</i> branch.
Also all branches that are created under the <i>release/</i> or <i>releases/</i> branches are excluded.
</div> 
</p>

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

### Parameters:
These allow you to choose options if the pipeline is started manually. For example, which part of the application needs to be built. The frontend and/or the backend code.  

💡<span style="color:black;padding:10px;">**Customization** </span>
<p>
<div style=background-color:mediumseagreen;padding:20px;color:white>
Only edit this section if the application consists of only one technology (for example, anything in C#, such as a Blazor app and an API backend) and so it doesn't make sense to have build options.
</div>
</p>

### Stages:
Stages serve as logical boundaries within a pipeline. It gives us the opportunity to divide the work and possibly process parts of the build pipeline in parallel.

In most cases we will define stages per application part. For example, one to build the frontend and one to build the backend.

![image.png](/.attachments/image-1ffe3772-fff5-4f17-95d8-bac01b25d86d.png)

💡<span style="color:black;padding:10px;">**Customization** </span>
<p>
<div style=background-color:mediumseagreen;padding:20px;color:white>
Normally no changes need to be made to this part of the pipeline as everything is controlled using the variables defined at the start of the pipeline.
</div>
</p>

