using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Model.Generation;
using Utility;
using System;
using UnityEngine.SceneManagement;

namespace Presenter.Generation
{
    public class DungeonGenerator : MonoBehaviour, IDungeonGenerator
    {
        [Tooltip("The list of modules that will be randomly selected for instantiation.")]
        [SerializeField]
        private Module[] CommonModules;

        [Tooltip("The list of unique module rooms included in every dungeon creation.")]
        [SerializeField]
        private Module[] UniqueModules;

        [Tooltip("The list of boss module rooms to choose from for every dungeon creation.")]
        [SerializeField]
        private Module[] BossModules;

        [Tooltip("The first module instantiated from which others will branch from.")]
        [SerializeField]
        private Module StartModule; 

        [Tooltip("How many times the algorithm will loop, instantiating modules and connecting them to exits.")]
        [SerializeField]
        private int Iterations = 5;

        [Tooltip("The number of times the generator will try instantiating new modules in a position where collisions are detected.")]
        [SerializeField]
        private int CollisionRetries = 5;

        [Tooltip("Only for debug purposes! To watch generation play out.")]
        [SerializeField]
        private float slowGenerateSeconds = 0;

        [HideInInspector]
        private Dictionary<ModuleConnector, float> distDic = new Dictionary<ModuleConnector, float>();
        private bool bossRoomInstantiated = false;

        void Start()
        {
            StartCoroutine(GenerateDungeon());
        }

        public IEnumerator GenerateDungeon()
        {
            var startModule = Instantiate(StartModule, Vector3.zero, Quaternion.identity).GetComponent<Module>();
            var pendingExits = new List<ModuleConnector>(startModule.GetExits());

            for (int iteration = 0; iteration < Iterations; iteration++)
            {
                var newExits = new List<ModuleConnector>();

                foreach (var pendingExit in pendingExits)
                {                                        
                    StartCoroutine(GenerateModule(CommonModules, newExits, pendingExit));
                    if (slowGenerateSeconds > 0 ) { yield return new WaitForSeconds(slowGenerateSeconds); }
                }

                if (slowGenerateSeconds > 0) { yield return new WaitForSeconds(slowGenerateSeconds); }
                pendingExits = newExits;

                // try
                // {
                //     pendingExits = newExits;
                // }
                // catch (Exception ex)
                // {
                //     Debug.LogError($"Error: {ex.Message}");
                //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                // }
            } 

            foreach(var exit in GameObject.FindGameObjectsWithTag("Exit"))
            {
                if (!exit.GetComponent<ModuleConnector>().IsConnected)
                {
                    float dist = Vector3.Distance(gameObject.transform.position, exit.transform.position);
                    distDic.Add(exit.GetComponent<ModuleConnector>(), dist);
                }
            }

            distDic.OrderByDescending(key => key.Value);
            //var furthestExit = distDic.FirstOrDefault();

            foreach (ModuleConnector exit in distDic.Keys)
            {
                if (!bossRoomInstantiated)
                {
                    //Instantiate the boss room at the furthest exit from the starting module
                    StartCoroutine(GenerateModule(BossModules, new List<ModuleConnector>(), exit, "Boss", false));
                    if (slowGenerateSeconds > 0) { yield return new WaitForSeconds(slowGenerateSeconds); }
                }
                else 
                {
                    break;
                }
            }            

            //distDic.Remove(furthestExit.Key);

            //Instantiate modules for unique rooms
            foreach(ModuleConnector exit in distDic.Keys)
            {
                if (!exit.IsConnected)
                {
                    StartCoroutine(GenerateModule(UniqueModules, new List<ModuleConnector>(), exit, "Unique"));
                    if (slowGenerateSeconds > 0) { yield return new WaitForSeconds(slowGenerateSeconds); }
                }
            }

            //Remove the mesh renderers as we no longer need them on the modules
            // var modulesInScene = FindObjectsOfType<Module>();
            // foreach(var moduleInScene in modulesInScene)
            // {
            //     Destroy(moduleInScene.GetComponent<OverlapBox>());
            //     Destroy(moduleInScene.GetComponent<Renderer>());
            // }
            
            if (!bossRoomInstantiated)
            {
                if (slowGenerateSeconds > 0) { yield return new WaitForSeconds(slowGenerateSeconds); }
                DeleteAll();
                StartCoroutine(GenerateDungeon());
            }
            
            yield return null;
        }

