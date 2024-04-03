using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Presenter.Generation;
using UnityEngine;
using Utility;

namespace Model.Generation
{
    public class DungeonPopulator : MonoBehaviour
    {
        [Tooltip("The list of modules that will be randomly selected for instantiation.")]
        [SerializeField]
        private Module[] _componentModules;

        //Module parameter contains a dungeon room module
        public IEnumerator PopulateDungeon(Module module)
        {
            var components = new List<ModuleConnector>(module.GetConnectors()).Where(m => m.tag == "Component");

            foreach (var component in components)
            {
                //Percent chance that a component is generated at all
                if (RandomizationUtil.GetRandom(component.InstantiateChance))
                {
                    StartCoroutine(GenerateModule(component, module));
                }
            }

            yield return null;
        }

        private IEnumerator GenerateModule(ModuleConnector component, Module roomModule)
        {
            var newTag = RandomizationUtil.GetRandom(component.Tags);

            var newModulePrefab = GenerationShared.GetRandomWithTag(_componentModules, newTag);
            var moduleObject = Instantiate(newModulePrefab, roomModule.transform);
            var newModule = moduleObject.GetComponent<Module>();

            var connectorToMatch = newModule.GetConnectors().FirstOrDefault(x => x.IsDefault);
            GenerationShared.MatchConnectors(newModule, component, connectorToMatch);

            component.IsConnected = true;
            connectorToMatch.IsConnected = true;

            yield return null;
        }
    }    
}
