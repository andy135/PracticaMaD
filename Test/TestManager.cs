using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    class TestManager
    {
        public static IUnityContainer ConfigureUnityContainer(string sectionName)
        {
            IUnityContainer container = new UnityContainer();

            UnityConfigurationSection section =
                (UnityConfigurationSection)ConfigurationManager.GetSection(sectionName);
            
            
            section.Configure(container, section.Containers.Default.Name);

            return container;
        }


        public static void ClearUnityContainer(IUnityContainer container)
        {

            container.Dispose();
        }
    }
}
