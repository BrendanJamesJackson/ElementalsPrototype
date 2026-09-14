using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StationaryParallax : MonoBehaviour
{
    public GameObject[] _backgroundLayers;

    public Camera _camera;

    [SerializeField] private float vertAdjustmentFactor;
    [SerializeField] private float horizAdjustmentFactor;

    // Start is called before the first frame update
    void Start()
    {
        //_camera = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        /*for (int i = 0; i < _backgroundLayers.Length; i++)
        {
            _backgroundLayers[i].transform.position = 
                new Vector3((_camera.transform.position.x * ((_backgroundLayers.Length - i)/7f) * horizAdjustmentFactor), _backgroundLayers[i].transform.position.y, 0);
        }

        for (int i = 0; i < _backgroundLayers.Length ; i++)
        {
            _backgroundLayers[i].transform.position =
                new Vector3(_backgroundLayers[i].transform.position.x,(-(_camera.transform.position.y) * ((_backgroundLayers.Length - i) / 7f) * vertAdjustmentFactor),0);
        }*/

        for (int i = 0; i < _backgroundLayers.Length; i++)
        {
            float depthFactor = (_backgroundLayers.Length - i) / 7f;

            float newX = _camera.transform.position.x * depthFactor * horizAdjustmentFactor;
            float newY = -_camera.transform.position.y * depthFactor * vertAdjustmentFactor;

            _backgroundLayers[i].transform.position = new Vector3(newX, newY, _backgroundLayers[i].transform.position.z);


        }
    }
}