        private IEnumerator GenerateModule(Module[] modules, List<ModuleConnector> newExits, ModuleConnector pendingExit, string newTag = null, bool retry = true)
        {
            if (newTag is null)
            {
                newTag = RandomizationUtil.GetRandom(pendingExit.Tags);
            }
            
            var newModulePrefab = GetRandomWithTag(modules, newTag);
            var moduleObject = Instantiate(newModulePrefab);
            var newModule = moduleObject.GetComponent<Module>();

            var newModuleExits = newModule.GetExits();
            var exitToMatch = newModuleExits.FirstOrDefault(x => x.IsDefault) ?? RandomizationUtil.GetRandom(newModuleExits);
            MatchExits(newModule, pendingExit, exitToMatch);
            
            Physics.SyncTransforms();

            if (slowGenerateSeconds > 0) { yield return new WaitForSeconds(slowGenerateSeconds); }
            newModule.GetComponent<OverlapBox>().DetectCollisions();

            if (!newModule.Colliding)
            {
                if (newTag == "Boss")
                {
                    bossRoomInstantiated = true;
                }

                pendingExit.IsConnected = true;
                exitToMatch.IsConnected = true;

                newExits.AddRange(newModuleExits.Where(e => e != exitToMatch));

                foreach (Transform child in exitToMatch.transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
            else
            {
                Debug.Log($"Destroying module {moduleObject.gameObject.name}.");
                
                Destroy(moduleObject.gameObject);

                exitToMatch.GetComponentInParent<Module>().Colliding = false;

                if (retry && CollisionRetries > 0)
                {
                    CollisionRetries--;

                    if (newTag == "Boss")
                    {
                        StartCoroutine(GenerateModule(modules, newExits, pendingExit, "Boss"));
                    }
                    else if (newTag == "Unique")
                    {
                        StartCoroutine(GenerateModule(modules, newExits, pendingExit, "Unique"));
                    }
                    else
                    {
                        StartCoroutine(GenerateModule(modules, newExits, pendingExit));
                    }
                }
                else
                {
                    CollisionRetries = 5;
                    pendingExit.IsConnected = newTag != "Boss";
                    Debug.Log($"Could not satisfy a collision. Leaving exit {pendingExit.gameObject.name}, {pendingExit.transform.parent.gameObject.name} empty.");
                }
            }

            yield return null;
        }

        private void MatchExits(Module module, ModuleConnector oldExit, ModuleConnector newExit)
        {            
            var forwardVectorToMatch = -oldExit.transform.forward;
            var correctiveRotation = Azimuth(forwardVectorToMatch) - Azimuth(newExit.transform.forward);
            module.transform.RotateAround(newExit.transform.position, Vector3.up, correctiveRotation);
            var correctiveTranslation = oldExit.transform.position - newExit.transform.position;
            module.transform.position += correctiveTranslation; 
        }

        private static Module GetRandomWithTag(IEnumerable<Module> modules, string tagToMatch)
        {
            var matchingModules = modules.Where(m => m.Tags.Contains(tagToMatch)).ToArray();
            return RandomizationUtil.GetRandom(matchingModules);
        }

        private static float Azimuth(Vector3 vector)
        {
            return Vector3.Angle(Vector3.forward, vector) * Mathf.Sign(vector.x);
        }

        private void DeleteAll()
        {
            foreach (ModuleConnector m in FindObjectsOfType<ModuleConnector>())
            {
                Destroy(m.gameObject);
            }

            foreach (Module m in FindObjectsOfType<Module>())
            {
                Destroy(m.gameObject);
            }
        }
    }
}