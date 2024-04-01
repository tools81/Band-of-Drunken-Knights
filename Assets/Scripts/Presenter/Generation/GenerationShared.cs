using UnityEngine;
using Model.Generation;
using Utility;
using System.Collections.Generic;
using System.Linq;

namespace Presenter.Generation 
{
    public static class GenerationShared
    {
        public static void MatchConnectors(Module module, ModuleConnector oldConnector, ModuleConnector newConnector)
        {            
            var forwardVectorToMatch = -oldConnector.transform.forward;
            var correctiveRotation = Azimuth(forwardVectorToMatch) - Azimuth(newConnector.transform.forward);
            module.transform.RotateAround(newConnector.transform.position, Vector3.up, correctiveRotation);
            var correctiveTranslation = oldConnector.transform.position - newConnector.transform.position;
            module.transform.position += correctiveTranslation; 
        }

        public static Module GetRandomWithTag(IEnumerable<Module> modules, string tagToMatch)
        {
            var matchingModules = modules.Where(m => m.Tags.Contains(tagToMatch)).ToArray();
            return RandomizationUtil.GetRandom(matchingModules);
        }

        public static float Azimuth(Vector3 vector)
        {
            return Vector3.Angle(Vector3.forward, vector) * Mathf.Sign(vector.x);
        }
    }
}