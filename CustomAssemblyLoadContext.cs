namespace  cyberforgepc
{
    using System;
    using System.Reflection;
    using System.Runtime.Loader;

    public class CustomAssemblyLoadContext : AssemblyLoadContext
    {
        public IntPtr LoadUnmanagedLibrary(string absolutePath) => LoadUnmanagedDll(absolutePath);

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName) => LoadUnmanagedDllFromPath(unmanagedDllName);

        protected override Assembly Load(AssemblyName assemblyName) => throw new NotImplementedException();
    }
}
