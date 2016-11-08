# SPIRV Replace

 > Usage: SPIRVReplace.exe [input-file] [entrypoint] 

Command line batch file program written in C# for compiling GLSL *[input-file]* into *SPIRV* bytecode and
 then updating the entrypoint with SPIRV bytecode from "main" to *[entrypoint]*. 

*[entrypoint]* is the value passed into pName for Vulkan's VkPipelineShaderStageCreateInfo

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

# IMPLEMENTATION
1. Compile GLSL code into SPIRV bytecode via glslangValidator.exe   
2. Disassembly SPIRV bytecode into AST representation via SPIRV-dis.exe
3. Perform search-and-replace on AST to change "main" to [entrypoint]
4. Reassembly SPIRV bytecode from AST representation via SPIRV-as.exe 

# BUILD 

Run Visual Studio solution

# LICENSE
Copyright 2016 David Young under MIT License
