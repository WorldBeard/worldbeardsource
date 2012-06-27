using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldBeardEngine
{
    public static class ReflectionUtil
    {

        // Utility reflection method to return an un-cast object of the specified type from the WorldBeardEngine assembly.
        public static object GetObjectFromTypeName(string typeName)
        {
            return Activator.CreateInstance(Type.GetType(typeName));
        }

    }
}
