1. This project use post build script for copy "BISC.Resources" to build project in "BISC" project: 
		cd $(ProjectDir)$(OutDir)
		mkdir BISC.Resources
		xcopy /S /Y $(ProjectDir)..\BISC.Resources $(ProjectDir)$(OutDir)BISC.Resources

2. For correct copy BISC.Resources needs to use "Rebuild solution" command.

3. If new file added to "BISC.Resources" it needs to include to source control. 
For it go to Team Explorer -> Source Control Explorer -> BISC.Resources 
next right click select "add items to folder" and include required files.