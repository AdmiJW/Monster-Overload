using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// All scene initializer shall inherit this, so that the script execution order dictates that
// this script shall always run before any other Awake()
[DefaultExecutionOrder(-5)]
public class AbstractSceneInitializer : MonoBehaviour {}
