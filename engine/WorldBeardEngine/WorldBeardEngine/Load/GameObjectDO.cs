using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WorldBeardEngine.Load
{
    public struct GameObjectDO
    {
        public int ID;
        public string Name;
        public bool IsSaved;
        public List<ComponentDO> ComponentDOs;
    }
}
