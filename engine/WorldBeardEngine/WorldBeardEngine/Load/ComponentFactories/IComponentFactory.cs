using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldBeardEngine.Components;

namespace WorldBeardEngine.Load.ComponentFactories
{
    interface IComponentFactory
    {
        Component GetComponent(ComponentDO componentDO);
    }
}
