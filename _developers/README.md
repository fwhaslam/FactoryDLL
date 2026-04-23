### Developers Configuration
=============================

This is a work in progress.  It is meant to contain values which are specific to 
developer / systems.  That way, the script that copies the DLLs can know where to 
post them.

It may turn out that I don't need this.  It depends a bit on what is feasible with 
Unity limitations on VS version.

Today, adding some 'web' content so this document folder will load correctly for 
other developers.

Turns out you just need to install ASP.NET, and VisualStudio will try to do so 
automatically.  Just let it happen.

## Developer Values
====================

unity.project.relative.path = the relative path to the unity project root folder.
	This is used by the CopyLibraryToUnity script.