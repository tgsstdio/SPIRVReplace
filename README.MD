# SPIRV Replace

 > Usage: SPIRVReplace.exe [input-file] [entrypoint]
 
Command line batch file program written in C# for compiling single stage **GLSL** shader files into **SPIRV** bytecode and
 then updating the entrypoint with SPIRV bytecode from "main" to *[entrypoint]*. 
 
 All output files will be new SPIRV bytecode file ending with *.spv.

 For example:
````
 > SPIRVReplace.exe sample.frag frag_func
 >> 
 >> Output file: sample.spv
````

Later on, you would supply "frag_func" as the stage's pName into Vulkan's VkPipelineShaderStageCreateInfo when create a new pipeline.

~~~~ C++
typedef struct VkPipelineShaderStageCreateInfo {
    VkStructureType                     sType;
    const void*                         pNext;
    VkPipelineShaderStageCreateFlags    flags;
    VkShaderStageFlagBits               stage;
    VkShaderModule                      module;
    const char*                         pName;
    const VkSpecializationInfo*         pSpecializationInfo;
} VkPipelineShaderStageCreateInfo;
~~~~

# IMPLEMENTATION
1. Compile GLSL code into SPIRV bytecode via glslangValidator.exe 
2. Disassembly SPIRV bytecode into AST representation via SPIRV-dis.exe
3. Perform search-and-replace on AST to change "main" to "[entrypoint]" (i.e. in quotes)
4. Reassembly SPIRV bytecode from AST representation via SPIRV-as.exe 

# BUILD 

Run Visual Studio solution in SPIRVReplace folder

# FOLDERS 
## SPIRVReplace/
 - Copyright (c) 2016 David Young under MIT License

## ThirdParty/ 
 - Source code for glslangValidator.exe, SPIRV-dis.exe, SPIRV-as.exe. LICENSE included.

## Bin 
Windows assemblies 

# TOOLS INVOLVED 
## WINDOWS 
### glslangValidator.exe   
Found in Bin/Bin32 folders of the WINDOWS version of [LunarG Vulkan SDK](https://vulkan.lunarg.com/sdk/home#windows) v1.0.30 
### SPIRV-dis.exe
Found in Bin/Bin32 folders of the WINDOWS version of [LunarG Vulkan SDK](https://vulkan.lunarg.com/sdk/home#windows) v1.0.30 
### SPIRV-as.exe
Found in Bin/Bin32 folders of the WINDOWS version of [LunarG Vulkan SDK](https://vulkan.lunarg.com/sdk/home#windows) v1.0.30  

### LINUX / OTHER 
Try modifying the .config found in the Bin folder to use linux version of these assemblies 


