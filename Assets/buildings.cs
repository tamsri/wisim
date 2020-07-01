using UnityEngine;

public class buildings : MonoBehaviour
{
    public GameObject building;
    public int max_building = 1000;
    public int seed = 999;
    // Start is called before the first frame update
    void Start(){
        Random.seed = seed;
        for(int i = 0; i < max_building; i++){
            GameObject new_obj = Instantiate(building, new Vector3(Random.Range(-90f, 90f), 1f,  Random.Range(-90f, 90f)), Quaternion.identity);
            new_obj.transform.localScale = new Vector3(Random.Range(.2f, 3f), Random.Range(.5f, 4f), Random.Range(.5f, 3f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
